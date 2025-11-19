# 🍽️ Cardápio Inteligente - TCC

## 📋 Descrição do Projeto

Aplicativo multiplataforma desenvolvido em **.NET MAUI** para auxiliar pessoas com **intolerância à lactose** na escolha de pratos em restaurantes. O sistema utiliza **Inteligência Artificial local** (modelo Phi-3-mini) para recomendar pratos sem lactose com base em um cardápio cadastrado em banco de dados **MySQL**.

---

## 🎯 Objetivos

- ✅ Facilitar a escolha de pratos para pessoas com intolerância à lactose
- ✅ Integrar IA local para recomendações personalizadas
- ✅ Funcionar em **Android**, **Windows Desktop** e **Tablets**
- ✅ Garantir segurança com autenticação JWT
- ✅ Armazenar dados de forma estruturada em MySQL

---

## 🏗️ Arquitetura do Sistema

```
┌─────────────────────────────────────────────────────────────┐
│                    APLICATIVO MAUI                          │
│  (Android / Windows / Tablet)                               │
│  - Tela de Login/Cadastro                                   │
│  - Listagem de Pratos                                       │
│  - Chat com IA                                              │
└────────────────────┬────────────────────────────────────────┘
                     │ HTTP/HTTPS (JWT)
                     ▼
┌─────────────────────────────────────────────────────────────┐
│                    API REST (.NET 8)                        │
│  - Autenticação JWT                                         │
│  - Controllers (Usuarios, Pratos, Ingredientes)             │
│  - Serviço de IA (LLamaSharp)                               │
└────────────┬───────────────────────┬────────────────────────┘
             │                       │
             ▼                       ▼
    ┌────────────────┐      ┌──────────────────┐
    │  MySQL         │      │  Modelo Phi-3    │
    │  (cardapio_db) │      │  (IA Local)      │
    └────────────────┘      └──────────────────┘
```

---

## 🛠️ Tecnologias Utilizadas

### **Backend (API)**
- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Entity Framework Core 8.0** - ORM
- **Pomelo.EntityFrameworkCore.MySql** - Provider MySQL
- **LLamaSharp 0.25.0** - Integração com modelo Phi-3
- **JWT Bearer Authentication** - Segurança

### **Frontend (MAUI)**
- **.NET MAUI 9.0** - Framework multiplataforma
- **XAML** - Interface de usuário
- **HttpClient** - Comunicação com API

### **Banco de Dados**
- **MySQL 8.0+** - Banco de dados relacional

### **Inteligência Artificial**
- **Phi-3-mini-4k-instruct-q4.gguf** - Modelo de linguagem da Microsoft
- **Quantização Q4** - Otimizado para CPU
- **Tamanho**: ~2.4 GB
- **Contexto**: 4096 tokens

---

## 📦 Requisitos do Sistema

### **Para Desenvolvimento:**
- **Visual Studio 2022** (17.8+) com workloads:
  - ASP.NET e desenvolvimento Web
  - Desenvolvimento Móvel com .NET (MAUI)
- **.NET 8 SDK** e **.NET 9 SDK**
- **MySQL Server 8.0+**
- **MySQL Workbench** (opcional, para gerenciar BD)

### **Para Executar a IA:**
- **CPU**: 4+ cores (recomendado: 6+ cores)
- **RAM**: 8 GB mínimo (recomendado: 16 GB)
- **Espaço em Disco**: 5 GB livres
- **Sistema Operacional**: Windows 10/11 ou Linux

### **Para Testar no Android:**
- **Emulador Android** (API 21+) ou
- **Dispositivo físico** com Android 5.0+

---

## 🚀 Instalação e Configuração

### **1. Clonar o Repositório**

```bash
git clone https://github.com/eduardo-bonaci/TCC.git
cd TCC
```

### **2. Configurar o Banco de Dados MySQL**

#### **2.1. Criar o Banco de Dados**

Execute o script SQL fornecido:

```bash
mysql -u root -p < database_setup.sql
```

Ou abra o arquivo `database_setup.sql` no **MySQL Workbench** e execute.

#### **2.2. Verificar Conexão**

Edite o arquivo `Cardapio_Inteligente.Api/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=cardapio_db;Uid=root;Pwd=SUA_SENHA_AQUI;"
}
```

**⚠️ IMPORTANTE**: Substitua `SUA_SENHA_AQUI` pela senha do seu MySQL.

### **3. Baixar o Modelo de IA Phi-3**

#### **3.1. Download do Modelo**

Acesse: [https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf](https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf)

Baixe o arquivo: **`Phi-3-mini-4k-instruct-q4.gguf`** (~2.4 GB)

