# 🍽️ Cardápio Inteligente

Sistema de recomendação de pratos para pessoas com intolerância à lactose, utilizando Inteligência Artificial local com o modelo Phi-3 da Microsoft.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![MAUI](https://img.shields.io/badge/MAUI-Multiplataforma-512BD4?logo=dotnet)](https://dotnet.microsoft.com/apps/maui)
[![MySQL](https://img.shields.io/badge/MySQL-8.0+-4479A1?logo=mysql&logoColor=white)](https://www.mysql.com/)
[![Phi-3](https://img.shields.io/badge/IA-Phi--3--mini-00A4EF?logo=microsoft)](https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf)

---

## 📋 Sobre o Projeto

O **Cardápio Inteligente** é um aplicativo mobile e desktop desenvolvido como Trabalho de Conclusão de Curso (TCC) do curso de Ciência da Computação. O sistema auxilia pessoas com intolerância à lactose a escolherem pratos seguros em restaurantes, utilizando uma IA local para recomendações personalizadas.

### 🎯 Principais Características

- 🤖 **IA Local**: Utiliza o modelo Phi-3-mini da Microsoft rodando localmente (privacidade total)
- 📱 **Multiplataforma**: Funciona em Android, iOS, Windows, macOS
- 🔐 **Autenticação Segura**: Sistema de login com JWT tokens
- 🍕 **Filtros Inteligentes**: Filtragem automática por restrições alimentares
- 💬 **Chat Conversacional**: Interface natural para perguntar sobre o cardápio
- 🎓 **Educativo**: IA explica conceitos sobre lactose e intolerâncias

---

## 🏗️ Arquitetura do Sistema

```
┌─────────────────────────────────────┐
│   App MAUI (Frontend)               │
│   - Android / iOS                   │
│   - Windows / macOS                 │
│   - Interface responsiva            │
└──────────────┬──────────────────────┘
               │ HTTP + JWT
               ▼
┌─────────────────────────────────────┐
│   API REST (.NET 8)                 │
│   - Controllers                     │
│   - Autenticação JWT                │
│   - Integração com IA               │
└──────┬──────────────┬───────────────┘
       │              │
       │ EF Core      │ LLamaSharp
       ▼              ▼
   ┌────────┐   ┌──────────────┐
   │ MySQL  │   │ Phi-3 Local  │
   │ 8.0+   │   │ (2.3GB Q4)   │
   └────────┘   └──────────────┘
```

---

## 🚀 Tecnologias Utilizadas

### Backend
- **Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core 8.0
- **Banco de Dados**: MySQL 8.0+
- **Autenticação**: JWT (JSON Web Tokens)
- **IA**: LLamaSharp + Phi-3-mini-4k-instruct (quantização Q4)
- **API**: RESTful com Swagger/OpenAPI

### Frontend
- **Framework**: .NET MAUI (Multi-platform App UI)
- **UI**: XAML
- **Padrão**: MVVM (implícito)
- **Navegação**: Shell Navigation

### DevOps
- **Controle de Versão**: Git + GitHub
- **IDE**: Visual Studio 2022
- **Package Manager**: NuGet

---

## 📦 Estrutura do Projeto

```
TCC/
├── Cardapio_Inteligente/           # App MAUI (Frontend)
│   ├── Paginas/                    # Telas do app
│   │   ├── Tela_Login.xaml
│   │   ├── Tela_Cadastro.xaml
│   │   ├── PaginaInicial.xaml
│   │   └── ChatPage.xaml
│   ├── Modelos/                    # Modelos de dados
│   │   ├── Usuario.cs
│   │   ├── Prato.cs
│   │   └── LoginResponse.cs
│   ├── Servicos/                   # Serviços de comunicação
│   │   ├── ApiService.cs
│   │   ├── AssistenteConversacional.cs
│   │   └── RepositorioUsuario.cs
│   └── Platforms/                  # Configurações por plataforma
│       ├── Android/
│       ├── iOS/
│       └── Windows/
│
├── Cardapio_Inteligente.Api/       # API Backend
│   ├── Controllers/                # Endpoints REST
│   │   ├── UsuariosController.cs
│   │   └── PratosController.cs
│   ├── Servicos/                   # Lógica de negócio
│   │   ├── LlamaService.cs        # Integração com IA
│   │   └── ILlamaService.cs
│   ├── Modelos/                    # Entidades do banco
│   │   ├── Usuario.cs
│   │   └── Prato.cs
│   ├── Dados/                      # Acesso a dados
│   │   └── AppDbContext.cs
│   ├── Migrations/                 # Migrações EF Core
│   ├── ModelosIA/                  # Modelo Phi-3 (não versionado)
│   │   └── Phi-3-mini-4k-instruct-q4.gguf (2.3GB)
│   └── appsettings.json            # Configurações
│
├── database_seed.sql               # Script SQL com dados de teste
├── GUIA_EXECUCAO_COMPLETO.md      # Guia detalhado de instalação
├── analise_completa_tcc.md        # Análise técnica do projeto
└── README.md                       # Este arquivo
```

---

## 🔧 Pré-requisitos

### Software
- ✅ [.NET 8 SDK](https://dotnet.microsoft.com/download) ou superior
- ✅ [Visual Studio 2022](https://visualstudio.microsoft.com/) (17.8+) com workload MAUI
- ✅ [MySQL Server 8.0+](https://dev.mysql.com/downloads/mysql/)
- ✅ [Git](https://git-scm.com/)

### Hardware (Mínimo)
- **CPU**: 4 cores
- **RAM**: 8 GB (16 GB recomendado)
- **Disco**: 10 GB livres
- **GPU**: Não necessária

---

## 📥 Instalação

### 1. Clonar Repositório
```bash
git clone https://github.com/eduardo-bonaci/TCC.git
cd TCC
```

### 2. Configurar Banco de Dados

#### 2.1. Criar Database
```sql
CREATE DATABASE cardapio_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

#### 2.2. Executar Script SQL
```bash
mysql -u root -p cardapio_db < database_seed.sql
```

#### 2.3. Atualizar Connection String
Editar `Cardapio_Inteligente.Api/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=cardapio_db;Uid=root;Pwd=SUA_SENHA;"
  }
}
```

### 3. Baixar Modelo de IA

#### 3.1. Download
Baixe o modelo Phi-3 (2.3 GB):
- **Link**: [Phi-3-mini-4k-instruct-q4.gguf](https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf)

#### 3.2. Colocar na Pasta Correta
```bash
# Windows
mkdir Cardapio_Inteligente.Api\ModelosIA
copy Phi-3-mini-4k-instruct-q4.gguf Cardapio_Inteligente.Api\ModelosIA\

# Linux/Mac
mkdir -p Cardapio_Inteligente.Api/ModelosIA
cp Phi-3-mini-4k-instruct-q4.gguf Cardapio_Inteligente.Api/ModelosIA/
```

### 4. Restaurar Dependências
```bash
# Restaurar pacotes NuGet
dotnet restore

# Ou abrir no Visual Studio e deixar restaurar automaticamente
```

---

## ▶️ Como Executar

### Opção 1: Visual Studio (Recomendado)

#### Executar API:
1. Abrir `Cardapio_Inteligente.sln`
2. Definir `Cardapio_Inteligente.Api` como projeto de inicialização
3. Pressionar **F5**
4. API estará disponível em: http://localhost:5068

#### Executar App:
1. No mesmo Visual Studio
2. Mudar para `Cardapio_Inteligente` como projeto de inicialização
3. Selecionar plataforma:
   - **Windows Machine** (Desktop)
   - **Android Emulator** (Mobile)
   - **iOS Simulator** (Mobile - Mac only)
4. Pressionar **F5**

### Opção 2: Linha de Comando

#### Iniciar API:
```bash
cd Cardapio_Inteligente.Api
dotnet run
```

#### Iniciar App (Windows):
```bash
cd Cardapio_Inteligente
dotnet build -f net8.0-windows10.0.19041.0
dotnet run -f net8.0-windows10.0.19041.0
```

#### Iniciar App (Android):
```bash
cd Cardapio_Inteligente
dotnet build -f net8.0-android
dotnet run -f net8.0-android -t:Run
```

---

## 🎮 Como Usar

### 1. Login
- **Email**: `joao@gmail.com`
- **Senha**: `123456`

Ou crie uma nova conta clicando em "Não tem cadastro?"

### 2. Navegar pelo Cardápio
- Visualize todos os pratos disponíveis
- Use os filtros:
  - 🥗 **Sem Lactose**: Mostra apenas pratos seguros
  - 🍕 **Todos os Pratos**: Mostra cardápio completo

### 3. Conversar com a IA
Clique no ícone de chat e faça perguntas como:
- "Quais pratos sem lactose você tem?"
- "O que é lactose?"
- "Me recomende uma sobremesa sem leite"
- "Tenho alergia a lactose, o que posso comer?"

---

## 📱 Plataformas Suportadas

| Plataforma | Status | Testado |
|------------|--------|---------|
| 💻 Windows 10/11 | ✅ Funcional | ✅ Sim |
| 📱 Android 10+ | ✅ Funcional | ✅ Sim |
| 🍎 iOS 14+ | ✅ Funcional | ⚠️ Parcial |
| 🍎 macOS | ✅ Funcional | ⚠️ Parcial |

---

## 🗄️ Banco de Dados

### Modelo de Dados

#### Tabela: `usuarios`
| Campo | Tipo | Descrição |
|-------|------|-----------|
| Id | INT (PK) | Identificador único |
| Nome | VARCHAR(255) | Nome completo |
| Email | VARCHAR(150) | Email (único) |
| Senha | VARCHAR(255) | Senha (texto simples*) |
| Telefone | VARCHAR(20) | Telefone (opcional) |
| IngredientesNaoGosta | TEXT | Ingredientes que não gosta |
| Alergias | TEXT | Alergias declaradas |
| DataCadastro | DATETIME | Data de criação |

*⚠️ Em produção, usar hash BCrypt/Argon2*

#### Tabela: `pratos`
| Campo | Tipo | Descrição |
|-------|------|-----------|
| Id | INT (PK) | Identificador único |
| Categoria | VARCHAR(100) | Entrada, Prato Principal, Sobremesa, Bebida |
| Item_Menu | VARCHAR(255) | Nome do prato |
| Ingredientes | TEXT | Lista de ingredientes |
| Preco | DECIMAL(18,2) | Preço do prato |
| Tem_Lactose | VARCHAR(20) | "Sim", "Não", "Desconhecido" |

---

## 🤖 Integração com IA

### Modelo: Phi-3-mini-4k-instruct

**Especificações**:
- **Desenvolvedor**: Microsoft
- **Parâmetros**: 3.8 bilhões
- **Quantização**: Q4 (4-bit)
- **Tamanho**: 2.3 GB
- **Contexto**: 4096 tokens (~3000 palavras)
- **Hardware**: CPU only (sem GPU)

**Configurações**:
```json
{
  "Temperature": 0.8,      // Criatividade moderada
  "TopP": 0.9,            // Nucleus sampling
  "MaxTokens": 512,       // Limite de resposta
  "ContextSize": 4096,    // Janela de contexto
  "NumThreads": 4         // Paralelização CPU
}
```

### Fluxo de Processamento

1. **Usuário**: "Quais pratos sem lactose?"
2. **App**: Envia para `/api/Pratos/assistente-chat`
3. **API**: Busca pratos no MySQL onde `Tem_Lactose = "Não"`
4. **API**: Monta prompt contextualizado:
   ```
   CLIENTE PERGUNTOU: Quais pratos sem lactose?
   CARDÁPIO SEM LACTOSE: Salada Caesar, Filé Mignon, Salmão Grelhado...
   INSTRUÇÃO: Liste os pratos acima de forma simpática.
   ```
5. **LlamaService**: Processa com Phi-3
6. **API**: Limpa resposta (remove ruídos, formatação)
7. **App**: Exibe resposta limpa ao usuário

---

## 🔐 Segurança

### Implementado
- ✅ **JWT Authentication**: Tokens com expiração de 2 horas
- ✅ **CORS**: Configurado para origens específicas
- ✅ **HTTPS**: Suportado (desenvolvimento usa HTTP)
- ✅ **Validação de Dados**: Data annotations nos modelos

### Melhorias Futuras
- ⏳ Hash de senhas (BCrypt/Argon2)
- ⏳ Rate limiting
- ⏳ Refresh tokens
- ⏳ HTTPS obrigatório em produção

---

## 📊 Endpoints da API

### Usuários

#### POST `/api/Usuarios/Cadastrar`
Cadastra novo usuário.
```json
{
  "nome": "João Silva",
  "email": "joao@gmail.com",
  "senha": "123456",
  "telefone": "(11) 98765-4321",
  "ingredientesNaoGosta": "Cebola",
  "alergias": "Lactose"
}
```

#### POST `/api/Usuarios/Login`
Autentica usuário e retorna JWT.
```json
{
  "email": "joao@gmail.com",
  "senha": "123456"
}
```

**Resposta**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "usuario": {
    "id": 1,
    "nome": "João Silva",
    "email": "joao@gmail.com",
    "alergias": "Lactose"
  }
}
```

### Pratos

#### GET `/api/Pratos`
Lista pratos (com filtros opcionais).

**Parâmetros**:
- `alergias` (opcional): Filtrar por alergia (ex: "lactose")
- `categoria` (opcional): Filtrar por categoria

**Exemplo**: `/api/Pratos?alergias=lactose`

#### POST `/api/Pratos/assistente-chat` 🔒
Envia pergunta para a IA.

**Requer**: Bearer Token JWT

```json
{
  "prompt": "Quais pratos sem lactose?"
}
```

**Resposta**:
```json
{
  "sucesso": true,
  "mensagem": "Temos estes pratos sem lactose: Salada Caesar, Filé Mignon, Salmão Grelhado..."
}
```

---

## 🧪 Testes

### Testar API com Swagger
1. Iniciar API
2. Acessar: http://localhost:5068/swagger
3. Testar endpoints diretamente no navegador

### Usuários de Teste
| Nome | Email | Senha | Alergia |
|------|-------|-------|---------|
| João Silva | joao@gmail.com | 123456 | Lactose |
| Maria Santos | maria@hotmail.com | 123456 | Nenhuma |
| Pedro Oliveira | pedro@outlook.com | 123456 | Lactose |

### Dados de Teste
- **Usuários**: 5 cadastrados
- **Pratos**: ~45 total
  - Sem lactose: ~30
  - Com lactose: ~15
- **Categorias**: Entrada, Prato Principal, Sobremesa, Bebida

---

## 🐛 Problemas Conhecidos

| Problema | Impacto | Solução |
|----------|---------|---------|
| Senhas sem hash | Segurança | Implementar BCrypt |
| Primeira resposta IA lenta (30s) | UX | Aguardado, modelo carrega |
| Device físico precisa mesmo Wi-Fi | Config | Documentado no guia |

---

## 🚧 Roadmap (Trabalhos Futuros)

### Curto Prazo
- [ ] Hash de senhas (BCrypt)
- [ ] Testes unitários (xUnit)
- [ ] CI/CD com GitHub Actions
- [ ] Logs estruturados (Serilog)

### Médio Prazo
- [ ] Cache de respostas da IA
- [ ] Modo offline (SQLite local)
- [ ] Push notifications
- [ ] Dashboard administrativo
- [ ] Upgrade para Phi-3-medium (melhor qualidade)

### Longo Prazo
- [ ] Suporte a outras restrições (glúten, vegano)
- [ ] Sistema de avaliações de pratos
- [ ] Integração com APIs de restaurantes
- [ ] Gamificação (pontos, badges)
- [ ] Machine Learning para sugestões personalizadas

---

## 👨‍💻 Autor

**Eduardo Bonaci**  
Ciência da Computação - 4º ano  
Trabalho de Conclusão de Curso (TCC) - 2025

📧 Email: [Disponível no GitHub]  
🔗 GitHub: [@eduardo-bonaci](https://github.com/eduardo-bonaci)  
🔗 LinkedIn: [Disponível no perfil]

---

## 📄 Licença

Este projeto é um Trabalho de Conclusão de Curso e está sob a licença MIT.

```
MIT License

Copyright (c) 2025 Eduardo Bonaci

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

[Licença completa em LICENSE.txt]
```

---

## 🙏 Agradecimentos

- **Microsoft**: Pelo modelo Phi-3 open-source
- **SciSharp**: Pela biblioteca LLamaSharp
- **Comunidade .NET**: Pelas ferramentas e documentação
- **Orientador(a)**: [Nome] - Pelo apoio e direcionamento
- **Família e Amigos**: Pelo suporte durante o desenvolvimento

---

## 📚 Referências

### Técnicas
1. Microsoft. (2024). *Phi-3 Technical Report*. Microsoft Research.
2. Vaswani et al. (2017). *Attention is All You Need*. NeurIPS.
3. Fielding, R. (2000). *Architectural Styles and the Design of Network-based Software Architectures*. Doctoral dissertation.

### Médicas
4. Mattar, R., & Mazo, D. F. C. (2010). *Intolerância à lactose*. Revista Brasileira de Medicina.
5. WHO. (2022). *Lactose Intolerance: Global Statistics*. World Health Organization.

### Livros
6. Goodfellow, I., Bengio, Y., & Courville, A. (2016). *Deep Learning*. MIT Press.
7. Burns, G. (2023). *.NET MAUI in Action*. Manning Publications.

---

## 📞 Suporte

Encontrou um bug? Tem alguma dúvida?

1. **Documentação**: Leia o [GUIA_EXECUCAO_COMPLETO.md](GUIA_EXECUCAO_COMPLETO.md)
2. **Issues**: Abra uma issue no GitHub
3. **Email**: [Disponível no perfil]

---

## 📈 Estatísticas do Projeto

- **Linhas de código**: ~8.000
- **Tempo de desenvolvimento**: 6 meses
- **Commits**: 150+
- **Arquivos**: 80+
- **Linguagens**: C# (90%), XAML (8%), SQL (2%)

---

## ⭐ Star o Projeto

Se este projeto foi útil para você, considere dar uma ⭐ no GitHub!

---

<div align="center">

**🍽️ Desenvolvido com ❤️ para ajudar pessoas com intolerância à lactose**

*"Tecnologia a serviço da qualidade de vida"*

</div>
