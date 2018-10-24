"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const mongoose_1 = __importDefault(require("mongoose"));
const Types = mongoose_1.default.Schema.Types;
const userSchema = new mongoose_1.default.Schema({
    createdAt: { type: Types.Date, required: true },
    email: { type: Types.String, required: true, unique: true },
    givenname: { type: Types.String, required: true },
    passwordHash: { type: Types.String, required: true },
    surname: { type: Types.String, required: true },
});
exports.default = userSchema;
//# sourceMappingURL=User.js.map