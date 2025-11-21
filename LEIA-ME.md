# 📱 Cardápio Inteligente - Versão Standalone

## 🎯 O que mudou?

**ANTES:**
- ❌ Tinha que rodar a API manualmente
- ❌ Instalar serviço Windows separado
- ❌ Executar vários comandos
- ❌ Cliente não conseguiria usar sozinho

**AGORA:**
- ✅ **Windows**: EXE único com API embutida - só abrir e usar!
- ✅ **Android**: APK com IA local - 100% offline!
- ✅ Tudo automático - cliente só instala e usa
- ✅ Sem servidores, sem nuvem - tudo local

---

## 🚀 Uso Rápido

### Para o Cliente (Windows)
1. Execute `Cardapio_Inteligente.exe`
2. Pronto! A API já está rodando automaticamente

### Para o Cliente (Android)
1. Instale o arquivo `.apk` no dispositivo
2. Pronto! Funciona offline

---

## 🔨 Para Desenvolvedores - Como Compilar

### Compilar tudo:
```powershell
.\build-all.ps1
```

### Apenas Windows:
```powershell
.\build-windows.ps1
```

### Apenas Android:
```powershell
.\build-android.ps1
```

**Arquivos gerados em**: `Output/Windows/` e `Output/Android/`

---

## 📂 Estrutura do Projeto

```
TCC/
├── Cardapio_Inteligente/          # ⭐ Aplicativo principal (MAUI)
│   ├── Controllers/               # API embutida (Windows)
│   ├── Servicos/                  # IA local e serviços
│   ├── Paginas/                   # Interface do usuário
│   └── Modelos/                   # Modelos de dados
│
├── Cardapio_Inteligente.Api/      # API original (mantida para o modelo .gguf)
│   └── ModelosIA/                 # ⚠️ Modelo de IA aqui (2.3GB)
│
├── build-windows.ps1              # Script para Windows
├── build-android.ps1              # Script para Android
├── build-all.ps1                  # Script para ambos
└── README_COMPILACAO.md           # 📖 Guia completo de compilação
```

---

## ⚙️ Como Funciona

### Windows
```
Cliente abre .exe
    ↓
API inicia automaticamente (localhost:5068)
    ↓
Interface se conecta à API local
    ↓
Tudo no mesmo processo!
```

### Android
```
Cliente abre app
    ↓
IA carrega do asset interno
    ↓
Processamento 100% no dispositivo
    ↓
Sem internet necessária!
```

---

## 📋 Pré-requisitos para Compilar

1. **.NET 9 SDK**
2. **Workloads MAUI**:
   ```powershell
   dotnet workload install maui
   dotnet workload install android
   ```
3. **Modelo de IA**: `Phi-3-mini-4k-instruct-q4.gguf` em `Cardapio_Inteligente.Api/ModelosIA/`

---

## 📝 Documentos Importantes

- **README_COMPILACAO.md** - Guia detalhado de compilação
- **LEIA-ME.md** (este arquivo) - Visão geral rápida
- **database_seed.sql** - Dados iniciais do banco (se necessário)

---

## 🎉 Pronto para Distribuir!

Após compilar:
1. **Windows**: Copie a pasta `Output/Windows/` para o cliente
2. **Android**: Envie o `.apk` de `Output/Android/` para o dispositivo

**Nenhuma instalação ou configuração adicional necessária!**

---

## 💡 Dicas

- O modelo de IA é grande (~2.3GB) - certifique-se de ter espaço
- Windows requer Windows 10 ou superior
- Android requer Android 5.0 (API 21) ou superior
- Primeira execução pode demorar um pouco (carregando IA)

---

## 🐛 Problemas?

Leia o **README_COMPILACAO.md** para troubleshooting detalhado.

---

## ✨ Tecnologias Usadas

- **.NET MAUI 9** - Framework multiplataforma
- **ASP.NET Core** - API embutida (Windows)
- **LLamaSharp** - IA local (Phi-3)
- **SQLite** - Banco de dados local
- **Entity Framework Core** - ORM

---

**Desenvolvido para funcionar 100% localmente - sem depender de nada externo! 🚀**
