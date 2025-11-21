-- ============================================================
-- SCRIPT SQL: CARDÁPIO INTELIGENTE - DADOS DE TESTE
-- ============================================================
-- Database: cardapio_db
-- Tabelas: usuarios, pratos
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
-- INSERIR PRATOS - ENTRADAS (COM E SEM LACTOSE)
-- ============================================================
INSERT INTO pratos (Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose) 
VALUES
-- ENTRADAS SEM LACTOSE
('Entrada', 'Salada Caesar Sem Lactose', 'Alface romana, croutons, molho caesar vegano, parmesão vegetal', 18.90, 'Não'),
('Entrada', 'Bruschetta de Tomate', 'Pão italiano, tomate fresco, manjericão, azeite, alho', 15.50, 'Não'),
('Entrada', 'Carpaccio de Salmão', 'Salmão fresco, azeite, limão, alcaparras', 28.90, 'Não'),
('Entrada', 'Chips de Batata Doce', 'Batata doce, sal marinho, páprica', 12.00, 'Não'),
('Entrada', 'Guacamole com Nachos', 'Abacate, tomate, cebola, limão, tortilhas de milho', 19.90, 'Não'),

-- ENTRADAS COM LACTOSE
('Entrada', 'Queijo Brie Assado', 'Queijo brie, mel, nozes, torradas', 24.90, 'Sim'),
('Entrada', 'Tábua de Queijos', 'Queijo brie, queijo gorgonzola, queijo gouda, torradas', 32.90, 'Sim'),
('Entrada', 'Creme de Queijo com Ervas', 'Cream cheese, ervas finas, pão italiano', 16.90, 'Sim');

-- ============================================================
-- INSERIR PRATOS - PRATOS PRINCIPAIS (COM E SEM LACTOSE)
-- ============================================================
INSERT INTO pratos (Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose) 
VALUES
-- PRATOS PRINCIPAIS SEM LACTOSE
('Prato Principal', 'Filé Mignon Grelhado', 'Filé mignon, batata rústica, legumes grelhados', 48.90, 'Não'),
('Prato Principal', 'Salmão Grelhado', 'Salmão fresco, arroz integral, aspargos', 52.90, 'Não'),
('Prato Principal', 'Frango Teriyaki', 'Peito de frango, molho teriyaki, arroz branco, brócolis', 38.90, 'Não'),
('Prato Principal', 'Picanha na Chapa', 'Picanha, arroz, feijão tropeiro, farofa', 55.90, 'Não'),
('Prato Principal', 'Risoto de Cogumelos Vegano', 'Arroz arbóreo, cogumelos variados, leite de coco', 42.90, 'Não'),
('Prato Principal', 'Espaguete ao Pesto', 'Macarrão, manjericão, azeite, alho, castanhas', 35.90, 'Não'),
('Prato Principal', 'Strogonoff de Frango Sem Lactose', 'Frango, molho de tomate, creme vegetal, arroz, batata palha', 39.90, 'Não'),
('Prato Principal', 'Poke Bowl', 'Salmão ou atum, arroz, edamame, abacate, gergelim', 44.90, 'Não'),
('Prato Principal', 'Churrasco Misto', 'Picanha, linguiça, frango, arroz, vinagrete', 58.90, 'Não'),
('Prato Principal', 'Camarão na Moranga', 'Camarão, moranga, arroz branco, farofa', 62.90, 'Não'),

-- PRATOS PRINCIPAIS COM LACTOSE
('Prato Principal', 'Lasanha à Bolonhesa', 'Massa, carne moída, molho bolonhesa, queijo muçarela, parmesão', 42.90, 'Sim'),
('Prato Principal', 'Pizza Quatro Queijos', 'Muçarela, gorgonzola, parmesão, catupiry', 45.90, 'Sim'),
('Prato Principal', 'Strogonoff de Carne Tradicional', 'Carne, creme de leite, champignon, arroz, batata palha', 46.90, 'Sim'),
('Prato Principal', 'Risoto de Queijo Brie', 'Arroz arbóreo, queijo brie, vinho branco', 48.90, 'Sim'),
('Prato Principal', 'Filé ao Molho Gorgonzola', 'Filé mignon, molho gorgonzola, batata gratinada', 54.90, 'Sim');

