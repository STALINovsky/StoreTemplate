﻿import { getCookie } from "../Extensions/CookieExtensions"
import { CartLine } from "../Model/CartLine";
import { CartLineViewModel } from "../ViewModel/CartLineViewModel";
import { ProductCardViewModel } from "../ViewModel/ProductCardViewModel";

type ProductId = number;
type ProductEventHandler = (target: CartLine) => void;

export class CartComponent {
    private readonly cartLines: Array<CartLine>;
    private readonly addProductEventHandlers: Array<ProductEventHandler>;
    private readonly removeProductEventHandlers: Array<ProductEventHandler>;
    private static readonly cartCookieName = "cart";

    private constructor(productCardComponents: Array<ProductCardViewModel>,
        cartLineComponents: Array<CartLineViewModel>) {
        this.cartLines = new Array<CartLine>();
        this.addProductEventHandlers = new Array<ProductEventHandler>();
        this.removeProductEventHandlers = new Array<ProductEventHandler>();

        for (let i = 0; i < productCardComponents.length; i++) {
            let current = productCardComponents[i];
            current.addEventHandlerToCardAdd((target: ProductCardViewModel) => {
                let productId = target.id;

                let cart = CartComponent.getInstance();
                cart.addProducts(productId);
            });
        }

        for (let i = 0; i < cartLineComponents.length; i++) {
            let current = cartLineComponents[i];
            //add cartLine remove
            current.addHandlerToCartLineRemove((target: CartLineViewModel) => {
                let productId = target.id;

                let cart = CartComponent.getInstance();
                cart.removeProducts(productId);
            });
            //add cartLine changeCount
            current.addHandlerToCartLineCountChange((target: CartLineViewModel) => {
                let productId = target.id;
                let productCount = target.count;

                let cart = CartComponent.getInstance();
                cart.setProductCount(productId, productCount);
            });
        }

    }

    private saveChanges(): void {
        let cookieString = CartComponent.cartCookieName + "=" + escape(JSON.stringify(this.cartLines)) + ";path=/;";
        document.cookie = cookieString;
    }

    addAddProductEventHandler(eventHandler: ProductEventHandler): void {
        this.addProductEventHandlers.push(eventHandler);
    }

    addRemoveProductEventHandler(eventHandler: ProductEventHandler): void {
        this.removeProductEventHandlers.push(eventHandler);
    }

    private static instance: CartComponent;

    static getInstance(): CartComponent {
        return CartComponent.instance;
    }

    private initCartByCookies(): void {
        let cookieCartString = getCookie(CartComponent.cartCookieName);

        if (cookieCartString != null) {
            let cartLines = JSON.parse(unescape(cookieCartString));
            for (let i = 0; i < cartLines.length; i++) {
                this.cartLines.push(cartLines[i]);
            }
        }
    }

    private getCartLineByProductIdOrDefault(productId: number): CartLine | null {
        for (let i = 0; i < this.cartLines.length; i++) {
            let currentLine = this.cartLines[i];
            if (currentLine.ProductId === productId) {
                return currentLine;
            }
        }
        return null;
    }

    addProducts(productId: ProductId, addedCount: number = 1): void {

        let cartLine = this.getCartLineByProductIdOrDefault(productId);
        if (cartLine == null) {
            cartLine = new CartLine(productId, addedCount);
            this.cartLines.push(cartLine);
        } else {
            cartLine.Count += addedCount;
        }

        this.callEvents(this.addProductEventHandlers, cartLine);
        this.saveChanges();
    }

    setProductCount(productId: ProductId, count: number) {
        let cartLine = this.getCartLineByProductIdOrDefault(productId);
        if (cartLine == null) {
            cartLine = new CartLine(productId, count);
            this.cartLines.push(cartLine);
        } else {
            cartLine.Count = count;
        }

        this.saveChanges();
    }

    removeProducts(productId: ProductId): void {
        let cartLine = this.getCartLineByProductIdOrDefault(productId);
        let cartLineIndex = this.cartLines.indexOf(cartLine);
        this.cartLines.splice(cartLineIndex, 1);

        this.callEvents(this.removeProductEventHandlers, cartLine);
        this.saveChanges();
    }

    get lines(): Iterable<CartLine> {
        return this.cartLines;
    }

    private callEvents(events: Array<ProductEventHandler>, cartLine: CartLine) {
        for (let i = 0; i < events.length; i++) {
            events[i].call(cartLine);
        }
    }

    public static initCartComponent(): void {
        let productCardComponents = ProductCardViewModel.getProductCards();
        let cartLineComponents = CartLineViewModel.getCartLines();

        CartComponent.instance = new CartComponent(productCardComponents, cartLineComponents);
        CartComponent.instance.initCartByCookies();
    }
}

CartComponent.initCartComponent();

