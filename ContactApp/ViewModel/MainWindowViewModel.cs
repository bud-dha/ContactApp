using System.Windows;
using System.Windows.Input;
using ContactApp.ViewModel.Base;
using ContactApp.Infrastructure.Comands;
using System.Collections.ObjectModel;
using ContactApp.Model;

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
        private Contact _contact;

        /// <summary>
        /// Текстовое поле id.
        /// </summary>
        private int _id;

        /// <summary>
        /// Текстовое поле имени.
        /// </summary>
        private string _surname;

        /// <summary>
        /// Текстовое поле фамилии.
        /// </summary>
        private string _name;

        /// <summary>
        /// Текстовое поле отчества.
        /// </summary>
        private string _patronymic;

        /// <summary>
        /// Текстовое поле номера телефона.
        /// </summary>
        private string _phone;

        /// <summary>
        /// Задает и возвращает объект класса Project.
        /// </summary>
        public Project Project
        {
            get => _project;
            set => Set(ref _project, value);
        }

        /// <summary>
        /// Задает и возвращает объект класса Contact.
        /// </summary>
        public Contact Contact
        {
            get => _contact;
            set 
            {
                _contact = value;
                OnPropertyChanged("Contact");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; set; }

        /// <summary>
        /// Задает и возвращает текстовое поле фамилии.
        /// </summary>
        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        /// <summary>
        /// Задает и возвращает текстовое поле фамилии.
        /// </summary>
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }

        /// <summary>
        /// Задает и возвращает текстовое поле имени.
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        /// <summary>
        /// Задает и возвращает текстовое поле отчества.
        /// </summary>
        public string Patronymic
        {
            get => _patronymic;
            set => Set(ref _patronymic, value);
        }

        /// <summary>
        /// Задает и возвращает текстовое поле номера телефона.
        /// </summary>
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        #endregion

        #region Команды

        #region CloseAplicationCommand

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



        #endregion

        public MainWindowViewModel()
        {
            CloseAplicationCommand = new LambdaCommand(OnCloseAplicationCommandExecuted, CanCloseAplicationCommandExecut);

            _project = ProjectSerializer.LoadFromFile();

            Contacts = new ObservableCollection<Contact>();

            foreach (var items in _project.Contacts)
            {
                Contacts.Add(items);
            }

        }


    }
}
