using System;
using System.Text.RegularExpressions;

namespace ContactApp.Model
{   
   [Serializable]
   public class Contact
    {
        /// <summary>
        /// Начальное значение Id.
        /// </summary>
        private static int _sid = 1;

        /// <summary>
        /// Id контакта.
        /// </summary>
        private int _id;

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
        public int ID { get => _id; }

        /// <summary>
        /// Задает и возвращает имя контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set 
            {                
                if (isFormatCorrect(value) && value.Length < 50)
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentException("Имя должно содержать от 2 до 50 символов и начинаться с большой буквы.");
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
                if (isFormatCorrect(value) && value.Length < 50)
                {
                    _surname = value;
                }
                else
                {
                    throw new ArgumentException("Фамилия должна содержать от 2 до 50 символов и начинаться с большой буквы.");
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
                if (value == "" | (isFormatCorrect(value) && value.Length < 50))
                {
                    _patronymic = value;
                }
                else
                {
                    throw new ArgumentException("Отчество должно содержать от 2 до 50 символов и начинаться с большой буквы.");
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

                if (regexObj.IsMatch(value) & value.Length == 12)
                {
                    _phone = value;
                }
                else
                {
                    throw new ArgumentException("Номер телефона должен соответствовать маске +79*********");
                }                
            }
        }

        /// <summary>
        /// Создает экземпляр кдасса <see cref="Contact">
        /// </summary>
        public Contact() 
        {
            _id = _sid++;
        }

        /// <summary>
        /// Возвращает соответсвие строки на формату.
        /// </summary>
        /// <param name="str"></param>        
        private bool isFormatCorrect(string str)
        {
            Regex regexObj = new Regex(@"^[A-ЯЁ][а-яё]");
            return regexObj.IsMatch(str);
        }
    }
}
