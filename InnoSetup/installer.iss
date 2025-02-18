﻿#define MyAppName "WinThumbsPreloader"
#define MyAppReleaseDirectory "..\WinThumbsPreloader\WinThumbsPreloader\bin\Release"
#define MyAppFilename MyAppName + ".exe"
#define MyAppFilepath MyAppReleaseDirectory + "\" + MyAppFilename
#dim Version[4]
#expr GetVersionComponents(MyAppFilepath, Version[0], Version[1], Version[2], Version[3])
#define MyAppVersion Str(Version[0]) + "." + Str(Version[1]) + "." + Str(Version[2])
#define MyAppPublisher1 "Dmitry Bruhov"
#define MyAppPublisher2 "Mutahar Farooq"
#define MyAppId "CF49DD18-AA76-4E79-97C2-4FEAED1AED5F"

//#include <idp.iss>
//#include <idplang\Russian.iss> 
#include "environment.iss"

[Setup]
AppCopyright=Copyright (c) 2018 {#MyAppPublisher1}, Copyright (c) 2025 {#MyAppPublisher2}
AppId={#MyAppId}
AppMutex={#MyAppId}
AppName={#MyAppName}
AppPublisher={#MyAppPublisher1}, {#MyAppPublisher2}
AppPublisherURL=https://github.com/Mfarooq360/WinThumbsPreloader
AppSupportURL=https://github.com/Mfarooq360/WinThumbsPreloader/issues
AppUpdatesURL=https://github.com/Mfarooq360/WinThumbsPreloader/releases
AppVerName={#MyAppName} {#MyAppVersion}
AppVersion={#MyAppVersion}
ArchitecturesAllowed=x86 x64
ArchitecturesInstallIn64BitMode=x64
ChangesEnvironment=yes
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DirExistsWarning=no
DisableReadyPage=yes
DisableProgramGroupPage=yes
LicenseFile=license.txt
MinVersion=6.1
OutputBaseFilename={#MyAppName}-{#MyAppVersion}-setup
OutputDir=Output
ShowLanguageDialog=no
UninstallDisplayIcon={app}\{#MyAppFilename}
UninstallDisplayName={#MyAppName}
VersionInfoTextVersion={#MyAppVersion}
VersionInfoVersion={#MyAppVersion}
WizardImageFile=WizardImageFile.bmp
WizardImageStretch=no
WizardSmallImageFile=WizardSmallImageFile.bmp
SolidCompression=yes
Compression=lzma2/max

[Languages]
Name: en; MessagesFile: "compiler:Default.isl"
Name: ru; MessagesFile: "compiler:Languages\Russian.isl"

[CustomMessages]
; .Net installer
en.NetFrameworkInstallerCaption=Installing .NET Framework 4.5.2. This might take a few minutes...
ru.NetFrameworkInstallerCaption=Установка .NET Framework 4.5.2...

en.NetFrameworkInstallerFail=.NET installation failed with code
ru.NetFrameworkInstallerFail=Ошибка установки .NET Framework. Код ошибки

; Context menu items
en.PreloadThumbnails=Preload thumbnails
ru.PreloadThumbnails=Загрузить эскизы

en.PreloadThumbnailsRecursively=Preload thumbnails recursively
ru.PreloadThumbnailsRecursively=Загрузить эскизы включая подпапки

[Files]
Source: "{#MyAppFilepath}"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyAppFilepath}.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyAppReleaseDirectory}\LICENSE.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyAppReleaseDirectory}\ru\*.resources.dll"; DestDir: "{app}\Languages\ru"; Flags: ignoreversion

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppFilename}"

[Run]
Filename: "{app}\{#MyAppFilename}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Registry]
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}"; ValueType: string; ValueName: "MUIVerb"; ValueData: "{#MyAppName}"
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}"; ValueType: string; ValueName: "Icon"; ValueData: """{app}\{#MyAppFilename}"",0"
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}"; ValueType: string; ValueName: "ExtendedSubCommandsKey"; ValueData: "Directory\shell\{#MyAppName}"
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}\Shell"
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}\Shell\Preload"; ValueType: string; ValueName: "MUIVerb"; ValueData: "{cm:PreloadThumbnails}"
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}\Shell\Preload"; ValueType: string; ValueName: "Icon"; ValueData: """{app}\{#MyAppFilename}"",0"
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}\Shell\Preload\command"; ValueType: string; ValueData: """{app}\{#MyAppFilename}"" -m ""%V"""
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}\Shell\PreloadRecursively"; ValueType: string; ValueName: "MUIVerb"; ValueData: "{cm:PreloadThumbnailsRecursively}"
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}\Shell\PreloadRecursively"; ValueType: string; ValueName: "Icon"; ValueData: """{app}\{#MyAppFilename}"",0"
Root: "HKCR"; Subkey: "Directory\shell\{#MyAppName}\Shell\PreloadRecursively\command"; ValueType: string; ValueData: """{app}\{#MyAppFilename}"" -m -r ""%V"""

Root: "HKCR"; Subkey: "Directory\Background\shell\{#MyAppName}"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "Directory\Background\shell\{#MyAppName}"; ValueType: string; ValueName: "MUIVerb"; ValueData: "{#MyAppName}"
Root: "HKCR"; Subkey: "Directory\Background\shell\{#MyAppName}"; ValueType: string; ValueName: "Icon"; ValueData: """{app}\{#MyAppFilename}"",0"
Root: "HKCR"; Subkey: "Directory\Background\shell\{#MyAppName}"; ValueType: string; ValueName: "ExtendedSubCommandsKey"; ValueData: "Directory\Background\ContextMenus\MenuWinThumbsPreloader"
Root: "HKCR"; Subkey: "Directory\Background\ContextMenus\Menu{#MyAppName}"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "Directory\Background\ContextMenus\Menu{#MyAppName}\shell\Preload"; ValueType: string; ValueName: "MUIVerb"; ValueData: "{cm:PreloadThumbnails}"
Root: "HKCR"; Subkey: "Directory\Background\ContextMenus\Menu{#MyAppName}\shell\Preload"; ValueType: string; ValueName: "Icon"; ValueData: """{app}\{#MyAppFilename}"",0"
Root: "HKCR"; Subkey: "Directory\Background\ContextMenus\Menu{#MyAppName}\shell\Preload\command"; ValueType: string; ValueData: """{app}\{#MyAppFilename}"" -m ""%V"""
Root: "HKCR"; Subkey: "Directory\Background\ContextMenus\Menu{#MyAppName}\shell\PreloadRecursively"; ValueType: string; ValueName: "MUIVerb"; ValueData: "{cm:PreloadThumbnailsRecursively}"
Root: "HKCR"; Subkey: "Directory\Background\ContextMenus\Menu{#MyAppName}\shell\PreloadRecursively"; ValueType: string; ValueName: "Icon"; ValueData: """{app}\{#MyAppFilename}"",0"
Root: "HKCR"; Subkey: "Directory\Background\ContextMenus\Menu{#MyAppName}\shell\PreloadRecursively"; ValueType: string; ValueName: "HasLUAShield"; ValueData: ""
Root: "HKCR"; Subkey: "Directory\Background\ContextMenus\Menu{#MyAppName}\shell\PreloadRecursively\command"; ValueType: string; ValueData: """{app}\{#MyAppFilename}"" -m -r ""%V"""

Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}"; ValueType: string; ValueName: "MUIVerb"; ValueData: "{#MyAppName}"
Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}"; ValueType: string; ValueName: "Icon"; ValueData: """{app}\{#MyAppFilename}"",0"
Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}"; ValueType: string; ValueName: "ExtendedSubCommandsKey"; ValueData: "Drive\shell\{#MyAppName}"
Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}\Shell"
Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}\Shell\Preload"; ValueType: string; ValueName: "MUIVerb"; ValueData: "{cm:PreloadThumbnails}"
Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}\Shell\Preload"; ValueType: string; ValueName: "Icon"; ValueData: """{app}\{#MyAppFilename}"",0"
Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}\Shell\Preload\command"; ValueType: string; ValueData: """{app}\{#MyAppFilename}"" -m %V"
Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}\Shell\PreloadRecursively"; ValueType: string; ValueName: "MUIVerb"; ValueData: "{cm:PreloadThumbnailsRecursively}"
Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}\Shell\PreloadRecursively"; ValueType: string; ValueName: "Icon"; ValueData: """{app}\{#MyAppFilename}"",0"
Root: "HKCR"; Subkey: "Drive\shell\{#MyAppName}\Shell\PreloadRecursively\command"; ValueType: string; ValueData: """{app}\{#MyAppFilename}"" -m -r %V"

Root: "HKLM"; Subkey: "SYSTEM\CurrentControlSet\Control\Session Manager\Environment"; ValueType: expandsz; ValueName: "Path"; ValueData: "{olddata};{app}" 

[Code]
function Framework45IsNotInstalled(): Boolean;
var
  bSuccess: Boolean;
  regVersion: Cardinal;
begin
  Result := True;

  bSuccess := RegQueryDWordValue(HKLM, 'Software\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', regVersion);
  if (True = bSuccess) and (regVersion >= 378389) then begin
    Result := False;
  end;
end;

function OnDownloadProgress(const Url, Filename: string; const Progress, ProgressMax: Int64): Boolean;
begin
  if ProgressMax <> 0 then
    Log(Format('  %d of %d bytes done.', [Progress, ProgressMax]))
  else
    Log(Format('  %d bytes done.', [Progress]));
  Result := True;
end;

procedure InitializeWizard;
begin
  if Framework45IsNotInstalled() then
  begin
    DownloadTemporaryFile('http://go.microsoft.com/fwlink/?LinkId=397707', ExpandConstant('{tmp}\NetFrameworkInstaller.exe'),'',@OnDownloadProgress);
  end;
end;

procedure InstallFramework;
var
  StatusText: string;
  ResultCode: Integer;
begin
  StatusText := WizardForm.StatusLabel.Caption;
  WizardForm.StatusLabel.Caption := ExpandConstant('{cm:NetFrameworkInstallerCaption}');
  WizardForm.ProgressGauge.Style := npbstMarquee;
  try
    if not Exec(ExpandConstant('{tmp}\NetFrameworkInstaller.exe'), '/passive /norestart', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
    begin
      MsgBox(ExpandConstant('{cm:NetFrameworkInstallerFail}') + ': ' + IntToStr(ResultCode) + '.', mbError, MB_OK);
    end;
  finally
    WizardForm.StatusLabel.Caption := StatusText;
    WizardForm.ProgressGauge.Style := npbstNormal;

    DeleteFile(ExpandConstant('{tmp}\NetFrameworkInstaller.exe'));
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  case CurStep of
    ssPostInstall:
      begin
        if Framework45IsNotInstalled() then
        begin
          InstallFramework();
        end;
      end;
  end;
begin
    if CurStep = ssPostInstall 
     then EnvAddPath(ExpandConstant('{app}'));
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
    if CurUninstallStep = usPostUninstall
    then EnvRemovePath(ExpandConstant('{app}'));
end;