#### **3.2. Colocar o Modelo na Pasta Correta**

Crie a pasta (se não existir):

```bash
mkdir -p Cardapio_Inteligente.Api/ModelosIA
```

Mova o arquivo baixado para:

```
Cardapio_Inteligente.Api/ModelosIA/Phi-3-mini-4k-instruct-q4.gguf
```

**⚠️ CRÍTICO**: Sem este arquivo, a API **NÃO VAI INICIAR**.

### **4. Restaurar Dependências**

#### **4.1. API**

```bash
cd Cardapio_Inteligente.Api
dotnet restore
```

#### **4.2. MAUI**

```bash
cd ../Cardapio_Inteligente
dotnet restore
```

### **5. Aplicar Migrations (Criar Tabelas)**

```bash
cd Cardapio_Inteligente.Api
dotnet ef database update
```

Se não tiver o `dotnet-ef` instalado:

```bash
dotnet tool install --global dotnet-ef
```

---

## ▶️ Executando o Projeto

### **1. Iniciar a API**

```bash
cd Cardapio_Inteligente.Api
dotnet run
```

A API estará disponível em:
- **HTTP**: `http://localhost:5068`
- **HTTPS**: `https://localhost:7068`
- **Swagger**: `http://localhost:5068/swagger`

**Logs esperados:**
```
🗄️ Banco de dados verificado/criado com sucesso.
🤖 Serviço de IA inicializado com sucesso.
🚀 API Cardápio Inteligente iniciada com sucesso!
```

### **2. Executar o Aplicativo MAUI**

#### **2.1. Windows Desktop**

No Visual Studio:
1. Defina `Cardapio_Inteligente` como projeto de inicialização
2. Selecione **Windows Machine** como target
3. Pressione **F5** ou clique em **Executar**

Ou via linha de comando:

```bash
cd Cardapio_Inteligente
dotnet build -f net9.0-windows10.0.19041.0
dotnet run -f net9.0-windows10.0.19041.0
```

#### **2.2. Android (Emulador)**

No Visual Studio:
1. Inicie um emulador Android (API 21+)
2. Selecione o emulador como target
3. Pressione **F5**

Ou via linha de comando:

```bash
cd Cardapio_Inteligente
dotnet build -f net9.0-android
dotnet run -f net9.0-android
```

**⚠️ IMPORTANTE**: No Android, a API deve estar acessível em `http://10.0.2.2:5068` (o app já está configurado para isso).

---

## 🧪 Testando o Sistema

### **1. Testar a API via Swagger**

Acesse: `http://localhost:5068/swagger`

#### **1.1. Cadastrar Usuário**

```
POST /api/Usuarios/Cadastrar
```

Body:
```json
{
  "nome": "João Silva",
  "email": "joao@gmail.com",
  "senha": "senha123",
  "telefone": "(11) 98765-4321",
  "ingredientesNaoGosta": "Cebola, Pimentão",
  "alergias": "Lactose"
}
```

#### **1.2. Fazer Login**

```
POST /api/Usuarios/Login
```

Body:
```json
{
  "email": "joao@gmail.com",
  "senha": "senha123"
}
```

Copie o **token** retornado.

#### **1.3. Listar Pratos (Autenticado)**

Clique em **Authorize** no Swagger e cole o token no formato:

```
Bearer SEU_TOKEN_AQUI
```

Depois teste:

```
GET /api/Pratos?alergias=lactose
```

#### **1.4. Testar IA**

```
POST /api/Pratos/assistente-chat
```

Body:
```json
{
  "prompt": "Quais pratos sem lactose você tem?"
}
```

### **2. Testar o Aplicativo MAUI**

#### **Fluxo Completo:**

1. **Tela Inicial**: Clique em "Já sou cliente"
2. **Login**: Use `teste@gmail.com` / `teste123`
3. **Cardápio**: Veja a lista de pratos
4. **Filtrar**: Clique em "Sem Lactose"
5. **Assistente**: Digite "Quais pratos sem lactose?" e clique em ➡
6. **Chat IA**: Clique em "Assistente" para abrir o chat completo

---

## 📊 Estrutura do Banco de Dados

### **Tabela: usuarios**

| Campo                  | Tipo          | Descrição                          |
|------------------------|---------------|------------------------------------|
| Id                     | INT (PK)      | Identificador único                |
| Nome                   | VARCHAR(255)  | Nome completo                      |
| Email                  | VARCHAR(150)  | Email (único)                      |
| Senha                  | VARCHAR(255)  | Senha (texto simples - ⚠️ melhorar)|
| Telefone               | VARCHAR(20)   | Telefone (opcional)                |
| IngredientesNaoGosta   | TEXT          | Ingredientes que não gosta         |
| Alergias               | VARCHAR(255)  | Alergias/Intolerâncias             |
| DataCadastro           | DATETIME      | Data de cadastro                   |

