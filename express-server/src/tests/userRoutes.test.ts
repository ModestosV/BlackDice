import request from "supertest";
import { Application } from "../app";
import User from "../app/models";
import { Connection } from "../connection";

describe("User Register tests", () => {
    let app: Application;
    let connDB: Connection;

    beforeAll(async () => {
        app = new Application();
        connDB = new Connection();
        connDB.connectTest();
    });

    afterAll(async () => {
        connDB.dropDB();
    });

    describe("Router Tests", () => {
        it("Successful register returns 201", async (done) => {
            const json = {
                email: "test@marc.com",
                password: "Thiscleartextisbad",
                username: "marc"
            };

            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/register")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(201);

            done();
        });

        it("Duplicate entry returns 412", async (done) => {
            const json = {
                email: "test@marc.com",
                password: "Thiscleartextisbad",
                username: "marc"
            };
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/register")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(412);

            done();
        });

        it("Request missing required body data returns 400", async (done) => {
            const json = {
                email: "test@marc2.com"
            };
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/register")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(400);

            done();
        });
    });

    describe("User Login tests", () => {
        it("Successful login returns 200", async (done) => {
            const json = {
                email: "test@marc.com",
                password: "Thiscleartextisbad"
            };
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/login")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(200);

            done();
        });

        it("Login attempt for non-existing user returns 400", async (done) => {
            const json = {
                email: "idontexist@marc.com",
                password: "qqqqqq"
            };
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/login")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(400);

            done();
        });
    });

    describe("User Logout tests", () => {
        it("Successful logout returns 200", async (done) => {
            const json = {
                email: "test@marc.com"
            };
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/logout")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(200);

            done();
        });

        it("Logout attempt for non-existing user returns 400", async (done) => {
            const json = {
                email: "idontexist@marc.com"
            };
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/account/logout")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(400);

            done();
        });
    });
});
