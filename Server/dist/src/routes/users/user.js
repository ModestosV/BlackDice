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
const errors_1 = __importDefault(require("../../utils/errors"));
const middlewares_1 = require("../../utils/middlewares");
const router = express_1.default.Router();
const User = mongoose_1.default.model("User");
router.post("/register", body_parser_1.default.json, async (req, res, next) => {
    try {
        global.console.log("register request going through");
        const passHash = req.body.password;
        const email = req.body.email;
        if (passHash && email) {
            const salt = moment_1.default();
            const finalHash = await bcrypt_1.default.hash(passHash, moment_1.default.toString());
            const userData = {
                createdAt: salt,
                email: { email },
                loggedIn: false,
                password: finalHash
            };
            const userDoc = await User.create(userData);
            if (userDoc) {
                return res.json(errors_1.default(200));
            }
            else {
                throw new Error("A database error occured while registering");
            }
        }
        return res.json(errors_1.default(400));
    }
    catch (err) {
        return next(err);
    }
}, middlewares_1.errorHandler);
router.post("/login", body_parser_1.default.json, async (req, res, next) => {
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
router.post("/logout", body_parser_1.default.json, async (req, res, next) => {
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