### **Tabela: pratos**

| Campo         | Tipo            | Descrição                     |
|---------------|-----------------|-------------------------------|
| Id            | INT (PK)        | Identificador único           |
| Categoria     | VARCHAR(100)    | Categoria do prato            |
| Item_Menu     | VARCHAR(255)    | Nome do prato                 |
| Ingredientes  | TEXT            | Lista de ingredientes         |
| Preco         | DECIMAL(18,2)   | Preço do prato                |
| Tem_Lactose   | VARCHAR(20)     | "Sim", "Não" ou "Desconhecido"|

---

## 🤖 Como Funciona a IA

### **Modelo Utilizado**

- **Nome**: Phi-3-mini-4k-instruct-q4
- **Desenvolvedor**: Microsoft
- **Tipo**: Modelo de linguagem generativo
- **Quantização**: Q4 (4 bits) - otimizado para CPU
- **Contexto**: 4096 tokens (~3000 palavras)

### **Fluxo de Processamento**

1. **Usuário** faz uma pergunta no app
2. **MAUI** envia para API via `POST /api/Pratos/assistente-chat`
3. **API** busca pratos relevantes no MySQL
4. **API** monta um prompt contextualizado com os pratos
5. **LLamaSharp** processa o prompt com o modelo Phi-3
6. **IA** gera resposta em linguagem natural
7. **API** limpa a resposta (remove ruídos)
8. **MAUI** exibe a resposta ao usuário

### **Exemplo de Prompt Interno**

```
CLIENTE PERGUNTOU: Quais pratos sem lactose você tem?

CARDÁPIO SEM LACTOSE DISPONÍVEL: Salada Tropical, Bruschetta, Carpaccio de Salmão, Frango Grelhado, Picanha na Chapa, Feijoada Completa, Moqueca de Peixe, Espaguete ao Alho e Óleo

INSTRUÇÃO: Liste TODOS os pratos sem lactose acima em sua resposta. Seja simpático e direto.

RESPOSTA:
```

### **Configurações de IA (appsettings.json)**

```json
"LlamaSettings": {
  "ModelPath": "ModelosIA/Phi-3-mini-4k-instruct-q4.gguf",
  "MaxTokens": 512,
  "Temperature": 0.8,
  "TopP": 0.9,
  "GpuLayerCount": 0,
  "NumThreads": 4,
  "ContextSize": 4096
}
```

- **MaxTokens**: Máximo de tokens gerados por resposta
- **Temperature**: Criatividade (0.7-1.1, maior = mais criativo)
- **TopP**: Diversidade (0.85-0.95)
- **GpuLayerCount**: 0 = CPU, >0 = GPU (se disponível)
- **NumThreads**: Threads da CPU (ajuste conforme seu processador)

---

## 🔒 Segurança

### **Autenticação JWT**

- **Algoritmo**: HMAC-SHA256
- **Validade**: 2 horas (configurável)
- **Claims**: Id, Nome, Email, Alergias

### **⚠️ MELHORIAS NECESSÁRIAS PARA PRODUÇÃO:**

1. **Senhas**: Implementar hash (BCrypt ou Argon2)
2. **HTTPS**: Forçar HTTPS em produção
3. **Refresh Token**: Implementar renovação de token
4. **Rate Limiting**: Limitar requisições por IP
5. **Validação de Entrada**: Sanitizar todos os inputs

---

## 🐛 Problemas Comuns e Soluções

### **1. API não inicia - Erro "Modelo não encontrado"**

**Causa**: Arquivo `Phi-3-mini-4k-instruct-q4.gguf` não está na pasta correta.

**Solução**:
```bash
# Verifique se o arquivo existe
ls -la Cardapio_Inteligente.Api/ModelosIA/

# Deve mostrar o arquivo .gguf (~2.4 GB)
```

### **2. Erro de conexão com MySQL**

**Causa**: MySQL não está rodando ou senha incorreta.

**Solução**:
```bash
# Verificar se MySQL está rodando
sudo systemctl status mysql  # Linux
# ou
net start MySQL80  # Windows

# Testar conexão
mysql -u root -p
```

### **3. MAUI não conecta na API (Android)**

**Causa**: Emulador Android não consegue acessar `localhost`.

