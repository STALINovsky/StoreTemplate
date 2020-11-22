System.register(["../Components/CartComponent", "../ViewModel/CartLineViewModel"], function (exports_1, context_1) {
    "use strict";
    var CartComponent_1, CartLineViewModel_1, CartViewComponent;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (CartComponent_1_1) {
                CartComponent_1 = CartComponent_1_1;
            },
            function (CartLineViewModel_1_1) {
                CartLineViewModel_1 = CartLineViewModel_1_1;
            }
        ],
        execute: function () {
            CartViewComponent = /** @class */ (function () {
                function CartViewComponent(cartElement) {
                    var _this = this;
                    this.cartElement = cartElement;
                    this.cartLines = CartLineViewModel_1.CartLineViewModel.getCartLines(cartElement);
                    for (var i = 0; i < this.cartLines.length; i++) {
                        var current = this.cartLines[i];
                        current.addHandlerToCartLineCountChange(function (target) {
                            _this.updateCart();
                        });
                        current.addHandlerToCartLineRemove(function (target) {
                            _this.removeCartLine(target);
                            _this.updateCart();
                        });
                    }
                    this.updateCart();
                }
                CartViewComponent.prototype.updateCart = function () {
                    var cartCountElement = this.cartElement.querySelector(CartViewComponent.cartCountSelector);
                    cartCountElement.textContent = this.count.toString();
                    var cartPriceElement = this.cartElement.querySelector(CartViewComponent.cartPriceSelector);
                    cartPriceElement.textContent = this.sum.toFixed(2);
                };
                CartViewComponent.prototype.removeCartLine = function (cartLine) {
                    var indexOfCartLine = this.cartLines.indexOf(cartLine);
                    this.cartLines.splice(indexOfCartLine, 1);
                };
                Object.defineProperty(CartViewComponent.prototype, "count", {
                    get: function () {
                        return this.cartLines.length;
                    },
                    enumerable: false,
                    configurable: true
                });
                Object.defineProperty(CartViewComponent.prototype, "sum", {
                    get: function () {
                        var sum = 0;
                        for (var i = 0; i < this.cartLines.length; i++) {
                            var current = this.cartLines[i];
                            sum += current.price * current.count;
                        }
                        return sum;
                    },
                    enumerable: false,
                    configurable: true
                });
                CartViewComponent.initCartViewComponent = function () {
                    CartViewComponent.cart = CartComponent_1.CartComponent.getInstance();
                    CartViewComponent.instance = new CartViewComponent(document.querySelector(CartViewComponent.cartSelector));
                };
                //selectors
                CartViewComponent.cartSelector = "div.user-cart";
                CartViewComponent.cartCountSelector = "span.user-cart-count";
                CartViewComponent.cartPriceSelector = "strong.user-cart-price";
                return CartViewComponent;
            }());
            exports_1("CartViewComponent", CartViewComponent);
            CartViewComponent.initCartViewComponent();
        }
    };
});
//# sourceMappingURL=CartViewComponent.js.map