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
        private Project Project { get; set; } // приватное свойство? а какой в это смысл?

        public MainWindow()
        {
            Project = ProjectSerializer.LoadFromFile();

            /*Об ошибках десериализации пользователь не узнает? Просто увидится, что исчезли все его +100500 контактов...*/

            if (Project == null)
            {
                Project = new Project();
                Project.Contacts = new List<Contact>();
            }
            /*
             * Одной строчкой делается:
             * Project ??= new Project() { Contacts = new List<Contact>() };
             * Да и нафига сюда дописывать "Contacts = new List<Contact>()", когда при инициализации класса это и так делается?
             * Получается, достаточно вот так:
             * Project ??= new Project();
             */

            InitializeComponent();

            UpdateListBox();
        }

        /// <summary>
        /// Добавляет и очищает заметки из ListBox.
        /// </summary>
        private void UpdateListBox()
        {
            /*а вот это очень плохо и черевато - использовать контролы из кодбехайнда... ещё не работал с потоками, видимо*/
            MainWindowListBox.Items.Clear();

            /*Название свойства Project и название типа Project совпадают.
             * В итоге метод ContactsByAlfabet - х** пойми что - то ли статический метод класса Project, 
             * то ли метод экземпляра Project класса Project? То же касается свойства Contacts.
             * Если б в студии статический анализатор не подсвечивал зелёным цветом классы, то была бы путаница.*/
            Project.Contacts = Project.ContactsByAlfabet();           

            for (int i = 0; i < Project.Contacts.Count; i++)
            {
                /*1) Ты используешь MainWindowListBox - контрол, который можно трогать только в графическом потоке.
                 *   Значит и этот метод выпоняется в графическом потоке.
                 *   Значит, будь у тебя гигантский список контактов, то графический поток завис бы на полчаса, чтобы разрулить этот код.
                 *   
                 *2) Зачем ToArray()?
                 *3) И ладно бы непонятно ради чего создал новый массив контактов перед циклом. 
                 *   Но тут каждую итерацию создаётся новый массив (причём 3 раза!). 
                 *   Снова представь, что будет с  гигантским списком контактов.*/
                MainWindowListBox.Items.Insert(i, Project.Contacts.ToArray()[i].Surname + " " +
                Project.Contacts.ToArray()[i].Name + " " + Project.Contacts.ToArray()[i].Patronymic);
            }
        }

        /// <summary>
        /// Удаляет контакт из списка.
        /// </summary>
        /// <param name="index">Индекс удаляемого из списка элемента</param>
        private void RemoveContactFromListBox(int index)
        {
            Project.Contacts.RemoveAt(index);
            if (MainWindowListBox.SelectedIndex == -1)
            {
                /* Тебе крупно везёт, что каждый раз MainWindowListBox.SelectedIndex == 0.
                   Так бы уже давно словил ArgumentOutOfRangeException*/

                MainWindowListBox.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Очищает правую панель.
        /// </summary>
        private void ClearSelectedContact()
        {
            /*Промолчу про кодбехайнд*/
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
                /*ужс*/
                MainWindowIDTextBox.Text = Project.Contacts.ToArray()[index].ID.ToString();
                MainWindowSurnameTextBox.Text = Project.Contacts.ToArray()[index].Surname;
                MainWindowNameTextBox.Text = Project.Contacts.ToArray()[index].Name;
                MainWindowPatronymicTextBox.Text = Project.Contacts.ToArray()[index].Patronymic;
                MainWindowPhoneTextBox.Text = Project.Contacts.ToArray()[index].Phone;
            }
        }

        /// <summary>
        /// Добавляет контакт.
        /// </summary>
        private void AddContact()
        {
            var contactWindow = new ContactWindow();
            var result = contactWindow.ShowDialog();
            if (result == true) // if (contactWindow.ShowDialog())
            {
                Project.Contacts.Add(contactWindow.Contact);
                ProjectSerializer.SaveToFile(Project);
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
            /*т.к. ты не используешь Binding, то можешь попасть в ситуацию, когда Project.Contacts.Count не стыкуется с MainWindowListBox.Items.Count*/
            var selectedIndex = MainWindowListBox.SelectedIndex;
            var selectedContact = Project.Contacts[selectedIndex];
            var contactWindow = new ContactWindow(); // в конструктор добавить Contact, не?
            contactWindow.Contact = selectedContact;            
            var result = contactWindow.ShowDialog();

            if (result == true) // снова зачем-то локальная перменная result...
            {
                var updatedData = contactWindow.Contact;
                /*про кодбехайнд я уже писал*/
                MainWindowListBox.Items.RemoveAt(selectedIndex);
                Project.Contacts.RemoveAt(selectedIndex);
                Project.Contacts.Insert(selectedIndex, updatedData);

                /* о_О wtf???
                  напёрстки какие-то...*/
                Project.Contacts[Project.Contacts.IndexOf(Project.Contacts[selectedIndex])] = Project.Contacts[selectedIndex];
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

            /*1) лишняя переменная
              2) почему не используешь интерполяцию?*/
            var result = MessageBox.Show("Вы действительно хотите удалить контакт " +
                MainWindowListBox.SelectedItem.ToString() + "?", "", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                RemoveContactFromListBox(MainWindowListBox.SelectedIndex);
                ProjectSerializer.CleanFile(Project);
            }
            else if (result == MessageBoxResult.No) //там были ещё варианты? или при закрытии окошка что-то другое должно быть?
            {
                return;
            }

            UpdateListBox();
        }

        private void MainWindowListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectedContact(MainWindowListBox.SelectedIndex);
        }

        /*Если бы использовал MVVM, то добавил бы одну команду и не плодил кучу одинаковых методов на разные кнопки*/
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
            Project.Contacts = Project.ContactsById();
            ProjectSerializer.SaveToFile(Project);
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть программу?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Project.Contacts = Project.ContactsById();
                ProjectSerializer.SaveToFile(Project);
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
