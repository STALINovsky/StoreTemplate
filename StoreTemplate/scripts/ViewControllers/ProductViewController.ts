namespace ViewControllers {

    export class ProductViewController {

        static productCardSelector = "div.product-card";
        static productCardAddButtonSelector = "button.product-card-add-btn";
        static productCardIdSelector = "label.product-card-id";

        static getProductCards(): NodeListOf<Element> {
            let productCards =
                document.querySelectorAll<Element>(ProductViewController.productCardSelector);
            return productCards;
        }

        static getProductCardsAddButtons(): NodeListOf<Element> | null {
            let addButtons: NodeListOf<Element> = document.querySelectorAll<Element>(ProductViewController.productCardAddButtonSelector);
            return addButtons;
        }

        static addHandlerToProductCardsAddButtons(eventType: string, eventHandler: EventListener): void {
            let addButtons: NodeListOf<Element> = ProductViewController.getProductCardsAddButtons();
            for (var i = 0; i < addButtons.length; i++) {
                addButtons[i].addEventListener(eventType, eventHandler);
            }
        }

        static getIdByProductCard(productCard: Element) : number {
            let productId: number = +productCard.querySelector(ProductViewController.productCardIdSelector).textContent;
            return productId;
        }
    }
}