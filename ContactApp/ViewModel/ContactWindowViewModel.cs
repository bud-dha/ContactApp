using System.Windows;
using ContactApp.Model;
using System.Windows.Media;
using System.Windows.Input;
using ContactApp.ViewModel.Base;
using ContactApp.Infrastructure.Comands;

namespace ContactApp.ViewModel
{
    class ContactWindowViewModel : ViewModelBase
    {
        #region Свойства

        /// <summary>
        /// Задает и возвращает имя контакта.
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        /// Задает и возвращает фамилию контакта.
        /// </summary>
        public string NewSurname { get; set; }

        /// <summary>
        /// Задает и возвращает отчество контакта.
        /// </summary>
        public string NewPatronymic { get; set; }

        /// <summary>
        /// Задает и возвращает номер телефона контакта.
        /// </summary>
        public string NewPhone { get; set; }

        /// <summary>
        /// Задает и возвращает объект класса Contact.
        /// </summary>
        public Contact NewContact { get; set; }

        #endregion

        #region Команды

        #region Команда принятия изменений

        public ICommand AceptChangesCommand { get; }

        private void OnAceptChangesCommandExecuted(object p)
        {
            NewContact = new Contact();
            NewContact.Name = NewName;
            NewContact.Surname = NewSurname;
            NewContact.Patronymic = NewPatronymic;
            NewContact.Phone = NewPhone;
            DataTransfer.Contacts.Add(NewContact);
            Window win = p as Window;
            win.Close();
        }

        private bool CanAceptChangesCommandExecuted(object p) => true;

        #endregion

        #endregion

        public ContactWindowViewModel()
        {
            AceptChangesCommand = new LambdaCommand(OnAceptChangesCommandExecuted, CanAceptChangesCommandExecuted);
        }
    }
}
