using System.Linq;
using System.Windows;
using ContactApp.Model;
using System.Windows.Input;
using ContactApp.ViewModel.Base;
using System.Collections.ObjectModel;
using ContactApp.Infrastructure.Comands;

namespace ContactApp.ViewModel
{   
    class MainWindowViewModel : ViewModelBase
    {
        #region Свойства

        /// <summary>
        /// Объект класса Project.
        /// </summary>
        private Project _project;

        /// <summary>
        /// Объект класса Contact.
        /// </summary>
        private Contact _selectedcontact;


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
        /// Звдает и возвращает спиcок контактов выводимых на ListBox.
        /// </summary>
        public ObservableCollection<Contact> ListBoxContacts { get; set; }     

        #endregion

        #region Команды

        #region Команда добавления контакта

        public ICommand AddContactCommand { get; }

        private void OnAddContactCommandExecuted(object p)
        {
            OpenContactWindowMethod(null);
            UpdateWindowMethod();
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
            DataTransfer.Contacts.Remove(SelectedContact);
            UpdateWindowMethod();
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
            SaveData();
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
                SaveData();
                Application.Current.Shutdown();
            }
        }

        private bool CanCloseAplicationCommandExecut(object p) => true;

        #endregion

        #endregion

        #region Методы

        /// <summary>
        /// Открывает окно добавления/редактирования контакта.
        /// </summary>
        /// <param name="contact"></param>
        void OpenContactWindowMethod(Contact contact)
        {
            ContactWindow contactWindow = new ContactWindow(contact);
            contactWindow.Owner = Application.Current.MainWindow;
            contactWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var dialogresult = contactWindow.ShowDialog();
            if (dialogresult == true)
            {
                _project.Contacts = ListBoxContacts.ToList();
                _project.Contacts = _project.ContactsById();
                ProjectSerializer.SaveToFile(_project);
            }
        }

        /// <summary>
        /// Обновляет данные окна.
        /// </summary>
        void UpdateWindowMethod()
        {
            ListBoxContacts.Clear();
            foreach (var items in DataTransfer.Contacts)
            {
                ListBoxContacts.Add(items);
            }
        }

        /// <summary>
        /// Сохраняет данные в xml файл.
        /// </summary>
        void SaveData()
        {
            _project.Contacts = ListBoxContacts.ToList();
            _project.Contacts = _project.ContactsById();
            ProjectSerializer.SaveToFile(_project);
        }

        #endregion

        public MainWindowViewModel()
        {
            _project = new Project();

            _project.Contacts = DataTransfer.Contacts;

            AddContactCommand = new LambdaCommand(OnAddContactCommandExecuted, CanAddContactCommandExecuted);

            EditContactCommand = new LambdaCommand(OnEditContactCommandExecuted, CanEditContactCommandExecuted);

            RemoveContactCommand = new LambdaCommand(OnRemoveContactCommandExecuted, CanRemoveContactCommandExecuted);

            CloseAplicationCommand = new LambdaCommand(OnCloseAplicationCommandExecuted, CanCloseAplicationCommandExecut);

            ListBoxContacts = new ObservableCollection<Contact>();
            foreach (var items in DataTransfer.Contacts)
            {
                ListBoxContacts.Add(items);
            }

        }


    }
}
