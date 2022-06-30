﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Возвращает список по ключевому слову.
        /// </summary>
        public List<Contact> FindContacts(string text)
        {
            if (Contacts.Count != 0)
            {
                return Contacts.Where(t => t.Name == text || t.Surname == text || 
                t.Patronymic == text || t.Phone == text).ToList();
            }
            return Contacts;
        }
    }
}