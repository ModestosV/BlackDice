"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const mongoose_1 = __importDefault(require("mongoose"));
const router = express_1.default.Router();
const userAccount = mongoose_1.default.model('User');
router.get('/account_info', async (req, res, next) => {
    const email = req.body.userID;
    if (!email) {
        global.console.log('This user ID is empty. Nice');
        global.console.log('Not really');
        return next(new Error('Not valide user ID'));
    }
    const user = await userAccount.find({ email: { email } }).exec();
    if (!user) {
        global.console.log('User needs to be created');
    }
}, errorHandler);
function errorHandler(err, req, res, next) {
    global.console.error('Error:');
    global.console.error(err);
    return res.json(err);
}
exports.default = router;
//# sourceMappingURL=users.js.map