System.register([], function (exports_1, context_1) {
    "use strict";
    var CartLine;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [],
        execute: function () {
            CartLine = /** @class */ (function () {
                function CartLine(name, count) {
                    this.Name = name;
                    this.Count = count;
                }
                return CartLine;
            }());
            exports_1("CartLine", CartLine);
        }
    };
});
//# sourceMappingURL=CartLine.js.map