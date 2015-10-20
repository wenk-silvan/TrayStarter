//-----------------------------------------------------------------------
// <copyright file="CommandItem.cs" company="Schindler Elevator Ltd.">
//    Copyright (c) 2015 Schindler Elevator Ltd. All rights reserved.
//    
//    This software is the confidential and proprietary information of
//    Schindler Elevator Ltd. ("Confidential Information"). You shall not disclose
//    such confidential information and shall use it only in accordance with
//    the terms of the license agreement you entered into with Schindler Ltd.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrayStarter.Commands
{
   public class CommandItem
   {  
      [XmlElement("name")]
      public string Name { get; set; }

      [XmlElement("command")]
      public string Command { get; set; }

      [XmlAttribute("runas")]
      public RunAsType RunAs { get; set; } 
   }
}
