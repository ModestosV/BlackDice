import request from "supertest";
import { Application } from "../app";
import User from "../app/models";
import { Connection } from "../connection";

describe("Feedback Routes tests", () => {
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

    describe("Send Feedback Tests", () => {
        it("Successful send returns 200", async (done) => {
            const json = {
                email: "foo@bar.com",
                message: "This game rocks!"
            };

            const conn = request(app.initRouters());
            spyOn(User("Feedback"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/feedback/send")
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

        it("Request missing required body data returns 400", async (done) => {
            const json = {
                email: "no@message.com"
            };
            const conn = request(app.initRouters());
            spyOn(User("Feedback"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/feedback/send")
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
