using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactApp.Model
{
    [Serializable]
    /// <summary>
    /// Хранит все пользовательские данные текущей сессии.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Возвращает и задает список контактов пользователя.
        /// </summary>
        public List<Contact> Contacts = new List<Contact>();
        
        /// <summary>
        /// Возвращает список отсортированный по Id.
        /// </summary>
        public List<Contact> ContactsById()
        {
            if (Contacts.Count != 0)
            {
                return Contacts.OrderBy(t => t.ID).ToList();
            }
            return Contacts;
        }

        /// <summary>
        /// Возвращает список отсортированный по алфавиту.
        /// </summary>
        /// <returns></returns>
        public List<Contact> ContactsByAlfabet()
        {
            if (Contacts.Count != 0)
            {
                return Contacts.OrderBy(t => t.Surname).ToList();
            }
            return Contacts;
        }
    }
}
