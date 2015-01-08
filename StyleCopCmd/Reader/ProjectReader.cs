using System;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Xml.Linq;

namespace StyleCopCmd.Reader
{
    /// <summary>
    /// This readed has the ability to collect information from a csproj file like to one below.
    /// <code>
    ///   <PropertyGroup>
    ///     <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    ///     <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    ///     <ProjectGuid>{D43514A9-B8FC-4C9D-BAAF-87033E9B6B3A}</ProjectGuid>
    ///     <OutputType>Exe</OutputType>
    ///     <AppDesignerFolder>Properties</AppDesignerFolder>
    ///     <RootNamespace>StyleCopCmd</RootNamespace>
    ///     <AssemblyName>StyleCopCmd</AssemblyName>
    ///     <TargetFrameworkVersion>v4.5.1</TargetFrameworkVersion>
    ///     <FileAlignment>512</FileAlignment>
    ///     <TargetFrameworkProfile />
    ///   </PropertyGroup>
    /// 
    ///   <ItemGroup>
    ///     <Compile Include="CommandLineOptions.cs" />
    ///     <Compile Include="Reporter\ConsoleReporter.cs" />
    ///     <Compile Include="Core\IStyleCopIssueReporter.cs" />
    ///     <Compile Include="Core\StyleCopExecutor.cs" />
    ///     <Compile Include="Reader\CsProject.cs" />
    ///     <Compile Include="Reader\FileUtil.cs" />
    ///     <Compile Include="Program.cs" />
    ///     <Compile Include="Reader\ProjectReader.cs" />
    ///     <Compile Include="Properties\AssemblyInfo.cs" />
    ///     <Compile Include="Reader\SolutionReader.cs" />
    ///     <Compile Include="Reader\VsSolution.cs" />
    ///     <Compile Include="Reporter\NUnit\Model.cs" />
    ///     <Compile Include="Reporter\NUnit\NUnitResultWriter.cs" />
    ///   </ItemGroup>
    /// </code>
    /// </summary>
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
            project.File = this.projectFile;

            var projectGuidElement = this.xmldoc.Descendants(MsBuildNamespace + "ProjectGuid");
            project.Guid = Guid.Parse(projectGuidElement.First().Value);

            var projecRootNamespaceElement = this.xmldoc.Descendants(MsBuildNamespace + "RootNamespace");
            project.RootNamespace = projecRootNamespaceElement.First().Value;

            var projectAssemblyNameElement = this.xmldoc.Descendants(MsBuildNamespace + "AssemblyName");
            project.AssemblyName = projectAssemblyNameElement.First().Value;

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
