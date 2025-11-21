# Script para compilar o APK do Android com IA local
# Execute como: .\build-android.ps1

Write-Host "🔨 Compilando Cardápio Inteligente para Android..." -ForegroundColor Cyan
Write-Host ""

# Limpa builds anteriores
Write-Host "🧹 Limpando builds anteriores..." -ForegroundColor Yellow
dotnet clean Cardapio_Inteligente\Cardapio_Inteligente.csproj -c Release

# Restaura pacotes
Write-Host "📦 Restaurando pacotes NuGet..." -ForegroundColor Yellow
dotnet restore Cardapio_Inteligente\Cardapio_Inteligente.csproj

# Compila o APK
Write-Host "🏗️ Compilando APK para Android..." -ForegroundColor Yellow
dotnet build Cardapio_Inteligente\Cardapio_Inteligente.csproj `
    -c Release `
    -f net9.0-android `
    -p:AndroidPackageFormat=apk

# Publica o APK
Write-Host "📦 Publicando APK..." -ForegroundColor Yellow
dotnet publish Cardapio_Inteligente\Cardapio_Inteligente.csproj `
    -c Release `
    -f net9.0-android `
    -p:AndroidPackageFormat=apk `
    -o .\Output\Android

Write-Host ""
Write-Host "✅ Build concluído!" -ForegroundColor Green
Write-Host "📁 APK em: .\Output\Android\" -ForegroundColor Cyan
Write-Host ""
Write-Host "📱 Para instalar no Android:" -ForegroundColor Yellow
Write-Host "   1. Copie o arquivo .apk para seu dispositivo" -ForegroundColor White
Write-Host "   2. Habilite 'Fontes Desconhecidas' nas configurações" -ForegroundColor White
Write-Host "   3. Toque no arquivo .apk para instalar" -ForegroundColor White
Write-Host ""
Write-Host "ℹ️ O APK já contém a IA local - funciona 100% offline!" -ForegroundColor Green
