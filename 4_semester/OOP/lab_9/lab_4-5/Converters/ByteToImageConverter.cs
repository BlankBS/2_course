using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace lab_4_5.Converters
{
    public class ByteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is byte[] bytes && bytes.Length > 0)
            {
                var image = new BitmapImage();
                using (var mem = new MemoryStream(bytes))
                {
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad; // Важно, чтобы закрыть поток
                    image.StreamSource = mem;
                    image.EndInit();
                }
                return image;
            }
            // Возвращаем стандартную иконку, если в базе пусто
            return new BitmapImage(new Uri("D:\\BSTU\\2_course\\4_semester\\OOP\\lab_8\\lab_4-5\\Resources\\Images\\placeholder.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => null;
    }
}