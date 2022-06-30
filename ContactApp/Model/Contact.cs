using System;
using System.Collections.Generic;
using System.Text;

namespace ContactApp.Model
{   
    class Contact
    {
        /// <summary>
        /// Начальное значение Id.
        /// </summary>
        private static int sId = 0;

        /// <summary>
        /// Id контакта.
        /// </summary>
        private readonly int Id;

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
        /// Возвращает ID контакта.
        /// </summary>
        public int ID { get => Id; }

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
                if (value.ToCharArray()[1] != 7)
                {
                    //throw new Exception("Номер телефона должет соответствовать маске +7***!");
                }
                if (value.Length > 12)
                {
                    throw new Exception("Недопустимое количество символов!");
                }
                _phone = value;
            }
        }

        /// <summary>
        /// Создает экземпляр кдасса <see cref="Contact">
        /// </summary>
        public Contact() { Id = sId++; }

        /// <summary>
        /// Создает экземпляр кдасса <see cref="Contact">
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="patrionymic"></param>
        /// <param name="phone"></param>
        public Contact(string name, string surname, string patrionymic, string phone)
        {
            Id = sId++;
            Name = name;
            Surname = surname;
            Patronymic = patrionymic;
            Phone = phone;
        }        
    }
}
