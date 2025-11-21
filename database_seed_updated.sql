-- ============================================================
-- SCRIPT SQL: CARDÁPIO INTELIGENTE - DADOS ATUALIZADOS
-- ============================================================
-- Database: cardapio_db
-- Tabelas: usuarios, pratos
-- Atualizado com dados do arquivo pratos.csv
-- ============================================================

USE cardapio_db;

-- ============================================================
-- LIMPAR DADOS EXISTENTES (CUIDADO EM PRODUÇÃO!)
-- ============================================================
SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE pratos;
TRUNCATE TABLE usuarios;
SET FOREIGN_KEY_CHECKS = 1;

-- ============================================================
-- INSERIR USUÁRIOS DE TESTE
-- ============================================================
INSERT INTO usuarios (Nome, Email, Senha, Telefone, IngredientesNaoGosta, Alergias, DataCadastro) 
VALUES
('João Silva', 'joao@gmail.com', '123456', '(11) 98765-4321', 'Cebola, Pimentão', 'Lactose', '2024-11-15 10:00:00'),
('Maria Santos', 'maria@hotmail.com', '123456', '(21) 99876-5432', 'Alho', 'Nenhuma', '2024-11-15 11:00:00'),
('Pedro Oliveira', 'pedro@outlook.com', '123456', '(31) 91234-5678', 'Coentro', 'Lactose', '2024-11-15 12:00:00'),
('Ana Costa', 'ana@gmail.com', '123456', '(41) 92345-6789', '', 'Lactose', '2024-11-15 13:00:00'),
('Carlos Eduardo', 'carlos@gmail.com', '123456', '(51) 93456-7890', 'Pimenta', 'Nenhuma', '2024-11-15 14:00:00');

-- ============================================================
-- INSERIR PRATOS - DADOS DO CSV pratos.csv
-- ============================================================
-- Nota: A coluna Ingredientes está armazenada como JSON array
-- Formato: ["Ingrediente1", "Ingrediente2", "Ingrediente3"]
-- ============================================================

INSERT INTO pratos (id, Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose) 
VALUES
(1, 'Bebidas', 'Refrigerante', '["confidencial"]', 2.55, 'Desconhecido'),
(2, 'Entradas', 'Dip de Espinafre e Alcachofra', '["Tomates", "Manjericão", "Alho", "Azeite de Oliva"]', 11.12, 'Não'),
(3, 'Sobremesas', 'Cheesecake de Nova York', '["Chocolate", "Manteiga", "Açúcar", "Ovos"]', 18.66, 'Sim'),
(4, 'Prato Principal', 'Frango Alfredo', '["Frango", "Fettuccine", "Molho Alfredo", "Parmesão"]', 29.55, 'Sim'),
(5, 'Prato Principal', 'Bife Grelhado', '["Frango", "Fettuccine", "Molho Alfredo", "Parmesão"]', 17.73, 'Sim'),
(6, 'Entradas', 'Cogumelos Recheados', '["Tomates", "Manjericão", "Alho", "Azeite de Oliva"]', 12.28, 'Não'),
(7, 'Sobremesas', 'Tiramisu', '["Chocolate", "Manteiga", "Açúcar", "Ovos"]', 10.47, 'Desconhecido'),
(8, 'Bebidas', 'Limonada', '["confidencial"]', 4.95, 'Sim'),
(9, 'Sobremesas', 'Bolo Vulcão de Chocolate', '["Chocolate", "Manteiga", "Açúcar", "Ovos"]', 16.08, 'Sim'),
(10, 'Bebidas', 'Iced Tea', '["confidencial"]', 4.43, 'Desconhecido'),
(11, 'Bebidas', 'Coffee', '["confidencial"]', 2.94, 'Sim'),
(12, 'Entradas', 'Bruschetta', '["Tomates", "Manjericão", "Alho", "Azeite de Oliva"]', 13.62, 'Não'),
(13, 'Prato Principal', 'Refogado de Vegetais', '["Frango", "Fettuccine", "Molho Alfredo", "Parmesão"]', 27.06, 'Desconhecido'),
(14, 'Prato Principal', 'Shrimp Scampi', '["Frango", "Fettuccine", "Molho Alfredo", "Parmesão"]', 19.87, 'Sim'),
(15, 'Sobremesas', 'Fruit Tart', '["Chocolate", "Manteiga", "Açúcar", "Ovos"]', 12.38, 'Desconhecido'),
(16, 'Entradas', 'Caprese Salad', '["Tomates", "Manjericão", "Alho", "Azeite de Oliva"]', 14.47, 'Sim');

-- ============================================================
-- VERIFICAR DADOS INSERIDOS
-- ============================================================
SELECT 'USUÁRIOS CADASTRADOS:' AS Info;
SELECT Id, Nome, Email, Alergias FROM usuarios;

SELECT 'TODOS OS PRATOS:' AS Info;
SELECT Id, Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose FROM pratos ORDER BY Categoria;

SELECT 'PRATOS SEM LACTOSE:' AS Info;
SELECT Id, Categoria, Item_Menu, Preco FROM pratos WHERE Tem_Lactose = 'Não' ORDER BY Categoria;

SELECT 'PRATOS COM LACTOSE:' AS Info;
SELECT Id, Categoria, Item_Menu, Preco FROM pratos WHERE Tem_Lactose = 'Sim' ORDER BY Categoria;

SELECT 'TOTAL DE PRATOS POR STATUS DE LACTOSE:' AS Info;
SELECT Tem_Lactose, COUNT(*) AS Total FROM pratos GROUP BY Tem_Lactose;

-- ============================================================
-- INGREDIENTES ÚNICOS DISPONÍVEIS
-- ============================================================
SELECT 'INGREDIENTES ÚNICOS DISPONÍVEIS (EXTRAÍDOS DOS PRATOS):' AS Info;
-- Nota: Esta query extrairá os ingredientes do formato JSON
-- Os ingredientes disponíveis são:
-- Tomates, Manjericão, Alho, Azeite de Oliva, Chocolate, Manteiga, 
-- Açúcar, Ovos, Frango, Fettuccine, Molho Alfredo, Parmesão

-- ============================================================
-- FIM DO SCRIPT
-- ============================================================
