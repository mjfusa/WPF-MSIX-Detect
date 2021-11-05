//#undef NET5_0
using System;
using System.Runtime.InteropServices;
using System.Text;
using Windows.ApplicationModel;

namespace WPF_MSIX_Detect
{
    public class Helpers
    {
        const long APPMODEL_ERROR_NO_PACKAGE = 15700L;

#if (! NET5_0)
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder packageFullName);
#endif
        public bool IsRunningInMSIXContainer()
        {
            var result = false;
            if (IsWindows7OrLower)
            {
                return false;
            }
            else
            {
#if (!NET5_0)
                    int length = 0;
                    StringBuilder sb = new StringBuilder(0);
                    int iLen = GetCurrentPackageFullName(ref length, sb);

                    sb = new StringBuilder(length);
                    var res = GetCurrentPackageFullName(ref iLen, sb);
                    result = res != APPMODEL_ERROR_NO_PACKAGE;
#else
                try
                {
                    Package package = Package.Current;
                    result = true;
                }
                catch
                {
                    result = false;
                }
#endif
                return result;
            }
        }
        private bool IsWindows7OrLower
        {
            get
            {
                int versionMajor = Environment.OSVersion.Version.Major;

                int versionMinor = Environment.OSVersion.Version.Minor;

                double version = versionMajor + (double)versionMinor / 10;

                return version <= 6.1;
            }
        }
    }

}
