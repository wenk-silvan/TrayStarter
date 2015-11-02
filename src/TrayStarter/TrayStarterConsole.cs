using System;
using System.Diagnostics;
using TrayStarter.Commands;
using System.Security;

namespace TrayStarter
{
   internal sealed class TrayStarterConsole
   {
      public void Run()
      {
         Console.WriteLine("Welcome to TrayStarter");
         Console.WriteLine();
         Console.WriteLine("What would you like to do?");

         while (true)
         {
            Console.Write("> ");
            var input = Console.ReadLine();
            Console.WriteLine();

            try
            {
               if (input == "help")
               {
                  foreach (var commandName in CommandManager.Instance.Commands)
                  {
                     Console.WriteLine("- {0}", commandName);
                  }
                  Console.WriteLine();
               }
               else
               {
                  var command = CommandManager.Instance[input];
                  var startInfo = TrayStarterConsole.CreateStartInfo(command);
                  switch (command.RunAs)
                  {
                     case RunAsType.Administrator: Process.Start(startInfo); break;
                     case RunAsType.User:
                         ProcessHelper.StartProcessAsUser(startInfo,
                            TrayStarter.Properties.Settings.Default.Domain,
                            TrayStarter.Properties.Settings.Default.UserName,
                            TrayStarter.Properties.Settings.Default.Password);
                            break;
                  }
               }
            }
            catch (TrayStarterException ex)
            {
               Console.WriteLine(ex.Message);
               Console.WriteLine();
            }
         }
      }

      /// <summary>
      /// Creates the start information for the process.
      /// </summary>
      /// <param name="command">The command.</param>
      private static ProcessStartInfo CreateStartInfo(CommandItem command)
      {
         var startInfo = new ProcessStartInfo();
         startInfo.FileName = "cmd.exe";
         startInfo.Arguments = string.Format("/C \"{0}\"", command.Command);
         startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
         startInfo.RedirectStandardOutput = true;
         startInfo.UseShellExecute = false;
         startInfo.CreateNoWindow = true;
         return startInfo;
      }
   }
}