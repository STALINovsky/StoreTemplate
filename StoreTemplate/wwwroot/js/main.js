var Model;
(function (Model) {
    var Product = /** @class */ (function () {
        function Product(name, price, imagePath) {
            this.Name = name;
            this.Price = price;
            this.ImagePath = imagePath;
        }
        return Product;
    }());
    Model.Product = Product;
})(Model || (Model = {}));
/// <reference path="Product.ts" />
var Model;
(function (Model) {
    var UserCart = /** @class */ (function () {
        function UserCart() {
            this.Products = new Array();
        }
        UserCart.prototype.AddProduct = function (product) {
            this.Products.push(product);
        };
        UserCart.prototype.RemoveProduct = function (product) {
            var deletedProductId = this.Products.indexOf(product);
            this.Products.slice(deletedProductId, 1);
        };
        return UserCart;
    }());
    Model.Cart = new UserCart();
})(Model || (Model = {}));
/// <reference path="Model/Product.ts" />
/// <reference path="Model/Cart.ts" />
//# sourceMappingURL=main.js.map