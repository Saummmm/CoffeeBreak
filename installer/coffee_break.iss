[Setup]
AppName=CoffeeBreak
AppVersion=1.0
DefaultDirName={autopf}\CoffeeBreak
DefaultGroupName=CoffeeBreak
OutputDir="../output"
OutputBaseFilename=CoffeeBreakInstaller
Compression=lzma
SolidCompression=yes

[Files]
Source: "..\bin\Release\CoffeeBreak.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\atdesk.ico"; DestDir: "{app}"
Source: "..\away.ico"; DestDir: "{app}"

[Icons]
Name: "{group}\CoffeeBreak"; Filename: "{app}\CoffeeBreak.exe"
Name: "{userstartup}\CoffeeBreak"; Filename: "{app}\CoffeeBreak.exe"

[Run]
Filename: "{app}\CoffeeBreak.exe"; Description: "Launch now"; Flags: nowait postinstall skipifsilent
