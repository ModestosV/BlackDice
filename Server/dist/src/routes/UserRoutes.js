"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
/**
 * IMPORTS FOR INTERFACES, CLASSES OR DEPENDENCIES
 */
const body_parser_1 = __importDefault(require("body-parser"));
const crypto_js_1 = __importDefault(require("crypto-js"));
const express_1 = __importDefault(require("express"));
const lodash_1 = __importDefault(require("lodash"));
const moment_1 = __importDefault(require("moment"));
/**
 * HELPER FUNCTIONS
 */
const models_1 = __importDefault(require("../app/models"));
const middlewares_1 = require("../utils/middlewares");
const utils_1 = require("../utils/utils");
class UserRoutes {
    constructor(router, user) {
        this.router = router;
        this.user = user;
    }
    Register() {
        this.router.post("/register", body_parser_1.default.json(), async (req, res, next) => {
            try {
                global.console.log("register request going through");
                const username = req.body.username;
                const passHash = req.body.password;
                const email = req.body.email;
                if (username && passHash && email) {
                    const salt = moment_1.default();
                    const finalHash = crypto_js_1.default.SHA512(passHash, salt.toString()).toString();
                    const userData = new this.user({
                        createdAt: salt,
                        email,
                        loggedInToken: undefined,
                        passwordHash: finalHash,
                        username
                    });
                    await userData.save();
                    res.status(200);
                    return res.json(userData);
                }
                res.status(400);
                return res.json("Request invalid");
            }
            catch (err) {
                const isDuplicate = err.message.toString().indexOf("duplicate") !== -1 ? true : false;
                if (isDuplicate) {
                    res.status(412);
                    return res.json("Duplicate Key");
                }
                return next(err);
            }
        }, middlewares_1.errorHandler);
    }
    Login() {
        this.router.post("/login", body_parser_1.default.json(), async (req, res, next) => {
            try {
                global.console.log("login request going through");
                const passHash = req.body.password;
                const email = req.body.email;
                const loginQuery = {
                    email
                };
                const userDoc = await this.user.findOne(loginQuery).exec();
                if (!userDoc) {
                    res.status(400);
                    return res.json("Email does not exist");
                }
                else {
                    const finalHash = crypto_js_1.default.SHA512(passHash, userDoc.get("createdAt").toString()).toString();
                    const token = utils_1.getToken(20); // Random token each time this request is made.
                    if (lodash_1.default.isEqual(finalHash, userDoc.get("passwordHash"))) {
                        userDoc.set("loggedInToken", token);
                        const updatedDoc = await userDoc.save();
                        if (updatedDoc) {
                            res.status(200);
                            return res.json(token);
                        }
                        else {
                            res.status(500);
                            return res.json("Server error");
                        }
                    }
                    else {
                        res.status(400);
                        return res.json("Incorrect password");
                    }
                }
            }
            catch (err) {
                return next(err);
            }
        }, middlewares_1.errorHandler);
    }
    Logout() {
        this.router.post("/logout", body_parser_1.default.json(), async (req, res, next) => {
            try {
                global.console.log("logout request going through");
                global.console.log(req.body);
                const email = req.body.email;
                const loginQuery = {
                    email
                };
                const userDoc = await this.user.findOne(loginQuery).exec();
                if (!userDoc) {
                    res.status(400);
                    return res.json("Email does not exist");
                }
                else {
                    if (userDoc.get("loggedInToken")) {
                        userDoc.set("loggedInToken", undefined);
                        const updatedDoc = await userDoc.save();
                        if (updatedDoc) {
                            res.status(200);
                            return res.json(updatedDoc);
                        }
                        else {
                            res.status(500);
                            return res.json("Server error");
                        }
                    }
                    else {
                        res.status(400);
                        return res.json("User is not loggedIn");
                    }
                }
            }
            catch (err) {
                return next(err);
            }
        }, middlewares_1.errorHandler);
    }
}
exports.UserRoutes = UserRoutes;
const userRoutes = new UserRoutes(express_1.default.Router(), models_1.default("User"));
userRoutes.Register();
userRoutes.Login();
userRoutes.Logout();
exports.default = userRoutes.router;
//# sourceMappingURL=UserRoutes.js.map