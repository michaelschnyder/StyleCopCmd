namespace StyleCopCmd.Reader
{
    using System;
    using System.IO;

    using Onion.SolutionParser.Parser;

    public class SolutionReader : IDisposable
    {
        private static readonly Guid CSharpProjectGuidType = new Guid("fae04ec0-301f-11d3-bf4b-00c04f79efbc");

        private readonly string filePath;

        public SolutionReader(string solutionFile)
        {
            this.filePath = FileUtil.GetPathRooted(solutionFile);
        }

        public VsSolution Read()
        {
            var solution = new VsSolution();

            var solutionRootPath = Path.GetDirectoryName(this.filePath);
            var solutionModel = SolutionParser.Parse(this.filePath);

            solution.RootPath = solutionRootPath;

            foreach (var projectItem in solutionModel.Projects)
            {
                if (projectItem.TypeGuid == CSharpProjectGuidType)
                {
                    var projectFilePath = Path.Combine(solutionRootPath, projectItem.Path);

                    using (var projectReader = new ProjectReader(projectFilePath))
                    {
                        var project = projectReader.Read();
                        solution.Projects.Add(project);
                    }
                }
            }

            return solution;
        }

        public void Dispose()
        {
        }
    }
}
