BEGIN TRANSACTION;
-----BRAND
INSERT INTO brand (id, name, logo_url) 
VALUES 
('019f784c-9213-7525-a8bb-7c2c8680b0ed', 'sogetec', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-7630-80a0-eac859dfd5c3', '3m', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-70df-80d0-fb6b0c1f76f2', 'abb', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-700d-bf9a-4a51054c841c', 'alvarion', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-77e7-be76-2eabcc8921cf', 'chauvin-arnoux', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-7429-a717-f79dcd3ff03b', 'bosch', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-73f0-8e63-3cd8c9253e00', 'espa', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-7c32-b8d6-4d89021d5e1f', 'legrand', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-71e1-be59-21edb6ef3bc7', 'df electric', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-714c-99d3-2124a189dc5e', 'merlin gerin', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-7e04-8e31-345e81c2b7f7', 'osram', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-7940-9c87-07fc5c9b8a59', 'philips', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-7f65-a5e0-7df5bd4ef215', 'schneider', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-7549-9bc4-15e781550bb7', 'telemecanique', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-70ac-b01d-47b59e7e2390', 'simel', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-7be0-a37a-20b7177bd422', 'semicron onron', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-7336-b79f-4eb3f793260a', 'ino', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-73e8-b359-4dfd9dfcb3de', 'ingelec', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-74f4-92df-e747aa966104', 'ebe', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-7704-9e28-77322efca892', 'fil', 'https://levis.com.br/images/logo.svg'),
('019f784c-9213-78c8-a6b7-d24ff124749a', 'luminaire', 'https://levis.com.br/images/logo.svg');

-----BRAND

-----CATEGORY GROUP
INSERT INTO category_group (id, name, name_fr, image_url, is_active, sort_order) 
VALUES 
('019f784c-9213-7525-a8bb-7c2c8680b0ed', 'Lighting', 'Luminaire', 'https://levis.com.br/images/logo.svg', true, 1);
-----CATEGORY GROUP

-----CATEGORY
INSERT INTO category (id, name, name_fr, slug, parent_id, group_id, description, image_url, is_active, sort_order) 
VALUES 
('019f784c-9213-7525-a8bb-7c2c8680b0ed', 'Traffic Light', 'Lampe de circulation', 'traffic-light', null, '019f784c-9213-7525-a8bb-7c2c8680b0ed', 'description', 'https://levis.com.br/images/logo.svg', true, 1),
('019f784c-9213-7630-80a0-eac859dfd5c3', 'indicator Light', 'Lampe de signalisation', 'indicator-light', null, '019f784c-9213-7525-a8bb-7c2c8680b0ed', 'description', 'https://levis.com.br/images/logo.svg', true, 2);
-----CATEGORY

-----PRODUCT ATTRIBUTE HEADER
INSERT INTO product_attribute_header (id, name, name_fr, sort_order) 
VALUES 
('019f784c-9213-7525-a8bb-7c2c8680b0ed', 'Characteristics & Features', 'Caracteristiques & Fonctions', 1),
('019f784c-9213-7630-80a0-eac859dfd5c3', 'Dimensions & Measurements', 'Dimensions & Mesures', 2),
('019f784c-9213-70df-80d0-fb6b0c1f76f2', 'Certifications & Warranties', 'Certifications & Garanties', 3);
-----PRODUCT ATTRIBUTE HEADER

-----PRODUCT ATTRIBUTE
INSERT INTO product_attribute (id, name, name_fr, header_id, is_variant) 
VALUES 
('019f7850-810b-7ca6-905d-976d73a42916', 'color', 'couleur', '019f784c-9213-7525-a8bb-7c2c8680b0ed', true),
('019f7850-810b-7902-80db-a81ea414ee19', 'type', 'type', '019f784c-9213-7525-a8bb-7c2c8680b0ed', true),
('019f7850-810b-7a04-b211-52a18e5d881b', 'material', 'matière', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-7eab-b1cf-96e7541cdefb', 'compatible with', 'compatible avec', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-7e05-8192-bf643e5d2a5d', 'application sector', 'secteur d''application', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-78f6-8873-5694fa35ed9f', 'number of switches', 'nombre de commutateurs', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-75f0-98be-0b85f7f0ba3a', 'control mechanism', 'mechanisme de controle', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-7de0-b223-0b8988275232', 'output ports', 'ports de sortie', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-737e-b02a-71d9a3c2652a', 'features', 'caracteristiques', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-78e1-b017-97be04ecdc4c', 'power supply', 'alimentation', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-7ee2-b7c5-afee6eef7dfe', 'number of poles', 'nombre de poles', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-70d8-91cd-824c099af0ee', 'number of rows', 'nombre de rangées', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-78ce-af91-c39670dc7fdf', 'number of racks', 'nombre de racks', '019f784c-9213-7525-a8bb-7c2c8680b0ed', false),
('019f7850-810b-7bcb-8841-4f9a3ddb6d86', 'depth', 'profondeur', '019f784c-9213-7630-80a0-eac859dfd5c3', false),
('019f7850-810b-75fe-a325-18cec783089e', 'height', 'hauteur', '019f784c-9213-7630-80a0-eac859dfd5c3', true),
('019f7850-810b-79ec-9084-bbadd969a417', 'width', 'largeur', '019f784c-9213-7630-80a0-eac859dfd5c3', true),
('019f7850-810b-7210-98c9-71b212c79602', 'length', 'longueur', '019f784c-9213-7630-80a0-eac859dfd5c3', true),
('019f7850-810b-77a4-8159-bdb06bb74ecd', 'amperage', 'amperage', '019f784c-9213-7630-80a0-eac859dfd5c3', true),
('019f7850-810b-76a5-b01b-147f298072c1', 'voltage', 'tension', '019f784c-9213-7630-80a0-eac859dfd5c3', true),
('019f7850-810b-7c4b-adce-7ec8f4849ebf', 'wattage', 'puissance', '019f784c-9213-7630-80a0-eac859dfd5c3', true),
('019f7850-810b-7c3c-b075-723726289bc0', 'operating temperature', 'temperature d''operation', '019f784c-9213-7630-80a0-eac859dfd5c3', false),
('019f7850-810b-76ba-9300-08247b4feeeb', 'weight', 'poids', '019f784c-9213-7630-80a0-eac859dfd5c3', false);
-----PRODUCT ATTRIBUTE
COMMIT;