"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function errorHandler(err, req, res, next) {
    global.console.error('Error:');
    global.console.error(err);
    return res.json(err);
}
exports.errorHandler = errorHandler;
//# sourceMappingURL=middlewares.js.map