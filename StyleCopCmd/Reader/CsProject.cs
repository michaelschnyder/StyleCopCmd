using System;
using System.Collections.Generic;
using System.IO;

namespace StyleCopCmd.Reader
{
    /// <summary>
    /// The cs project.
    /// </summary>
    public class CsProject
    {
        public CsProject()
        {
            this.Files = new List<FileInfo>();
        }

        public string Directory { get; set; }

        public List<FileInfo> Files { get; set; }

        public Guid Guid { get; set; }
        
        public string File { get; set; }
        
        public string RootNamespace { get; set; }
        
        public string AssemblyName { get; set; }
    }
}