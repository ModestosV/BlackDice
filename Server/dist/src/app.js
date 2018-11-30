"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const router_1 = __importDefault(require("./routes/router"));
class Application {
    constructor() {
        this.app = express_1.default();
    }
    /**
     * initRouters
     */
    initRouters() {
        this.app.use(router_1.default);
        this.app.use(this.logErrorHandler);
        return this.app;
    }
    logErrorHandler(req, res, next) {
        // If an error gets here everything should explode because something stupid happened or forgot to catch an error.
        const err = new Error("404 - Not Found");
        err.name = "404";
        res.statusCode = 404;
        global.console.error("Error:");
        global.console.error(err.name);
        global.console.error(err.message);
        global.console.error(err.stack);
        throw err;
    }
}
exports.Application = Application;
//# sourceMappingURL=app.js.map