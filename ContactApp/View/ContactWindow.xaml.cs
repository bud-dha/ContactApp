using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ContactApp.Model;

namespace ContactApp
{
    /// <summary>
    /// Логика взаимодействия для ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
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
        /// Возвращает и задает свойство для передачи двнных контакта.
        /// </summary>
        public Contact TransferContact
        {
            get => _transferContact;
            set {
                _transferContact = value;
                UpdateWindow();               
            }
        }

        public ContactWindow()
        {
            InitializeComponent();                        
        }

        /// <summary>
        /// Заполняет поля формы данными из плоля _note.
        /// </summary>
        private void UpdateWindow()
        {
            ContactWindowNameTextBox.Text = _transferContact.Name;
            ContactWindowSurnameTextBox.Text = _transferContact.Surname;
            ContactWindowPatronymicTextBox.Text = _transferContact.Patronymic;
            ContactWindowPhoneTextBox.Text = _transferContact.Phone;
        }

        /// <summary>
        /// Обновляет данные в объекте _note.
        /// </summary>
        private void UpdateContact()
        {
            _transferContact.Surname = ContactWindowSurnameTextBox.Text;
            _transferContact.Name = ContactWindowNameTextBox.Text;
            _transferContact.Patronymic = ContactWindowPatronymicTextBox.Text;
            _transferContact.Phone = ContactWindowPhoneTextBox.Text;
        }

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

        private void ContactWindowSurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _transferContact.Surname = ContactWindowSurnameTextBox.Text;
                ContactWindowSurnameTextBox.BorderBrush = _correctValueColor;
                _titleError = "";
            }
            catch (Exception exception)
            {
                ContactWindowSurnameTextBox.BorderBrush = _wrongValueColor;
                _titleError = exception.Message;
            }
        }

        private void ContactWindowNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _transferContact.Name = ContactWindowNameTextBox.Text;
                ContactWindowNameTextBox.BorderBrush = _correctValueColor;
                _titleError = "";
            }
            catch (Exception exception)
            {
                ContactWindowNameTextBox.BorderBrush = _wrongValueColor;
                _titleError = exception.Message;
            }
        }

        private void ContactWindowPatronymicTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _transferContact.Patronymic = ContactWindowPatronymicTextBox.Text;
                ContactWindowPatronymicTextBox.BorderBrush = _correctValueColor;
                _titleError = "";
            }
            catch (Exception exception)
            {
                ContactWindowPatronymicTextBox.BorderBrush = _wrongValueColor;
                _titleError = exception.Message;
            }
        }

        private void ContactWindowPhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _transferContact.Phone = ContactWindowPhoneTextBox.Text;
                ContactWindowPhoneTextBox.BorderBrush = _correctValueColor;
                _titleError = "";
            }
            catch (Exception exception)
            {
                ContactWindowPhoneTextBox.BorderBrush = _wrongValueColor;
                _titleError = exception.Message;
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            CheckOnErrors();
            UpdateContact();
        }

    }
}
