using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGP7.Tools
{
    public class DiskIO
    {
        String systemPath = FileSystem.Current.AppDataDirectory;
        String fullPath;
        public DiskIO(String fileName)
        {
            fullPath = Path.Combine(systemPath, fileName);
        }
        public async void WriteToFile(String text)
        {
            await File.WriteAllTextAsync(fullPath, text);
        }

        public String ReadFromFile()
        {
             return File.ReadAllTextAsync(fullPath).Result;
        }
    }
}
