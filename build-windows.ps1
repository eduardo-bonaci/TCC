# Script para compilar o EXE do Windows com API embutida
# Execute como: .\build-windows.ps1

Write-Host "🔨 Compilando Cardápio Inteligente para Windows..." -ForegroundColor Cyan
Write-Host ""

# Limpa builds anteriores
Write-Host "🧹 Limpando builds anteriores..." -ForegroundColor Yellow
dotnet clean Cardapio_Inteligente\Cardapio_Inteligente.csproj -c Release

# Restaura pacotes
Write-Host "📦 Restaurando pacotes NuGet..." -ForegroundColor Yellow
dotnet restore Cardapio_Inteligente\Cardapio_Inteligente.csproj

# Compila para Windows (x64)
Write-Host "🏗️ Compilando aplicativo Windows..." -ForegroundColor Yellow
dotnet build Cardapio_Inteligente\Cardapio_Inteligente.csproj -c Release -f net9.0-windows10.0.19041.0

# Publica o aplicativo
Write-Host "📦 Publicando aplicativo..." -ForegroundColor Yellow
dotnet publish Cardapio_Inteligente\Cardapio_Inteligente.csproj `
    -c Release `
    -f net9.0-windows10.0.19041.0 `
    -p:RuntimeIdentifierOverride=win10-x64 `
    -p:PublishSingleFile=true `
    -p:SelfContained=true `
    -p:PublishReadyToRun=true `
    -o .\Output\Windows

Write-Host ""
Write-Host "✅ Build concluído!" -ForegroundColor Green
Write-Host "📁 Arquivos em: .\Output\Windows\" -ForegroundColor Cyan
Write-Host ""
Write-Host "🚀 Para executar:" -ForegroundColor Yellow
Write-Host "   cd Output\Windows" -ForegroundColor White
Write-Host "   .\Cardapio_Inteligente.exe" -ForegroundColor White
Write-Host ""
Write-Host "ℹ️ O executável já contém a API embutida - não precisa rodar nada separadamente!" -ForegroundColor Green
