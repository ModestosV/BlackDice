"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function errorHandler(err, req, res, next) {
    global.console.error("Error:");
    global.console.error(err.message);
    global.console.error(err.stack);
    return res.json({ 500: "Internal Server Error" });
}
exports.errorHandler = errorHandler;
//# sourceMappingURL=middlewares.js.map