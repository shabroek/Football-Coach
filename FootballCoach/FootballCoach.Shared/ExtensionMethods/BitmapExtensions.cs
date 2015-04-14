using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Media.Imaging;

namespace Isah.Core
{
    public static class BitmapExtensions
    {
        /// <summary>
        /// Convert a byte[] to a BitmapImage so it can be shown in a WPF application
        /// </summary>
        [ExcludeFromCodeCoverage]
        public static BitmapImage ToBitmap(this byte[] value)
        {
            if (value == null) return null;

            try
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.CreateOptions = BitmapCreateOptions.None;
                bi.CacheOption = BitmapCacheOption.Default;
                bi.StreamSource = new MemoryStream(value);
                bi.EndInit();
                bi.Freeze();
                return bi;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}