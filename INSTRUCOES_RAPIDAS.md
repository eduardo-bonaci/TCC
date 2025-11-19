# ⚡ INSTRUÇÕES RÁPIDAS - CARDÁPIO INTELIGENTE

## 🚨 URGENTE - ENTREGA EM 20/11/2024

### ✅ CHECKLIST PRÉ-EXECUÇÃO (FAÇA NESTA ORDEM!)

#### **1. BANCO DE DADOS (15 minutos)**

```bash
# 1.1. Iniciar MySQL
sudo systemctl start mysql  # Linux
# ou
net start MySQL80  # Windows

# 1.2. Executar script de criação
mysql -u root -p < database_setup.sql

# 1.3. Verificar se criou
mysql -u root -p
USE cardapio_db;
SHOW TABLES;
SELECT COUNT(*) FROM pratos;  # Deve retornar 30+
EXIT;
```

#### **2. MODELO DE IA (30 minutos - DOWNLOAD GRANDE!)**

```bash
# 2.1. Baixar modelo (2.4 GB)
# Acesse: https://huggingface.co/microsoft/Phi-3-mini-4k-instruct-gguf
# Baixe: Phi-3-mini-4k-instruct-q4.gguf

# 2.2. Criar pasta
mkdir -p Cardapio_Inteligente.Api/ModelosIA

# 2.3. Mover arquivo
mv ~/Downloads/Phi-3-mini-4k-instruct-q4.gguf Cardapio_Inteligente.Api/ModelosIA/

# 2.4. Verificar
ls -lh Cardapio_Inteligente.Api/ModelosIA/
# Deve mostrar arquivo de ~2.4 GB
```

#### **3. CONFIGURAR API (5 minutos)**

```bash
# 3.1. Editar appsettings.json
nano Cardapio_Inteligente.Api/appsettings.json

# 3.2. Alterar senha do MySQL (linha 11)
"DefaultConnection": "Server=localhost;Port=3306;Database=cardapio_db;Uid=root;Pwd=SUA_SENHA;"

# 3.3. Salvar (Ctrl+O, Enter, Ctrl+X)
```

#### **4. RESTAURAR DEPENDÊNCIAS (10 minutos)**

```bash
# 4.1. API
cd Cardapio_Inteligente.Api
dotnet restore
dotnet build

# 4.2. MAUI
cd ../Cardapio_Inteligente
dotnet restore
dotnet build -f net9.0-windows10.0.19041.0
```

---

## ▶️ EXECUTAR O PROJETO

### **PASSO 1: Iniciar API**

```bash
cd Cardapio_Inteligente.Api
dotnet run
```

**✅ Logs esperados:**
```
🗄️ Banco de dados verificado/criado com sucesso.
🤖 Serviço de IA inicializado com sucesso.
🚀 API Cardápio Inteligente iniciada com sucesso!
```

**❌ Se der erro:**
- "Modelo não encontrado" → Verifique se o .gguf está na pasta ModelosIA/
- "Connection refused" → Verifique se MySQL está rodando
- "Access denied" → Verifique senha no appsettings.json

### **PASSO 2: Testar API no Swagger**

Abra: `http://localhost:5068/swagger`

1. **Cadastrar usuário**: POST /api/Usuarios/Cadastrar
2. **Fazer login**: POST /api/Usuarios/Login (copie o token)
3. **Authorize**: Clique no cadeado, cole: `Bearer SEU_TOKEN`
4. **Listar pratos**: GET /api/Pratos?alergias=lactose
5. **Testar IA**: POST /api/Pratos/assistente-chat

### **PASSO 3: Executar MAUI (Windows)**

**No Visual Studio:**
1. Abrir solução `Cardapio_Inteligente.sln`
2. Definir `Cardapio_Inteligente` como projeto de inicialização
3. Selecionar **Windows Machine**
4. Pressionar **F5**

**Ou via terminal:**
```bash
cd Cardapio_Inteligente
dotnet run -f net9.0-windows10.0.19041.0
```

### **PASSO 4: Testar Fluxo Completo**

1. **Tela Inicial** → "Já sou cliente"
2. **Login** → `teste@gmail.com` / `teste123`
3. **Cardápio** → Ver lista de pratos
4. **Filtrar** → Clicar "Sem Lactose"
5. **Assistente** → Digitar "Quais pratos sem lactose?" → ➡
6. **Chat** → Clicar "Assistente" (botão superior)

---

## 🐛 PROBLEMAS COMUNS

### **API não inicia**

