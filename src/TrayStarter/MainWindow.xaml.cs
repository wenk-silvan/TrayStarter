using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.Serialization;
using TrayStarter.Commands;
using System.Diagnostics;

namespace TrayStarter
{
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         this.DataContext = this;
         this.Hide();

         //Create NotifyIcon
         NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
         notifyIcon.Text = "TrayStarter";
         notifyIcon.Icon = new System.Drawing.Icon("Resources/captain-america.ico");
         notifyIcon.Visible = true;


         //Create ContextMenu
         var contextmenu = new ContextMenu();
         notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu();
         var menuItem1 = new System.Windows.Forms.MenuItem("Exit", Exit_Click);
         notifyIcon.ContextMenu.MenuItems.Add(menuItem1);

         foreach (var commandName in CommandManager.Instance.Commands)
         {
            notifyIcon.ContextMenu.MenuItems.Add(new System.Windows.Forms.MenuItem(commandName, ExecuteCommand_Click));
         }

         InitializeComponent();
      }

      private void ExecuteCommand_Click(object sender, EventArgs e)
      {
         try
         {
            var input = "OpenTempDir";
            var command = CommandManager.Instance[input];
            var startInfo = MainWindow.CreateStartInfo(command);
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
         catch (TrayStarterException ex)
         {
            Console.WriteLine(ex.Message);
            Console.WriteLine();
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
         startInfo.RedirectStandardOutput = true;
         startInfo.UseShellExecute = false;
         startInfo.CreateNoWindow = true;
         return startInfo;
      }

      private void Exit_Click(object sender, EventArgs e)
      {
         this.Close();
      }
   }
}
