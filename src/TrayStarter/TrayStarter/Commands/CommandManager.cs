using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace TrayStarter.Commands
{
   public class CommandManager
   {
      public IDictionary<string, CommandItem> commands;

      private static CommandManager instance = null;

      private CommandManager()
      {
         this.commands = this.GetCommands().ToDictionary(c => c.Name, c => c);
      }

      public static CommandManager Instance
      {
         get { return CommandManager.instance ?? (CommandManager.instance = new CommandManager()); }
      }

      public CommandItem this[string name]
      {
         get
         {
            try
            {
               return this.commands[name];
            }
            catch (KeyNotFoundException)
            {
               throw new TrayStarterException("The entered command was not found in the command list.");
            }
         }
      }

      private IList<CommandItem> GetCommands()
      {
         var serializer = new XmlSerializer(typeof(CommandList));
         using (var stream = XmlReader.Create(TrayStarter.Properties.Settings.Default.CommandsFile))
         {
            return ((CommandList)serializer.Deserialize(stream)).Commands;
         }
      }
   }
}