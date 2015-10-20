using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrayStarter.Commands
{
   [XmlRoot("commands")] 
   public class CommandList
    {
      [XmlElement("commanditem")]
      public List<CommandItem> Commands { get; set; }
    }
}
