using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ContactApp.Model
{   
   [Serializable]
   public class Contact : IDataErrorInfo
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
        /// Id контакта.
        /// </summary>
        public int ID { get=>_id; }

        /// <summary>
        /// Имя контакта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия контакта.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Отчество уонтакта.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Номер телефона контакта.
        /// </summary>
        public string Phone { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (!isFormatCorrect(Name))
                        {
                            error = "Имя должно содержать от 2 до 50 символов и начинаться с большой буквы.";
                        }
                        break;

                    case "Surname":
                        if (!isFormatCorrect(Surname))
                        {
                            error = "Фамилия должна содержать от 2 до 50 символов и начинаться с большой буквы.";
                        }
                        break;

                    case "Patronymic":
                        if (!isFormatCorrect(Patronymic))
                        {
                            error = "Отчество должно содержать от 2 до 50 символов и начинаться с большой буквы.";
                        }
                        break;

                    case "Phone":
                        Regex regexObj = new Regex(@"[+][7][9][0-9]{9}");
                        if (!regexObj.IsMatch(Phone) & Phone.Length != 12)
                        {
                            error = "Номер телефона должен соответствовать маске +79*********";
                        }
                        break;
                }
                return error;
            }
        }
            public string Error
        {
            get { throw new NotImplementedException(); }
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
