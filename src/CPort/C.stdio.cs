using System;
using System.Collections.Generic;
using System.Text;

namespace CPort
{
#pragma warning disable IDE1006
    /// <summary>
    /// stdio.h functions
    /// </summary>
    static partial class C
    {
        /// <summary>
        /// End of file marker
        /// </summary>
        public const int EOF = -1;

        /// <summary>
        /// Extract file mode
        /// </summary>
        private static CFileMode ExtractFileMode(Pointer<char> mode)
        {
            CFileMode fMode = CFileMode.Read;
            switch (mode.Value)
            {
                case 'w':
                    fMode = CFileMode.Write;
                    break;
                case 'a':
                    fMode = CFileMode.Append;
                    break;
                case 'r':
                default:
                    fMode = CFileMode.Read;
                    break;
            }
            mode++;
            if (mode++ == '+') fMode |= CFileMode.Update;
            if (mode++ == 'b') fMode |= CFileMode.Binary;
            return fMode;
        }

        /// <summary>
        /// fopen()
        /// </summary>
        public static FILE fopen(Pointer<char> filename, Pointer<char> mode)
        {
            if (filename == NULL || mode == NULL) return null;

            // Extract mode
            CFileMode fMode = ExtractFileMode(mode);

            // Open file from host
            var stream = SystemHost.OpenFile(filename.GetString(), fMode, out Encoding enc);
            if (stream == null) return null;

            return new FILE(stream, fMode, enc ?? SystemHost.DefaultFileEncoding);
        }

        /// <summary>
        /// freopen()
        /// </summary>
        public static FILE freopen(Pointer<char> filename, Pointer<char> mode, FILE stream)
        {
            if (filename == NULL || mode == NULL || stream == null) return null;

            // Extract mode
            CFileMode fMode = ExtractFileMode(mode);

            // Reopen file from host
            var s = C.SystemHost.OpenFile(filename.GetString(), fMode, out Encoding enc);
            if (s == null) return null;

            stream.Reopen(s, fMode, enc ?? SystemHost.DefaultFileEncoding);
            return stream;
        }

        /// <summary>
        /// fclose()
        /// </summary>
        public static int fclose(FILE file)
        {
            if (file == null) return EOF;
            file.Dispose();
            return 0;
        }

    }
#pragma warning restore IDE1006
}
