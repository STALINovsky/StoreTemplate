import { CartComponent } from "../Components/CartComponent"
import { CartLineViewModel } from "../ViewModel/CartLineViewModel"

export class CartViewComponent {
    //selectors
    private static readonly cartSelector = "div.user-cart";
    private static readonly cartCountSelector = "span.user-cart-count";
    private static readonly cartPriceSelector = "strong.user-cart-price";
    //static fields
    private static instance: CartViewComponent;
    private static cart: CartComponent;
    //dynamic fields 
    private cartElement: Element;
    private cartLines: Array<CartLineViewModel>;

    private updateCart() {
        let cartCountElement = this.cartElement.querySelector(CartViewComponent.cartCountSelector);
        cartCountElement.textContent = this.count.toString();

        let cartPriceElement = this.cartElement.querySelector(CartViewComponent.cartPriceSelector);
        cartPriceElement.textContent = this.sum.toFixed(2);
    }

    private constructor(cartElement: Element) {
        this.cartElement = cartElement;
        this.cartLines = CartLineViewModel.getCartLines(cartElement);

        for (let i = 0; i < this.cartLines.length; i++) {
            let current = this.cartLines[i];
            current.addHandlerToCartLineCountChange((target: CartLineViewModel) => {
                 this.updateCart();
            });
            current.addHandlerToCartLineRemove((target: CartLineViewModel) => {
                this.removeCartLine(target);
                this.updateCart();
            });
        }
        this.updateCart();
    }

    private removeCartLine(cartLine: CartLineViewModel) {
        let indexOfCartLine = this.cartLines.indexOf(cartLine);
        this.cartLines.splice(indexOfCartLine, 1);
    }

    public get count(): number {
        return this.cartLines.length;
    }

    public get sum(): number {
        let sum: number = 0;
        for (let i = 0; i < this.cartLines.length; i++) {
            let current = this.cartLines[i];
            sum += current.price * current.count;
        }
        return sum;
    }

    public static initCartViewComponent(): void {
        CartViewComponent.cart = CartComponent.getInstance();
        CartViewComponent.instance = new CartViewComponent(
            document.querySelector(CartViewComponent.cartSelector));
    }
}


CartViewComponent.initCartViewComponent();
