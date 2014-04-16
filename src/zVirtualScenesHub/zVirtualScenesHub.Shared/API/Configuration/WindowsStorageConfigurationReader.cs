using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using zVirtualClient.Configuration;

namespace zVirtualScenesHub.API.Configuration
{
    public class WindowsStorageConfigurationReader : IConfigurationReader
    {
        private string FileName(string Key)
        {
            return string.Format("{0}.setting");
        }

        public async Task<T> ReadSetting<T>(string Key)
        {
            var data = await ReadFileContentsAsync(FileName(Key));
            if (string.IsNullOrEmpty(data)) return default(T);
            return (T) Convert.ChangeType(data, typeof (T), null);
        }

        public async Task WriteSetting<T>(string Key, T Value)
        {
            string sValue = (string)Convert.ChangeType(Value, typeof(string), null);
            await WriteDataToFileAsync(FileName(Key), sValue);
        }

        public async Task WriteDataToFileAsync(string fileName, string content)
        {
            byte[] data = Encoding.Unicode.GetBytes(content);

            var folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            using (var s = await file.OpenStreamForWriteAsync())
            {
                await s.WriteAsync(data, 0, data.Length);
            }
        }

        public async Task<string> ReadFileContentsAsync(string fileName)
        {
            var folder = ApplicationData.Current.LocalFolder;

            try
            {
                var file = await folder.OpenStreamForReadAsync(fileName);

                using (var streamReader = new StreamReader(file))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
