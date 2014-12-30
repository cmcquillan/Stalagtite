using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.Parsing
{
    public struct Command : IEquatable<Command>
    {
        public static readonly Command Empty = new Command() { Verb = String.Empty };

        public string Verb { get; set; }
        public string ArgText { get; set; }
        public string[] ArgList { get; set; }
        public string CmdText { get; set; }

        public bool Equals(Command other)
        {
            return String.Equals(Verb, other.Verb);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Command))
                return false;

            return this.Equals((Command)obj);
        }

        public override int GetHashCode()
        {
            return Verb.GetHashCode();
        }

        public static bool operator ==(Command a, Command b)
        {
            return String.Equals(a.Verb, b.Verb, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool operator !=(Command a, Command b)
        {
            return !String.Equals(a.Verb, b.Verb, StringComparison.InvariantCultureIgnoreCase);
        }

    }
}
