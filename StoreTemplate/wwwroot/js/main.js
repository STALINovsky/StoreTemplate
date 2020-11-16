var Model;
(function (Model) {
    var Product = /** @class */ (function () {
        function Product(name, price, imagePath) {
            this.name = name;
            this.price = price;
            this.imagePath = imagePath;
        }
        return Product;
    }());
    Model.Product = Product;
})(Model || (Model = {}));
///<reference path="UserCart.ts"/>
var Model;
(function (Model) {
    var ProductCartEventArgs = /** @class */ (function () {
        function ProductCartEventArgs(productId, sender) {
            this.targetProductId = productId;
            this.sender = sender;
        }
        return ProductCartEventArgs;
    }());
    Model.ProductCartEventArgs = ProductCartEventArgs;
})(Model || (Model = {}));
///<reference path="ProductCartEventArgs.ts"/>
var Model;
(function (Model) {
    var UserCart = /** @class */ (function () {
        function UserCart() {
            this.productId = new Array();
            this.addProductEventHandlers = new Array();
            this.removeProductEventHandlers = new Array();
        }
        UserCart.prototype.saveChanges = function () {
            document.cookie = "cart=" + JSON.stringify(this);
        };
        UserCart.prototype.addAddProductEventHandler = function (eventHandler) {
            this.addProductEventHandlers.push(eventHandler);
        };
        UserCart.prototype.addRemoveProductEventHandler = function (eventHandler) {
            this.removeProductEventHandlers.push(eventHandler);
        };
        UserCart.getInstance = function () {
            if (UserCart.instance == null) {
                UserCart.instance = new UserCart();
            }
            return UserCart.instance;
        };
        UserCart.prototype.addProductId = function (productId) {
            this.productId.push(productId);
            UserCart.callEvents(this.addProductEventHandlers, productId);
            this.saveChanges();
        };
        UserCart.prototype.removeProductId = function (productId) {
            var deletedProductId = this.productId.indexOf(productId);
            this.productId.slice(deletedProductId, 1);
            UserCart.callEvents(this.removeProductEventHandlers, productId);
        };
        Object.defineProperty(UserCart.prototype, "productIds", {
            get: function () {
                return this.productId;
            },
            enumerable: false,
            configurable: true
        });
        UserCart.callEvents = function (events, productId) {
            for (var i = 0; i < events.length; i++) {
                var productEventArgs = new Model.ProductCartEventArgs(productId, this.instance);
                events[i].call(productEventArgs);
            }
        };
        return UserCart;
    }());
    Model.UserCart = UserCart;
})(Model || (Model = {}));
var ViewControllers;
(function (ViewControllers) {
    var ProductViewController = /** @class */ (function () {
        function ProductViewController() {
        }
        ProductViewController.getProductCards = function () {
            var productCards = document.querySelectorAll(ProductViewController.productCardSelector);
            return productCards;
        };
        ProductViewController.getProductCardsAddButtons = function () {
            var addButtons = document.querySelectorAll(ProductViewController.productCardAddButtonSelector);
            return addButtons;
        };
        ProductViewController.addHandlerToProductCardsAddButtons = function (eventType, eventHandler) {
            var addButtons = ProductViewController.getProductCardsAddButtons();
            for (var i = 0; i < addButtons.length; i++) {
                addButtons[i].addEventListener(eventType, eventHandler);
            }
        };
        ProductViewController.getIdByProductCard = function (productCard) {
            var productId = +productCard.querySelector(ProductViewController.productCardIdSelector).textContent;
            return productId;
        };
        ProductViewController.productCardSelector = "div.product-card";
        ProductViewController.productCardAddButtonSelector = "button.product-card-add-btn";
        ProductViewController.productCardIdSelector = "label.product-card-id";
        return ProductViewController;
    }());
    ViewControllers.ProductViewController = ProductViewController;
})(ViewControllers || (ViewControllers = {}));
/// <reference path="Model/Product.ts" />
/// <reference path="Model/UserCart.ts" />
/// <reference path="ViewControllers/ProductViewController.ts" />
var CartModule = /** @class */ (function () {
    function CartModule() {
    }
    CartModule.main = function () {
        ViewControllers.ProductViewController.addHandlerToProductCardsAddButtons("click", CartModule.addProductToCart);
    };
    CartModule.addProductToCart = function (event) {
        var clickedBtn = event.target;
        var productCard = clickedBtn.parentElement.parentElement;
        var productId = ViewControllers.ProductViewController.getIdByProductCard(productCard);
        var cart = Model.UserCart.getInstance();
        cart.addProductId(productId);
    };
    return CartModule;
}());
if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", CartModule.main);
}
else {
    CartModule.main();
}
//# sourceMappingURL=main.js.map