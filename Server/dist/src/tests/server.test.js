"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const app = require('../server');
const supertest_1 = __importDefault(require("supertest"));
let server;
beforeAll(() => {
    server = supertest_1.default(app);
});
describe('Testing the inital call to the root location of the server', () => {
    test('Root Call', async () => {
        const response = await server.get('/');
        expect(response.status).toBe(200);
    });
});
//# sourceMappingURL=server.test.js.map