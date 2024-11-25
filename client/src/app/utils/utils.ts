import { Category } from "../models/category";

export function currencyFormat(amount: number) {
    return amount.toLocaleString("vi", {
        style: "currency",
        currency: "VND",
    });
}

export function buildCategoryTree(categories: Category[]) {
    const categoryMap = new Map<
        number,
        { id: number; name: string; pId: number; children: any[] }
    >();
    const filteredCategories = categories.filter((cat) => cat.pId !== null);
    filteredCategories.forEach((cat) => {
        categoryMap.set(cat.id, { ...cat, children: [] });
    });

    const tree: any[] = [];
    filteredCategories.forEach((cat) => {
        const node = categoryMap.get(cat.id);
        if (cat.pId && categoryMap.has(cat.pId)) {
            categoryMap.get(cat.pId)!.children.push(node);
        } else {
            tree.push(node);
        }
    });
    return tree;
}
