"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const mongoose_1 = __importDefault(require("mongoose"));
const User_1 = __importDefault(require("./models/User"));
mongoose_1.default.connect("mongodb://192.168.99.100/blackdice", { useNewUrlParser: true });
mongoose_1.default.set("useCreateIndex", true);
/**
 * THIS IS WERE WE INSTANTIATE THE DB MODELS
 */
mongoose_1.default.model("User", User_1.default);
/**
 * THIS IS WERE THE CONNECTION GETS VALIDATED
 */
const db = mongoose_1.default.connection;
db.on("error", global.console.error.bind(console, "Connection error:"));
db.once("open", () => global.console.log("Connected to the database named blackdice"));
//# sourceMappingURL=connection.js.map