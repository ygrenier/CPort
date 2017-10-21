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
        private static CFileMode ExtractFileMode(PChar mode)
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
        public static FILE fopen(PChar filename, PChar mode)
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
        public static FILE freopen(PChar filename, PChar mode, FILE stream)
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

        /// <summary>
        /// fflush()
        /// </summary>
        public static int fflush(FILE file)
        {
            if (file?.Source == null) return EOF;
            file.Source.Flush();
            return 0;
        }

        /// <summary>
        /// fprintf()
        /// </summary>
        public static int fprintf(FILE stream, PChar format, params object[] args)
        {
            return stream.Write(sprintf((string)format, args));
        }

        /// <summary>
        /// sprintf()
        /// </summary>
        public static int sprintf(PChar s, PChar format, params object[] args)
        {
            string r = sprintf((string)format, args);
            if (string.IsNullOrEmpty(r)) return 0;
            strcpy(s, r);
            return r.Length;
        }

        /// <summary>
        /// fgetc()
        /// </summary>
        public static int fgetc(FILE stream)
        {
            if (stream == null) return EOF;
            return stream.Read();
        }

        /// <summary>
        /// fgets()
        /// </summary>
        public static PChar fgets(PChar s, int n, FILE stream)
        {
            if (stream == null || s.IsNull || n == 0) return NULL;
            int cnt = 0;
            PChar p = s;
            n--;
            while (cnt < n)
            {
                int c = stream.Read();
                if (c == EOF)
                {
                    if (cnt == 0) return NULL;
                    break;
                }
                p.Value = (char)c;
                p++;
                if (c == 10) break;
                cnt++;
            }
            p.Value = '\0';
            return s;
        }

        /// <summary>
        /// getc()
        /// </summary>
        public static int getc(FILE stream)
        {
            return fgetc(stream);
        }

        /// <summary>
        /// fputc()
        /// </summary>
        public static int fputc(int c, FILE stream)
        {
            if (stream == null || c <= 0) return EOF;
            if (stream.Write(((char)c).ToString()) == EOF) return EOF;
            return c;
        }

        /// <summary>
        /// fputs()
        /// </summary>
        public static int fputs(PChar s, FILE stream)
        {
            if (stream == null || s.IsNull) return EOF;
            return stream.Write(s.GetString());
        }

        /// <summary>
        /// putc()
        /// </summary>
        public static int putc(int c, FILE stream)
        {
            return fputc(c, stream);
        }

        /// <summary>
        /// ungetc()
        /// </summary>
        public static int ungetc(int c, FILE stream)
        {
            if (stream == null || c == EOF) return EOF;
            return stream.UnreadChar((char)c) ? c : EOF;
        }
    }
#pragma warning restore IDE1006
}
