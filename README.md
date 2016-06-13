# TrayStarter
![Version](https://img.shields.io/badge/version-V1.0.0-red.svg)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![Language](https://img.shields.io/badge/language-C%23%20.Net%204.5-blue.svg)](http://php.net/)

A C# written program, that invokes batch commands from the windows tray bar.

## Settings
Setting | Default Value | Description
--------|---------------|------------
CommandsFile | ./Commands.xml | The commands file to load
Username | ./App.config | The username of this useraccount 
Domain | ./App.config | The domain of this useraccount
Password | ./App.config | The password of this useraccount

### Example Commands File
```
<?xml version="1.0" encoding="utf-8" ?>
<commands>
   <commanditem name="OpenExplorer" runas="administrator">
      <command>explorer</command>
   </commanditem>
   <commanditem name="Github" runas="user">
      <command>start https://github.com/</command>
   </commanditem>
</commands>
```

### Example App Config File
```
<configuration>
  ...
  ...
   <userSettings>
    <TrayStarter.Properties.Settings>
       <setting name="CommandsFile" serializeAs="String">
          <value>./Commands.xml</value>
       </setting>
       <setting name="UserName" serializeAs="String">
          <value>Foo</value>
       </setting>
      </TrayStarter.Properties.Settings>
   </userSettings>
</configuration>
```

## License
MIT
