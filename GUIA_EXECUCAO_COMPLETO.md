# 📱 GUIA COMPLETO DE EXECUÇÃO - CARDÁPIO INTELIGENTE

## 🎯 Visão Geral
Este guia ensina como executar o aplicativo **Cardápio Inteligente** em:
- 📱 **Celulares Android** (emulador ou dispositivo físico)
- 📱 **Tablets Android** (emulador ou dispositivo físico)
- 💻 **Desktop Windows**

---

## 📋 PRÉ-REQUISITOS

### 1️⃣ Software Necessário

#### Para API (.NET)
- ✅ **.NET 8 SDK** (ou superior)
  - Download: https://dotnet.microsoft.com/download
  - Verificar: `dotnet --version`

#### Para App MAUI
- ✅ **Visual Studio 2022** (17.8 ou superior)
  - Workloads necessários:
    - ✅ Desenvolvimento para dispositivos móveis com .NET
    - ✅ Desenvolvimento para desktop com .NET
  - Download: https://visualstudio.microsoft.com/

#### Para Banco de Dados
- ✅ **MySQL Server 8.0+**
  - Download: https://dev.mysql.com/downloads/mysql/
  - Ou use XAMPP/WAMP
- ✅ **MySQL Workbench** (recomendado)
  - Download: https://dev.mysql.com/downloads/workbench/

#### Para IA
- ✅ **Modelo Phi-3-mini-4k-instruct-q4.gguf** (~2.3GB)
  - Download: https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf
  - Arquivo: `Phi-3-mini-4k-instruct-q4.gguf`

### 2️⃣ Requisitos de Hardware

#### Mínimo:
- **CPU**: 4 cores
- **RAM**: 8 GB
- **Disco**: 10 GB livres
- **GPU**: Não necessária

#### Recomendado:
- **CPU**: 6+ cores
- **RAM**: 16 GB
- **Disco**: 20 GB livres (SSD preferível)

---

## 🚀 PASSO A PASSO - CONFIGURAÇÃO INICIAL

### ETAPA 1: Configurar Banco de Dados MySQL

#### 1.1. Iniciar MySQL Server
```bash
# Windows (XAMPP)
Abrir XAMPP Control Panel → Start MySQL

# Windows (Serviço)
services.msc → MySQL → Iniciar

# Linux
sudo service mysql start
```

#### 1.2. Criar Database
Abra MySQL Workbench ou linha de comando:
```sql
CREATE DATABASE cardapio_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

#### 1.3. Executar Script SQL com Dados
```bash
# Navegue até a pasta do projeto
cd /caminho/para/TCC

# Execute o script
mysql -u root -p cardapio_db < database_seed.sql
```

Ou pelo MySQL Workbench:
1. Abrir `database_seed.sql`
2. Executar todo o script (Ctrl+Shift+Enter)

#### 1.4. Verificar Dados Inseridos
```sql
USE cardapio_db;

-- Verificar usuários
SELECT * FROM usuarios;

-- Verificar pratos
SELECT COUNT(*) AS Total, Tem_Lactose FROM pratos GROUP BY Tem_Lactose;
```

**Resultado esperado**: 
- 5 usuários cadastrados
- ~45 pratos (30+ sem lactose, 15+ com lactose)

---

### ETAPA 2: Baixar e Configurar Modelo de IA

#### 2.1. Baixar Modelo Phi-3
1. Acesse: https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf
2. Clique em "Files and versions"
3. Baixe: `Phi-3-mini-4k-instruct-q4.gguf` (2.3 GB)

#### 2.2. Criar Pasta e Copiar Modelo
```bash
# Windows PowerShell
cd C:\caminho\para\TCC\Cardapio_Inteligente.Api
mkdir ModelosIA
copy C:\Downloads\Phi-3-mini-4k-instruct-q4.gguf ModelosIA\

