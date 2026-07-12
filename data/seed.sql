BEGIN TRANSACTION;
-----BRAND
INSERT INTO brand (id, name, logo_url) 
VALUES 
(19308, 'sogetec', 'https://levis.com.br/images/logo.svg'),
(19508, '3m', 'https://levis.com.br/images/logo.svg'),
(19580, 'abb', 'https://levis.com.br/images/logo.svg'),
(21433, 'alvarion', 'https://levis.com.br/images/logo.svg'),
(21670, 'chauvin-arnoux', 'https://levis.com.br/images/logo.svg'),
(21987, 'bosch', 'https://levis.com.br/images/logo.svg'),
(22708, 'espa', 'https://levis.com.br/images/logo.svg'),
(23875, 'legrand', 'https://levis.com.br/images/logo.svg'),
(24713, 'df electric', 'https://levis.com.br/images/logo.svg'),
(26651, 'merlin gerin', 'https://levis.com.br/images/logo.svg'),
(26823, 'osram', 'https://levis.com.br/images/logo.svg'),
(26842, 'philips', 'https://levis.com.br/images/logo.svg'),
(26984, 'schneider', 'https://levis.com.br/images/logo.svg'),
(29585, 'telemecanique', 'https://levis.com.br/images/logo.svg'),
(31540, 'simel', 'https://levis.com.br/images/logo.svg'),
(34513, 'semicron onron', 'https://levis.com.br/images/logo.svg'),
(35917, 'ino', 'https://levis.com.br/images/logo.svg'),
(36849, 'ingelec', 'https://levis.com.br/images/logo.svg'),
(37838, 'ebe', 'https://levis.com.br/images/logo.svg'),
(38046, 'fil', 'https://levis.com.br/images/logo.svg'),
(38063, 'luminaire', 'https://levis.com.br/images/logo.svg');
-----BRAND

-----CATEGORY GROUP
INSERT INTO category_group (id, name, name_fr, image_url, is_active, sort_order) 
VALUES 
(19308, 'Lighting', 'Luminaire', 'https://levis.com.br/images/logo.svg', 1, 1);
-----CATEGORY GROUP

-----CATEGORY
INSERT INTO category (id, name, name_fr, slug, parent_id, group_id, description, image_url, is_active, sort_order) 
VALUES 
(19308, 'Traffic Light', 'Lampe de circulation', 'traffic-light', null, 19308, 'description', 'https://levis.com.br/images/logo.svg', 1, 1),
(19508, 'indicator Light', 'Lampe de signalisation', 'indicator-light', null, 19308, 'description', 'https://levis.com.br/images/logo.svg', 1, 2);
-----CATEGORY

-----PRODUCT ATTRIBUTE HEADER
INSERT INTO product_attribute_header (id, name, name_fr, sort_order) 
VALUES 
(19308, 'Characteristics & Features', 'Caracteristiques & Fonctions', 1),
(19508, 'Dimensions & Measurements', 'Dimensions & Mesures', 2),
(19580, 'Certifications & Warranties', 'Certifications & Garanties', 3);
-----PRODUCT ATTRIBUTE HEADER

-----PRODUCT ATTRIBUTE
INSERT INTO product_attribute (id, name, name_fr, header_id, is_variant) 
VALUES 
(19308, 'color', 'couleur', 19308, 1),
(19508, 'type', 'type', 19308, 1),
(19580, 'material', 'matière', 19308, 0),
(20580, 'compatible with', 'compatible avec', 19308, 0),
(21433, 'application sector', 'secteur d''application', 19308, 0),
(21670, 'number of switches', 'nombre de commutateurs', 19308, 0),
(21987, 'control mechanism', 'mechanisme de controle', 19308, 0),
(22708, 'output ports', 'ports de sortie', 19308, 0),
(23875, 'features', 'caracteristiques', 19308, 0),
(24713, 'power supply', 'alimentation', 19308, 0),
(25713, 'number of poles', 'nombre de poles', 19308, 0),
(25850, 'number of rows', 'nombre de rangées', 19308, 0),
(25860, 'number of racks', 'nombre de racks', 19308, 0),
(26651, 'depth', 'profondeur', 19508, 0),
(26823, 'height', 'hauteur', 19508, 1),
(26842, 'width', 'largeur', 19508, 1),
(26942, 'length', 'longueur', 19508, 1),
(26984, 'amperage', 'amperage', 19508, 1),
(29585, 'voltage', 'tension', 19508, 1),
(31540, 'wattage', 'puissance', 19508, 1),
(34513, 'operating temperature', 'temperature d''operation', 19508, 0),
(35917, 'weight', 'poids', 19508, 0);
-----PRODUCT ATTRIBUTE
COMMIT;