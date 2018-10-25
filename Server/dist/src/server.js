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
const httpServer = __importStar(require("http"));
const app_1 = __importDefault(require("./app"));
const stamp = { pattern: "UTC:yyyy-mm-dd'T'HH:MM:ss".toString() };
let server = null;
const host = 'localhost';
const port = 5500;
console_stamp_1.default(console, stamp);
server = httpServer.createServer(app_1.default);
server.listen(port, host);
server.on('error', logError);
server.on('listening', listen);
function logError(err, req, res, next) {
    // If an error gets here everything should explode because I did something stupid or forgot to do something.
    res.status(res.statusCode || 500);
    global.console.error('Error:');
    global.console.error(err.name);
    global.console.error(err.message);
    global.console.error(err.stack);
    throw err;
}
function listen() {
    let stringErr = 'Server is currently listing on: ';
    stringErr = stringErr.concat(host, ' ', port.toString());
    global.console.log('');
    global.console.log(stringErr);
    global.console.log('');
}
//# sourceMappingURL=server.js.map