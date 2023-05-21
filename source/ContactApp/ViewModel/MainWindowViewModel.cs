using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using QRCoder;
using ContactApp.Model;
using ContactApp.ViewModel.Base;
using ContactApp.Infrastructure.Comands;

namespace ContactApp.ViewModel
{   
    class MainWindowViewModel : ViewModelBase
    {

        #region Поля класса

        // <summary>
        /// Объект класса Project.
        /// </summary>
        private Project _project;

        /// <summary>
        /// Объект класса Contact.
        /// </summary>
        private Contact _selectedcontact;

        #endregion

        #region Свойства класса

        /// <summary>
        /// Задает и возвращает объект класса Contact.
        /// </summary>
        public Contact SelectedContact
        {
            get => _selectedcontact;
            set 
            {
                _selectedcontact = value;
                OnPropertyChanged("SelectedContact");
            }
        }

        /// <summary>
        /// Задает и возвращает Qr-код контакта.
        /// </summary>
        public BitmapImage QrCodeImage { get; set; }

        /// <summary>
        /// Звдает и возвращает спиcок контактов выводимых на ListBox.
        /// </summary>
        public ObservableCollection<Contact> ListBoxContacts { get; set; }

        #endregion

        #region Методы класса

        /// <summary>
        /// Открывает окно добавления/редактирования контакта.
        /// </summary>
        /// <param name="contact"></param>
        void OpenContactWindowMethod(Contact contact)
        {
            DataTransfer.CurentContact = contact;
            ContactWindow contactWindow = new ContactWindow();
            contactWindow.Owner = Application.Current.MainWindow;
            contactWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var dialogresult = contactWindow.ShowDialog();
            if (dialogresult == true)
            {
                SaveDataMethod();
            }
        }

        /// <summary>
        /// Обновляет данные окна.
        /// </summary>
        void UpdateWindowMethod()
        {
            ListBoxContacts.Clear();
            _project.Contacts = DataTransfer.Contacts;
            foreach (var items in _project.ContactsByAlfabet())
            {
                ListBoxContacts.Add(items);
            }
        }

        /// <summary>
        /// Сохраняет данные в xml файл.
        /// </summary>
        void SaveDataMethod()
        {
            _project.Contacts = ListBoxContacts.ToList();
            _project.Contacts = _project.ContactsById();
            ProjectSerializer.SaveToFile(_project);
        }

        /// <summary>
        /// Генерирует и сохраняет qr-код.
        /// </summary>
        void AddQrCodeMethod()
        {
            if (SelectedContact == null || SelectedContact.Email == null) return;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(SelectedContact.Email, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            MemoryStream memoryStream = new MemoryStream();
            qrCodeImage.Save(memoryStream, ImageFormat.Png);

            QrCodeImage = new BitmapImage();
            QrCodeImage.BeginInit();
            QrCodeImage.StreamSource = new MemoryStream(memoryStream.ToArray());
            QrCodeImage.EndInit();

            SaveQrCode();
        }

        /// <summary>
        /// Сохраняет Qr-код контакта.
        /// </summary>
        void SaveQrCode()
        {
            string qrCodePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QrCodes");
            if (!Directory.Exists(qrCodePath))
                Directory.CreateDirectory(qrCodePath);

            string qrCodeFileName = $"{SelectedContact.Surname}_{SelectedContact.Name}.png";
            SelectedContact.QrCode = Path.Combine(qrCodePath, qrCodeFileName);
            using (var fileStream = new FileStream(SelectedContact.QrCode, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(QrCodeImage));
                encoder.Save(fileStream);
            }
        }

        #endregion

        #region Команды

        #region Команда добавления контакта

        public ICommand AddContactCommand { get; }

        private void OnAddContactCommandExecuted(object p)
        {
            OpenContactWindowMethod(null);
            AddQrCodeMethod();
            UpdateWindowMethod();
            SaveDataMethod();
        }

        private bool CanAddContactCommandExecuted(object p) => true;

        #endregion

        #region Команда редактирования контакта

        public ICommand EditContactCommand { get; }

        private void OnEditContactCommandExecuted(object p)
        {
            if (SelectedContact == null)
            {
                MessageBox.Show("Выберите контакт");
            }
            else
            OpenContactWindowMethod(SelectedContact);
            AddQrCodeMethod();
            UpdateWindowMethod();
            SaveDataMethod();
        }

        private bool CanEditContactCommandExecuted(object p) => true;

        #endregion

        #region Команда удаления контакта

        public ICommand RemoveContactCommand { get; }

        private void OnRemoveContactCommandExecuted(object p)
        {
            if (SelectedContact == null)
            {
                MessageBox.Show("Выберите контакт");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить контакт: " +
                $"{SelectedContact.Surname + " " + SelectedContact.Name + " " + SelectedContact.Patronymic}?", "", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DataTransfer.Contacts.Remove(SelectedContact);
                }
            }
            UpdateWindowMethod();
            SaveDataMethod();
        }

        private bool CanRemoveContactCommandExecuted(object p) => true;

        #endregion

        #region Команда закрытия программы

        public ICommand CloseAplicationCommand { get; }

        private void OnCloseAplicationCommandExecuted(object p)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть программу?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {                
                Application.Current.Shutdown();
            }
        }

        private bool CanCloseAplicationCommandExecut(object p) => true;

        #endregion

        #region Команда открытия ссылки

        public ICommand OpenLinkCommand { get; }

        private void OnOpenLinkCommandExecuted(object p)
        {
            Process.Start(new ProcessStartInfo(SelectedContact.QrCode) { UseShellExecute = true });
        }

        private bool CanOpenLinkCommandExecut(object p) => true;

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            _project = new Project();

            _project.Contacts = DataTransfer.Contacts;

            AddContactCommand = new LambdaCommand(OnAddContactCommandExecuted, CanAddContactCommandExecuted);

            EditContactCommand = new LambdaCommand(OnEditContactCommandExecuted, CanEditContactCommandExecuted);

            RemoveContactCommand = new LambdaCommand(OnRemoveContactCommandExecuted, CanRemoveContactCommandExecuted);

            CloseAplicationCommand = new LambdaCommand(OnCloseAplicationCommandExecuted, CanCloseAplicationCommandExecut);

            OpenLinkCommand = new LambdaCommand(OnOpenLinkCommandExecuted, CanOpenLinkCommandExecut);

            ListBoxContacts = new ObservableCollection<Contact>();
            foreach (var items in DataTransfer.Contacts)
            {
                ListBoxContacts.Add(items);
            }

            UpdateWindowMethod();

        }
    }
}
