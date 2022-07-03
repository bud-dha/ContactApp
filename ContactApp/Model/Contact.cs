using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactApp.Model
{   
   public class Contact
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
                Regex regexObj = new Regex(@"^[A-ЯЁ][а-яё]");

                if (regexObj.IsMatch(value) || value.Length > 50)
                {
                    _phone = value;
                }
                else
                {
                    throw new Exception("Имя должно начинаться с большой буквы");
                }
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
                Regex regexObj = new Regex(@"^[A-ЯЁ][а-яё]");

                if (regexObj.IsMatch(value) || value.Length > 50)
                {
                    _phone = value;
                }
                else
                {
                    throw new Exception("Фамилия должна начинаться с большой буквы");
                }
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
                Regex regexObj = new Regex(@"^[A-ЯЁ][а-яё]");

                if (regexObj.IsMatch(value) || value.Length > 50)
                {
                    _phone = value;
                }
                else
                {
                    throw new Exception("Отчество должно начинаться с большой буквы");
                }
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
                Regex regexObj = new Regex(@"[+][7][9][0-9]{9}");

                if (regexObj.IsMatch(value))
                {
                    _phone = value;
                }
                else
                {
                    throw new Exception("Номер телефона должен соответствовать маске +79*********");
                }                
            }
        }

        public Contact() 
        {
            Name = "Имя";
            Surname = "Фамилия";
            Patronymic = "Отчество";
            Phone = "+79999999099";
        }

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
