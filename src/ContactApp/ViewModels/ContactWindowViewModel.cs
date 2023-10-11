using ContactApp.Infrastructure.Comands;
using ContactApp.Model;
using ContactApp.ViewModel.Base;
using System.Windows;
using System.Windows.Input;

namespace ContactApp.ViewModel
{
    class ContactWindowViewModel : ViewModelBase
    {
        #region Свойства класса

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
        /// Задает и возвращает электронную почту контакта.
        /// </summary>
        public string NewEmail { get; set; }

        /// <summary>
        /// Задает и возвращает объект класса Contact.
        /// </summary>
        public Contact NewContact { get; set; }

        #endregion

        #region Методы класса

        /// <summary>
        /// Обновляет форму редактирования контакта.
        /// </summary>
        void UpdateWindowMethod()
        {
            if (DataTransfer.CurentContact == null)
            {
                return;
            }
            NewName = DataTransfer.CurentContact.Name;
            NewSurname = DataTransfer.CurentContact.Surname;
            NewPatronymic = DataTransfer.CurentContact.Patronymic;
            NewPhone = DataTransfer.CurentContact.Phone;
            NewEmail = DataTransfer.CurentContact.Email;
        }

        /// <summary>
        /// Добавляет новый контакт.
        /// </summary>
        void AddContactMethod()
        {
            NewContact = new Contact();
            NewContact.Name = NewName;
            NewContact.Surname = NewSurname;
            NewContact.Patronymic = NewPatronymic;
            NewContact.Phone = NewPhone;
            NewContact.Email = NewEmail;
            DataTransfer.Contacts.Add(NewContact);
        }

        /// <summary>
        /// Редактирует контакт.
        /// </summary>
        void EditContactMethod()
        {
            DataTransfer.CurentContact.Name = NewName;
            DataTransfer.CurentContact.Surname = NewSurname;
            DataTransfer.CurentContact.Patronymic = NewPatronymic;
            DataTransfer.CurentContact.Phone = NewPhone;
            DataTransfer.CurentContact.Email = NewEmail;
        }

        #endregion

        #region Команды

        #region Команда принятия изменений

        public ICommand AceptChangesCommand { get; }

        private void OnAceptChangesCommandExecuted(object p)
        {
            if (DataTransfer.CurentContact == null)
            {
                AddContactMethod();
            }
            else
            {
                EditContactMethod();
            }
            Window win = p as Window;
            win.Close();
        }

        private bool CanAceptChangesCommandExecuted(object p) => true;

        #endregion

        #endregion

        public ContactWindowViewModel()
        {
            AceptChangesCommand = new LambdaCommand(OnAceptChangesCommandExecuted, CanAceptChangesCommandExecuted);

            UpdateWindowMethod();
        }
    }
}