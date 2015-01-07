using System.IO;
using System.Reflection;

namespace StyleCopCmd.Reader
{
    public class FileUtil
    {
        public static string GetPathRooted(string fileOrFilePath)
        {
            string filePath;

            if (!Path.IsPathRooted(fileOrFilePath))
            {
                filePath = fileOrFilePath;
            }
            else
            {
                filePath = Path.Combine(Assembly.GetExecutingAssembly().Location, fileOrFilePath);
            }

            return Path.GetFullPath(filePath);
        }
    }
}
