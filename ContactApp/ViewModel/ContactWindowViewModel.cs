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
        /// Имя контакта.
        /// </summary>
        private string _newname;

        /// <summary>
        /// Фамилия контакта.
        /// </summary>
        private string _newsurname;

        /// <summary>
        /// Отчество контакта.
        /// </summary>
        private string _newpatronymic;

        /// <summary>
        /// Номер контакта.
        /// </summary>
        private string _newphone;

        /// <summary>
        /// Объект класса Contact.
        /// </summary>
        private Contact _newcontact;

        /// <summary>
        /// Задает и возвращает имя контакта.
        /// </summary>
        public string NewName
        {
            get => _newname;
            set => Set(ref _newname, value);
        }

        /// <summary>
        /// Задает и возвращает фамилию контакта.
        /// </summary>
        public string NewSurname
        {
            get => _newsurname;
            set => Set(ref _newsurname, value);
        }

        /// <summary>
        /// Задает и возвращает отчество контакта.
        /// </summary>
        public string NewPatronymic
        {
            get => _newpatronymic;
            set => Set(ref _newpatronymic, value);
        }

        /// <summary>
        /// Задает и возвращает номер телефона контакта.
        /// </summary>
        public string NewPhone
        {
            get => _newphone;
            set => Set(ref _newphone, value);
        }

        /// <summary>
        /// Задает и возвращает объект класса Contact.
        /// </summary>
        public Contact NewContact
        {
            get => _newcontact;
            set => Set(ref _newcontact, value);
        }

        #endregion

        #region Команды

        #region Команда принятия изменений

        public ICommand AceptChangesCommand { get; }

        private void OnAceptChangesCommandExecuted(object p)
        {
            NewContact.Name = NewName;
            NewContact.Surname = NewSurname;
            NewContact.Patronymic = NewPatronymic;
            NewContact.Phone = NewPhone;
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
