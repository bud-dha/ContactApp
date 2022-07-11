using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace ContactApp.Model
{
    /// <summary>
    /// Сохраняет и выгружает пользовательские данные.
    /// </summary>
    public static class ProjectSerializer
    {
        /// <summary>
        /// Название файла сохранения.
        /// </summary>
        public static string FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + "ContactApp.xml";
        
        /// <summary>
        /// Сохраняет данные в файл.
        /// </summary>
        /// <param name="project"></param>
        public static void SaveToFile(Project project)
        {
            XmlSerializer xml = new XmlSerializer(typeof (Project));
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                /*Почему здесь не используешь try-catch, как в методе LoadFromFile?*/
                xml.Serialize(fs, project);            
            }
        }

        /// <summary>
        /// Удаляет данные из файла.
        /// </summary>
        /// <param name="project"></param>
        public static void CleanFile(Project project)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Project));
            /*???*/
        }

        /// <summary>
        /// Выгружает данные из файла.
        /// </summary>
        /// <returns></returns>
        public static Project LoadFromFile()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Project));
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                // можно проще сделать
                Project project = null; // 1) это убираем
                /*Почему здесь используешь try-catch, а в методе SaveToFile - нет?*/
                try
                {
                    project = (Project)xml.Deserialize(fs); // 2) return xml.Deserialize(fs) as Project;
                }
                catch // Проверь, почему после удаления контакта, у тебя возникате ошибка десериализации.
                {
                    return project; // 3) return null;
                }
                return project; // 4) это тоже не нужно
            }
        }
    }
}