```bash
# Verificar porta ocupada
netstat -ano | findstr :5068  # Windows
lsof -i :5068  # Linux

# Matar processo
taskkill /PID <PID> /F  # Windows
kill -9 <PID>  # Linux
```

### **MySQL não conecta**

```bash
# Verificar status
sudo systemctl status mysql  # Linux
sc query MySQL80  # Windows

# Reiniciar
sudo systemctl restart mysql  # Linux
net stop MySQL80 && net start MySQL80  # Windows
```

### **MAUI não compila**

```bash
# Limpar e recompilar
dotnet clean
dotnet restore
dotnet build -f net9.0-windows10.0.19041.0
```

### **IA muito lenta**

Edite `appsettings.json`:
```json
"NumThreads": 2,  // Reduzir de 4 para 2
"MaxTokens": 256  // Reduzir de 512 para 256
```

---

## 📱 TESTAR NO ANDROID

### **Opção 1: Emulador**

```bash
# 1. Iniciar emulador no Android Studio
# 2. No Visual Studio, selecionar emulador
# 3. Pressionar F5
```

### **Opção 2: Dispositivo Físico**

```bash
# 1. Ativar "Depuração USB" no celular
# 2. Conectar via USB
# 3. No Visual Studio, selecionar dispositivo
# 4. Pressionar F5
```

**⚠️ IMPORTANTE**: API deve estar em `http://10.0.2.2:5068` (já configurado).

---

## 📊 DADOS DE TESTE

### **Usuário Pré-cadastrado**
- **Email**: `teste@gmail.com`
- **Senha**: `teste123`
- **Alergias**: Lactose

### **Pratos Sem Lactose (Total: 15)**
- Salada Tropical
- Bruschetta
- Carpaccio de Salmão
- Frango Grelhado
- Picanha na Chapa
- Feijoada Completa
- Moqueca de Peixe
- Espaguete ao Alho e Óleo
- Nhoque ao Sugo
- Salada de Frutas
- Sorvete de Coco
- Açaí na Tigela
- Suco Natural de Laranja
- Refrigerante Lata
- Água Mineral
- Chá Gelado

### **Perguntas para Testar IA**
1. "Quais pratos sem lactose você tem?"
2. "O que é lactose?"
3. "Sugira um prato principal sem lactose"
4. "Quais sobremesas não têm leite?"
5. "Recomende uma entrada sem lactose"

---

## 🎯 CHECKLIST FINAL ANTES DA DEFESA

- [ ] MySQL rodando e populado
- [ ] Modelo Phi-3 na pasta correta
- [ ] API iniciando sem erros
- [ ] Swagger acessível
- [ ] Login funcionando
- [ ] Listagem de pratos OK
- [ ] Filtro "Sem Lactose" OK
- [ ] IA respondendo perguntas
- [ ] MAUI rodando no Windows
- [ ] (Opcional) MAUI rodando no Android
- [ ] README.md atualizado
- [ ] Código comentado
- [ ] Apresentação preparada

---

## 📞 COMANDOS ÚTEIS

```bash
# Ver logs da API em tempo real
dotnet run --verbosity detailed

# Verificar versão do .NET
dotnet --version

# Listar SDKs instalados
dotnet --list-sdks

# Verificar conexão MySQL
mysql -u root -p -e "SELECT VERSION();"

# Tamanho do modelo
du -h Cardapio_Inteligente.Api/ModelosIA/*.gguf

# Processos usando porta 5068
netstat -ano | findstr :5068
```

---

## ⏰ CRONOGRAMA SUGERIDO (HOJE - 19/11)

| Horário | Atividade | Duração |
|---------|-----------|---------|
| 14:00 | Configurar MySQL | 15 min |
| 14:15 | Baixar modelo Phi-3 | 30 min |
| 14:45 | Configurar API | 10 min |
| 14:55 | Testar API | 20 min |
| 15:15 | Testar MAUI Windows | 30 min |
| 15:45 | Testar MAUI Android | 30 min |
| 16:15 | Preparar apresentação | 60 min |
| 17:15 | Revisar código | 30 min |
| 17:45 | Ensaiar defesa | 30 min |
| 18:15 | **PRONTO!** | ✅ |

---

## 🚀 BOA SORTE NA DEFESA!

**Lembre-se:**
- Demonstre confiança
- Explique as escolhas técnicas
- Mostre o app funcionando
- Destaque o diferencial da IA local
- Seja honesto sobre limitações
- Mencione melhorias futuras

**Você consegue! 💪**
