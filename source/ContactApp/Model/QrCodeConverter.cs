using System;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Windows.Data;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;

namespace YourApp.Converters
{
    public class EmailToQrCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Проверяем, что входное значение - это строка
            if (value is string email)
            {
                // Генерируем ссылку на почту в формате mailto:
                string mailtoLink = $"mailto:{email}";

                // Создаем экземпляр генератора QR-кода
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(mailtoLink, QRCodeGenerator.ECCLevel.Q);

                // Создаем экземпляр изображения QR-кода из объекта QRCodeData
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                // Сохраняем изображение QR-кода во временном файле
                string tempFilePath = Path.GetTempFileName();
                qrCodeImage.Save(tempFilePath, ImageFormat.Png);

                // Создаем объект BitmapImage для отображения изображения в элементе управления Image
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = new Uri(tempFilePath);
                bitmapImage.EndInit();

                return bitmapImage;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
