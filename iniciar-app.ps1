# Script de inicialização automática - Windows
# Este script inicia a API em segundo plano e depois o aplicativo MAUI

Write-Host "🚀 Iniciando Cardápio Inteligente..." -ForegroundColor Green
Write-Host ""

# Verifica se o MySQL está rodando
Write-Host "🔍 Verificando MySQL..." -ForegroundColor Yellow
$mysqlRunning = Get-Service -Name "MySQL*" -ErrorAction SilentlyContinue | Where-Object {$_.Status -eq "Running"}

if (-not $mysqlRunning) {
    Write-Host "⚠️  MySQL não está rodando. Tentando iniciar..." -ForegroundColor Red
    try {
        Start-Service -Name "MySQL*" -ErrorAction Stop
        Write-Host "✅ MySQL iniciado com sucesso!" -ForegroundColor Green
        Start-Sleep -Seconds 3
    }
    catch {
        Write-Host "❌ Não foi possível iniciar o MySQL automaticamente." -ForegroundColor Red
        Write-Host "   Por favor, inicie o MySQL manualmente e tente novamente." -ForegroundColor Yellow
        Read-Host "Pressione ENTER para sair"
        exit 1
    }
}
else {
    Write-Host "✅ MySQL está rodando!" -ForegroundColor Green
}

Write-Host ""

# Inicia a API em segundo plano
Write-Host "🌐 Iniciando API..." -ForegroundColor Yellow

$apiPath = "$PSScriptRoot\Cardapio_Inteligente.Api"
$apiProcess = Start-Process -FilePath "dotnet" -ArgumentList "run --project `"$apiPath`"" -PassThru -WindowStyle Minimized

Write-Host "✅ API iniciada (PID: $($apiProcess.Id))" -ForegroundColor Green
Write-Host "⏳ Aguardando API carregar (30 segundos)..." -ForegroundColor Yellow
Start-Sleep -Seconds 30

Write-Host ""

# Inicia o aplicativo MAUI
Write-Host "📱 Iniciando aplicativo MAUI..." -ForegroundColor Yellow

$mauiPath = "$PSScriptRoot\Cardapio_Inteligente"

try {
    Set-Location $mauiPath
    dotnet build -f net8.0-windows10.0.19041.0 -c Release
    
    # Procura pelo executável
    $exePath = Get-ChildItem -Path "$mauiPath\bin\Release\net8.0-windows10.0.19041.0\win10-x64" -Filter "Cardapio_Inteligente.exe" -Recurse | Select-Object -First 1
    
    if ($exePath) {
        Write-Host "✅ Iniciando aplicativo..." -ForegroundColor Green
        Start-Process $exePath.FullName
    }
    else {
        Write-Host "⚠️  Executável não encontrado. Usando dotnet run..." -ForegroundColor Yellow
        dotnet run -f net8.0-windows10.0.19041.0
    }
}
catch {
    Write-Host "❌ Erro ao iniciar aplicativo: $_" -ForegroundColor Red
}

Write-Host ""
Write-Host "✅ Sistema iniciado com sucesso!" -ForegroundColor Green
Write-Host "   - API rodando em: http://localhost:5068" -ForegroundColor Cyan
Write-Host "   - Swagger disponível em: http://localhost:5068/swagger" -ForegroundColor Cyan
Write-Host ""
Write-Host "Para parar a API, feche a janela ou pressione CTRL+C no terminal da API" -ForegroundColor Yellow
Write-Host ""

Read-Host "Pressione ENTER para sair"
