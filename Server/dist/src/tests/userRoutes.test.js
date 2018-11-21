"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const supertest_1 = __importDefault(require("supertest"));
const app_1 = require("../app");
const models_1 = __importDefault(require("../app/models"));
const connection_1 = require("../connection");
describe("User Register tests", () => {
    let app;
    let connDB;
    beforeAll(async () => {
        app = new app_1.Application();
        connDB = new connection_1.Connection();
        connDB.connectTest();
    });
    afterAll(async () => {
        connDB.dropDB();
    });
    it("Router Tests", () => {
        test("Successful register returns 200", async (done) => {
            const json = {
                email: "test@marc.com",
                password: "Thiscleartextisbad",
                username: "marc"
            };
            const conn = supertest_1.default(app.initRouters());
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
        test("Duplicate entry returns 412", async (done) => {
            const json = {
                email: "test@marc.com",
                password: "Thiscleartextisbad",
                username: "marc"
            };
            const conn = supertest_1.default(app.initRouters());
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
        test("Request missing required body data returns 400", async (done) => {
            const json = {
                email: "test@marc2.com"
            };
            const conn = supertest_1.default(app.initRouters());
            spyOn(models_1.default("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/register")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(400)
                .end((err) => {
                if (err) {
                    return done(err);
                }
                return done();
            });
        });
    });
    it("User Login tests", () => {
        test("Successful login returns 200", async (done) => {
            const json = {
                email: "test@marc.com",
                password: "Thiscleartextisbad"
            };
            const conn = supertest_1.default(app.initRouters());
            spyOn(models_1.default("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/login")
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
        test("Login attempt for non-existing user returns 400", async (done) => {
            const json = {
                email: "idontexist@marc.com",
                password: "qqqqqq"
            };
            const conn = supertest_1.default(app.initRouters());
            spyOn(models_1.default("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/login")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(400)
                .end((err) => {
                if (err) {
                    return done(err);
                }
                return done();
            });
        });
    });
    it("User Logout tests", () => {
        test("Successful logout returns 200", async (done) => {
            const json = {
                email: "test@marc.com"
            };
            const conn = supertest_1.default(app.initRouters());
            spyOn(models_1.default("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/logout")
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
        test("Logout attempt for non-existing user returns 400", async (done) => {
            const json = {
                email: "idontexist@marc.com"
            };
            const conn = supertest_1.default(app.initRouters());
            spyOn(models_1.default("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/logout")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(400)
                .end((err) => {
                if (err) {
                    return done(err);
                }
                return done();
            });
        });
    });
});
//# sourceMappingURL=userRoutes.test.js.map