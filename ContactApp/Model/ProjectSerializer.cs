using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ContactApp.Model
{
    /// <summary>
    /// Сохраняет и выгружает пользовательские данные.
    /// </summary>
    class ProjectSerializer
    {
        /// <summary>
        /// Название файла сохранения.
        /// </summary>
        public static string FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + "ContactApp"; 
    }
}
