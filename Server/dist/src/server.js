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
server.on('error', handleError);
server.on('listening', listen);
// Routes (Weren't working properly in router.ts, investigate...)
app_1.default.get('/register', (req, res, next) => {
    app_1.default.locals.email = req.query.email;
    app_1.default.locals.password = req.query.password;
    return res.send('true');
});
app_1.default.get('/login', (req, res, next) => {
    if (req.query.email == app_1.default.locals.email && req.query.password == app_1.default.locals.password) {
        app_1.default.locals.loggedInUser = req.query.email;
        res.send('true');
    }
    else {
        res.send('false');
    }
});
app_1.default.get('/logout', (req, res, next) => {
    if (req.query.email == app_1.default.locals.loggedInUser) {
        app_1.default.locals.loggedInUser = null;
        res.send('true');
    }
    else {
        res.send('false');
    }
});
function handleError(err, req, res, next) {
    // If an error gets here everything should explode because I did something stupid or forgot to do something.
    let error = {
        code: res.statusCode || 500,
        name: err.name,
        message: err.message
    };
    return res.json(error);
}
function listen() {
    let stringMessage = 'Server is currently listing on: ';
    stringMessage = stringMessage.concat(host, ' ', port.toString());
    global.console.log('');
    global.console.log(stringMessage);
    global.console.log('');
}
//# sourceMappingURL=server.js.map