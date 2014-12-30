using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.Actions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class VerbAttribute : Attribute
    {
        public VerbAttribute(string commandVerb)
        {
            Verb = commandVerb;
        }

        public string Verb { get; private set; }

        public bool AllowPartial { get; set; }
    }
}
