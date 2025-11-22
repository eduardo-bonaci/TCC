# 📱 Cardápio Inteligente - Instruções de Instalação e Uso

## 📋 Pré-requisitos

### Para Windows Desktop e Android:
1. **.NET 8.0 SDK** - [Download aqui](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **MySQL Server** (versão 5.7 ou superior)
   - Instalação: [Download MySQL](https://dev.mysql.com/downloads/mysql/)
   - **IMPORTANTE**: Durante a instalação, configure SEM SENHA para o usuário root, OU
   - Configure a senha e depois atualize no arquivo `appsettings.json`
3. **Visual Studio 2022** (recomendado) com:
   - Workload: ".NET Multi-platform App UI development"
   - OU Visual Studio Code com extensões C#

### Para desenvolvimento Android:
4. **Android SDK** (instalado automaticamente com Visual Studio)
5. Dispositivo Android físico ou Emulador Android

---

## 🗄️ Configuração do Banco de Dados MySQL

### 1. Criar o banco de dados:
```sql
CREATE DATABASE cardapio_db;
USE cardapio_db;
```

### 2. Criar tabela de usuários:
```sql
CREATE TABLE usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Senha VARCHAR(256) NOT NULL,
    Telefone VARCHAR(20),
    Alergias TEXT,
    IngredientesNaoGosta TEXT,
    DataCadastro DATETIME NOT NULL
);
```

### 3. Criar tabela de pratos:
```sql
CREATE TABLE prato (
    id INT AUTO_INCREMENT PRIMARY KEY,
    Categoria VARCHAR(30) NOT NULL DEFAULT ' ',
    Item_Menu TEXT,
    Ingredientes TEXT,
    Preco DOUBLE NOT NULL DEFAULT 1,
    Tem_Lactose ENUM('Desconhecido','Sim','Não') NOT NULL
);
```

### 4. Inserir dados de exemplo (pratos.csv):
Use o arquivo CSV fornecido ou insira manualmente alguns pratos de teste.

### 5. Verificar a connection string:
Abra `Cardapio_Inteligente.Api/appsettings.json` e verifique:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=cardapio_db;Uid=root;Pwd=;"
}
```
- Se você configurou senha no MySQL, altere `Pwd=;` para `Pwd=sua_senha;`

---

## 🚀 Instalação e Execução

### Opção 1: Execução Automatizada (Recomendado - Windows)

1. **Clone ou extraia o repositório**
2. **Execute o script de inicialização**:
   - Windows: Clique duplo em `iniciar-app.bat` OU abra PowerShell e execute `.\iniciar-app.ps1`
   - O script irá:
     - ✅ Verificar e iniciar o MySQL
     - ✅ Iniciar a API em segundo plano
     - ✅ Compilar e executar o aplicativo MAUI

### Opção 2: Execução Manual

#### Passo 1: Iniciar a API
```bash
cd Cardapio_Inteligente.Api
dotnet restore
dotnet run
```
A API estará disponível em: `http://localhost:5068`

#### Passo 2: Executar o aplicativo MAUI (em outro terminal)

**Para Windows:**
```bash
cd Cardapio_Inteligente
dotnet restore
dotnet build -f net8.0-windows10.0.19041.0
dotnet run -f net8.0-windows10.0.19041.0
```

**Para Android (dispositivo físico conectado via USB):**
```bash
cd Cardapio_Inteligente
dotnet restore
dotnet build -f net8.0-android
dotnet run -f net8.0-android
```

---

## 📱 Configuração para Android

### Para Emulador Android:
O app já está configurado para usar `10.0.2.2:5068` que é o IP especial do emulador para acessar localhost do host.

### Para Dispositivo Físico Android:
1. **Conecte o dispositivo via USB e ative depuração USB**
2. **Descubra o IP da sua máquina na rede local**:
   - Windows: Execute `ipconfig` e procure por "IPv4 Address" (ex: 192.168.1.100)
   - Linux/Mac: Execute `ifconfig` ou `ip addr`
3. **Atualize o IP no código**:
   - Abra `Cardapio_Inteligente/servicos/ApiService.cs`
   - Na linha ~53, altere o IP:
   ```csharp
   "http://192.168.1.100:5068"  // ⚠️ COLOQUE O IP DA SUA MÁQUINA AQUI
   ```
4. **Configure o firewall do Windows**:
   - Abra "Firewall do Windows Defender"
   - Crie uma regra de entrada para permitir porta 5068
5. **Recompile o aplicativo**

---

## 🧪 Testando a Instalação

### 1. Testar a API:
Abra o navegador e acesse: `http://localhost:5068/swagger`

Você deve ver a documentação Swagger da API.

### 2. Testar conexão com banco:
Na página do Swagger, teste o endpoint:
- `GET /api/Pratos` - Deve retornar a lista de pratos

### 3. Testar o aplicativo:
1. Execute o aplicativo MAUI
2. Crie uma conta de usuário
3. Faça login
4. Navegue pelos pratos

---

## 🔧 Solução de Problemas

### Erro: "Não foi possível conectar à API"
**Causa**: A API não está rodando ou está bloqueada pelo firewall.
**Solução**:
1. Verifique se a API está rodando (deve aparecer "API iniciada em http://localhost:5068")
2. Verifique o firewall do Windows
3. Para Android físico: certifique-se de que o IP está correto e que ambos estão na mesma rede

### Erro: "MySQL não está acessível"
**Causa**: MySQL não está rodando ou connection string está incorreta.
**Solução**:
1. Inicie o serviço MySQL: `net start MySQL` (Windows) ou `sudo systemctl start mysql` (Linux)
2. Verifique se o banco `cardapio_db` existe
3. Verifique a connection string no `appsettings.json`

### Erro: "Modelo de IA não encontrado"
**Causa**: O arquivo Phi-3-mini-4k-instruct-q4.gguf não está presente.
**Solução**:
1. O modelo já deve estar em `Cardapio_Inteligente.Api/ModelosIA/`
2. Se não estiver, baixe de: https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf
3. Coloque o arquivo na pasta `ModelosIA/`

### App Android não conecta à API
**Causa**: IP incorreto ou firewall bloqueando.
**Solução**:
1. Verifique se está usando o IP correto da máquina (não `localhost`)
2. Teste o IP: abra o navegador do Android e acesse `http://SEU_IP:5068/swagger`
3. Libere a porta 5068 no firewall

---

## 📚 Estrutura do Projeto

```
TCC/
├── Cardapio_Inteligente/           # Aplicativo MAUI (Frontend)
│   ├── Paginas/                    # Telas do app
│   ├── Servicos/                   # Serviços HTTP e API
│   └── Modelos/                    # Classes de modelo
│
├── Cardapio_Inteligente.Api/       # API Backend
│   ├── Controllers/                # Endpoints da API
│   ├── Dados/                      # DbContext e acesso ao MySQL
│   ├── Modelos/                    # Entidades do banco
│   ├── Servicos/                   # Serviço de IA (LLama)
│   └── ModelosIA/                  # Arquivo .gguf do modelo Phi-3
│
├── iniciar-app.bat                 # Script de inicialização Windows
├── iniciar-app.ps1                 # Script PowerShell
└── iniciar-app.sh                  # Script Linux/Mac
```

---

## 🎯 Funcionalidades

- ✅ Autenticação com JWT
- ✅ Cadastro de usuários com preferências alimentares
- ✅ Listagem de pratos filtrados por categoria e alergias
- ✅ Chat com IA (Phi-3) para recomendações
- ✅ Suporte a Windows Desktop
- ✅ Suporte a Android (tablet e celular)
- ✅ Tudo roda localmente (sem dependência de nuvem)

---

## 📞 Suporte

Para problemas ou dúvidas:
1. Verifique este arquivo de instruções
2. Verifique os logs no console da API
3. Verifique se todos os pré-requisitos estão instalados

---

## ⚙️ Variáveis de Configuração Importantes

### appsettings.json (API):
- `ConnectionStrings:DefaultConnection` - String de conexão MySQL
- `JwtSettings:Secret` - Chave secreta para JWT
- `LlamaSettings:ModelPath` - Caminho do modelo de IA

### ApiService.cs (MAUI):
- Linha ~52: IP para Android físico conectar à API
