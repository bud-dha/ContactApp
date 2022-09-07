using System.Collections.Generic;

namespace ContactApp.Model
{
    public static class DataTransfer
    {
        public static List<Contact> Contacts = ProjectSerializer.LoadFromFile().Contacts;

        public static Contact CurentContact;
    }
}
