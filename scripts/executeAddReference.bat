@echo off
REM Wrapper para ejecutar el script de PowerShell principal.

REM %~dp0 es la ruta a la carpeta donde reside este script .bat
SET SCRIPT_DIR=%~dp0
SET POWERSHELL_SCRIPT_PATH=%SCRIPT_DIR%addReferenceToStarter.ps1
REM (Asegúrate que 'addReferenceToStarter.ps1' sea el nombre correcto de tu script principal de PowerShell)

ECHO Intentando ejecutar script de PowerShell: %POWERSHELL_SCRIPT_PATH%
ECHO Argumentos recibidos por el wrapper: %*

REM powershell.exe -ExecutionPolicy Bypass -NoProfile -File addReferenceToStarter <argumentos_para_ps1>
REM %* pasa todos los argumentos recibidos por este .bat al script de PowerShell.
powershell.exe -ExecutionPolicy Bypass -NoProfile -File "%POWERSHELL_SCRIPT_PATH%" %*

REM Capturar el código de salida de PowerShell
SET PWSH_EXIT_CODE=%ERRORLEVEL%

IF %PWSH_EXIT_CODE% NEQ 0 (
    ECHO El script de PowerShell falló con el código de error: %PWSH_EXIT_CODE%
    exit /b %PWSH_EXIT_CODE%
)

ECHO Script de PowerShell ejecutado exitosamente a través del wrapper.
exit /b 0