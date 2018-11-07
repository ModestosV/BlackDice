"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const bcrypt_1 = __importDefault(require("bcrypt"));
const body_parser_1 = __importDefault(require("body-parser"));
const express_1 = __importDefault(require("express"));
const lodash_1 = __importDefault(require("lodash"));
const moment_1 = __importDefault(require("moment"));
const mongoose_1 = __importDefault(require("mongoose"));
const User_1 = __importDefault(require("../../models/User"));
const errors_1 = __importDefault(require("../../utils/errors"));
const middlewares_1 = require("../../utils/middlewares");
const router = express_1.default.Router();
const User = mongoose_1.default.model("User", User_1.default);
router.post("/register", body_parser_1.default.json(), async (req, res, next) => {
    try {
        global.console.log("register request going through");
        global.console.log(req.body);
        const passHash = req.body.password;
        const email = req.body.email;
        const userName = req.body.username;
        if (passHash && email) {
            const salt = moment_1.default();
            const finalHash = passHash;
            const userData = {
                createdAt: salt,
                email,
                username: userName,
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
        const passHash = req.body.password;
        const email = req.body.email;
        const loginQuery = {
            email: { email }
        };
        const userDoc = await User.findOne(loginQuery).exec();
        if (!userDoc) {
            throw new Error("A database error occured while logging in");
        }
        else {
            const hash = bcrypt_1.default.hash(passHash, userDoc.get("createdAt"));
            if (lodash_1.default.isEqual(hash, userDoc.get("password"))) {
                userDoc.set("loggedIn", true);
                const updatedDoc = await userDoc.save();
                if (updatedDoc) {
                    return res.json(errors_1.default(200));
                }
                else {
                    throw new Error("A database error occured while logging");
                }
            }
            else {
                return res.send(errors_1.default(400));
            }
        }
    }
    catch (err) {
        return next(err);
    }
}, middlewares_1.errorHandler);
router.post("/logout", body_parser_1.default.json(), async (req, res, next) => {
    try {
        const passHash = req.body.password;
        const email = req.body.email;
        const loginQuery = {
            email: { email }
        };
        const userDoc = await User.findOne(loginQuery).exec();
        if (!userDoc) {
            throw new Error("A database error occured while logging out");
        }
        else {
            const hash = bcrypt_1.default.hash(passHash, userDoc.get("createdAt"));
            if (lodash_1.default.isEqual(hash, userDoc.get("password"))) {
                userDoc.set("loggedIn", false);
                const updatedDoc = await userDoc.save();
                if (updatedDoc) {
                    return res.json(errors_1.default(200));
                }
                else {
                    throw new Error("A database error occured while logging out");
                }
            }
            else {
                return res.send(errors_1.default(400));
            }
        }
    }
    catch (err) {
        return next(err);
    }
}, middlewares_1.errorHandler);
exports.default = router;
//# sourceMappingURL=user.js.map