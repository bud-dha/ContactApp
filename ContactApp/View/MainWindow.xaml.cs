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
        /// Возвращает и задает текущий список заметок пользователя.
        /// </summary>
        private List<Contact> CurentContacts { get; set; }

        /// <summary>
        /// Объект класса Project.
        /// </summary>
        private Project Project { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Project = new Project();
            Project.Contacts = new List<Contact>();
            CurentContacts = new List<Contact>();
            CurentContacts = Project.Contacts;
        }

        /// <summary>
        /// Добавляет и очищает заметки из ListBox.
        /// </summary>
        private void UpdateListBox()
        {
            MainWindowListBox.Items.Clear();
            Project.Contacts = Project.ContactsByAlfabet();           

            for (int i = 0; i < Project.Contacts.Count; i++)
            {
                MainWindowListBox.Items.Insert(i, Project.Contacts.ToArray()[i].Surname + " " +
                Project.Contacts.ToArray()[i].Name + " " + Project.Contacts.ToArray()[i].Patronymic);
            }
        }

        /// <summary>
        /// Удаляет заметку из списка.
        /// </summary>
        /// <param name="index">Индекс удаляемого из списка элемента</param>
        private void RemoveContact(int index)
        {
            Project.Contacts.RemoveAt(index);
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
            MainWindowIDTextBox.Text = Project.Contacts.ToArray()[index].ID.ToString();
            MainWindowSurnameTextBox.Text = Project.Contacts.ToArray()[index].Surname;
            MainWindowNameTextBox.Text = Project.Contacts.ToArray()[index].Name;
            MainWindowPatronymicTextBox.Text = Project.Contacts.ToArray()[index].Patronymic;
            MainWindowPhoneTextBox.Text = Project.Contacts.ToArray()[index].Phone;
        }

        /// <summary>
        /// Добавляет контакт.
        /// </summary>
        private void AddContact()
        {
            var contactWindow = new ContactWindow();
            var result = contactWindow.ShowDialog();
            if (result == true)
            {
                Project.Contacts.Add(contactWindow.Contact);
            }
            UpdateListBox();
        }

        /// <summary>
        /// Редактирует контакт.
        /// </summary>
        private void EditContact()
        {
            var selectedIndex = MainWindowListBox.SelectedIndex;
            var selectedContact = Project.Contacts[selectedIndex];
            var contactWindow = new ContactWindow();
            contactWindow.Contact = selectedContact;
            var result = contactWindow.ShowDialog();

            if (result == true)
            {
                var updatedData = contactWindow.Contact;

                MainWindowListBox.Items.RemoveAt(selectedIndex);
                Project.Contacts.RemoveAt(selectedIndex);
                Project.Contacts.Insert(selectedIndex, updatedData);
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
                return;
            }

            var result = MessageBox.Show("Вы действительно хотите удалить контакт " +
                MainWindowListBox.SelectedItem.ToString() + "?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                RemoveContact(MainWindowListBox.SelectedIndex);
            }
            else if (result == MessageBoxResult.No)
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
            UpdateListBox();

            var contactWindow = new ContactWindow();
            var result = contactWindow.ShowDialog();

            if (result == true)
            {
                MessageBox.Show("Контакт сохранен");
            }
            else
            {
                MessageBox.Show("Контакт не будет сохранен!");
            }
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

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть программу?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void FindTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CurentContacts = Project.FindContacts(FindTextBox.Text);
                for (int i = 0; i < FindTextBox.Text.Length; i++)
                {
                    MainWindowListBox.Items.Add(Project.Contacts.ToArray()[i].Surname + Project.Contacts.ToArray()[i].Name + Project.Contacts.ToArray()[i].Patronymic);
                }
            }
        }

        private void FindTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (FindTextBox.Text.Length == 0)
            {
                MainWindowListBox.Items.Clear();
            }
        }
    }
}
