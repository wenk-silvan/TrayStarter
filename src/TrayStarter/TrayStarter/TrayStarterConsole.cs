using System;
using TrayStarter.Commands;

namespace TrayStarter
{
   internal sealed class TrayStarterConsole
   {
      public void Run()
      {
         Console.WriteLine("Welcome to TrayStarter");
         Console.WriteLine();

         Console.WriteLine("What would you like to start?");
         Console.Write("> ");
         var eingabe = Console.ReadLine();
         Console.WriteLine("Your Command: {0}", eingabe);

         try {
            var command = CommandManager.Instance[eingabe];
            Console.WriteLine(command.Command);
         }
         catch(TrayStarterException ex) {
            Console.WriteLine(ex.Message);
         }

         Console.ReadKey();
      }
   }
}