using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stalagtite.Utility
{
    public static class CharacterSearch
    {
        public const int POSITION_NOT_FOUND = -1;
        public static int FindPosition(char[] testString, char[] p)
        {
            int candidatePosition = 0;
            int checkLength = p.Length;

            for(int pos = 0; pos < testString.Length; pos++)
            {
                if (testString[pos] == p[candidatePosition])
                    candidatePosition++;
                else
                    candidatePosition = 0;

                if (candidatePosition == checkLength)
                    return pos - (checkLength - 1); //Offset by one since it is a length and not an index.
            }

            return POSITION_NOT_FOUND;
        }
    }
}
