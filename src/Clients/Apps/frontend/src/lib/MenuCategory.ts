export interface SubCategoryItem {
  id: string;
  name: string;
  url: string;
}

export interface CategoryGroup {
  id: string;
  title: string;
  items: SubCategoryItem[];
}

export interface MegaMenuCategory {
  id: string;
  title: string; // E.g., "Computers & Accessories"
  groups: CategoryGroup[]; // E.g., ["Computers", "Accessories"]
  imageUrl: string;
  imageAlt: string;
}