# Linux/Mac
cd /caminho/para/TCC/Cardapio_Inteligente.Api
mkdir ModelosIA
cp ~/Downloads/Phi-3-mini-4k-instruct-q4.gguf ModelosIA/
```

#### 2.3. Verificar Caminho
Estrutura deve ficar:
```
TCC/
├── Cardapio_Inteligente.Api/
│   ├── ModelosIA/
│   │   └── Phi-3-mini-4k-instruct-q4.gguf  ✅ (2.3 GB)
│   ├── Program.cs
│   └── ...
```

---

### ETAPA 3: Configurar Strings de Conexão

#### 3.1. Editar appsettings.json da API
Arquivo: `Cardapio_Inteligente.Api/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=cardapio_db;Uid=root;Pwd=SUA_SENHA_MYSQL;"
  },
  // ... resto do arquivo
}
```

⚠️ **Importante**: Altere `SUA_SENHA_MYSQL` para sua senha real do MySQL.

---

## 💻 EXECUTAR NO WINDOWS DESKTOP

### Passo 1: Iniciar API
```bash
# Abrir terminal na pasta da API
cd C:\caminho\para\TCC\Cardapio_Inteligente.Api

# Executar API
dotnet run
```

**Saída esperada**:
```
🔄 Carregando modelo Phi-3-mini de: C:\...\ModelosIA\Phi-3-mini-4k-instruct-q4.gguf
✅ Modelo carregado com sucesso!
🗄️ Banco de dados verificado/criado com sucesso.
🤖 Serviço de IA inicializado com sucesso.
🚀 API Cardápio Inteligente iniciada com sucesso!
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5068
```

✅ **API rodando em**: http://localhost:5068

### Passo 2: Testar API no Navegador
Abra: http://localhost:5068/swagger

Você deve ver a documentação Swagger com endpoints:
- `/api/Usuarios/Login`
- `/api/Usuarios/Cadastrar`
- `/api/Pratos`
- `/api/Pratos/assistente-chat`

### Passo 3: Executar App MAUI no Windows

#### Opção A: Visual Studio (Recomendado)
1. Abrir `Cardapio_Inteligente.sln` no Visual Studio
2. No topo, selecionar: **Windows Machine**
3. Clicar em ▶️ **Run** (F5)

#### Opção B: Linha de Comando
```bash
cd C:\caminho\para\TCC\Cardapio_Inteligente
dotnet build -f net8.0-windows10.0.19041.0
dotnet run -f net8.0-windows10.0.19041.0
```

### Passo 4: Testar Aplicação
1. **Tela de Login** abre automaticamente
2. **Login com usuário de teste**:
   - Email: `joao@gmail.com`
   - Senha: `123456`
3. **Navegar pelo cardápio**
4. **Testar filtros** (Com/Sem lactose)
5. **Testar chat com IA**

---

## 📱 EXECUTAR NO ANDROID (EMULADOR)

### Passo 1: Iniciar API (mesmo processo Windows)
```bash
cd C:\caminho\para\TCC\Cardapio_Inteligente.Api
dotnet run
```

### Passo 2: Configurar Emulador Android

#### 2.1. Criar/Iniciar Emulador no Visual Studio
1. Ir em **Tools → Device Manager**
2. Criar novo dispositivo ou usar existente:
   - **Recomendado**: Pixel 5 (API 34 - Android 14)
   - **Alternativa**: Any Android 10+ device
3. Iniciar emulador

### Passo 3: Executar App no Emulador

#### 3.1. No Visual Studio
1. Abrir `Cardapio_Inteligente.sln`
2. Selecionar: **Android Emulator** (dropdown superior)
3. Escolher seu emulador da lista
4. Clicar em ▶️ **Run**

#### 3.2. Linha de Comando
```bash
cd C:\caminho\para\TCC\Cardapio_Inteligente
dotnet build -f net8.0-android -t:Run
```

### Passo 4: Verificar Conexão

O app tentará conectar em:
1. ✅ `http://10.0.2.2:5068` (localhost do PC no emulador)
2. ⏭️ `http://localhost:5068` (fallback)

**Logs no Output do Visual Studio**:
```
🔄 Tentando conectar em: http://10.0.2.2:5068
✅ Conectado com sucesso em: http://10.0.2.2:5068
```

### Passo 5: Testar Aplicação
1. App abre no emulador
2. Fazer login: `joao@gmail.com` / `123456`
3. Testar todas as funcionalidades

