# Script de Teste - Endpoint de Ingredientes (PowerShell)
# Execute este script no Windows para testar o novo endpoint

Write-Host "🧪 Testando Endpoint de Ingredientes" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host ""

# Variáveis
$ApiUrl = "http://localhost:5068"
$Endpoint = "/api/Ingredientes"
$FullUrl = "$ApiUrl$Endpoint"

Write-Host "📍 URL da API: $FullUrl" -ForegroundColor Yellow
Write-Host ""

# Teste 1: Endpoint sem autenticação
Write-Host "🔹 Teste 1: Tentando acessar sem token..." -ForegroundColor White

try {
    $response = Invoke-WebRequest -Uri $FullUrl -Method Get -UseBasicParsing -ErrorAction SilentlyContinue
    Write-Host "❌ FALHOU - Esperado 401, mas endpoint respondeu: $($response.StatusCode)" -ForegroundColor Red
} catch {
    if ($_.Exception.Response.StatusCode -eq 401) {
        Write-Host "✅ OK - Retornou 401 (autenticação necessária)" -ForegroundColor Green
    } else {
        Write-Host "❌ FALHOU - Erro: $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "---"
Write-Host ""

# Teste 2: Instruções para teste com autenticação
Write-Host "🔹 Teste 2: Teste com autenticação" -ForegroundColor White
Write-Host ""
Write-Host "Para testar com token válido, siga estes passos:" -ForegroundColor Yellow
Write-Host ""
Write-Host "1. Faça login para obter o token:" -ForegroundColor White
Write-Host ""
Write-Host '$body = @{' -ForegroundColor Gray
Write-Host '    Email = "seu-email@teste.com"' -ForegroundColor Gray
Write-Host '    Senha = "sua-senha"' -ForegroundColor Gray
Write-Host '} | ConvertTo-Json' -ForegroundColor Gray
Write-Host ""
Write-Host '$login = Invoke-RestMethod -Uri "http://localhost:5068/api/Usuarios/Login" -Method Post -Body $body -ContentType "application/json"' -ForegroundColor Gray
Write-Host '$token = $login.token' -ForegroundColor Gray
Write-Host ""
Write-Host "2. Teste o endpoint de ingredientes:" -ForegroundColor White
Write-Host ""
Write-Host '$headers = @{' -ForegroundColor Gray
Write-Host '    Authorization = "Bearer $token"' -ForegroundColor Gray
Write-Host '}' -ForegroundColor Gray
Write-Host ""
Write-Host '$ingredientes = Invoke-RestMethod -Uri "http://localhost:5068/api/Ingredientes" -Method Get -Headers $headers' -ForegroundColor Gray
Write-Host '$ingredientes' -ForegroundColor Gray
Write-Host ""
Write-Host "---"
Write-Host ""

# Teste 3: Estrutura de arquivos
Write-Host "🔹 Teste 3: Verificando estrutura de arquivos..." -ForegroundColor White
Write-Host ""

$files = @(
    @{Path="Cardapio_Inteligente.Api\Controllers\IngredientesController.cs"; Name="IngredientesController.cs"},
    @{Path="Cardapio_Inteligente\Paginas\Tela_Cadastro.xaml"; Name="Tela_Cadastro.xaml"},
    @{Path="Cardapio_Inteligente\Paginas\Tela_Cadastro.xaml.cs"; Name="Tela_Cadastro.xaml.cs"},
    @{Path="INSTRUCOES_MODELO_PHI3.md"; Name="INSTRUCOES_MODELO_PHI3.md"},
    @{Path="CORRECOES_IMPLEMENTADAS.md"; Name="CORRECOES_IMPLEMENTADAS.md"}
)

foreach ($file in $files) {
    if (Test-Path $file.Path) {
        Write-Host "✅ $($file.Name) existe" -ForegroundColor Green
    } else {
        Write-Host "❌ $($file.Name) NÃO ENCONTRADO" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "---"
Write-Host ""

# Teste 4: Modelo IA
Write-Host "🔹 Teste 4: Verificando modelo de IA..." -ForegroundColor White
Write-Host ""

if (Test-Path "Cardapio_Inteligente.Api\ModelosIA") {
    Write-Host "✅ Pasta ModelosIA existe" -ForegroundColor Green
    
    $models = Get-ChildItem -Path "Cardapio_Inteligente.Api\ModelosIA" -Filter "*.gguf" -ErrorAction SilentlyContinue
    
    if ($models) {
        Write-Host "✅ Modelo .gguf encontrado:" -ForegroundColor Green
        foreach ($model in $models) {
            $size = "{0:N2} MB" -f ($model.Length / 1MB)
            Write-Host "   - $($model.Name) ($size)" -ForegroundColor Cyan
        }
    } else {
        Write-Host "⚠️  Nenhum modelo .gguf encontrado" -ForegroundColor Yellow
        Write-Host "   Baixe o modelo Phi-3 conforme INSTRUCOES_MODELO_PHI3.md" -ForegroundColor Yellow
    }
} else {
    Write-Host "⚠️  Pasta ModelosIA não existe" -ForegroundColor Yellow
    Write-Host "   Crie a pasta e baixe o modelo conforme INSTRUCOES_MODELO_PHI3.md" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "✅ Testes concluídos!" -ForegroundColor Green
Write-Host ""
Write-Host "📚 Consulte os seguintes arquivos para mais informações:" -ForegroundColor Yellow
Write-Host "   - CORRECOES_IMPLEMENTADAS.md" -ForegroundColor White
Write-Host "   - INSTRUCOES_MODELO_PHI3.md" -ForegroundColor White
Write-Host ""
