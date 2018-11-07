"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const errors_1 = __importDefault(require("./errors"));
function errorHandler(err, req, res, next) {
    global.console.error("Error:");
    global.console.error(err.message);
    global.console.error(err.stack);
    return res.json(errors_1.default(500));
}
exports.errorHandler = errorHandler;
//# sourceMappingURL=middlewares.js.map