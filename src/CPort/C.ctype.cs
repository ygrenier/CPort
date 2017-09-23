using System;
using System.Collections.Generic;
using System.Text;

namespace CPort
{
#pragma warning disable IDE1006
    /// <summary>
    /// ctype.h functions
    /// </summary>
    static partial class C
    {
        const string HexaDigitChars = "0123456789ABCDEFabcdef";

        /// <summary>
        /// isalnum()
        /// </summary>
        public static bool isalnum(char c) => Char.IsLetterOrDigit(c);

        /// <summary>
        /// isalpha()
        /// </summary>
        public static bool isalpha(char c) => Char.IsLetter(c);

        /// <summary>
        /// iscntrl()
        /// </summary>
        public static bool iscntrl(char c) => Char.IsControl(c);

        /// <summary>
        /// isdigit()
        /// </summary>
        public static bool isdigit(char c) => Char.IsDigit(c);

        /// <summary>
        /// isgraph()
        /// </summary>
        public static bool isgraph(char c) => c > 0x20 && c <= 0x7E;

        /// <summary>
        /// islower()
        /// </summary>
        public static bool islower(char c) => Char.IsLower(c);

        /// <summary>
        /// isprint()
        /// </summary>
        public static bool isprint(char c) => c >= 0x20 && c <= 0x7E;

        /// <summary>
        /// ispunct()
        /// </summary>
        public static bool ispunct(char c) => Char.IsPunctuation(c);

        /// <summary>
        /// isspace()
        /// </summary>
        public static bool isspace(char c) => Char.IsWhiteSpace(c);

        /// <summary>
        /// isupper()
        /// </summary>
        public static bool isupper(char c) => Char.IsUpper(c);

        /// <summary>
        /// isxdigit()
        /// </summary>
        public static bool isxdigit(char c) => HexaDigitChars.IndexOf(c) >= 0;

        /// <summary>
        /// tolower()
        /// </summary>
        public static char tolower(char c) => Char.ToLower(c);

        /// <summary>
        /// toupper()
        /// </summary>
        public static char toupper(char c) => Char.ToUpper(c);
    }
#pragma warning restore IDE1006
}
