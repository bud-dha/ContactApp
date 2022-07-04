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
            Project = ProjectSerializer.LoadFromFile();
            if (Project == null)
            {
                Project = new Project();
                Project.Contacts = new List<Contact>();
            }

            InitializeComponent();
            
            CurentContacts = new List<Contact>();
            CurentContacts = Project.Contacts;

            UpdateListBox();
        }

        /// <summary>
        /// Добавляет и очищает заметки из ListBox.
        /// </summary>
        private void UpdateListBox()
        {
            MainWindowListBox.Items.Clear();
            CurentContacts = Project.ContactsByAlfabet();           

            for (int i = 0; i < Project.Contacts.Count; i++)
            {
                MainWindowListBox.Items.Insert(i, CurentContacts.ToArray()[i].Surname + " " +
                CurentContacts.ToArray()[i].Name + " " + CurentContacts.ToArray()[i].Patronymic);
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
            else
            {
                MainWindowIDTextBox.Text = CurentContacts.ToArray()[index].ID.ToString();
                MainWindowSurnameTextBox.Text = CurentContacts.ToArray()[index].Surname;
                MainWindowNameTextBox.Text = CurentContacts.ToArray()[index].Name;
                MainWindowPatronymicTextBox.Text = CurentContacts.ToArray()[index].Patronymic;
                MainWindowPhoneTextBox.Text = CurentContacts.ToArray()[index].Phone;
            }
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
                ProjectSerializer.SaveToFile(Project);
            }
            UpdateListBox();
            ProjectSerializer.SaveToFile(Project);
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
            var selectedContact = CurentContacts[selectedIndex];            
            var contactWindow = new ContactWindow();
            contactWindow.Contact = selectedContact;            
            var result = contactWindow.ShowDialog();

            if (result == true)
            {
                var updatedData = contactWindow.Contact;

                MainWindowListBox.Items.RemoveAt(selectedIndex);
                CurentContacts.RemoveAt(selectedIndex);
                CurentContacts.Insert(selectedIndex, updatedData);
                Project.Contacts[Project.Contacts.IndexOf(CurentContacts[selectedIndex])] = CurentContacts[selectedIndex];
                UpdateListBox();
                ProjectSerializer.SaveToFile(Project);
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
                RemoveContact(MainWindowListBox.SelectedIndex);
            }
            else if (result == MessageBoxResult.No)
            {
                return;
            }

            UpdateListBox();
            ProjectSerializer.SaveToFile(Project);
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

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            ProjectSerializer.SaveToFile(Project);
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
                ProjectSerializer.SaveToFile(Project);
                e.Cancel = true;
            }
        }
    }
}
