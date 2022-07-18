using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContactApp.Model;

namespace ContactApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Объект класса Project.
        /// </summary>
        private Project _project;

        public MainWindow()
        {
            _project = ProjectSerializer.LoadFromFile();

            InitializeComponent();

            UpdateListBox();
        }

        /// <summary>
        /// Добавляет и очищает заметки из ListBox.
        /// </summary>
        private void UpdateListBox()
        {
            MainWindowListBox.Items.Clear();
            _project.Contacts = _project.ContactsByAlfabet();

            foreach (Contact contact in _project.Contacts)
            {
                MainWindowListBox.Items.Add(contact.Surname + " " + contact.Name + " " + contact.Patronymic);
            }
        }

        /// <summary>
        /// Удаляет контакт из списка.
        /// </summary>
        /// <param name="index">Индекс удаляемого из списка элемента</param>
        private void RemoveContactFromListBox(int index)
        {
            _project.Contacts.RemoveAt(index);
            if (MainWindowListBox.SelectedIndex == -1)
            {
                MainWindowListBox.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Очищает правую панель.
        /// </summary>
        private void ClearSelectedContact()
        {
            MainWindowIDTextBox.Text = "";
            MainWindowSurnameTextBox.Text = "";
            MainWindowNameTextBox.Text = "";
            MainWindowPatronymicTextBox.Text = "";
            MainWindowPhoneTextBox.Text = "";
        }

        /// <summary>
        /// Заполняет данные на правой панели главного окна.
        /// </summary>
        ///  /// <param name="index">Индекс выделенного элемента</param>
        private void UpdateSelectedContact(int index)
        {
            if (MainWindowListBox.SelectedIndex == -1)
            {
                ClearSelectedContact();
            }
            else
            {
                Contact contact = _project.Contacts[index];
                MainWindowIDTextBox.Text = contact.ID.ToString();
                MainWindowSurnameTextBox.Text = contact.Surname;
                MainWindowNameTextBox.Text = contact.Name;
                MainWindowPatronymicTextBox.Text = contact.Patronymic;
                MainWindowPhoneTextBox.Text = contact.Phone;
            }
        }

        /// <summary>
        /// Добавляет контакт.
        /// </summary>
        private void AddContact()
        {
            var contactWindow = new ContactWindow();            
            if ((bool)contactWindow.ShowDialog())
            {
                _project.Contacts.Add(contactWindow.TransferContact);                
            }
            UpdateListBox();
        }

        /// <summary>
        /// Редактирует контакт.
        /// </summary>
        private void EditContact()
        {
            if (MainWindowListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать контакт");
                return;
            }

            var selectedIndex = MainWindowListBox.SelectedIndex;
            var selectedContact = _project.Contacts[selectedIndex];
            var contactWindow = new ContactWindow();
            contactWindow.TransferContact = selectedContact;                        

            if ((bool)contactWindow.ShowDialog())
            {
                var updatedData = contactWindow.TransferContact;

                MainWindowListBox.Items.RemoveAt(selectedIndex);
                _project.Contacts.RemoveAt(selectedIndex);
                _project.Contacts.Insert(selectedIndex, updatedData);
                _project.Contacts[_project.Contacts.IndexOf(_project.Contacts[selectedIndex])] = _project.Contacts[selectedIndex];
                UpdateListBox();
            }
        }

        /// <summary>
        /// Удаляет контакт.
        /// </summary>
        private void RemoveContact()
        {
            if (MainWindowListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать контакт");
                return;
            }

            var result = MessageBox.Show("Вы действительно хотите удалить контакт " +
                MainWindowListBox.SelectedItem.ToString() + "?", "", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                RemoveContactFromListBox(MainWindowListBox.SelectedIndex);                
            }
            else 
            {
                return;
            }

            UpdateListBox();
        }

        private void MainWindowListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectedContact(MainWindowListBox.SelectedIndex);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddContact();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditContact();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveContact();
        }

        private void MenuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            AddContact();
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            EditContact();
        }

        private void MenuItemRemove_Click(object sender, RoutedEventArgs e)
        {
            RemoveContact();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть программу?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _project.Contacts = _project.ContactsById();
                ProjectSerializer.SaveToFile(_project);
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
