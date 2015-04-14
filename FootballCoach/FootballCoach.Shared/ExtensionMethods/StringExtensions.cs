using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace Isah.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// test if string is null or empty string("")
        /// </summary>
        [ExcludeFromCodeCoverage]
        public static bool IsNullOrEmpty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Formats a string using string.Format method with the CurrentCulture.
        /// Use it for localizable text
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public static string FormatUser(this string format, params object[] args)
        {
            return String.Format(CultureInfo.CurrentCulture, format, args);
        }

        /// <summary>
        /// Formats a string using string.Format method with the InvariantCulture.
        /// Use this for non-localizable text
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage] 
        public static string FormatInvariant(this string format, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }

        public static T Deserialize<T>(this string xml)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new DataContractSerializer(typeof (T));
                return (T) deserializer.ReadObject(stream);
            }
        }

        public static string Serialize<T>(this T objectToSerialize)
        {
            using (Stream stream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof (T));
                serializer.WriteObject(stream, objectToSerialize);

                stream.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(stream))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }

        public static string FormatUrl(this string url)
        {
            var items = url.Split('/');
            String result = "";
            if (url.StartsWith("/")) result = "/";

            foreach (var item in items.Where(s => !String.IsNullOrEmpty(s)))
            {
                result += item.TrimEnd() + '/';
            }
            if (!url.EndsWith("/")) result = result.TrimEnd('/');
            return result;
        }
    }
}