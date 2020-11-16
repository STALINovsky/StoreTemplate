namespace Model {
    export class Product {
        id: number;
        name: string;
        price: number;
        imagePath: string;
        count: number;

        constructor(name: string, price: number, imagePath: string) {
            this.name = name;
            this.price = price;
            this.imagePath = imagePath;
        }
    }
}

