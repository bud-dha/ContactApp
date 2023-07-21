using System;

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
        /// Id контакта.
        /// </summary>
        public int ID { get => _id; }

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

        /// <summary>
        /// Возвращает и задает электронную почту контакта.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Возвращает и задает qr-code контакта.
        /// </summary>
        public string QrCode { get; set; }

        /// <summary>
        /// Создает экземпляр кдасса <see cref="Contact">
        /// </summary>
        public Contact()
        {
            _id = _sid++;
        }
    }
}