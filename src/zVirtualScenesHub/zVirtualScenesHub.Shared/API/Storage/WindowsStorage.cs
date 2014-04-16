using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace zVirtualClient.Storage
{
    /// <summary>
    /// IsolatedStorage device derived class
    /// </summary>
    public class WindowsStorage : IStoreage
    {


        public WindowsStorage() { }

        public async Task<bool> Save(string file, System.IO.MemoryStream contents)
        {
            byte[] data = contents.ToArray();

            var folder = ApplicationData.Current.LocalFolder;
            var f = await folder.CreateFileAsync(file, CreationCollisionOption.ReplaceExisting);

            using (var s = await f.OpenStreamForWriteAsync())
            {
                await s.WriteAsync(data, 0, data.Length);
            }
            return true;
        }

        public async Task<string> Load(string fileName)
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



        public async Task<bool> FileExists(string file)
        {
            var files = await ApplicationData.Current.LocalFolder.GetFilesAsync();
            var existing = (from f in files where f.Name == file select f).FirstOrDefault();
            return existing != null;
        }
    }
}