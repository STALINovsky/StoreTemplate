﻿export class ProductCardViewModel {
    //selectors
    private static readonly productCardSelector = "div.product-card";
    private static readonly productCardAddButtonSelector = "button.product-card-add-btn";
    private static readonly productCardIdSelector = "label.product-id";
    //static fields
    private static addButtonEventType = "click";

    static getProductCards(): Array<ProductCardViewModel> {
        let productCards =
            document.querySelectorAll<Element>(ProductCardViewModel.productCardSelector);

        let viewComponents = new Array<ProductCardViewModel>();
        for (let i = 0; i < productCards.length; i++) {
            let current = productCards[i];
            viewComponents.push(new ProductCardViewModel(current));
        }

        return viewComponents;
    }

    private productCardElement: Element;
    public constructor(element: Element) {
        this.productCardElement = element;
    }

    public addEventHandlerToCardAdd(eventHandler: (target: ProductCardViewModel) => void) {
        let addButton = this.productCardElement.querySelector<Element>
            (ProductCardViewModel.productCardAddButtonSelector);

        addButton.addEventListener(ProductCardViewModel.addButtonEventType, () => { eventHandler(this); });
    }

    public get id(): number {
        let productId: number = +this.productCardElement.
            querySelector(ProductCardViewModel.productCardIdSelector).textContent;
        return productId;
    }

}