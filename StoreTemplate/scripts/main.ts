/// <reference path="Model/Product.ts" />
/// <reference path="Model/UserCart.ts" />
/// <reference path="ViewControllers/ProductViewController.ts" />

class CartModule {
    static main(): void {
        ViewControllers.ProductViewController.addHandlerToProductCardsAddButtons("click", CartModule.addProductToCart);
    }

    static addProductToCart(event: Event): void {
        let clickedBtn: Element = event.target as Element;
        let productCard: Element = clickedBtn.parentElement.parentElement;
        let productId: number = ViewControllers.ProductViewController.getIdByProductCard(productCard);
        let cart = Model.UserCart.getInstance();
        cart.addProductId(productId);
    }
}

if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", CartModule.main);
} else {
    CartModule.main();
}