-- ============================================================
-- INSERIR PRATOS - SOBREMESAS (COM E SEM LACTOSE)
-- ============================================================
INSERT INTO pratos (Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose) 
VALUES
-- SOBREMESAS SEM LACTOSE
('Sobremesa', 'Sorvete de Coco Vegano', 'Leite de coco, açúcar, essência de baunilha', 12.90, 'Não'),
('Sobremesa', 'Salada de Frutas', 'Frutas da estação, suco de laranja, hortelã', 14.90, 'Não'),
('Sobremesa', 'Brownie Vegano', 'Chocolate amargo, farinha, açúcar, óleo de coco', 16.90, 'Não'),
('Sobremesa', 'Mousse de Chocolate Vegano', 'Chocolate amargo, leite de coco, ágar-ágar', 15.90, 'Não'),
('Sobremesa', 'Açaí na Tigela', 'Açaí, banana, granola sem leite, frutas', 18.90, 'Não'),

-- SOBREMESAS COM LACTOSE
('Sobremesa', 'Pudim de Leite Condensado', 'Leite condensado, leite, ovos, caramelo', 13.90, 'Sim'),
('Sobremesa', 'Cheesecake de Frutas Vermelhas', 'Cream cheese, frutas vermelhas, biscoito', 19.90, 'Sim'),
('Sobremesa', 'Petit Gateau', 'Chocolate, manteiga, sorvete de creme', 22.90, 'Sim'),
('Sobremesa', 'Tiramisu', 'Mascarpone, café, biscoito champagne, cacau', 18.90, 'Sim');

-- ============================================================
-- INSERIR PRATOS - BEBIDAS (MAIORIA SEM LACTOSE)
-- ============================================================
INSERT INTO pratos (Categoria, Item_Menu, Ingredientes, Preco, Tem_Lactose) 
VALUES
-- BEBIDAS SEM LACTOSE
('Bebida', 'Suco Natural de Laranja', 'Laranja fresca', 8.90, 'Não'),
('Bebida', 'Suco Verde Detox', 'Couve, limão, gengibre, maçã', 12.90, 'Não'),
('Bebida', 'Refrigerante Lata', 'Coca-Cola, Guaraná, Sprite', 5.90, 'Não'),
('Bebida', 'Água Mineral 500ml', 'Água mineral natural', 4.50, 'Não'),
('Bebida', 'Chá Gelado de Limão', 'Chá preto, limão, açúcar', 7.90, 'Não'),
('Bebida', 'Café Expresso', 'Café arábica 100%', 6.90, 'Não'),
('Bebida', 'Caipirinha de Limão', 'Cachaça, limão, açúcar, gelo', 15.90, 'Não'),

-- BEBIDAS COM LACTOSE
('Bebida', 'Milkshake de Chocolate', 'Sorvete de chocolate, leite, calda', 16.90, 'Sim'),
('Bebida', 'Cappuccino', 'Café expresso, leite vaporizado, espuma', 9.90, 'Sim'),
('Bebida', 'Vitamina de Frutas', 'Leite, banana, morango, mel', 12.90, 'Sim');

-- ============================================================
-- VERIFICAR DADOS INSERIDOS
-- ============================================================
SELECT 'USUÁRIOS CADASTRADOS:' AS Info;
SELECT Id, Nome, Email, Alergias FROM usuarios;

SELECT 'PRATOS SEM LACTOSE:' AS Info;
SELECT Id, Categoria, Item_Menu, Preco FROM pratos WHERE Tem_Lactose = 'Não' ORDER BY Categoria;

SELECT 'PRATOS COM LACTOSE:' AS Info;
SELECT Id, Categoria, Item_Menu, Preco FROM pratos WHERE Tem_Lactose = 'Sim' ORDER BY Categoria;

SELECT 'TOTAL DE PRATOS:' AS Info;
SELECT Tem_Lactose, COUNT(*) AS Total FROM pratos GROUP BY Tem_Lactose;

-- ============================================================
-- FIM DO SCRIPT
-- ============================================================
