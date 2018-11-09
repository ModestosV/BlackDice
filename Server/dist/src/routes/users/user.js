"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const models_1 = __importDefault(require("../../app/models"));
const UserRoutersClass_1 = require("./UserRoutersClass");
const userRouter = new UserRoutersClass_1.UserRoutersClass(express_1.default.Router());
const User = models_1.default("User");
userRouter.Register();
userRouter.Login();
userRouter.Logout();
exports.default = userRouter.router;
//# sourceMappingURL=user.js.map