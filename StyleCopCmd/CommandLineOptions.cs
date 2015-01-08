using CommandLine;
using CommandLine.Text;
    using System.Collections.Generic;


namespace StyleCopCmd
{
    // Define a class to receive parsed values
    public class CommandLineOptions
    {
        public IList<string> Items { get; set; }

        [OptionArray('s', "solution", HelpText = "Define one or more solution for which the analysation should be done")]
        public string[] Solutions { get; set; }

        [OptionArray('p', "project", HelpText = "Define one or more projects for which the analysation should be done")]
        public List<string> Projects { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
