@echo off
chcp 65001 >nul
title Cardápio Inteligente - Inicialização

echo.
echo ========================================
echo   Cardápio Inteligente
echo ========================================
echo.

REM Verifica MySQL
echo [1/4] Verificando MySQL...
sc query MySQL >nul 2>&1
if errorlevel 1 (
    echo ⚠️  MySQL não está instalado ou não é um serviço do Windows
    echo    Certifique-se de que o MySQL está rodando
    pause
    exit /b 1
)

net start MySQL >nul 2>&1
if errorlevel 1 (
    echo ✅ MySQL já está rodando
) else (
    echo ✅ MySQL iniciado com sucesso
)

echo.

REM Inicia a API em nova janela
echo [2/4] Iniciando API...
start "Cardapio API" /MIN cmd /c "cd /d %~dp0Cardapio_Inteligente.Api && dotnet run"

echo ✅ API iniciada
echo ⏳ Aguardando 30 segundos para a API carregar...
timeout /t 30 /nobreak >nul

echo.

REM Compila o app MAUI
echo [3/4] Compilando aplicativo...
cd /d %~dp0Cardapio_Inteligente
dotnet build -f net8.0-windows10.0.19041.0 -c Release >nul 2>&1

echo.

REM Executa o app MAUI
echo [4/4] Iniciando aplicativo...
dotnet run -f net8.0-windows10.0.19041.0

echo.
echo ✅ Sistema finalizado
pause
