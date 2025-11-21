# 🚀 Guia de Compilação - Cardápio Inteligente

## ✅ O que foi implementado

### Windows (EXE)
- ✅ API embutida no aplicativo (roda automaticamente)
- ✅ Não precisa rodar comandos separados
- ✅ Cliente abre o EXE e está pronto para usar
- ✅ IA local processada internamente
- ✅ Banco de dados SQLite local

### Android (APK)
- ✅ IA local embutida no app
- ✅ Funciona 100% offline
- ✅ Não precisa de servidor ou internet
- ✅ Banco de dados SQLite local
- ✅ Tudo rodando dentro do dispositivo

---

## 📋 Pré-requisitos

1. **.NET 9 SDK** instalado
2. **Workloads do .NET MAUI** instalados:
   ```powershell
   dotnet workload install maui
   dotnet workload install android
   ```
3. **Visual Studio 2022** (recomendado) com:
   - .NET MAUI
   - Android SDK
   - Windows App SDK

---

## 🔨 Como Compilar

### Opção 1: Compilar tudo de uma vez
```powershell
.\build-all.ps1
```

### Opção 2: Compilar apenas Windows
```powershell
.\build-windows.ps1
```

### Opção 3: Compilar apenas Android
```powershell
.\build-android.ps1
```

---

## 📦 Arquivos Gerados

Após a compilação, os arquivos estarão em:

- **Windows**: `Output\Windows\Cardapio_Inteligente.exe`
- **Android**: `Output\Android\com.companyname.cardapio_inteligente-Signed.apk`

---

## 🚀 Como Usar

### Windows
1. Navegue até `Output\Windows\`
2. Execute `Cardapio_Inteligente.exe`
3. **Pronto!** A API já roda automaticamente junto com o app

**Não precisa:**
- ❌ Rodar API separadamente
- ❌ Instalar serviços Windows
- ❌ Executar scripts adicionais

### Android
1. Copie o arquivo `.apk` para seu dispositivo Android
2. Habilite "Fontes Desconhecidas" nas configurações
3. Toque no arquivo `.apk` para instalar
4. **Pronto!** A IA já está embutida no app

**Não precisa:**
- ❌ Conexão com internet (funciona offline)
- ❌ Servidor externo
- ❌ Configurações adicionais

---

## 🗂️ Estrutura do Projeto

```
TCC/
├── Cardapio_Inteligente/          # Aplicativo MAUI (interface + API embutida)
│   ├── Controllers/               # Controllers da API interna (Windows)
│   ├── Servicos/                  # Serviços (IA, API, Database)
│   ├── Paginas/                   # Páginas XAML
│   └── Modelos/                   # Models compartilhados
│
├── Cardapio_Inteligente.Api/      # API original (OBSOLETO - não usar mais)
├── Cardapio_Inteligente.WindowsService/  # Windows Service (OBSOLETO - não usar mais)
│
├── build-windows.ps1              # Script para compilar Windows
├── build-android.ps1              # Script para compilar Android
├── build-all.ps1                  # Script para compilar tudo
└── Output/                        # Pasta com os builds gerados
    ├── Windows/                   # EXE do Windows
    └── Android/                   # APK do Android
```

---

## 🔧 Arquitetura

### Windows
```
[Cliente abre Cardapio_Inteligente.exe]
           ↓
[MauiProgram inicia ApiHostedService]
           ↓
[API ASP.NET Core roda em localhost:5068]
           ↓
[Interface MAUI se conecta à API local]
           ↓
[Tudo no mesmo processo - nenhuma configuração externa]
```

### Android
```
[Cliente abre o app Android]
           ↓
[MauiProgram carrega LlamaServiceLocal]
           ↓
[IA carrega modelo .gguf dos assets]
           ↓
[Processamento 100% local no dispositivo]
           ↓
[Sem necessidade de internet ou servidor]
```

---

## ⚠️ Arquivos/Pastas Obsoletos (podem ser removidos)

Após a nova implementação, estes não são mais necessários:

- ❌ `Cardapio_Inteligente.Api/` (API foi integrada no MAUI)
- ❌ `Cardapio_Inteligente.WindowsService/` (não precisa mais de serviço Windows)
- ❌ `install-windows-service.ps1` (não precisa instalar serviço)
- ❌ `uninstall-windows-service.ps1`
- ❌ `GUIA_AUTO_INICIAR_API.md` (API agora inicia automaticamente)
- ❌ `build-and-package.ps1` (substituído pelos novos scripts)
- ❌ `build-and-package.sh`

---

## 📝 Modelo de IA

O modelo `Phi-3-mini-4k-instruct-q4.gguf` precisa estar em:
- **Windows**: `Cardapio_Inteligente.Api/ModelosIA/`
- **Android**: Será copiado automaticamente para os assets do APK

**Tamanho**: ~2.3GB

Se você não tiver o modelo, baixe de: https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf

---

## 🐛 Troubleshooting

### Build falha no Windows
```powershell
# Reinstalar workload
dotnet workload repair
dotnet workload install maui
```

### Build falha no Android
```powershell
# Verificar Android SDK
dotnet workload install android
```

### Modelo de IA não encontrado
Certifique-se de que o arquivo `.gguf` está na pasta `Cardapio_Inteligente.Api/ModelosIA/`

---

## ✨ Resumo

| Plataforma | API | IA | Offline | Auto-start |
|-----------|-----|----|---------|----|
| **Windows** | ✅ Embutida | ✅ Local | ✅ Sim | ✅ Sim |
| **Android** | ✅ Interna | ✅ Local | ✅ Sim | ✅ Sim |

**Cliente só precisa:**
1. Abrir o executável (Windows) ou instalar o APK (Android)
2. Usar o aplicativo

**Nenhuma configuração adicional necessária!** 🎉
