using System.IO;
using System.Xml.Serialization;

using StyleCopCmd.Writer.NUnit.Model;

namespace StyleCopCmd.Writer.NUnit
{
    public class NUnitResultWriter
    {
        private readonly string outputFile;

        public NUnitResultWriter(string outputFile)
        {
            this.outputFile = outputFile;
        }

        public void Write()
        {
            var serializer = new XmlSerializer(typeof(resultsType));

            serializer.Serialize(new FileStream(this.outputFile, FileMode.CreateNew), new resultsType());
        }
    }
}
