BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "brand" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	"logo_url"	TEXT NOT NULL,
	"created_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"last_modified_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_brand" PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "category" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	"name_fr"	TEXT NOT NULL,
	"slug"	TEXT NOT NULL,
	"parent_id"	INTEGER,
	"group_id"	INTEGER NOT NULL,
	"description"	TEXT,
	"image_url"	TEXT,
	"is_active"	INTEGER NOT NULL,
	"sort_order"	INTEGER NOT NULL,
	"created_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"last_modified_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_category" PRIMARY KEY("id" AUTOINCREMENT),
	CONSTRAINT "FK_category_group" FOREIGN KEY("group_id") REFERENCES "category_group"("id") ON DELETE CASCADE,
	CONSTRAINT "FK_category_category" FOREIGN KEY("parent_id") REFERENCES "category"("id") ON DELETE SET NULL
);
CREATE TABLE IF NOT EXISTS "category_group" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	"name_fr"	TEXT NOT NULL,
	"image_url"	TEXT NOT NULL,
	"is_active"	INTEGER NOT NULL,
	"sort_order"	INTEGER NOT NULL,
	"created_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"last_modified_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_category_group" PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "product" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	"name_fr"	TEXT NOT NULL,
	"slug"	TEXT NOT NULL,
	"description"	TEXT NOT NULL,
	"is_active"	INTEGER NOT NULL,
	"brand_id"	INTEGER NOT NULL,
	"category_id"	INTEGER NOT NULL,
	"created_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"last_modified_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_product" PRIMARY KEY("id" AUTOINCREMENT),
	CONSTRAINT "FK_product_brand" FOREIGN KEY("brand_id") REFERENCES "brand"("id") ON DELETE CASCADE,
	CONSTRAINT "FK_product_category" FOREIGN KEY("category_id") REFERENCES "category"("id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "product_attribute" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	"name_fr"	TEXT NOT NULL,
	"header_id"	INTEGER NOT NULL,
	"is_variant"	INTEGER NOT NULL,
	"created_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"last_modified_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_product_attribute" PRIMARY KEY("id" AUTOINCREMENT),
	CONSTRAINT "FK_product_attribute_header" FOREIGN KEY("header_id") REFERENCES "product_attribute_header"("id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "product_attribute_header" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	"name_fr"	TEXT NOT NULL,
	"sort_order"	INTEGER NOT NULL,
	"created_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"last_modified_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_product_attribute_header" PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "product_image" (
	"id"	INTEGER NOT NULL,
	"url"	TEXT NOT NULL,
	"preview_url"	TEXT NOT NULL,
	"product_id"	INTEGER NOT NULL,
	"created_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"last_modified_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_product_image" PRIMARY KEY("id" AUTOINCREMENT),
	CONSTRAINT "FK_product_image_product" FOREIGN KEY("product_id") REFERENCES "product"("id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "product_item" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	"name_fr"	TEXT NOT NULL,
	"slug"	TEXT NOT NULL,
	"code"	TEXT NOT NULL,
	"sku"	TEXT NOT NULL,
	"description"	TEXT,
	"is_active"	INTEGER NOT NULL,
	"product_id"	INTEGER NOT NULL,
	"price"	TEXT NOT NULL,
	"cost"	TEXT NOT NULL,
	"initial_stock"	TEXT NOT NULL,
	"final_stock"	TEXT NOT NULL,
	"quantity_unit"	INTEGER NOT NULL,
	"created_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"last_modified_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"Details"	TEXT,
	CONSTRAINT "PK_product_item" PRIMARY KEY("id" AUTOINCREMENT),
	CONSTRAINT "FK_product_item_product" FOREIGN KEY("product_id") REFERENCES "product"("id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "product_variant" (
	"id"	INTEGER NOT NULL,
	"variant_id"	INTEGER NOT NULL,
	"value"	TEXT NOT NULL,
	"item_id"	INTEGER NOT NULL,
	"created_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	"last_modified_on"	TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
	CONSTRAINT "PK_product_variant" PRIMARY KEY("id" AUTOINCREMENT),
	CONSTRAINT "FK_product_variant_product_item" FOREIGN KEY("item_id") REFERENCES "product_item"("id") ON DELETE CASCADE,
	CONSTRAINT "FK_product_variant_product_variant" FOREIGN KEY("variant_id") REFERENCES "product_attribute"("id") ON DELETE CASCADE
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
	"variant_id"
);
CREATE INDEX IF NOT EXISTS "IX_product_variant_item" ON "product_variant" (
	"item_id"
);
COMMIT;
