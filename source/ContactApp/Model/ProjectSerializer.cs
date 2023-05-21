using System;
using System.IO;
using System.Xml.Serialization;

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
        private static string FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ContactApp\contactapp.xml";
        
        /// <summary>
        /// Сохраняет данные в файл.
        /// </summary>
        /// <param name="project"></param>
        public static void SaveToFile(Project project)
        {
            XmlSerializer xml = new XmlSerializer(typeof (Project));
            using (var sw = new StreamWriter(FileName, false))
            {
                try
                {
                    xml.Serialize(sw, project);
                }
                catch 
                {
                    throw new ArgumentException("Не удалось записать данные в файл");
                }
            }
        }

        /// <summary>
        /// Выгружает данные из файла.
        /// </summary>
        /// <returns></returns>
        public static Project LoadFromFile()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Project));
            if (File.Exists(FileName))
            {
                using (FileStream fs2 = new FileStream(FileName, FileMode.Open))
                {
                    try
                    {
                        return xml.Deserialize(fs2) as Project;
                    }
                    catch
                    {
                        throw new ArgumentException("Ошибка. Файл проекта был не правильно сохранен.");                        
                    }
                }
            }
            else 
            {
                Directory.CreateDirectory(FileName.Replace("\\contactapp.xml", ""));
                File.Create(FileName);
                return new Project();
            }
        }
    }
}