**Solução**: O app já está configurado para usar `10.0.2.2:5068` (IP especial do emulador que aponta para o host).

Certifique-se de que a API está rodando em `http://localhost:5068` (não HTTPS).

### **4. IA demora muito para responder**

**Causa**: CPU fraca ou muitos threads configurados.

**Solução**: Ajuste `NumThreads` em `appsettings.json`:
```json
"NumThreads": 2  // Reduza para 2 se estiver lento
```

### **5. Erro "CS0266" ao compilar API**

**Causa**: Conversão de tipo no LlamaService.

**Solução**: Já corrigido no código. Se persistir, verifique a linha:
```csharp
ContextSize = (uint)_settings.ContextSize,  // Conversão explícita
```

---

## 📚 Documentação Adicional

### **Endpoints da API**

#### **Usuários**

- `POST /api/Usuarios/Cadastrar` - Cadastrar novo usuário
- `POST /api/Usuarios/Login` - Fazer login
- `GET /api/Usuarios/{id}` - Obter dados do usuário (autenticado)

#### **Pratos**

- `GET /api/Pratos` - Listar todos os pratos (autenticado)
- `GET /api/Pratos?alergias=lactose` - Filtrar pratos sem lactose
- `POST /api/Pratos/assistente-chat` - Conversar com IA (autenticado)

#### **Ingredientes**

- `GET /api/Ingredientes` - Listar ingredientes únicos (público)

### **Modelos de Dados**

#### **LoginDto**
```csharp
{
  "email": "string",
  "senha": "string"
}
```

#### **LoginResponse**
```csharp
{
  "token": "string",
  "usuario": { /* objeto Usuario */ }
}
```

---

## 📝 Checklist para Defesa do TCC

### **Demonstração Prática:**

- [ ] Mostrar cadastro de novo usuário
- [ ] Fazer login
- [ ] Filtrar pratos sem lactose
- [ ] Perguntar à IA sobre o cardápio
- [ ] Mostrar resposta da IA em tempo real
- [ ] Demonstrar em Android e Windows

### **Aspectos Técnicos a Destacar:**

- [ ] Arquitetura em camadas (API + MAUI)
- [ ] Uso de IA local (não depende de internet)
- [ ] Multiplataforma (Android, Windows, Tablet)
- [ ] Segurança com JWT
- [ ] Banco de dados relacional
- [ ] Integração com modelo Phi-3 da Microsoft

### **Possíveis Perguntas da Banca:**

1. **Por que IA local ao invés de API externa?**
   - Privacidade dos dados
   - Não depende de internet
   - Sem custos de API
   - Controle total sobre o modelo

2. **Por que MySQL ao invés de SQLite?**
   - Escalabilidade
   - Suporte a múltiplos usuários simultâneos
   - Melhor para ambiente de produção
   - Facilita backup e manutenção

3. **Como garantir que a IA não dá informações erradas?**
   - Prompt engineering (instruções claras)
   - Validação das respostas
   - Fallback para respostas diretas
   - Limpeza de ruídos na resposta

4. **Qual o diferencial do seu app?**
   - Foco específico em lactose
   - IA contextualizada com cardápio real
   - Multiplataforma
   - Interface simples e intuitiva

---

## 🎓 Melhorias Futuras (Pós-TCC)

1. **Segurança**:
   - Hash de senhas (BCrypt)
   - Refresh tokens
   - Rate limiting

2. **Funcionalidades**:
   - Favoritar pratos
   - Histórico de pedidos
   - Avaliações de pratos
   - Notificações push

3. **IA**:
   - Fine-tuning do modelo para cardápios
   - Suporte a mais alergias (glúten, frutos do mar, etc.)
   - Recomendações personalizadas

4. **Infraestrutura**:
   - Deploy em nuvem (Azure/AWS)
   - CI/CD com GitHub Actions
   - Monitoramento (Application Insights)

---

## 👥 Autor

**Eduardo Bonaci**  
Curso: Ciência da Computação - 4º Ano  
Instituição: [Nome da Instituição]  
Ano: 2025

---

## 📄 Licença

Este projeto é um Trabalho de Conclusão de Curso (TCC) e está disponível para fins educacionais.

---

## 🙏 Agradecimentos

- **Microsoft** - Modelo Phi-3
- **LLamaSharp** - Biblioteca de integração
- **Comunidade .NET** - Suporte e documentação

---

## 📞 Suporte

Para dúvidas ou problemas:
1. Verifique a seção "Problemas Comuns"
2. Consulte os logs da API
3. Abra uma issue no GitHub

---

**🚀 Boa sorte na defesa do TCC!**