---

## 📱 EXECUTAR NO ANDROID (DISPOSITIVO FÍSICO)

### Passo 1: Ativar Modo Desenvolvedor no Android
1. **Configurações → Sobre o telefone**
2. Tocar 7x em **Número da versão**
3. Voltar → **Opções do desenvolvedor**
4. Ativar: **Depuração USB**

### Passo 2: Conectar Dispositivo
1. Conectar telefone/tablet ao PC via USB
2. Autorizar depuração no dispositivo (popup)
3. Verificar no Visual Studio: Device Manager deve listar o dispositivo

### Passo 3: Descobrir IP da Máquina na Rede Local

#### Windows:
```bash
ipconfig
```
Procure por **IPv4 Address** da sua rede Wi-Fi  
Exemplo: `192.168.1.100`

#### Linux/Mac:
```bash
ifconfig
```

### Passo 4: Atualizar ApiService.cs
Abrir: `Cardapio_Inteligente/servicos/ApiService.cs`

Linha ~48, alterar:
```csharp
return new[] 
{ 
    "http://10.0.2.2:5068",      // Emulador
    "http://192.168.1.100:5068", // ✅ SEU IP AQUI
    "http://localhost:5068"
};
```

### Passo 5: Garantir que API Aceita Conexões Externas

#### 5.1. Editar launchSettings.json da API
Arquivo: `Cardapio_Inteligente.Api/Properties/launchSettings.json`

Adicionar `0.0.0.0` para escutar em todas as interfaces:
```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "http://0.0.0.0:5068",  // ✅ ALTERADO
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

#### 5.2. Liberar Porta no Firewall (Windows)
```bash
# PowerShell como Administrador
netsh advfirewall firewall add rule name="Cardapio API" dir=in action=allow protocol=TCP localport=5068
```

### Passo 6: Conectar Dispositivo e API na Mesma Rede Wi-Fi
⚠️ **Crucial**: Celular/Tablet e PC devem estar na mesma rede Wi-Fi!

### Passo 7: Executar API e App
```bash
# Terminal 1: Iniciar API
cd Cardapio_Inteligente.Api
dotnet run

# Visual Studio: Executar no dispositivo físico
```

### Passo 8: Testar Conexão
No app, fazer login. Se funcionar, conexão está OK!

**Logs esperados**:
```
🔄 Tentando conectar em: http://10.0.2.2:5068
⚠️ Falha ao conectar em http://10.0.2.2:5068: ...
🔄 Tentando conectar em: http://192.168.1.100:5068
✅ Conectado com sucesso em: http://192.168.1.100:5068
```

---

## 🧪 TESTES FUNCIONAIS

### 1. Teste de Login
- [ ] Abrir app
- [ ] Inserir: `joao@gmail.com` / `123456`
- [ ] Clicar "Entrar"
- [ ] ✅ Deve navegar para tela inicial

### 2. Teste de Cadastro
- [ ] Clicar "Não tem cadastro?"
- [ ] Preencher todos os campos
- [ ] Selecionar alergia "Lactose"
- [ ] Salvar
- [ ] ✅ Deve voltar para login

### 3. Teste de Listagem de Pratos
- [ ] Fazer login
- [ ] Verificar lista de pratos
- [ ] ✅ Deve mostrar ~45 pratos

### 4. Teste de Filtro "Sem Lactose"
- [ ] Clicar "Sem Lactose"
- [ ] ✅ Deve mostrar apenas pratos com `Tem_Lactose = "Não"`
- [ ] ✅ Contagem: ~30 pratos

### 5. Teste de Filtro "Com Lactose"
- [ ] Clicar "Todos os Pratos"
- [ ] ✅ Deve mostrar todos (~45 pratos)

### 6. Teste de Chat com IA
- [ ] Clicar no botão de chat/assistente
- [ ] Perguntar: "Quais pratos sem lactose?"
- [ ] ✅ IA deve listar pratos sem lactose
- [ ] Perguntar: "O que é lactose?"
- [ ] ✅ IA deve explicar conceito

### 7. Teste de Logout
- [ ] Sair do app
- [ ] Reabrir app
- [ ] ✅ Deve voltar para tela de login

---

## 🐛 TROUBLESHOOTING (RESOLUÇÃO DE PROBLEMAS)

### ❌ Problema: API não inicia - "Modelo não encontrado"
**Erro**:
```
❌ Modelo não encontrado: C:\...\ModelosIA\Phi-3-mini-4k-instruct-q4.gguf
```

**Solução**:
1. Verificar se arquivo existe: `Cardapio_Inteligente.Api/ModelosIA/Phi-3-mini-4k-instruct-q4.gguf`
2. Verificar tamanho: ~2.3 GB
3. Baixar novamente se necessário

---

### ❌ Problema: API não conecta ao MySQL
**Erro**:
```
Unable to connect to any of the specified MySQL hosts
```

**Solução**:
1. Verificar se MySQL está rodando:
   ```bash
   # Windows
   services.msc → MySQL → Status: "Em execução"
   
   # Linux
   sudo service mysql status
   ```
2. Testar conexão manual:
   ```bash
   mysql -u root -p
   ```
3. Verificar senha em `appsettings.json`

---

### ❌ Problema: App não conecta à API (Android)
**Erro no app**: "Não foi possível conectar ao servidor"

**Solução para Emulador**:
1. Verificar se API está rodando: http://localhost:5068/swagger
2. Verificar logs do app: deve mostrar tentativas de conexão
3. Usar `http://10.0.2.2:5068` (já configurado)

