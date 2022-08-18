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
        public ContactWindowViewModel()
        {
            CloseContactWindowCommand = new LambdaCommand(OnCloseContactWindowCommandExecuted, CanCloseContactWindowCommandExecut);
        }

        #region Свойства

        /// <summary>
        /// Цвет некоректного ввода.
        /// </summary>
        private readonly Brush _wrongValueColor = Brushes.Red;

        /// <summary>
        /// Цвет корректного ввода.
        /// </summary>
        private readonly Brush _correctValueColor = Brushes.Gray;

        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        private string _titleError;

        /// <summary>
        /// Свойство для пердачи данных контакта.
        /// </summary>
        private Contact _transferContact = new Contact();

        /// <summary>
        /// Текстовое поле фамилии.
        /// </summary>
        private string _surname;

        /// <summary>
        /// Текстовое поле имени.
        /// </summary>
        private string _name;

        /// <summary>
        /// Текстовое поле отчества.
        /// </summary>
        private string _patronymic;

        /// <summary>
        /// Текстовое поле отчества.
        /// </summary>
        private string _phone;
 
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

        /// <summary>
        /// Возвращает и задает свойство для передачи данных контакта.
        /// </summary>
        public Contact TransferContact
        {
            get => _transferContact;
            set
            {
                _transferContact = value;                
            }
        }

        #endregion

        #region Команды

        #region CloseContactWindowCommand

        public ICommand CloseContactWindowCommand { get; }

        private void OnCloseContactWindowCommandExecuted(object p)
        {
              
        }

        private bool CanCloseContactWindowCommandExecut(object p) => true;

        #endregion

        #endregion

        /// <summary>
        /// Проверяет форму на наличие ошибок.
        /// </summary>
        private bool CheckOnErrors()
        {
            if (_titleError.Length > 1)
            {
                MessageBox.Show(_titleError);
                return false;
            }
            return true;
        }


    }
}
