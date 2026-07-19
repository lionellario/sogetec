START TRANSACTION;
DROP TABLE IF EXISTS product_variant;
DROP TABLE IF EXISTS product_image;
DROP TABLE IF EXISTS product_item;
DROP TABLE IF EXISTS product_attribute;
DROP TABLE IF EXISTS product;
DROP TABLE IF EXISTS category;
DROP TABLE IF EXISTS product_attribute_header;
DROP TABLE IF EXISTS category_group;
DROP TABLE IF EXISTS brand;

CREATE TABLE brand (
	id UUID NOT NULL,
	name	TEXT NOT NULL,
	logo_url	TEXT NOT NULL,
	created_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	last_modified_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_brand" PRIMARY KEY("id")
);
CREATE TABLE category_group (
	id UUID NOT NULL,
	name	TEXT NOT NULL,
	name_fr	TEXT NOT NULL,
	image_url	TEXT NOT NULL,
	is_active	BOOLEAN NOT NULL DEFAULT FALSE,
	sort_order	SMALLINT NOT NULL,
	created_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	last_modified_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_category_group" PRIMARY KEY("id")
);
CREATE TABLE category (
	id UUID NOT NULL,
	name	TEXT NOT NULL,
	name_fr	TEXT NOT NULL,
	slug	TEXT NOT NULL,
	parent_id	UUID NULL,
	group_id	UUID NOT NULL,
	description	TEXT NULL,
	image_url	TEXT NULL,
	is_active	BOOLEAN NOT NULL DEFAULT FALSE,
	sort_order	SMALLINT NOT NULL,
	created_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	last_modified_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_category" PRIMARY KEY("id"),
	CONSTRAINT "FK_category_group" FOREIGN KEY("group_id") REFERENCES "category_group"("id") ON DELETE CASCADE,
	CONSTRAINT "FK_category_category" FOREIGN KEY("parent_id") REFERENCES "category"("id") ON DELETE SET NULL
);
CREATE TABLE product_attribute_header (
	id UUID NOT NULL,
	name	TEXT NOT NULL,
	name_fr	TEXT NOT NULL,
	sort_order	SMALLINT NOT NULL,
	created_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	last_modified_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_product_attribute_header" PRIMARY KEY("id")
);
CREATE TABLE product (
	id UUID NOT NULL,
	name	TEXT NOT NULL,
	name_fr	TEXT NOT NULL,
	slug	TEXT NOT NULL,
	description	TEXT NOT NULL,
	is_active	BOOLEAN NOT NULL DEFAULT FALSE,
	price	NUMERIC(18,2) NOT NULL,
	cost	NUMERIC(18,2) NOT NULL,
	quantity_unit	TEXT NOT NULL,
	brand_id	UUID NOT NULL,
	category_id	UUID NOT NULL,
	created_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	last_modified_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_product" PRIMARY KEY("id"),
	CONSTRAINT "FK_product_brand" FOREIGN KEY("brand_id") REFERENCES "brand"("id") ON DELETE CASCADE,
	CONSTRAINT "FK_product_category" FOREIGN KEY("category_id") REFERENCES "category"("id") ON DELETE CASCADE
);
CREATE TABLE product_attribute (
	id UUID NOT NULL,
	name	TEXT NOT NULL,
	name_fr	TEXT NOT NULL,
	header_id	UUID NOT NULL,
	is_variant	BOOLEAN NOT NULL DEFAULT FALSE,
	created_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	last_modified_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_product_attribute" PRIMARY KEY("id"),
	CONSTRAINT "FK_product_attribute_header" FOREIGN KEY("header_id") REFERENCES "product_attribute_header"("id") ON DELETE CASCADE
);
CREATE TABLE product_image (
	id UUID NOT NULL,
	url	TEXT NOT NULL,
	preview_url	TEXT NOT NULL,
	product_id	UUID NOT NULL,
	created_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	last_modified_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_product_image" PRIMARY KEY("id"),
	CONSTRAINT "FK_product_image_product" FOREIGN KEY("product_id") REFERENCES "product"("id") ON DELETE CASCADE
);
CREATE TABLE product_item (
	id UUID NOT NULL,
	name	TEXT NOT NULL,
	name_fr	TEXT NOT NULL,
	slug	TEXT NOT NULL,
	code	TEXT NOT NULL,
	sku	TEXT NOT NULL,
	description	TEXT NULL,
	is_active	BOOLEAN NOT NULL DEFAULT FALSE,
	product_id	UUID NOT NULL,
	price_adjustment	NUMERIC(18,2) NOT NULL,
	cost_adjustment	NUMERIC(18,2) NOT NULL,
	initial_stock	NUMERIC(18,3) NOT NULL,
	final_stock	NUMERIC(18,3) NOT NULL,
	created_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	last_modified_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	details	JSONB NULL,
	CONSTRAINT "PK_product_item" PRIMARY KEY("id"),
	CONSTRAINT "FK_product_item_product" FOREIGN KEY("product_id") REFERENCES "product"("id") ON DELETE CASCADE
);
CREATE TABLE product_variant (
	id UUID NOT NULL,
	attribute_id	UUID NOT NULL,
	value	TEXT NOT NULL,
	item_id	UUID NOT NULL,
	created_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	last_modified_on	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_product_variant" PRIMARY KEY("id"),
	CONSTRAINT "FK_product_variant_product_item" FOREIGN KEY("item_id") REFERENCES "product_item"("id") ON DELETE CASCADE,
	CONSTRAINT "FK_product_variant_product_attribute" FOREIGN KEY("attribute_id") REFERENCES "product_attribute"("id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_category_group" ON "category" (
	"group_id"
);
CREATE INDEX IF NOT EXISTS "IX_category_parent" ON "category" (
	"parent_id"
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_category_slug" ON "category" (
	"slug"
);
CREATE INDEX IF NOT EXISTS "IX_product_attribute_header" ON "product_attribute" (
	"header_id"
);
CREATE INDEX IF NOT EXISTS "IX_product_brand" ON "product" (
	"brand_id"
);
CREATE INDEX IF NOT EXISTS "IX_product_category" ON "product" (
	"category_id"
);
CREATE INDEX IF NOT EXISTS "IX_product_image_product" ON "product_image" (
	"product_id"
);
CREATE INDEX IF NOT EXISTS "IX_product_item_product" ON "product_item" (
	"product_id"
);
CREATE INDEX IF NOT EXISTS "IX_product_variant_attribute" ON "product_variant" (
	"attribute_id"
);
CREATE INDEX IF NOT EXISTS "IX_product_variant_item" ON "product_variant" (
	"item_id"
);
COMMIT;
