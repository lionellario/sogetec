export interface Category {
  id: number;
  name: string;
  nameFr: string;
  slug: string;
  groupId: number;
  groupName: string;
  groupNameFr: string;
  groupSortOrder: number;
  groupImageUrl: string;
  parentId?: number;
  parentName?: string;
  sortOrder: number;
  isActive: boolean;
  createdAt: string;
  lastModifiedAt: string;
}

// Extends Category to include an array of its children
export interface TreeCategory extends Category {
  children: TreeCategory[];
}

// Defines the top-level Group structure
export interface CategoryGroup {
  id: number;
  name: string;
  nameFr: string;
  imageUrl: string;
  sortOrder: number;
  categories: TreeCategory[];
}

export function buildCategoryTree(flatCategories: Category[]): CategoryGroup[] {
  const categoryMap = new Map<number, TreeCategory>();
  const groupMap = new Map<number, CategoryGroup>();
  const rootCategories: TreeCategory[] = [];

  // Sort upfront so children inherit the correct ordering
  const sortedCategories = [...flatCategories].sort((a, b) => a.sortOrder - b.sortOrder);

  // Pass 1: Initialize the map with TreeCategory objects
  for (const cat of sortedCategories) {
    categoryMap.set(cat.id, { ...cat, children: [] });
  }

  // Pass 2: Build the hierarchy
  for (const cat of sortedCategories) {
    const currentTreeCat = categoryMap.get(cat.id)!;

    if (cat.parentId) {
      // It's a subcategory; link it to its parent
      const parent = categoryMap.get(cat.parentId);
      if (parent) {
        parent.children.push(currentTreeCat);
      } else {
        // Fallback: if parentId is missing from the list, treat as root
        rootCategories.push(currentTreeCat);
      }
    } else {
      // It's a top-level category
      rootCategories.push(currentTreeCat);
    }
  }

  // Pass 3: Group the root-level categories by Group
  for (const rootCat of rootCategories) {
    if (!groupMap.has(rootCat.groupId)) {
      groupMap.set(rootCat.groupId, {
        id: rootCat.groupId,
        name: rootCat.groupName,
        nameFr: rootCat.groupNameFr,
        imageUrl: rootCat.groupImageUrl,
        sortOrder: rootCat.groupSortOrder,
        categories: []
      });
    }
    groupMap.get(rootCat.groupId)!.categories.push(rootCat);
  }

  return Array.from(groupMap.values()).sort((a, b) => a.sortOrder - b.sortOrder);
}