**Solução para Dispositivo Físico**:
1. Verificar se PC e celular estão na mesma rede Wi-Fi
2. Descobrir IP do PC: `ipconfig` (Windows) ou `ifconfig` (Linux)
3. Atualizar `ApiService.cs` com IP correto
4. Liberar porta 5068 no Firewall
5. Testar no navegador do celular: `http://192.168.1.XXX:5068/swagger`

---

### ❌ Problema: IA demora muito ou não responde
**Sintoma**: Chat fica em "Digitando..." por mais de 1 minuto

**Solução**:
1. **Hardware insuficiente**: Modelo precisa de 8GB RAM
   - Fechar outros programas
   - Verificar uso de RAM no Task Manager
2. **Primeiro carregamento é lento**: 20-40 segundos é normal
3. **Timeout**: Aumentar em `ApiService.cs` linha 23:
   ```csharp
   Timeout = TimeSpan.FromSeconds(120) // 2 minutos
   ```

---

### ❌ Problema: Banco vazio (nenhum prato)
**Sintoma**: Lista de pratos vazia ou mensagem "Cardápio vazio"

**Solução**:
1. Executar script SQL novamente:
   ```bash
   mysql -u root -p cardapio_db < database_seed.sql
   ```
2. Verificar no MySQL Workbench:
   ```sql
   SELECT COUNT(*) FROM pratos;
   ```
   Deve retornar ~45

---

### ❌ Problema: Erro de compilação no MAUI
**Erro**: "Workload 'maui' not installed"

**Solução**:
```bash
# Instalar workload MAUI
dotnet workload install maui

# Ou via Visual Studio Installer:
# Selecionar "Desenvolvimento para dispositivos móveis com .NET"
```

---

### ❌ Problema: Erro HTTP 401 (Unauthorized)
**Sintoma**: "Token inválido" ou "Não autorizado"

**Solução**:
1. Fazer logout e login novamente
2. Verificar se JWT está sendo salvo:
   - No código: `Preferences.Get("jwt_token")`
3. Token expira em 2 horas (configurável em `appsettings.json`)

---

### ❌ Problema: Android não permite HTTP (cleartext)
**Erro**: "Cleartext HTTP traffic not permitted"

**Solução**: Já configurado no projeto!
- `AndroidManifest.xml`: `usesCleartextTraffic="true"`
- `network_security_config.xml`: permite HTTP para localhost e 192.168.x.x

Se persistir:
1. Verificar se arquivos existem em `Platforms/Android/Resources/xml/`
2. Rebuild completo do projeto

---

## 📊 CHECKLIST DE VALIDAÇÃO FINAL

Antes de entregar o TCC, verificar:

