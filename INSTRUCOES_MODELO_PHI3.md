# 🤖 Instruções: Configuração do Modelo Phi-3-mini-4k-instruct-gguf

## 📥 Download do Modelo

### Opção 1: Download Direto do HuggingFace (Recomendado)

1. Acesse: https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf

2. Escolha uma das versões quantizadas (mais leves e rápidas):
   - **Phi-3-mini-4k-instruct-q4.gguf** (Recomendado para CPU) - ~2.4GB
   - **Phi-3-mini-4k-instruct-fp16.gguf** (Maior qualidade) - ~7.6GB
   - **Phi-3-mini-4k-instruct-q8.gguf** (Balanço qualidade/tamanho) - ~4.1GB

3. Clique no arquivo desejado e depois em "Download"

### Opção 2: Download via CLI

```bash
# Instale o Hugging Face CLI
pip install huggingface_hub

# Baixe o modelo
huggingface-cli download microsoft/Phi-3-mini-4k-instruct-gguf \
  Phi-3-mini-4k-instruct-q4.gguf \
  --local-dir ./ModelosIA \
  --local-dir-use-symlinks False
```

## 📂 Instalação

### 1. Criar Pasta de Modelos

Na raiz do projeto da API, crie a pasta `ModelosIA`:

```bash
cd Cardapio_Inteligente.Api
mkdir ModelosIA
```

### 2. Mover o Modelo

Copie o arquivo `.gguf` baixado para a pasta `ModelosIA`:

```bash
# Windows (PowerShell)
Copy-Item "C:\Users\SeuUsuario\Downloads\Phi-3-mini-4k-instruct-q4.gguf" -Destination ".\ModelosIA\"

# Linux/Mac
cp ~/Downloads/Phi-3-mini-4k-instruct-q4.gguf ./ModelosIA/
```

### 3. Verificar Estrutura de Pastas

Sua estrutura deve ficar assim:

```
Cardapio_Inteligente.Api/
├── Controllers/
├── Dados/
├── ModelosIA/                              ← NOVA PASTA
│   └── Phi-3-mini-4k-instruct-q4.gguf     ← MODELO AQUI
├── Modelos/
├── Servicos/
├── appsettings.json
├── Program.cs
└── ...
```

## ⚙️ Configuração

### 1. Verificar appsettings.json

O arquivo `appsettings.json` já está configurado:

```json
{
  "LlamaSettings": {
    "ModelPath": "ModelosIA/Phi-3-mini-4k-instruct-q4.gguf",
    "MaxTokens": 512,
    "Temperature": 0.8,
    "TopP": 0.9,
    "GpuLayerCount": 0,
    "NumThreads": 4,
    "ContextSize": 4096
  }
}
```

### 2. Ajustar para GPU (Opcional)

Se você tem GPU NVIDIA com CUDA:

```json
{
  "LlamaSettings": {
    "ModelPath": "ModelosIA/Phi-3-mini-4k-instruct-q4.gguf",
    "MaxTokens": 512,
    "Temperature": 0.8,
    "TopP": 0.9,
    "GpuLayerCount": 32,  ← ALTERE PARA 32 ou mais
    "NumThreads": 4,
    "ContextSize": 4096
  }
}
```

## 🚀 Execução

### 1. Restaurar Pacotes

```bash
cd Cardapio_Inteligente.Api
dotnet restore
```

### 2. Executar a API

```bash
dotnet run
```

### 3. Verificar Log de Inicialização

Você deve ver no console:

```
🔄 Carregando modelo Phi-3-mini de: C:\...\ModelosIA\Phi-3-mini-4k-instruct-q4.gguf
✅ Modelo carregado com sucesso!
```

## ⚠️ Troubleshooting

### Erro: "Modelo não encontrado"

```
❌ Modelo não encontrado: ...\ModelosIA\Phi-3-mini-4k-instruct-q4.gguf
```

**Solução:**
- Verifique se o arquivo está na pasta `ModelosIA`
- Confirme se o nome do arquivo no `appsettings.json` está correto
- Certifique-se de que a extensão é `.gguf`

### Erro: "Out of Memory"

**Solução:**
- Use uma versão mais leve (q4 em vez de fp16)
- Reduza `ContextSize` no appsettings.json:
  ```json
  "ContextSize": 2048  // ou menor
  ```

### Performance Lenta

**Solução CPU:**
```json
"NumThreads": 8  // Aumente baseado nos cores da sua CPU
```

**Solução GPU:**
```json
"GpuLayerCount": 32  // Ativa aceleração GPU (requer CUDA)
```

## 🧪 Teste

### Testar via API

```bash
# Usando curl
curl -X POST "http://localhost:5068/api/Pratos/assistente-chat" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer SEU_TOKEN" \
  -d '{
    "Prompt": "Quais pratos sem lactose vocês têm?"
  }'
```

### Testar via MAUI App

1. Execute a API
2. Execute o app MAUI
3. Faça login
4. Acesse o chat
5. Pergunte sobre pratos sem lactose

## 📊 Especificações do Modelo

- **Nome:** Phi-3-mini-4k-instruct
- **Desenvolvedor:** Microsoft
- **Contexto:** 4096 tokens
- **Parâmetros:** 3.8B
- **Formato:** GGUF (otimizado para llama.cpp)
- **Linguagens:** Português, Inglês, e mais

## 🔗 Links Úteis

- HuggingFace: https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf
- Documentação Phi-3: https://azure.microsoft.com/en-us/products/phi-3/
- LLamaSharp (lib usada): https://github.com/SciSharp/LLamaSharp

## ✅ Checklist de Configuração

- [ ] Modelo baixado do HuggingFace
- [ ] Arquivo `.gguf` na pasta `ModelosIA`
- [ ] `appsettings.json` configurado corretamente
- [ ] API inicia sem erros
- [ ] Log mostra "Modelo carregado com sucesso"
- [ ] Chat responde perguntas via API
- [ ] App MAUI conecta e recebe respostas

---

**Última atualização:** 2025
**Versão do Modelo:** Phi-3-mini-4k-instruct-gguf (q4)
