"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const app_1 = __importDefault(require("../../app"));
const express_1 = __importDefault(require("express"));
const middlewares_1 = require("../../utils/middlewares");
const router = express_1.default.Router();
// using the app to store local variables is a bad practice but fro local dev it is ok, until we get the database up...
// TODO remove app variables and install proper data checks with database.
router.post('/register', (req, res, next) => {
    global.console.log('register request going through');
    let response1 = {
        password: req.param("password", "super top secret password"),
        email: req.param("email", "email")
    };
    return res.json(response1);
}, middlewares_1.errorHandler);
router.get('/register', (req, res, next) => {
    app_1.default.locals = {};
    app_1.default.locals.email = req.query.email;
    app_1.default.locals.password = req.query.password;
    return res.send('true');
});
router.get('/login', (req, res, next) => {
    if (req.query.email == app_1.default.locals.email && req.query.password == app_1.default.locals.password) {
        app_1.default.locals.loggedInUser = req.query.email;
        res.send('true');
    }
    else {
        res.send('false');
    }
});
router.get('/logout', (req, res, next) => {
    if (req.query.email == app_1.default.locals.loggedInUser) {
        app_1.default.locals.loggedInUser = null;
        res.send('true');
    }
    else {
        res.send('false');
    }
});
exports.default = router;
//# sourceMappingURL=user.js.map