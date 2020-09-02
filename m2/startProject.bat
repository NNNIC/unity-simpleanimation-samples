cd /d %~dp0

if exist %~dp0unityProject goto :_open

start "" "%unitypath2020%" -createproject %~dp0unityProject
goto :eof

:_open

start "" "%unitypath2020%" -projectPath %~dp0unityProject
goto :eof