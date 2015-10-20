using System.Xml.Serialization;

namespace TrayStarter.Commands
{
   /// <summary>
   /// Contains meta and detailed information about a executable command.
   /// </summary>
   public class CommandItem
   {
      /// <summary>
      /// Gets or sets the name of the command. This name will be used for the command line interface and the context menu.
      /// </summary>
      [XmlAttribute("name")]
      public string Name { get; set; }

      /// <summary>
      /// Gets or sets the run as type.
      /// </summary>
      [XmlAttribute("runas")]
      public RunAsType RunAs { get; set; }

      /// <summary>
      /// Gets or sets the command.
      /// </summary>
      [XmlElement("command")]
      public string Command { get; set; }
   }
}
