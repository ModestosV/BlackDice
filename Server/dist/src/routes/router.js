"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
/**
 * IMPORTS
 */
const express_1 = __importDefault(require("express"));
const UserRoutes_1 = __importDefault(require("./UserRoutes"));
/**
 * CONSTANT PROPERTIES
 */
const router = express_1.default.Router();
router.use("/account", UserRoutes_1.default);
exports.default = router;
//# sourceMappingURL=router.js.map