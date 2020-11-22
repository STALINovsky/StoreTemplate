export class CartLineViewModel {
    //selectors
    private static readonly cartLineSelector = "div.cart-line";
    private static readonly idSelector = "input.product-id";
    private static readonly removeButtonSelector = "button.cart-line-delete";
    private static readonly productPriceSelector = "strong.product-price";
    private static readonly productCountSelector = "input.product-count";
    //static fields 
    private static readonly  removeButtonEventType = "click";
    private static readonly  countInputEventType = "change";

    private cartLineElement: Element;
    constructor(cartLineElement: Element) {
        this.cartLineElement = cartLineElement;
    }

    public static getCartLines(parentNode : ParentNode = document): Array<CartLineViewModel> {
        let cartLines = parentNode.querySelectorAll<Element>
            (CartLineViewModel.cartLineSelector);

        let viewComponents = new Array<CartLineViewModel>();
        for (let i = 0; i < cartLines.length; i++) {
            let current = cartLines[i];

            viewComponents.push(new CartLineViewModel(current));
        }

        return viewComponents;
    }



    public addHandlerToCartLineRemove(eventHandler: (target: CartLineViewModel) => void): void {
        let removeButton = this.cartLineElement.querySelector
            (CartLineViewModel.removeButtonSelector);

        removeButton.addEventListener(CartLineViewModel.removeButtonEventType,
            () => {
                eventHandler(this);
                this.remove();
            });

    }

    public addHandlerToCartLineCountChange(eventHandler: (target: CartLineViewModel) => void): void {
        let countInput = this.cartLineElement.querySelector
            (CartLineViewModel.productCountSelector);

        countInput.addEventListener(CartLineViewModel.countInputEventType,
            () => { eventHandler(this); });
    }

    public get id(): number {
        let productIdInput = this.cartLineElement.querySelector
            (CartLineViewModel.idSelector) as HTMLInputElement;
        let productId = + productIdInput.value;
        return productId;
    }

    public get price(): number {
        let productPrice = + this.cartLineElement.querySelector
            (CartLineViewModel.productPriceSelector).textContent;
        return productPrice;
    }

    public get count(): number {
        let countInput =  this.cartLineElement.querySelector
            (CartLineViewModel.productCountSelector) as HTMLInputElement;
        let productCount = + countInput.value;
        return productCount;
    }

    public insertCartLineInElement(element: Element) {
        element.appendChild(this.cartLineElement);
    }

    public remove() {
        this.cartLineElement.parentElement.removeChild(this.cartLineElement);
    }

}