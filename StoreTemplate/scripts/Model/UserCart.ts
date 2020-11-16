///<reference path="ProductCartEventArgs.ts"/>

namespace Model {
    type ProductId = number;
    type ProductEventHandler = { (evtArgs: ProductCartEventArgs): void };
    export class UserCart {
        private readonly productId: Array<ProductId>;
        private readonly addProductEventHandlers: Array<ProductEventHandler>;
        private readonly removeProductEventHandlers: Array<ProductEventHandler>;

        private constructor() {
            this.productId = new Array<ProductId>();
            this.addProductEventHandlers = new Array<ProductEventHandler>();
            this.removeProductEventHandlers = new Array<ProductEventHandler>();
        }

        private saveChanges(): void {
            document.cookie = "cart=" + JSON.stringify(this);
        }

        public addAddProductEventHandler(eventHandler : ProductEventHandler): void {
            this.addProductEventHandlers.push(eventHandler);
        }

        public addRemoveProductEventHandler(eventHandler: ProductEventHandler): void {
            this.removeProductEventHandlers.push(eventHandler);
        }

        public static instance: UserCart;
        public static getInstance(): UserCart {
            if (UserCart.instance == null) {
                UserCart.instance = new UserCart();
            }
            return UserCart.instance;
        }

        addProductId(productId: ProductId): void {
            this.productId.push(productId);
            UserCart.callEvents(this.addProductEventHandlers, productId);
            this.saveChanges();
        }

        removeProductId(productId: ProductId): void {
            let deletedProductId: number = this.productId.indexOf(productId);
            this.productId.slice(deletedProductId, 1);
            UserCart.callEvents(this.removeProductEventHandlers, productId);
        }

        public get productIds(): Iterable<ProductId> {
            return this.productId;
        }

        private static callEvents(events: Array<ProductEventHandler>, productId: number) {
            for (let i = 0; i < events.length; i++) {
                let productEventArgs = new ProductCartEventArgs(productId,this.instance);
                events[i].call(productEventArgs);
            }
        }
    }
}

