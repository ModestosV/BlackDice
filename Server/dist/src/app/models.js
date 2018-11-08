"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const mongoose_1 = __importDefault(require("mongoose"));
const User_1 = __importDefault(require("../models/User"));
const schemas = {
    User: mongoose_1.default.model("User", User_1.default)
};
function getModel(name) {
    return schemas[name];
}
exports.default = getModel;
//# sourceMappingURL=models.js.map