using System;

namespace TrayStarter
{
   internal class TrayStarterException : Exception
   {
      public TrayStarterException(string message)
         : base(message)
      {
      }
   }
}