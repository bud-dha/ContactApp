using System;
using System.Collections.Generic;
using System.Text;

namespace ContactApp.Model
{
    /// <summary>
    /// Хранит все пользовательские данные текущей сессии.
    /// </summary>
    class Project
    {
        /// <summary>
        /// Возвращает и задает список контактов пользователя.
        /// </summary>
        public List<Contact> Contacts = new List<Contact>();
    }
}
