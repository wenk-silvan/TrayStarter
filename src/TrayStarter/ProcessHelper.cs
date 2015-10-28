using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace TrayStarter
{
   internal class ProcessHelper
   {
      private const int Logon32ProviderDefault = 0;
      private const int Logon32LogonInteractive = 2;

      [DllImport("advapi32.dll", SetLastError = true)]
      private static extern bool LogonUser(string username, string domain, string password, int logonType, int logonProvider, out IntPtr token);

      public static bool StartProcessAsUser(ProcessStartInfo commandLine, string domain, string user, string password)
      {
         IntPtr userToken;
         if (!ProcessHelper.LogonUser(user, domain, password, Logon32LogonInteractive, Logon32ProviderDefault, out userToken))
            return false;

         using (WindowsIdentity.Impersonate(userToken))
         {
            return Process.Start(commandLine) != null;
         }
      }
   }
}