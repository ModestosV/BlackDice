"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const crypto_js_1 = __importDefault(require("crypto-js"));
const body_parser_1 = __importDefault(require("body-parser"));
const lodash_1 = __importDefault(require("lodash"));
const moment_1 = __importDefault(require("moment"));
const models_1 = __importDefault(require("../../app/models"));
const middlewares_1 = require("../../utils/middlewares");
class UserRoutersClass {
    constructor(r) {
        this.router = r;
        this.User = models_1.default("User");
    }
    Register() {
        this.router.post("/register", body_parser_1.default.json(), async (req, res, next) => {
            try {
                global.console.log("register request going through");
                global.console.log("BOI");
                const username = req.body.username;
                const passHash = req.body.password;
                const email = req.body.email;
                global.console.log("BOI1");
                if (username && passHash && email) {
                    global.console.log("BOI2");
                    const salt = moment_1.default();
                    const finalHash = crypto_js_1.default.SHA512(passHash, salt.toString()).toString();
                    global.console.log("BOI3");
                    const userData = new this.User({
                        createdAt: salt,
                        email,
                        loggedIn: false,
                        passwordHash: finalHash,
                        username
                    });
                    await userData.save();
                    res.status(200);
                    return res.json(userData); // TODO: Add the passwordless token generation
                }
                global.console.log("BOI4");
                res.status(400);
                return res.json("Request invalid");
            }
            catch (err) {
                global.console.log("BOI5 >:(");
                return next(err);
            }
        }, middlewares_1.errorHandler);
    }
    Login() {
        this.router.post("/login", body_parser_1.default.json(), async (req, res, next) => {
            try {
                global.console.log("login request going through");
                global.console.log(req.body);
                // TODO: Token Validation & password validation
                const passHash = req.body.password;
                const email = req.body.email;
                const loginQuery = {
                    email
                };
                const userDoc = await this.User.findOne(loginQuery).exec();
                if (!userDoc) {
                    res.status(400);
                    return res.json("Email does not exist");
                }
                else {
                    const finalHash = crypto_js_1.default.SHA512(passHash, userDoc.get("createdAt").toString()).toString();
                    if (lodash_1.default.isEqual(finalHash, userDoc.get("passwordHash"))) {
                        userDoc.set("loggedIn", true);
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
                // TODO: Token Validation needs to be done
                const passHash = req.body.password;
                const email = req.body.email;
                const loginQuery = {
                    email
                };
                const userDoc = await this.User.findOne(loginQuery).exec();
                if (!userDoc) {
                    res.status(400);
                    return res.json("Email does not exist");
                }
                else {
                    // TODO: Needs to be changed to use token validation and check if the user is logged in.
                    const finalHash = crypto_js_1.default.SHA512(passHash, userDoc.get("createdAt").toString()).toString();
                    if (lodash_1.default.isEqual(finalHash, userDoc.get("passwordHash"))) {
                        userDoc.set("loggedIn", false);
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
                        return res.json("Incorrect password");
                    }
                }
            }
            catch (err) {
                return next(err);
            }
        }, middlewares_1.errorHandler);
    }
}
exports.UserRoutersClass = UserRoutersClass;
//# sourceMappingURL=UserRoutersClass.js.map