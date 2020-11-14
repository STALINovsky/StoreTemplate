/// <reference path="Product.ts" />

namespace Model {
    class UserCart {
        private Products: Array<Product>

        constructor() {
            this.Products = new Array<Product>();
        }

        AddProduct(product: Product) {
            this.Products.push(product);
        }

        RemoveProduct(product: Product) {
            let deletedProductId: number = this.Products.indexOf(product);
            this.Products.slice(deletedProductId, 1);
        }
    }
    export let Cart: UserCart = new UserCart(); 
}

