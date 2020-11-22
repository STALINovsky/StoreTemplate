System.register([], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    function getCookie(name) {
        var cookieName = name + "=";
        var cookieRecords = document.cookie.split(";");
        for (var i = 0; i < cookieRecords.length; i++) {
            var currentRecord = cookieRecords[i];
            while (currentRecord.charAt(0) === " ") {
                currentRecord = currentRecord.substring(1, currentRecord.length);
            }
            if (currentRecord.indexOf(cookieName) === 0)
                return currentRecord.substring(cookieName.length, currentRecord.length);
        }
        return null;
    }
    exports_1("getCookie", getCookie);
    return {
        setters: [],
        execute: function () {
        }
    };
});
//# sourceMappingURL=CookieExtensions.js.map