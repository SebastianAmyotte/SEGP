using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGP7.Tools
{
    // Author: Sebastian Amyotte
    // Description: Simple DiskIO operations that helps abstract code away
    // Usage:
    // String resultsFromDisk = new DiskIO("filename.txt").ReadFromFile(); //Reading
    // new DiskIO("filename.txt").WriteToFile("Hello World"); //Writing
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
