/// <summary>
/// Created at 2021/3/28 23:42.
/// @author Liangcheng Juves
/// </summary>

using System.Runtime.InteropServices;
using System.Text;

namespace DesktopShortcutCreator
{
    class Kernel32
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);


        bool WriteINI(string SectionName, string KeyName, string StringToWrite, string INIFileName)
        {
            bool Return;
            Return = WritePrivateProfileString(SectionName, KeyName, StringToWrite, INIFileName);
            return Return;
        }

        /// <summary>
        /// Sample Code:
        /// StringBuilder sb = new StringBuilder(500);
        /// uint res = GetPrivateProfileString("AppName", "KeyName", "", sb, (uint)sb.Capacity, @"c:\test.ini");
        /// Console.WriteLine(sb.ToString());
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString,
         uint nSize, string lpFileName);
         
    }
}