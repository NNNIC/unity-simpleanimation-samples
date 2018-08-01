cd /d %~dp0
set INCPATH=%cd%

cd ..
cd ..
cd ..
cd ..
cd ..

pause

tools\bin\InsertInclude.exe "%INCPATH%" unityProject\Assets

pause