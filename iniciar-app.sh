#!/bin/bash

# Script de inicialização - Linux/Mac

echo "🚀 Iniciando Cardápio Inteligente..."
echo ""

# Verifica MySQL
echo "🔍 Verificando MySQL..."
if command -v mysql &> /dev/null; then
    if mysqladmin ping -h localhost --silent 2>/dev/null; then
        echo "✅ MySQL está rodando!"
    else
        echo "⚠️  MySQL não está respondendo"
        echo "   Tentando iniciar..."
        sudo systemctl start mysql 2>/dev/null || sudo service mysql start 2>/dev/null
        sleep 3
    fi
else
    echo "❌ MySQL não encontrado"
    exit 1
fi

echo ""

# Inicia API em background
echo "🌐 Iniciando API..."
cd "$(dirname "$0")/Cardapio_Inteligente.Api"
dotnet run &
API_PID=$!

echo "✅ API iniciada (PID: $API_PID)"
echo "⏳ Aguardando API carregar (30 segundos)..."
sleep 30

echo ""

# Inicia aplicativo MAUI
echo "📱 Iniciando aplicativo MAUI..."
cd "$(dirname "$0")/Cardapio_Inteligente"

dotnet build -f net8.0 -c Release
dotnet run -f net8.0

echo ""
echo "✅ Sistema finalizado"
echo ""
echo "Para parar a API, execute: kill $API_PID"
