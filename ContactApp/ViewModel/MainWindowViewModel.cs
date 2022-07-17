using System;
using System.Collections.Generic;
using System.Text;
using ContactApp.ViewModel.Base;

namespace ContactApp.ViewModel
{
    // А что если унаследовать от ContactWindowViewModel????
    class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Текстовое поле id.
        /// </summary>
        private int _id;

        /// <summary>
        /// Текстовое поле имени.
        /// </summary>
        private string _surname;

        /// <summary>
        /// Текстовое поле фамилии.
        /// </summary>
        private string _name;

        /// <summary>
        /// Текстовое поле отчества.
        /// </summary>
        private string _patronymic;

        /// <summary>
        /// Текстовое поле номера телефона.
        /// </summary>
        private string _phone;

        /// <summary>
        /// Задает и возвращает текстовое поле фамилии.
        /// </summary>
        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        /// <summary>
        /// Задает и возвращает текстовое поле фамилии.
        /// </summary>
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }

        /// <summary>
        /// Задает и возвращает текстовое поле имени.
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        /// <summary>
        /// Задает и возвращает текстовое поле отчества.
        /// </summary>
        public string Patronymic
        {
            get => _patronymic;
            set => Set(ref _patronymic, value);
        }

        /// <summary>
        /// Задает и возвращает текстовое поле номера телефона.
        /// </summary>
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

    }
}
