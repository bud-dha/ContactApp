using System.Collections.Generic;

namespace ContactApp.Model
{
    public static class DataTransfer
    {
        /// <summary>
        /// Список контактов.
        /// </summary>
        public static List<Contact> Contacts = ProjectSerializer.LoadFromFile().Contacts;

        /// <summary>
        /// Текущий контакт.
        /// </summary>
        public static Contact CurentContact;
    }
}