# Script para compilar todas as versões
# Execute como: .\build-all.ps1

Write-Host "🔨 Compilando Cardápio Inteligente - TODAS AS PLATAFORMAS" -ForegroundColor Cyan
Write-Host ""

# Windows
Write-Host "=" * 60 -ForegroundColor Cyan
Write-Host "WINDOWS (EXE com API embutida)" -ForegroundColor Cyan
Write-Host "=" * 60 -ForegroundColor Cyan
.\build-windows.ps1

Write-Host ""
Write-Host ""

# Android
Write-Host "=" * 60 -ForegroundColor Cyan
Write-Host "ANDROID (APK com IA local)" -ForegroundColor Cyan
Write-Host "=" * 60 -ForegroundColor Cyan
.\build-android.ps1

Write-Host ""
Write-Host ""
Write-Host "✅ TODOS OS BUILDS CONCLUÍDOS!" -ForegroundColor Green
Write-Host ""
Write-Host "📦 Arquivos gerados:" -ForegroundColor Cyan
Write-Host "   Windows: .\Output\Windows\Cardapio_Inteligente.exe" -ForegroundColor White
Write-Host "   Android: .\Output\Android\*.apk" -ForegroundColor White
Write-Host ""
