export interface Product {
  id: number;
  name: string;
  price: number;
  image: string;
  rating: number;
  sale?: boolean;
}

export const products: Product[] = [
  {
    id: 1,
    name: "Apple iPhone 14",
    price: 899,
    image: "/products/iphone.png",
    rating: 5,
  },
  {
    id: 2,
    name: "Nikon Camera",
    price: 652,
    image: "/products/camera.png",
    rating: 5,
  },
  {
    id: 3,
    name: "Wireless Earbuds",
    price: 800,
    image: "/products/buds.png",
    rating: 5,
    sale: true,
  },
];
