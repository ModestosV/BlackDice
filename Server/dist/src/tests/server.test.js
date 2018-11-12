"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const supertest_1 = __importDefault(require("supertest"));
const app_1 = __importDefault(require("../app"));
const models_1 = __importDefault(require("../app/models"));
describe("Testing the inital call to the root location of the server", () => {
    test("Route that doesn't exist", async () => {
        const conn = supertest_1.default(app_1.default);
        const response = await conn.get("/");
        expect(response.status).toBe(404);
    });
    test("Test Register Router Improperly", () => {
        const conn = supertest_1.default(app_1.default);
        conn.post("/account/register").expect(400);
    });
    test("Test Register Router", async (done) => {
        const json = {
            email: "test@marc2.com",
            password: "Thiscleartextisbad",
            username: "marc2"
        };
        const conn = supertest_1.default(app_1.default);
        spyOn(models_1.default("User"), "create").and.returnValue(Promise.resolve(json));
        conn.post("/account/register")
            .send(json)
            .set("Content-Type", "application/json")
            .expect(200)
            .end((err) => {
            if (err) {
                return done(err);
            }
            return done();
        });
    });
    test("Test Register Router", async (done) => {
        const json = {
            email: "test@marc2.com",
            password: "Thiscleartextisbad",
            username: "marc2"
        };
        const conn = supertest_1.default(app_1.default);
        spyOn(models_1.default("User"), "create").and.returnValue(Promise.resolve(json));
        conn.post("/account/register")
            .send(json)
            .set("Content-Type", "application/json")
            .expect(412)
            .end((err) => {
            if (err) {
                return done(err);
            }
            return done();
        });
    });
});
//# sourceMappingURL=server.test.js.map