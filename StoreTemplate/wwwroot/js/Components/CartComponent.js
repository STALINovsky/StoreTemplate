System.register(["../Extensions/CookieExtensions", "../Model/CartLine", "../ViewModel/CartLineViewModel", "../ViewModel/ProductCardViewModel"], function (exports_1, context_1) {
    "use strict";
    var CookieExtensions_1, CartLine_1, CartLineViewModel_1, ProductCardViewModel_1, CartComponent;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (CookieExtensions_1_1) {
                CookieExtensions_1 = CookieExtensions_1_1;
            },
            function (CartLine_1_1) {
                CartLine_1 = CartLine_1_1;
            },
            function (CartLineViewModel_1_1) {
                CartLineViewModel_1 = CartLineViewModel_1_1;
            },
            function (ProductCardViewModel_1_1) {
                ProductCardViewModel_1 = ProductCardViewModel_1_1;
            }
        ],
        execute: function () {
            CartComponent = /** @class */ (function () {
                function CartComponent(productCardComponents, cartLineComponents) {
                    this.cartLines = new Array();
                    this.addProductEventHandlers = new Array();
                    this.removeProductEventHandlers = new Array();
                    for (var i = 0; i < productCardComponents.length; i++) {
                        var current = productCardComponents[i];
                        current.addEventHandlerToCardAdd(function (target) {
                            var productName = target.name;
                            var cart = CartComponent.getInstance();
                            cart.addProducts(productName);
                        });
                    }
                    for (var i = 0; i < cartLineComponents.length; i++) {
                        var current = cartLineComponents[i];
                        //add cartLine remove
                        current.addHandlerToCartLineRemove(function (target) {
                            var productName = target.name;
                            var cart = CartComponent.getInstance();
                            cart.removeProducts(productName);
                        });
                        //add cartLine changeCount
                        current.addHandlerToCartLineCountChange(function (target) {
                            var productName = target.name;
                            var productCount = target.count;
                            var cart = CartComponent.getInstance();
                            cart.setProductCount(productName, productCount);
                        });
                    }
                }
                CartComponent.prototype.saveChanges = function () {
                    var cookieString = CartComponent.cartCookieName + "=" + escape(JSON.stringify(this.cartLines)) + ";path=/;";
                    document.cookie = cookieString;
                };
                CartComponent.prototype.addAddProductEventHandler = function (eventHandler) {
                    this.addProductEventHandlers.push(eventHandler);
                };
                CartComponent.prototype.addRemoveProductEventHandler = function (eventHandler) {
                    this.removeProductEventHandlers.push(eventHandler);
                };
                CartComponent.getInstance = function () {
                    return CartComponent.instance;
                };
                CartComponent.prototype.initCartByCookies = function () {
                    var cookieCartString = CookieExtensions_1.getCookie(CartComponent.cartCookieName);
                    if (cookieCartString != null) {
                        var cartLines = JSON.parse(unescape(cookieCartString));
                        for (var i = 0; i < cartLines.length; i++) {
                            this.cartLines.push(cartLines[i]);
                        }
                    }
                };
                CartComponent.prototype.getCartLineByProductNameOrDefault = function (name) {
                    for (var i = 0; i < this.cartLines.length; i++) {
                        var currentLine = this.cartLines[i];
                        if (currentLine.Name === name) {
                            return currentLine;
                        }
                    }
                    return null;
                };
                CartComponent.prototype.addProducts = function (name, addedCount) {
                    if (addedCount === void 0) { addedCount = 1; }
                    var cartLine = this.getCartLineByProductNameOrDefault(name);
                    if (cartLine == null) {
                        cartLine = new CartLine_1.CartLine(name, addedCount);
                        this.cartLines.push(cartLine);
                    }
                    else {
                        cartLine.Count += addedCount;
                    }
                    this.callEvents(this.addProductEventHandlers, cartLine);
                    this.saveChanges();
                };
                CartComponent.prototype.setProductCount = function (name, count) {
                    var cartLine = this.getCartLineByProductNameOrDefault(name);
                    if (cartLine == null) {
                        cartLine = new CartLine_1.CartLine(name, count);
                        this.cartLines.push(cartLine);
                    }
                    else {
                        cartLine.Count = count;
                    }
                    this.saveChanges();
                };
                CartComponent.prototype.removeProducts = function (productName) {
                    var cartLine = this.getCartLineByProductNameOrDefault(productName);
                    var cartLineIndex = this.cartLines.indexOf(cartLine);
                    this.cartLines.splice(cartLineIndex, 1);
                    this.callEvents(this.removeProductEventHandlers, cartLine);
                    this.saveChanges();
                };
                Object.defineProperty(CartComponent.prototype, "lines", {
                    get: function () {
                        return this.cartLines;
                    },
                    enumerable: false,
                    configurable: true
                });
                CartComponent.prototype.callEvents = function (events, cartLine) {
                    for (var i = 0; i < events.length; i++) {
                        events[i].call(cartLine);
                    }
                };
                CartComponent.initCartComponent = function () {
                    var productCardComponents = ProductCardViewModel_1.ProductCardViewModel.getProductCards();
                    var cartLineComponents = CartLineViewModel_1.CartLineViewModel.getCartLines();
                    CartComponent.instance = new CartComponent(productCardComponents, cartLineComponents);
                    CartComponent.instance.initCartByCookies();
                };
                CartComponent.cartCookieName = "cart";
                return CartComponent;
            }());
            exports_1("CartComponent", CartComponent);
            CartComponent.initCartComponent();
        }
    };
});
//# sourceMappingURL=CartComponent.js.map