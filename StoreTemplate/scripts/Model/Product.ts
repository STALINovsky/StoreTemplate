namespace Model {
    export class Product {
        Name: string
        Price: number
        ImagePath: string
        Count: number

        constructor(name: string, price: number, imagePath: string) {
            this.Name = name;
            this.Price = price;
            this.ImagePath = imagePath;
        }
    }
}

