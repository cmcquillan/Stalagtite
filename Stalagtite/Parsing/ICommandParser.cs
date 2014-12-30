using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.Parsing
{
    public interface ICommandParser
    {
        Command GetCommand(string cmdText);
    }
}
