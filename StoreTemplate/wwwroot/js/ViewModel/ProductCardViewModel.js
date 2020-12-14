System.register([], function (exports_1, context_1) {
    "use strict";
    var ProductCardViewModel;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [],
        execute: function () {
            ProductCardViewModel = /** @class */ (function () {
                function ProductCardViewModel(element) {
                    this.productCardElement = element;
                }
                ProductCardViewModel.getProductCards = function () {
                    var productCards = document.querySelectorAll(ProductCardViewModel.productCardSelector);
                    var viewComponents = new Array();
                    for (var i = 0; i < productCards.length; i++) {
                        var current = productCards[i];
                        viewComponents.push(new ProductCardViewModel(current));
                    }
                    return viewComponents;
                };
                ProductCardViewModel.prototype.addEventHandlerToCardAdd = function (eventHandler) {
                    var _this = this;
                    var addButton = this.productCardElement.querySelector(ProductCardViewModel.productCardAddButtonSelector);
                    addButton.addEventListener(ProductCardViewModel.addButtonEventType, function () { eventHandler(_this); });
                };
                Object.defineProperty(ProductCardViewModel.prototype, "name", {
                    get: function () {
                        var nameItem = this.productCardElement.querySelector(ProductCardViewModel.productCardNameSelector);
                        var productName = nameItem.textContent;
                        return productName;
                    },
                    enumerable: false,
                    configurable: true
                });
                //selectors
                ProductCardViewModel.productCardSelector = "div.product-card";
                ProductCardViewModel.productCardAddButtonSelector = "button.product-card-add-btn";
                ProductCardViewModel.productCardNameSelector = ".product-name";
                //static fields
                ProductCardViewModel.addButtonEventType = "click";
                return ProductCardViewModel;
            }());
            exports_1("ProductCardViewModel", ProductCardViewModel);
        }
    };
});
//# sourceMappingURL=ProductCardViewModel.js.map