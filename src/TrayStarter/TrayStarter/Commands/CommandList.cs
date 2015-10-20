using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrayStarter.Commands
{
   /// <summary>
   /// Contains Data about the command list
   /// </summary>
   [XmlRoot("commands")] 
   public class CommandList
    {
       /// <summary>
       /// Gets or sets a list of commands.
       /// </summary>
      [XmlElement("commanditem")]
      public List<CommandItem> Commands { get; set; }
    }
}
