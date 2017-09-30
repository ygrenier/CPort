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
                    mode++;
                    break;
                case 'a':
                    fMode = CFileMode.Append;
                    mode++;
                    break;
                case 'r':
                default:
                    fMode = CFileMode.Read;
                    mode++;
                    break;
            }
            if (mode == '+') { fMode |= CFileMode.Update; mode++; }
            if (mode == 'b') { fMode |= CFileMode.Binary; mode++; }
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
            var res = SystemHost.OpenFile(filename.GetString(), fMode);
            if (res?.Item1 == null) return null;

            return new FILE(res.Item1, fMode, res.Item2 ?? SystemHost.DefaultFileEncoding);
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
            var res = SystemHost.OpenFile(filename.GetString(), fMode);
            if (res?.Item1 == null) return null;

            stream.Reopen(res.Item1, fMode, res.Item2 ?? SystemHost.DefaultFileEncoding);
            return stream;
        }

        /// <summary>
        /// fclose()
        /// </summary>
        public static int fclose(FILE file)
        {
            if (file == null || file.Source == null) return EOF;
            file.Dispose();
            return 0;
        }

    }
#pragma warning restore IDE1006
}
