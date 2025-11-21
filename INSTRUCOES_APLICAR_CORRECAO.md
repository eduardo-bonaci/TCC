# 🚀 Instruções para Aplicar as Correções

## 📝 Resumo da Correção

Foi **removido o campo de texto livre** "Digite sua resposta aqui" e mantido **apenas os checkboxes** para seleção de ingredientes.

## 📁 Arquivos Modificados

### 1. **Cardapio_Inteligente/servicos/AssistenteConversacional.cs**
- ❌ Removida pergunta sobre ingredientes
- ❌ Removida pergunta sobre alergias
- ✅ Mantidas apenas perguntas de identificação (nome, email, senha, telefone)

### 2. **Cardapio_Inteligente/Paginas/Tela_Cadastro.xaml**
- ❌ Removido Frame com perguntas conversacionais
- ❌ Removido Entry de texto livre
- ❌ Removidos botões "Iniciar" e "Enviar"
- ❌ Removida seção "Dados Coletados"
- ✅ Adicionados campos Entry diretos (Nome, Email, Senha, Telefone)
- ✅ Mantidos checkboxes de ingredientes (carregados dinamicamente)
- ✅ Mantidos radio buttons de lactose

### 3. **Cardapio_Inteligente/Paginas/Tela_Cadastro.xaml.cs**
- ❌ Removida dependência do AssistenteConversacional
- ❌ Removidos Dictionary de respostas e ObservableCollection de resumo
- ❌ Removidos métodos btnIniciar_Clicked, btnEnviar_Clicked, ObterResumo
- ✅ Simplificado o constructor
- ✅ Atualizado btnSalvar_Clicked para usar campos Entry diretos
- ✅ Adicionada validação de campos obrigatórios

### 4. **database_seed_updated.sql** (NOVO)
- ✅ Script SQL com dados do arquivo pratos.csv fornecido
- ✅ Ingredientes em formato JSON correto

### 5. **CORRECAO_INGREDIENTES.md** (NOVO)
- ✅ Documentação completa das alterações

## 🔧 Como Aplicar as Correções no Seu Repositório

### Opção 1: Aplicar manualmente os arquivos modificados

1. **Copie os arquivos modificados do diretório `/home/user/TCC/` para o seu projeto:**
   ```bash
   # Copiar arquivos modificados
   cp /home/user/TCC/Cardapio_Inteligente/servicos/AssistenteConversacional.cs SEU_PROJETO/Cardapio_Inteligente/servicos/
   cp /home/user/TCC/Cardapio_Inteligente/Paginas/Tela_Cadastro.xaml SEU_PROJETO/Cardapio_Inteligente/Paginas/
   cp /home/user/TCC/Cardapio_Inteligente/Paginas/Tela_Cadastro.xaml.cs SEU_PROJETO/Cardapio_Inteligente/Paginas/
   
   # Copiar novos arquivos
   cp /home/user/TCC/database_seed_updated.sql SEU_PROJETO/
   cp /home/user/TCC/CORRECAO_INGREDIENTES.md SEU_PROJETO/
   ```

2. **Atualizar o banco de dados:**
   ```bash
   mysql -u seu_usuario -p cardapio_db < database_seed_updated.sql
   ```

3. **Recompilar o projeto:**
   ```bash
   cd SEU_PROJETO
   dotnet clean
   dotnet build
   ```

### Opção 2: Aplicar via Git

1. **Adicionar as alterações:**
   ```bash
   cd /home/user/TCC
   git add .
   git commit -m "fix: Removido campo de texto livre e mantido apenas checkboxes para ingredientes"
   ```

2. **Push para o repositório:**
   ```bash
   git push origin main
   ```

3. **No seu ambiente local, puxar as alterações:**
   ```bash
   git pull origin main
   ```

## 🧪 Testar as Correções

1. **Iniciar a API:**
   ```bash
   cd Cardapio_Inteligente.Api
   dotnet run
   ```

2. **Iniciar o App:**
   ```bash
   cd Cardapio_Inteligente
   dotnet run
   ```

3. **Verificar na tela de cadastro:**
   - ✅ Campos Nome, Email, Senha e Telefone aparecem diretamente
   - ✅ Checkboxes de ingredientes são carregados do banco
   - ✅ Radio buttons de lactose funcionam
   - ❌ **NÃO DEVE APARECER:** Campo "Digite sua resposta aqui"
   - ❌ **NÃO DEVE APARECER:** Botões "Iniciar" e "Enviar"

## 📊 Estrutura do Banco de Dados

### Tabela `pratos`
```sql
Ingredientes: TEXT (formato JSON)
Exemplo: '["Tomates", "Manjericão", "Alho", "Azeite de Oliva"]'
```

### Ingredientes Disponíveis (do CSV fornecido)
- Tomates
- Manjericão
- Alho
- Azeite de Oliva
- Chocolate
- Manteiga
- Açúcar
- Ovos
- Frango
- Fettuccine
- Molho Alfredo
- Parmesão

## ✅ Checklist de Verificação

- [ ] Arquivos copiados para o projeto
- [ ] Banco de dados atualizado com `database_seed_updated.sql`
- [ ] Projeto recompilado sem erros
- [ ] API iniciada e funcionando
- [ ] App iniciado e funcionando
- [ ] Tela de cadastro mostra campos diretos
- [ ] Checkboxes de ingredientes aparecem
- [ ] Campo de texto livre **NÃO** aparece
- [ ] Botões "Iniciar" e "Enviar" **NÃO** aparecem
- [ ] Cadastro completa com sucesso

## 🆘 Problemas Comuns

### Erro: "lblPergunta não existe"
**Solução:** O campo foi renomeado, certifique-se de copiar o arquivo `.xaml` completo

### Erro: "txtResposta não existe"
**Solução:** O campo foi removido, certifique-se de copiar o arquivo `.xaml.cs` completo

### Checkboxes não aparecem
**Solução:** 
1. Verifique se a API está rodando
2. Verifique se o banco tem pratos cadastrados
3. Execute o script `database_seed_updated.sql`

## 📞 Suporte

Se encontrar problemas, verifique:
1. **CORRECAO_INGREDIENTES.md** - Documentação completa
2. Logs da API
3. Logs do aplicativo

---

**Status:** ✅ Pronto para uso  
**Data:** 21/11/2025
