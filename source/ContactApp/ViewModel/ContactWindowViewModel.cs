using System;
using System.IO;
using System.Windows;
using ContactApp.Model;
using System.Windows.Media;
using System.Windows.Input;
using Gma.QrCodeNet.Encoding;
using ContactApp.ViewModel.Base;
using System.Windows.Media.Imaging;
using ContactApp.Infrastructure.Comands;
using Gma.QrCodeNet.Encoding.Windows.Render;
using ContactApp.Core;

namespace ContactApp.ViewModel
{
    class ContactWindowViewModel : ViewModelBase
    {
        #region Свойства

        /// <summary>
        /// Задает и возвращает имя контакта.
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        /// Задает и возвращает фамилию контакта.
        /// </summary>
        public string NewSurname { get; set; }

        /// <summary>
        /// Задает и возвращает отчество контакта.
        /// </summary>
        public string NewPatronymic { get; set; }

        /// <summary>
        /// Задает и возвращает номер телефона контакта.
        /// </summary>
        public string NewPhone { get; set; }

        /// <summary>
        /// Задает и возвращает электронную почту  контакта.
        /// </summary>
        public string NewEmail { get; set; }

        /// <summary>
        /// Задает и возвращает объект класса Contact.
        /// </summary>
        public Contact NewContact { get; set; }

        #endregion

        #region Команды

        #region Команда принятия изменений

        public ICommand AceptChangesCommand { get; }

        private void OnAceptChangesCommandExecuted(object p)
        {
            if (DataTransfer.CurentContact == null)
            {
                AddContactMethod();
            }
            else 
            {
                EditContactMethod();
            }
            GenerateQrCode();
            Window win = p as Window;
            win.Close();
        }

        private bool CanAceptChangesCommandExecuted(object p) => true;

        #endregion

        #endregion

        #region Методы

        /// <summary>
        /// Обновляет форму редактирования контакта.
        /// </summary>
        void UpdateWindowMethod()
        {
            if (DataTransfer.CurentContact == null)
            {
                return;
            }
            NewName = DataTransfer.CurentContact.Name;
            NewSurname = DataTransfer.CurentContact.Surname;
            NewPatronymic = DataTransfer.CurentContact.Patronymic;
            NewPhone = DataTransfer.CurentContact.Phone;
            NewEmail = DataTransfer.CurentContact.Email;
        }

        /// <summary>
        /// Добавляет новый контакт.
        /// </summary>
        void AddContactMethod()
        {
            NewContact = new Contact();
            NewContact.Name = NewName;
            NewContact.Surname = NewSurname;
            NewContact.Patronymic = NewPatronymic;
            NewContact.Phone = NewPhone;
            NewContact.Email = NewEmail;
            DataTransfer.Contacts.Add(NewContact);
        }

        /// <summary>
        /// Редактирует контакт.
        /// </summary>
        void EditContactMethod()
        {
            DataTransfer.CurentContact.Name = NewName;
            DataTransfer.CurentContact.Surname = NewSurname;
            DataTransfer.CurentContact.Patronymic = NewPatronymic;
            DataTransfer.CurentContact.Phone = NewPhone;
            DataTransfer.CurentContact.Email = NewEmail;
        }

        /// <summary>
        /// Генерирует qr-code.
        /// </summary>
        void GenerateQrCode()
        {
            QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = new QrCode();
            var fileName = Path.Combine(ProjectInformationHelper.ContactAppImagesPath, $"{NewSurname}.JPEG");

            encoder.TryEncode(NewEmail, out qrCode);
            WriteableBitmapRenderer wRenderer = new WriteableBitmapRenderer(new FixedModuleSize(2, QuietZoneModules.Two), Colors.Black, Colors.White);
            WriteableBitmap wBitmap = new WriteableBitmap(58, 58, 240, 240, PixelFormats.Gray8, null);
            wRenderer.Draw(wBitmap, qrCode.Matrix);

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                BitmapEncoder encoder2 = new PngBitmapEncoder();
                encoder2.Frames.Add(BitmapFrame.Create(wBitmap));
                encoder2.Save(fileStream);
            }
        }
               

        #endregion

        public ContactWindowViewModel()
        {
            AceptChangesCommand = new LambdaCommand(OnAceptChangesCommandExecuted, CanAceptChangesCommandExecuted);

            UpdateWindowMethod();
        }
    }
}
