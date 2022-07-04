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
        /// 
        /// </summary>
        /// <param name="project"></param>
        public static void SaveToFile(Project project)
        {
            XmlSerializer xml = new XmlSerializer(typeof (Project));
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, project);            
            }
        }

        public static Project LoadFromFile()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Project));
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                Project project = (Project)xml.Deserialize(fs);
                return project;
            }
        }
    }
}
