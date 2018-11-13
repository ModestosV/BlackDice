"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const errorMessages = {
    500: JSON.parse('{ "500" :"Update failed" }'),
    400: JSON.parse('{ "400" :"Bad Request" }'),
    200: JSON.parse('{ "200" : "Request was completed successfully" }')
};
function getStatus(statusCode) {
    return errorMessages[statusCode.toString()];
}
exports.default = getStatus;
//# sourceMappingURL=errors.js.map