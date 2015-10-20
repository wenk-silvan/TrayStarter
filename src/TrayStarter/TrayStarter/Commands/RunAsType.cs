using System.Xml.Serialization;

namespace TrayStarter.Commands
{
   /// <summary>
   /// The runtype of the command.
   /// </summary>
   public enum RunAsType
   {
      /// <summary>
      /// A command with this runtype, will be executed with administrator priviledges.
      /// </summary>
      [XmlEnum("administrator")]
      Administrator,

      /// <summary>
      /// A command with this runtype, will be executed with user priviledges.
      /// </summary>
      [XmlEnum("user")]
      User
   }
}
