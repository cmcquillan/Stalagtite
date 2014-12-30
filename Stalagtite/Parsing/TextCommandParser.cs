using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.Parsing
{
    public class TextCommandParser : ICommandParser
    {
        public Command GetCommand(string cmdText)
        {
            var cmd = new Command();

            string[] cmdElements = cmdText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (cmdElements.Length == 0)
                return Command.Empty;

            cmd.Verb = cmdElements[0].ToLower();
            cmd.ArgList = cmdElements.Skip(1).ToArray();

            int spaceIndex = cmdText.IndexOf(' ') + 1;
            cmd.CmdText = cmdText;
            cmd.ArgText = cmdText.Substring(spaceIndex, cmdText.Length - spaceIndex);
            return cmd;
        }
    }
}
