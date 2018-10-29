"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const bcrypt_1 = __importDefault(require("bcrypt"));
const express_1 = __importDefault(require("express"));
const lodash_1 = __importDefault(require("lodash"));
const moment_1 = __importDefault(require("moment"));
const mongoose_1 = __importDefault(require("mongoose"));
const middlewares_1 = require("../../utils/middlewares");
const router = express_1.default.Router();
const User = mongoose_1.default.model("User");
// using the app to store local variables is a bad practice but fro local dev it is ok, until we get the database up...
// TODO remove app variables and install proper data checks with database.
router.post("/register", async (req, res, next) => {
    try {
        global.console.log("register request going through");
        const passHash = req.query.password;
        const email = req.query.email;
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
                return res.json({ 200: "Request was completed successfully" });
            }
            else {
                throw new Error("A database error occured while registering");
            }
        }
        return res.json({ 400: "Bad Request" });
    }
    catch (err) {
        return next(err);
    }
}, middlewares_1.errorHandler);
router.post("/login", async (req, res, next) => {
    try {
        const email = req.query.email;
        const passHash = req.query.password;
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
                    return res.json({ 200: "Request was completed successfully" });
                }
                else {
                    return res.send({ 500: "Update failed" });
                }
            }
            else {
                return res.send({ 400: "Bad Request" });
            }
        }
    }
    catch (err) {
        return next(err);
    }
}, middlewares_1.errorHandler);
router.post("/logout", async (req, res, next) => {
    // TODO finish the logout router.
    try {
        const email = req.query.email;
        const passHash = req.query.password;
        const loginQuery = {
            email
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
                    return res.json({ 200: "Request was completed successfully" });
                }
                else {
                    return res.send({ 500: "Update failed" });
                }
            }
            else {
                return res.send({ 400: "Bad Request" });
            }
        }
    }
    catch (err) {
        return next(err);
    }
}, middlewares_1.errorHandler);
exports.default = router;
//# sourceMappingURL=user.js.map