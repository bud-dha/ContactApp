using System;
using System.IO;

namespace ContactApp.Core
{
    public static class ProjectInformationHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly string ContactAppSerializerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ContactApp", "contactapp.xml");

        /// <summary>
        /// 
        /// </summary>
        public static readonly string ContactAppImagesPath = "/Images";
    }
}
