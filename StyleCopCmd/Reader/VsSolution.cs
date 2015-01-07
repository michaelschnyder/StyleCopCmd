namespace StyleCopCmd.Reader
{
    using System.Collections.Generic;

    public class VsSolution
    {
        public VsSolution()
        {
            this.Projects = new List<CsProject>();
        }

        public string RootPath { get; set; }

        public List<CsProject> Projects { get; set; }
    }
}
