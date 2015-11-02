using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace TrayStarter.Commands
{
   public class CommandManager
   {
      /// <summary>
      /// Sets a key and a value for the commands.
      /// </summary>
      public IDictionary<string, CommandItem> commands;

      private static CommandManager instance = null;

      /// <summary>
      /// Prevents a default instance of the <see cref="CommandManager"/> class from being created.
      /// </summary>
      private CommandManager()
      {
         this.commands = this.GetCommands().ToDictionary(c => c.Name, c => c);
      }

      /// <summary>
      /// This singleton get the instance if there is not an instance yet.
      /// </summary>
      public static CommandManager Instance
      {
         get { return CommandManager.instance ?? (CommandManager.instance = new CommandManager()); }
      }

      /// <summary>
      /// Gets the commands and Sets key and value for the commands
      /// </summary>
      public ICollection<string> Commands { get { return this.commands.Keys; } }

      /// <summary>
      /// Gets the <see cref="CommandItem"/> with the specified name.
      /// </summary>
      /// <exception cref="TrayStarterException">The entered command was not found in the command list.</exception>
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

      /// <summary>
      /// Gets the Commands from the xml and deserializes it.
      /// </summary>
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