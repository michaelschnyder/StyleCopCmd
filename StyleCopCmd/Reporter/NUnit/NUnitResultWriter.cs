using System.IO;
using System.Xml.Serialization;

namespace StyleCopCmd.Reporter.NUnit
{
    public class NUnitResultWriter
    {
        private readonly string outputFile;

        public NUnitResultWriter(string outputFile)
        {
            this.outputFile = outputFile;

            if (File.Exists(this.outputFile))
            {
                File.Delete(this.outputFile);
            }
        }

        public void Write(resultType root)
        {
            var serializer = new XmlSerializer(typeof(resultType));

            using (var stream = new FileStream(this.outputFile, FileMode.Create))
            {
                serializer.Serialize(stream, root);
            }
        }
    }
}
