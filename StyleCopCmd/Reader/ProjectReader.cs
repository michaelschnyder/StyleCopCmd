using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace StyleCopCmd.Reader
{
    public class ProjectReader : IDisposable
    {
        private static readonly XNamespace MsBuildNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        private XDocument xmldoc;

        private string projectFile;

        public ProjectReader(string projectFile)
        {
            this.projectFile = projectFile;
            this.xmldoc = XDocument.Load(projectFile);
        }

        public CsProject Read()
        {
            var project = new CsProject();

            project.Directory = Path.GetDirectoryName(this.projectFile);

            var projectGuidElement = this.xmldoc.Descendants(MsBuildNamespace + "ProjectGuid");

            project.Guid = Guid.Parse(projectGuidElement.First().Value);

            var compileIncludeElements = this.xmldoc.Descendants(MsBuildNamespace + "Compile");
            
            foreach (var file in compileIncludeElements)
            {
                string localPath = file.Attribute("Include").Value;

                project.Files.Add(new FileInfo(Path.Combine(Path.GetDirectoryName(this.projectFile), localPath)));
            }

            return project;
        }

        public void Dispose()
        {
            this.xmldoc = null;
        }
    }
}
