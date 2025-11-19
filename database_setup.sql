-- ============================================================================
-- SCRIPT DE CONFIGURAÇÃO DO BANCO DE DADOS - CARDÁPIO INTELIGENTE
-- ============================================================================
-- Este script cria o banco de dados, tabelas e popula com dados de exemplo
-- Execute este script no MySQL Workbench ou via linha de comando
-- ============================================================================

-- Criar banco de dados
CREATE DATABASE IF NOT EXISTS cardapio_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE cardapio_db;

-- ============================================================================
-- TABELA: usuarios
-- ============================================================================
CREATE TABLE IF NOT EXISTS usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Senha VARCHAR(255) NOT NULL,
    Telefone VARCHAR(20),
    IngredientesNaoGosta TEXT,
    Alergias VARCHAR(255),
    DataCadastro DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_email (Email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================================
-- TABELA: pratos
-- ============================================================================
CREATE TABLE IF NOT EXISTS pratos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Categoria VARCHAR(100) NOT NULL,
    Item_Menu VARCHAR(255) NOT NULL,
    Ingredientes TEXT,
    Preco DECIMAL(18,2) NOT NULL,
    Tem_Lactose VARCHAR(20) DEFAULT 'Desconhecido',
    INDEX idx_categoria (Categoria),
    INDEX idx_lactose (Tem_Lactose)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================================
-- POPULAR TABELA DE PRATOS COM DADOS DE EXEMPLO
-- ============================================================================

-- Limpar dados existentes (opcional - remova se quiser manter dados)
TRUNCATE TABLE pratos;

-- ENTRADAS
INSERT INTO pratos (Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose) VALUES
('Entrada', 'Salada Caesar', 'Alface, Croutons, Molho Caesar, Parmesão', 18.90, 'Sim'),
('Entrada', 'Salada Tropical', 'Alface, Manga, Abacaxi, Molho de Maracujá', 16.50, 'Não'),
('Entrada', 'Bruschetta', 'Pão Italiano, Tomate, Manjericão, Azeite', 14.90, 'Não'),
('Entrada', 'Carpaccio de Salmão', 'Salmão, Alcaparras, Limão, Azeite', 24.90, 'Não'),
('Entrada', 'Croquete de Queijo', 'Queijo, Farinha, Ovos, Temperos', 12.90, 'Sim');

-- PRATOS PRINCIPAIS
INSERT INTO pratos (Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose) VALUES
('Prato Principal', 'Frango Grelhado', 'Frango, Alho, Limão, Ervas', 32.90, 'Não'),
('Prato Principal', 'Picanha na Chapa', 'Picanha, Sal Grosso, Alho', 48.90, 'Não'),
('Prato Principal', 'Salmão ao Molho de Maracujá', 'Salmão, Maracujá, Mel, Manteiga', 42.90, 'Sim'),
('Prato Principal', 'Risoto de Cogumelos', 'Arroz Arbóreo, Cogumelos, Parmesão, Vinho Branco', 38.90, 'Sim'),
('Prato Principal', 'Feijoada Completa', 'Feijão Preto, Carnes Suínas, Linguiça, Arroz', 35.90, 'Não'),
('Prato Principal', 'Lasanha Bolonhesa', 'Massa, Carne Moída, Molho de Tomate, Queijo, Bechamel', 34.90, 'Sim'),
('Prato Principal', 'Strogonoff de Frango', 'Frango, Creme de Leite, Champignon, Batata Palha', 36.90, 'Sim'),
('Prato Principal', 'Moqueca de Peixe', 'Peixe, Leite de Coco, Dendê, Pimentão, Tomate', 44.90, 'Não'),
('Prato Principal', 'Filé Mignon ao Molho Madeira', 'Filé Mignon, Molho Madeira, Manteiga', 52.90, 'Sim'),
('Prato Principal', 'Espaguete ao Alho e Óleo', 'Macarrão, Alho, Azeite, Pimenta', 28.90, 'Não');

-- MASSAS
INSERT INTO pratos (Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose) VALUES
('Massa', 'Penne ao Pesto', 'Penne, Manjericão, Parmesão, Pinhão, Azeite', 32.90, 'Sim'),
('Massa', 'Espaguete Carbonara', 'Espaguete, Bacon, Ovos, Parmesão, Pimenta', 34.90, 'Sim'),
('Massa', 'Ravioli de Ricota', 'Ravioli, Ricota, Espinafre, Molho de Tomate', 36.90, 'Sim'),
('Massa', 'Nhoque ao Sugo', 'Nhoque, Molho de Tomate, Manjericão', 29.90, 'Não');

-- SOBREMESAS
INSERT INTO pratos (Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose) VALUES
('Sobremesa', 'Pudim de Leite', 'Leite Condensado, Leite, Ovos, Açúcar', 12.90, 'Sim'),
('Sobremesa', 'Mousse de Chocolate', 'Chocolate, Creme de Leite, Ovos', 14.90, 'Sim'),
('Sobremesa', 'Salada de Frutas', 'Frutas Variadas, Mel', 10.90, 'Não'),
('Sobremesa', 'Sorvete de Coco', 'Leite de Coco, Açúcar, Coco Ralado', 11.90, 'Não'),
('Sobremesa', 'Petit Gateau', 'Chocolate, Farinha, Ovos, Manteiga, Sorvete', 18.90, 'Sim'),
('Sobremesa', 'Brownie', 'Chocolate, Farinha, Ovos, Manteiga, Nozes', 15.90, 'Sim'),
('Sobremesa', 'Açaí na Tigela', 'Açaí, Banana, Granola, Mel', 16.90, 'Não');

-- BEBIDAS
INSERT INTO pratos (Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose) VALUES
('Bebida', 'Suco Natural de Laranja', 'Laranja', 8.90, 'Não'),
('Bebida', 'Limonada Suíça', 'Limão, Leite Condensado, Água', 9.90, 'Sim'),
('Bebida', 'Refrigerante Lata', 'Refrigerante', 5.90, 'Não'),
('Bebida', 'Água Mineral', 'Água', 4.50, 'Não'),
('Bebida', 'Milk Shake de Morango', 'Leite, Sorvete, Morango', 14.90, 'Sim'),
('Bebida', 'Smoothie de Frutas Vermelhas', 'Morango, Framboesa, Banana, Iogurte', 12.90, 'Sim'),
('Bebida', 'Chá Gelado', 'Chá, Limão, Hortelã', 7.90, 'Não');

-- ============================================================================
-- USUÁRIO DE TESTE (OPCIONAL)
-- ============================================================================
-- Senha: teste123
INSERT INTO usuarios (Nome, Email, Senha, Telefone, IngredientesNaoGosta, Alergias, DataCadastro) VALUES
('Usuário Teste', 'teste@gmail.com', 'teste123', '(11) 98765-4321', 'Cebola, Pimentão', 'Lactose', NOW());

-- ============================================================================
-- VERIFICAÇÃO
-- ============================================================================
SELECT 'Banco de dados configurado com sucesso!' AS Status;
SELECT COUNT(*) AS Total_Pratos FROM pratos;
SELECT COUNT(*) AS Total_Usuarios FROM usuarios;

-- Exibir pratos sem lactose
SELECT Categoria, Item_Menu, Preco, Tem_Lactose 
FROM pratos 
WHERE Tem_Lactose = 'Não' 
ORDER BY Categoria, Item_Menu;