### Backend (API)
- [ ] API inicia sem erros
- [ ] Swagger acessível em http://localhost:5068/swagger
- [ ] Modelo Phi-3 carrega com sucesso
- [ ] Banco de dados tem ~45 pratos e 5 usuários
- [ ] Endpoint `/api/Usuarios/Login` funciona
- [ ] Endpoint `/api/Pratos` retorna lista
- [ ] Endpoint `/api/Pratos/assistente-chat` responde perguntas

### Frontend (MAUI)
- [ ] App compila sem erros
- [ ] Login funciona em todas as plataformas
- [ ] Cadastro funciona
- [ ] Lista de pratos carrega
- [ ] Filtros "Com/Sem Lactose" funcionam
- [ ] Chat com IA responde corretamente
- [ ] Navegação entre telas sem erros

### Integração
- [ ] App Android (emulador) conecta à API
- [ ] App Android (físico) conecta à API
- [ ] App Windows conecta à API
- [ ] Token JWT persiste entre sessões
- [ ] Logout e re-login funcionam

### Performance
- [ ] API inicia em < 30 segundos
- [ ] Primeira resposta da IA em < 40 segundos
- [ ] Respostas subsequentes em < 10 segundos
- [ ] Lista de pratos carrega em < 3 segundos

---

## 🎓 DICAS PARA A DEFESA DO TCC

### 1. Demonstração Prática
**Preparar**:
- API rodando
- App em emulador Android
- App em Windows lado a lado
- Banco com dados de teste

**Roteiro de Demo (5 minutos)**:
1. Mostrar tela de login → fazer login
2. Mostrar cardápio completo (~45 pratos)
3. Aplicar filtro "Sem Lactose" → mostrar redução
4. Abrir chat → perguntar "Quais pratos sem lactose?"
5. Mostrar resposta da IA listando pratos
6. Perguntar "O que é lactose?" → IA explica conceito
7. **Destacar**: IA local (privacidade), multiplataforma

### 2. Pontos Fortes a Destacar
- ✅ **IA Local** (não depende de cloud, privacidade)
- ✅ **Multiplataforma** (Android, iOS, Windows - um código)
- ✅ **Arquitetura profissional** (API REST separada)
- ✅ **Autenticação segura** (JWT)
- ✅ **Personalização** (filtros por restrições alimentares)

### 3. Perguntas Prováveis da Banca

**P: Por que usar IA local ao invés de OpenAI/Claude?**  
R: Privacidade, custo zero, funciona offline, dados sensíveis não saem do dispositivo.

**P: Como a IA recomenda pratos?**  
R: Busca pratos no MySQL, monta prompt contextualizado com lista, LLM processa e filtra.

**P: Por que senhas sem hash?**  
R: Simplificação para escopo acadêmico. Em produção, usaria BCrypt/Argon2.

**P: Qual a diferença para apps existentes?**  
R: IA especializada em lactose, local, explicações educativas além de recomendações.

**P: Como funciona em dispositivo físico?**  
R: API roda na máquina, app conecta via Wi-Fi no IP local da rede.

---

## 📞 CONTATOS E RECURSOS

### Documentação Oficial
- .NET MAUI: https://learn.microsoft.com/dotnet/maui/
- Entity Framework: https://learn.microsoft.com/ef/core/
- LLamaSharp: https://github.com/SciSharp/LLamaSharp
- Phi-3: https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf

### Ferramentas Úteis
- Postman (testar API): https://www.postman.com/
- DB Browser (visualizar SQLite): https://sqlitebrowser.org/
- Android Studio (emuladores): https://developer.android.com/studio

---

## ✅ CONCLUSÃO

Seguindo este guia, você conseguirá:
- ✅ Executar a API com IA funcionando
- ✅ Rodar o app em Windows Desktop
- ✅ Rodar o app em emulador Android
- ✅ Rodar o app em dispositivo físico Android
- ✅ Testar todas as funcionalidades
- ✅ Preparar demo para defesa

**Boa sorte na defesa do TCC! 🎓🚀**

---

**Última atualização**: 19 de novembro de 2025  
**Versão**: 1.0
