"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const middlewares_1 = require("../utils/middlewares");
const user_1 = __importDefault(require("./users/user"));
const router = express_1.default.Router();
router.get("/", (req, res, next) => {
    const response = {
        greeting: "Hello World"
    };
    return res.json(response);
});
router.use("/account", user_1.default);
router.get("/chracterInfo", (req, res, next) => {
    if (!req.params.characterID) {
        global.console.log("This character ID is empty. Nice");
        global.console.log("Not really");
        return next(new Error("Not valide Chracter ID"));
    }
}, middlewares_1.errorHandler);
exports.default = router;
//# sourceMappingURL=router.js.map