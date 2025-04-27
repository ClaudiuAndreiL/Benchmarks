using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.DeniedContent.GraphVersion
{

    public enum DeniedMatchTypeEnum
    {
        /// <summary>
        /// Match found anywhere in the input, allowing substitutions.
        /// e.g., "1nfo" or "bas1nf0asd" matches "info"
        /// </summary>
        ContainsMatch,

        /// <summary>
        /// Must match the entire input string, but substitutions are allowed.
        /// e.g., "1ng" matches "ing"
        /// </summary>
        FuzzyMatch,

        /// <summary>
        /// Must match exactly — same characters, no substitutions.
        /// e.g., "ing" matches only "ing"
        /// </summary>
        ExactMatch
    }

    public static class ExperimentalConstants
    {
        public static readonly HashSet<char> EmptyCharacters = new()
            {
                ' ',  // Space
                '.',  // Dot
                '_',
                '-'
            };

        // Dictionary for character substitutions
        public static readonly Dictionary<string, HashSet<string>> Substitutions = new()
            {
                // Lowercase letters
                { "a", new HashSet<string> { "a", "A", "4", "@" } },
                { "b", new HashSet<string> { "b", "B", "8" } },
                { "c", new HashSet<string> { "c", "C" } },
                { "d", new HashSet<string> { "d", "D" } },
                { "e", new HashSet<string> { "e", "E", "3", "€" } },
                { "f", new HashSet<string> { "f", "F" } },
                { "g", new HashSet<string> { "g", "G", "9", "q" } },
                { "h", new HashSet<string> { "h", "H" } },
                { "i", new HashSet<string> { "i", "I", "1", "!", "l" } },
                { "j", new HashSet<string> { "j", "J" } },
                { "k", new HashSet<string> { "k", "K" } },
                { "l", new HashSet<string> { "l", "L", "1", "I" } },
                { "m", new HashSet<string> { "m", "M", "rn", "nn" } },
                { "n", new HashSet<string> { "n", "N" } },
                { "o", new HashSet<string> { "o", "O", "0" } },
                { "p", new HashSet<string> { "p", "P" } },
                { "q", new HashSet<string> { "q", "Q", "9" } },
                { "r", new HashSet<string> { "r", "R" } },
                { "s", new HashSet<string> { "s", "S", "$" } },
                { "t", new HashSet<string> { "t", "T", "7" } },
                { "u", new HashSet<string> { "u", "U", "v" } },
                { "v", new HashSet<string> { "v", "V" } },
                { "w", new HashSet<string> { "w", "W", "vv" } },
                { "x", new HashSet<string> { "x", "X" } },
                { "y", new HashSet<string> { "y", "Y" } },
                { "z", new HashSet<string> { "z", "Z", "2" } },

                // Uppercase letters
                { "A", new HashSet<string> { "A", "a", "4", "@" } },
                { "B", new HashSet<string> { "B", "b", "8" } },
                { "C", new HashSet<string> { "C", "c" } },
                { "D", new HashSet<string> { "D", "d" } },
                { "E", new HashSet<string> { "E", "e", "3", "€" } },
                { "F", new HashSet<string> { "F", "f" } },
                { "G", new HashSet<string> { "G", "g", "9", "q" } },
                { "H", new HashSet<string> { "H", "h" } },
                { "I", new HashSet<string> { "I", "i", "1", "!" } },
                { "J", new HashSet<string> { "J", "j" } },
                { "K", new HashSet<string> { "K", "k" } },
                { "L", new HashSet<string> { "L", "l", "1", "I" } },
                { "M", new HashSet<string> { "M", "m", "rn", "nn" } },
                { "N", new HashSet<string> { "N", "n" } },
                { "O", new HashSet<string> { "O", "o", "0" } },
                { "P", new HashSet<string> { "P", "p" } },
                { "Q", new HashSet<string> { "Q", "q", "9" } },
                { "R", new HashSet<string> { "R", "r" } },
                { "S", new HashSet<string> { "S", "s", "$" } },
                { "T", new HashSet<string> { "T", "t", "7" } },
                { "U", new HashSet<string> { "U", "u", "v" } },
                { "V", new HashSet<string> { "V", "v" } },
                { "W", new HashSet<string> { "W", "w", "vv" } },
                { "X", new HashSet<string> { "X", "x" } },
                { "Y", new HashSet<string> { "Y", "y" } },
                { "Z", new HashSet<string> { "Z", "z", "2" } },

                // Digits
                { "0", new HashSet<string> { "0", "o", "O" } },
                { "1", new HashSet<string> { "1", "i", "I", "l", "!" } },
                { "2", new HashSet<string> { "2", "Z" } },
                { "3", new HashSet<string> { "3", "E" } },
                { "4", new HashSet<string> { "4", "A" } },
                { "5", new HashSet<string> { "5", "S" } },
                { "6", new HashSet<string> { "6", "G" } },
                { "7", new HashSet<string> { "7", "T" } },
                { "8", new HashSet<string> { "8", "B" } },
                { "9", new HashSet<string> { "9", "g", "q" } },

                // Multi-character keys
                { "rn", new HashSet<string> { "m" } },
                { "nn", new HashSet<string> { "m" } },
                { "vv", new HashSet<string> { "w" } },

                // Special characters
                //TODO: whatever characters we move to EmptyCharacters need removing from here
                { "@", new HashSet<string> { "@", "a", "4" } },
                { "£", new HashSet<string> { "£", "L" } },
                { "$", new HashSet<string> { "$", "S" } },
                { "^", new HashSet<string> { "^" } },
                { "{", new HashSet<string> { "{" } },
                { "}", new HashSet<string> { "}" } },
                { "\\", new HashSet<string> { "\\" } },
                { "/", new HashSet<string> { "/" } },
                { "[", new HashSet<string> { "[" } },
                { "]", new HashSet<string> { "]" } },
                { "(", new HashSet<string> { "(" } },
                { ")", new HashSet<string> { ")" } },
                { "~", new HashSet<string> { "~" } },
                { "€", new HashSet<string> { "€", "E" } },
                { "!", new HashSet<string> { "!", "1", "i", "I" } },
                { "\"", new HashSet<string> { "\"" } },
                { "%", new HashSet<string> { "%" } },
                { "&", new HashSet<string> { "&" } },
                { "'", new HashSet<string> { "'" } },
                { "*", new HashSet<string> { "*" } },
                { "+", new HashSet<string> { "+" } },
                { "<", new HashSet<string> { "<", ">" } },
                { ">", new HashSet<string> { ">", "<" } },
                { "=", new HashSet<string> { "=" } },
                { "?", new HashSet<string> { "?" } }
            };

        public static readonly Dictionary<string, HashSet<string>> MultiCharSubstitutions = Substitutions!
            .Where(x => x.Key.Length > 1)
            .ToDictionary(z => z.Key, v => v.Value);

        public static readonly Dictionary<char, string> SingleCharToStringDict = Substitutions
            .Keys.Where(x => x.Length == 1)
            .Select(x => x[0])
            .Distinct()
            .ToDictionary(x => x, v => v.ToString());
    }


}
