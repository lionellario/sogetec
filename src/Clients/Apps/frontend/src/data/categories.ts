import type { MegaMenuCategory } from "../lib/MenuCategory";

export const categories = [
  "Computers & Accessories",
  "Smartphones & Tablets",
  "TV, Video & Audio",
  "Speakers & Home Music",
  "Cameras & Photo",
  "Printers & Ink",
  "Charging Stations",
  "Headphones",
  "Wearables",
  "Powerbanks",
  "Video Games",
];

export const mockMenuData: MegaMenuCategory[] = [
  {
    id: "comp-acc",
    title: "Computers & Accessories",
    imageUrl: "https://via.placeholder.com/200x150?text=Computers",
    imageAlt: "Computers and Accessories Banner",
    groups: [
      {
        id: "g-computers",
        title: "Computers",
        items: [
          { id: "c1", name: "Laptops", url: "/laptops" },
          { id: "c2", name: "Desktops", url: "/desktops" },
          { id: "c3", name: "Gaming PCs", url: "/gaming-pcs" },
        ],
      },
      {
        id: "g-accessories",
        title: "Accessories",
        items: [
          { id: "a1", name: "Monitors", url: "/monitors" },
          { id: "a2", name: "Keyboards", url: "/keyboards" },
          { id: "a3", name: "Mice", url: "/mice" },
        ],
      },
    ],
  },
  {
    id: "smart-tab",
    title: "Smartphones & Tablets",
    imageUrl: "https://via.placeholder.com/200x150?text=Mobile",
    imageAlt: "Smartphones and Tablets Banner",
    groups: [
      {
        id: "g-smartphones",
        title: "Smartphones",
        items: [
          { id: "p1", name: "iOS Devices", url: "/ios" },
          { id: "p2", name: "Android Devices", url: "/android" },
        ],
      },
      {
        id: "g-tablets",
        title: "Tablets",
        items: [
          { id: "t1", name: "iPads", url: "/ipads" },
          { id: "t2", name: "Android Tablets", url: "/android-tablets" },
        ],
      },
    ],
  },
  {
    id: "tv-video",
    title: "TV, Video & Audio",
    imageUrl: "https://via.placeholder.com/200x150?text=TV",
    imageAlt: "TV, Video and Audio Banner",
    groups: [
      {
        id: "g-tv",
        title: "TV",
        items: [
          { id: "tv1", name: "Smart TVs", url: "/smart-tvs" },
          { id: "tv2", name: "LED TVs", url: "/led-tvs" },
        ],
      },
      {
        id: "g-video",
        title: "Video",
        items: [
          { id: "v1", name: "Cameras", url: "/cameras" },
          { id: "v2", name: "Camcorders", url: "/camcorders" },
        ],
      },
    ],
  },
  {
    id: "speakers",
    title: "Speakers & Home Music",
    imageUrl: "https://via.placeholder.com/200x150?text=Speakers",
    imageAlt: "Speakers and Home Music Banner",
    groups: [
      {
        id: "g-speakers",
        title: "Speakers",
        items: [
          { id: "s1", name: "Wireless Speakers", url: "/wireless-speakers" },
          { id: "s2", name: "Home Theaters", url: "/home-theaters" },
        ],
      },
    ],
  },
  {
    id: "cameras",
    title: "Cameras & Photo",
    imageUrl: "https://via.placeholder.com/200x150?text=Cameras",
    imageAlt: "Cameras and Photo Banner",
    groups: [
      {
        id: "g-cameras",
        title: "Cameras",
        items: [
          { id: "c1", name: "DSLR Cameras", url: "/dslr-cameras" },
          { id: "c2", name: "Mirrorless Cameras", url: "/mirrorless-cameras" },
        ],
      },
    ],
  },
  {
    id: "printers",
    title: "Printers & Ink",
    imageUrl: "https://via.placeholder.com/200x150?text=Printers",
    imageAlt: "Printers and Ink Banner",
    groups: [
      {
        id: "g-printers",
        title: "Printers",
        items: [
          { id: "p1", name: "Inkjet Printers", url: "/inkjet-printers" },
          { id: "p2", name: "Laser Printers", url: "/laser-printers" },
        ],
      },
    ],
  },
  {
    id: "chargers",
    title: "Charging Stations",
    imageUrl: "https://via.placeholder.com/200x150?text=Charging",
    imageAlt: "Charging Stations Banner",
    groups: [
      {
        id: "g-chargers",
        title: "Chargers",
        items: [
          { id: "c1", name: "USB Chargers", url: "/usb-chargers" },
          { id: "c2", name: "Wall Chargers", url: "/wall-chargers" },
        ],
      },
    ],
  },
  {
    id: "computers",
    title: "Computers",
    imageUrl: "https://via.placeholder.com/200x150?text=Computers",
    imageAlt: "Computers Banner",
    groups: [
      {
        id: "g-computers",
        title: "Computers",
        items: [
          { id: "c1", name: "Laptops", url: "/laptops" },
          { id: "c2", name: "Desktops", url: "/desktops" },
          { id: "c3", name: "Gaming PCs", url: "/gaming-pcs" },
        ],
      },
    ],
  },
];
