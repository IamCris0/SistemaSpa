@echo off
echo ============================================================
echo   GENERADOR AUTOMATICO DE VISTAS - Sistema Spa Web
echo ============================================================
echo.
echo Ejecutando generador de vistas...
echo.

PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& '%~dp0Generar-TodasLasVistas-v2.ps1'"

echo.
echo ============================================================
echo Presiona cualquier tecla para cerrar...
pause > nul
