using System;

namespace CPort
{
    /// <summary>
    /// C port static class
    /// </summary>
    public static partial class C
    {
        /// <summary>
        /// NULL pointer
        /// </summary>
        public static readonly NullPointer NULL = NullPointer.Null;

        #region System host

        static ISystemHost _syshost = null;

        /// <summary>
        /// Define the system host
        /// </summary>
        public static void SetSystemHost(ISystemHost system) => _syshost = system;

        /// <summary>
        /// Access to the current system host
        /// </summary>
        public static ISystemHost SystemHost => _syshost ?? (_syshost = new DefaultSystemHost());

        #endregion

    }
}
