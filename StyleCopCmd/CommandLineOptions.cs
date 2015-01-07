using CommandLine;
using CommandLine.Text;

namespace StyleCopCmd
{
    using System.Collections.Generic;

    // Define a class to receive parsed values
    public class CommandLineOptions
    {
        public IList<string> Items { get; set; }

        [OptionArray('s', "solution", HelpText = "Input file to be processed.")]
        public string[] Solutions { get; set; }

        [OptionArray('p', "project", HelpText = "Prints all messages to standard output.")]
        public List<string> Projects { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
