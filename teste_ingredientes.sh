#!/bin/bash

# Script de Teste - Endpoint de Ingredientes
# Execute este script para testar o novo endpoint

echo "🧪 Testando Endpoint de Ingredientes"
echo "====================================="
echo ""

# Variáveis
API_URL="http://localhost:5068"
ENDPOINT="/api/Ingredientes"

echo "📍 URL da API: $API_URL$ENDPOINT"
echo ""

# Teste 1: Endpoint sem autenticação (deve falhar com 401)
echo "🔹 Teste 1: Tentando acessar sem token..."
response=$(curl -s -o /dev/null -w "%{http_code}" "$API_URL$ENDPOINT")

if [ "$response" = "401" ]; then
    echo "✅ OK - Retornou 401 (autenticação necessária)"
else
    echo "❌ FALHOU - Esperado 401, recebeu: $response"
fi

echo ""
echo "---"
echo ""

# Instruções para teste com autenticação
echo "🔹 Teste 2: Teste com autenticação"
echo ""
echo "Para testar com token válido, siga estes passos:"
echo ""
echo "1. Faça login para obter o token:"
echo ""
echo "curl -X POST \"$API_URL/api/Usuarios/Login\" \\"
echo "  -H \"Content-Type: application/json\" \\"
echo "  -d '{\"Email\":\"seu-email@teste.com\", \"Senha\":\"sua-senha\"}'"
echo ""
echo "2. Copie o token da resposta"
echo ""
echo "3. Teste o endpoint de ingredientes:"
echo ""
echo "curl -X GET \"$API_URL$ENDPOINT\" \\"
echo "  -H \"Authorization: Bearer SEU_TOKEN_AQUI\""
echo ""
echo "---"
echo ""

# Teste de estrutura de arquivos
echo "🔹 Teste 3: Verificando estrutura de arquivos..."
echo ""

if [ -f "Cardapio_Inteligente.Api/Controllers/IngredientesController.cs" ]; then
    echo "✅ IngredientesController.cs existe"
else
    echo "❌ IngredientesController.cs NÃO ENCONTRADO"
fi

if [ -f "Cardapio_Inteligente/Paginas/Tela_Cadastro.xaml" ]; then
    echo "✅ Tela_Cadastro.xaml existe"
else
    echo "❌ Tela_Cadastro.xaml NÃO ENCONTRADO"
fi

if [ -f "Cardapio_Inteligente/Paginas/Tela_Cadastro.xaml.cs" ]; then
    echo "✅ Tela_Cadastro.xaml.cs existe"
else
    echo "❌ Tela_Cadastro.xaml.cs NÃO ENCONTRADO"
fi

if [ -f "INSTRUCOES_MODELO_PHI3.md" ]; then
    echo "✅ INSTRUCOES_MODELO_PHI3.md existe"
else
    echo "❌ INSTRUCOES_MODELO_PHI3.md NÃO ENCONTRADO"
fi

if [ -f "CORRECOES_IMPLEMENTADAS.md" ]; then
    echo "✅ CORRECOES_IMPLEMENTADAS.md existe"
else
    echo "❌ CORRECOES_IMPLEMENTADAS.md NÃO ENCONTRADO"
fi

echo ""
echo "---"
echo ""

# Verificar modelo IA
echo "🔹 Teste 4: Verificando modelo de IA..."
echo ""

if [ -d "Cardapio_Inteligente.Api/ModelosIA" ]; then
    echo "✅ Pasta ModelosIA existe"
    
    if ls Cardapio_Inteligente.Api/ModelosIA/*.gguf 1> /dev/null 2>&1; then
        echo "✅ Modelo .gguf encontrado:"
        ls -lh Cardapio_Inteligente.Api/ModelosIA/*.gguf
    else
        echo "⚠️  Nenhum modelo .gguf encontrado"
        echo "   Baixe o modelo Phi-3 conforme INSTRUCOES_MODELO_PHI3.md"
    fi
else
    echo "⚠️  Pasta ModelosIA não existe"
    echo "   Crie a pasta e baixe o modelo conforme INSTRUCOES_MODELO_PHI3.md"
fi

echo ""
echo "====================================="
echo "✅ Testes concluídos!"
echo ""
echo "📚 Consulte os seguintes arquivos para mais informações:"
echo "   - CORRECOES_IMPLEMENTADAS.md"
echo "   - INSTRUCOES_MODELO_PHI3.md"
echo ""
