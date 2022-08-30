using System;
using System.Text;
using System.Collections.Generic;

namespace ContactApp.Model
{
    public static class DataTransfer
    {
      public static List<Contact> Contacts = ProjectSerializer.LoadFromFile().Contacts;      
    }
}
