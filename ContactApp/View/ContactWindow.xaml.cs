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
        public ContactWindow(Contact p)
        {
            InitializeComponent();

            if (p != null)
            {                
                ContactWindowSurnameTextBox.Text = p.Surname;
                ContactWindowNameTextBox.Text = p.Name;
                ContactWindowPatronymicTextBox.Text = p.Patronymic;
                ContactWindowPhoneTextBox.Text = p.Phone;
            }
        }
    }
}
