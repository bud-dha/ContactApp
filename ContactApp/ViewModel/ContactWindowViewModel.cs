using System;
using System.Collections.Generic;
using System.Text;
using ContactApp.ViewModel.Base;

namespace ContactApp.ViewModel
{
    class ContactWindowViewModel : ViewModelBase
    {
        private string _title;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }


        /// <summary>
        /// Текстовое поле фамилии.
        /// </summary>
        private string _contactWondowSurname;

        /// <summary>
        /// Задает и возвращает текстовое поле фамилии.
        /// </summary>
        public string ContactWindowSurname
        {
            get => _contactWondowSurname;
            set => Set(ref _contactWondowSurname, value);
        }
    }
}
