using System;
using System.Collections.Generic;
using System.Text;

namespace ContactApp.Model
{   
    class Contact
    {
        /// <summary>
        /// Id контакта.
        /// </summary>
        private readonly int Id = 0;

        /// <summary>
        /// Имя контакта.
        /// </summary>
        private string _name;

        /// <summary>
        /// Фамилия контакта.
        /// </summary>
        private string _surname;

        /// <summary>
        /// Отчество уонтакта.
        /// </summary>
        private string _patronymic;

        /// <summary>
        /// Номер телефона контакта.
        /// </summary>
        private string _phone;

        /// <summary>
        /// Задает и возвращает имя контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set 
            {
                if (value.Length < 2 || value.Length > 50)
                {
                    throw new Exception("Недопустимое количество символов!");                
                }
                _name = value;
            }
        }

        /// <summary>
        /// Задает и возвращает фамилию контакта.
        /// </summary>
        public string Surname
        {
            get => _surname;
            set
            {
                if (value.Length < 2 || value.Length > 50)
                {
                    throw new Exception("Недопустимое количество символов!");
                }
                _surname = value;
            }
        }

        /// <summary>
        /// Задает и возвращает отчество контакта.
        /// </summary>
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                if (value.Length < 2 || value.Length > 50)
                {
                    throw new Exception("Недопустимое количество символов!");
                }
                _patronymic = value;
            }
        }

        /// <summary>
        /// Задает и возвращает номер контакта.
        /// </summary>
        public string Phone
        {
            get => _phone;
            set
            {
                if (value.ToCharArray()[0] != 7)
                {
                    throw new Exception("Неверно введена первая цифра!");
                }
                if (value.Length > 11)
                {
                    throw new Exception("Недопустимое количество символов!");
                }
                _phone = value;
            }
        }

        /// <summary>
        /// Создает экземпляр кдасса <see cref="Contact">
        /// </summary>
        public Contact() { }

        /// <summary>
        /// Создает экземпляр кдасса <see cref="Contact">
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="patrionymic"></param>
        /// <param name="phone"></param>
        public Contact(string name, string surname, string patrionymic, string phone)
        {
            Id++;
            _name = name;
            _surname = surname;
            _patronymic = patrionymic;
            _phone = phone;
        }        
    }
}
