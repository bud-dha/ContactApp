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
        /// Звдает и возвращает спиcок контактов выводимых на ListBox.
        /// </summary>
        public ObservableCollection<Contact> ListBoxContacts { get; set; }     

        #endregion

        #region Команды

        #region Команда закрытия программы

        public ICommand CloseAplicationCommand { get; }

        private void OnCloseAplicationCommandExecuted(object p)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть программу?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Project.Contacts = ListBoxContacts.ToList();
                Project.Contacts = Project.ContactsById();
                ProjectSerializer.SaveToFile(Project);
                Application.Current.Shutdown();
            }
        }

        private bool CanCloseAplicationCommandExecut(object p) => true;

        #endregion

        #region Команда удаления контакта

        public ICommand RemoveContactCommand { get; }

        private void OnRemoveContactCommandExecuted(object p)
        {
            if (SelectedContact != null)
            {
                MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить контакт: " +
                $"{SelectedContact.Surname + " " + SelectedContact.Name + " " + SelectedContact.Patronymic}?", "", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {                    
                    ListBoxContacts.Remove(SelectedContact);
                }
            }
        }

        private bool CanRemoveContactCommandExecuted(object p) => true;

        #endregion


        #endregion

        public MainWindowViewModel()
        {
            _project = ProjectSerializer.LoadFromFile();

            CloseAplicationCommand = new LambdaCommand(OnCloseAplicationCommandExecuted, CanCloseAplicationCommandExecut);

            RemoveContactCommand = new LambdaCommand(OnRemoveContactCommandExecuted, CanRemoveContactCommandExecuted);

            ListBoxContacts = new ObservableCollection<Contact>();
            foreach (var items in _project.Contacts)
            {
                ListBoxContacts.Add(items);
            }
        }


    }
}
