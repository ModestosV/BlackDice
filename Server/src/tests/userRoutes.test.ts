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

    it("Router Tests", () => {
        test("Successful register returns 200", async (done) => {
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
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
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
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
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
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
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
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
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
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
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
            const conn = request(app.initRouters());
            spyOn(User("User"), "create").and.returnValue(Promise.resolve(json));
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
