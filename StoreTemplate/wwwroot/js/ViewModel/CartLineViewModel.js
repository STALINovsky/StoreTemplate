System.register([], function (exports_1, context_1) {
    "use strict";
    var CartLineViewModel;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [],
        execute: function () {
            CartLineViewModel = /** @class */ (function () {
                function CartLineViewModel(cartLineElement) {
                    this.cartLineElement = cartLineElement;
                }
                CartLineViewModel.getCartLines = function (parentNode) {
                    if (parentNode === void 0) { parentNode = document; }
                    var cartLines = parentNode.querySelectorAll(CartLineViewModel.cartLineSelector);
                    var viewComponents = new Array();
                    for (var i = 0; i < cartLines.length; i++) {
                        var current = cartLines[i];
                        viewComponents.push(new CartLineViewModel(current));
                    }
                    return viewComponents;
                };
                CartLineViewModel.prototype.addHandlerToCartLineRemove = function (eventHandler) {
                    var _this = this;
                    var removeButton = this.cartLineElement.querySelector(CartLineViewModel.removeButtonSelector);
                    removeButton.addEventListener(CartLineViewModel.removeButtonEventType, function () {
                        eventHandler(_this);
                        _this.remove();
                    });
                };
                CartLineViewModel.prototype.addHandlerToCartLineCountChange = function (eventHandler) {
                    var _this = this;
                    var countInput = this.cartLineElement.querySelector(CartLineViewModel.productCountSelector);
                    countInput.addEventListener(CartLineViewModel.countInputEventType, function () { eventHandler(_this); });
                };
                Object.defineProperty(CartLineViewModel.prototype, "id", {
                    get: function () {
                        var productIdInput = this.cartLineElement.querySelector(CartLineViewModel.idSelector);
                        var productId = +productIdInput.value;
                        return productId;
                    },
                    enumerable: false,
                    configurable: true
                });
                Object.defineProperty(CartLineViewModel.prototype, "price", {
                    get: function () {
                        var productPrice = +this.cartLineElement.querySelector(CartLineViewModel.productPriceSelector).textContent;
                        return productPrice;
                    },
                    enumerable: false,
                    configurable: true
                });
                Object.defineProperty(CartLineViewModel.prototype, "count", {
                    get: function () {
                        var countInput = this.cartLineElement.querySelector(CartLineViewModel.productCountSelector);
                        var productCount = +countInput.value;
                        return productCount;
                    },
                    enumerable: false,
                    configurable: true
                });
                CartLineViewModel.prototype.insertCartLineInElement = function (element) {
                    element.appendChild(this.cartLineElement);
                };
                CartLineViewModel.prototype.remove = function () {
                    this.cartLineElement.parentElement.removeChild(this.cartLineElement);
                };
                //selectors
                CartLineViewModel.cartLineSelector = "div.cart-line";
                CartLineViewModel.idSelector = "input.product-id";
                CartLineViewModel.removeButtonSelector = "button.cart-line-delete";
                CartLineViewModel.productPriceSelector = "strong.product-price";
                CartLineViewModel.productCountSelector = "input.product-count";
                //static fields 
                CartLineViewModel.removeButtonEventType = "click";
                CartLineViewModel.countInputEventType = "change";
                return CartLineViewModel;
            }());
            exports_1("CartLineViewModel", CartLineViewModel);
        }
    };
});
//# sourceMappingURL=CartLineViewModel.js.map