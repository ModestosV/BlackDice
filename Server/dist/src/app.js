"use strict";
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (Object.hasOwnProperty.call(mod, k)) result[k] = mod[k];
    result["default"] = mod;
    return result;
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
const router = __importStar(require("./routes/router"));
const express_1 = __importDefault(require("express"));
const app = express_1.default();
require('./connection');
app.use(router.default);
app.use(logErrorHandler);
function logErrorHandler(req, res, next) {
    // If an error gets here everything should explode because I did something stupid or forgot to do something.
    let err = new Error('404 - Not Found');
    err.name = '404';
    res.statusCode = 404;
    global.console.error('Error:');
    global.console.error(err.name);
    global.console.error(err.message);
    global.console.error(err.stack);
    throw err;
}
module.exports = app;
//# sourceMappingURL=app.js.map