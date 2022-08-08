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
        private Contact _selectedcontact;

        /// <summary>
        /// Текстовое поле id.
        /// </summary>
        private int _id;
        
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
            _project = ProjectSerializer.LoadFromFile();

            CloseAplicationCommand = new LambdaCommand(OnCloseAplicationCommandExecuted, CanCloseAplicationCommandExecut);     

            Contacts = new ObservableCollection<Contact>();

            foreach (var items in _project.Contacts)
            {
                Contacts.Add(items);
            }            
        }


    }
}
