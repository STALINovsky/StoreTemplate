///<reference path="UserCart.ts"/>
namespace Model {

    export class ProductCartEventArgs {

        constructor(productId: number, sender: UserCart) {
            this.targetProductId = productId;
            this.sender = sender;
        }

        targetProductId: number;
        sender: UserCart;
    }

}