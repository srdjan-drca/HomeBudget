@ECHO OFF

ECHO Deleting folders
DEL /S /Q bin 1>nul
RMDIR /S /Q bin 1>nul
DEL /S /Q obj 1>nul
RMDIR /S /Q obj 1>nul

ECHO Deleting files
DEL /S /Q *.suo 1>nul
DEL /S /Q *.user 1>nul
DEL /S /Q obj 1>nul

ECHO Project files are deleted
