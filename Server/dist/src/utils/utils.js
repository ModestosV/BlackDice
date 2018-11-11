"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const bs58_1 = __importDefault(require("bs58"));
const crypto_1 = __importDefault(require("crypto"));
function getToken(bytes) {
    bytes = bytes || 16;
    const buf = crypto_1.default.randomBytes(bytes);
    return bs58_1.default.encode(buf);
}
exports.getToken = getToken;
//# sourceMappingURL=utils.js.map