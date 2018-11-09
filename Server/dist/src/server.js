"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (Object.hasOwnProperty.call(mod, k)) result[k] = mod[k];
    result["default"] = mod;
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
// Starting the server code
const console_stamp_1 = __importDefault(require("console-stamp"));
const dotenv_1 = __importDefault(require("dotenv"));
const httpServer = __importStar(require("http"));
const app_1 = __importDefault(require("./app"));
const stamp = { pattern: "UTC:yyyy-mm-dd'T'HH:MM:ss" };
dotenv_1.default.config();
let server = null;
const host = process.env.HOST || "localhost";
const port = parseInt(process.env.PORT ? process.env.PORT : "5500", 10);
console_stamp_1.default(console, stamp);
server = httpServer.createServer(app_1.default);
server.listen(port, host);
server.on("error", handleError);
server.on("listening", listen);
function handleError(err, req, res, next) {
    // If an error gets here everything should explode because I did something stupid or forgot to do something.
    const error = {
        code: res.statusCode || 500,
        message: err.message,
        name: err.name
    };
    return res.json(error);
}
function listen() {
    let stringMessage = "Server is currently listing on: ";
    stringMessage = stringMessage.concat(host, " ", port.toString());
    global.console.log("");
    global.console.log(stringMessage);
    global.console.log("");
}
//# sourceMappingURL=server.js.map