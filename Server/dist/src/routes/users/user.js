"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const body_parser_1 = __importDefault(require("body-parser"));
const express_1 = __importDefault(require("express"));
const lodash_1 = __importDefault(require("lodash"));
const moment_1 = __importDefault(require("moment"));
const mongoose_1 = __importDefault(require("mongoose"));
const User_1 = __importDefault(require("../../models/User"));
const middlewares_1 = require("../../utils/middlewares");
const router = express_1.default.Router();
const User = mongoose_1.default.model("User", User_1.default);
router.post("/register", body_parser_1.default.json(), async (req, res, next) => {
    try {
        global.console.log("register request going through");
        global.console.log(req.body);
        const username = req.body.username;
        const passHash = req.body.password;
        const email = req.body.email;
        if (username && passHash && email) {
            const salt = moment_1.default();
            const finalHash = passHash;
            const userData = {
                createdAt: salt,
                email: email,
                username: username,
                loggedIn: false,
                passwordHash: finalHash
            };
            User.create(userData);
            res.status(200);
            return res.json(userData);
        }
        res.status(400);
        return res.json("Request invalid");
    }
    catch (err) {
        return next(new Error("A database error occured while registering"));
    }
}, middlewares_1.errorHandler);
router.post("/login", body_parser_1.default.json(), async (req, res, next) => {
    try {
        global.console.log("login request going through");
        global.console.log(req.body);
        const passHash = req.body.password;
        const email = req.body.email;
        const loginQuery = {
            email: email
        };
        const userDoc = await User.findOne(loginQuery).exec();
        if (!userDoc) {
            res.status(400);
            return res.json("Email does not exist");
        }
        else {
            if (lodash_1.default.isEqual(passHash, userDoc.get("passwordHash"))) {
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
router.post("/logout", body_parser_1.default.json(), async (req, res, next) => {
    try {
        global.console.log("logout request going through");
        global.console.log(req.body);
        const passHash = req.body.password;
        const email = req.body.email;
        const loginQuery = {
            email: email
        };
        const userDoc = await User.findOne(loginQuery).exec();
        if (!userDoc) {
            res.status(400);
            return res.json("Email does not exist");
        }
        else {
            if (lodash_1.default.isEqual(passHash, userDoc.get("passwordHash"))) {
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
exports.default = router;
//# sourceMappingURL=user.js.map