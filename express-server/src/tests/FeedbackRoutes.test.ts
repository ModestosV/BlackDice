import cryptojs from "crypto-js";
import request from "supertest";
import { Application } from "../app";
import Models from "../app/models";
import { Connection } from "../connection";
import { getToken } from "../utils/utils";

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
            spyOn(Models("Feedback"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/feedback/send")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(200);

            done();
        });

        it("Request missing required body data returns 400", async (done) => {
            const json = {
                email: "no@message.com"
            };
            const conn = request(app.initRouters());
            spyOn(Models("Feedback"), "create").and.returnValue(Promise.resolve(json));
            conn.post("/feedback/send")
                .send(json)
                .set("Content-Type", "application/json")
                .expect(400);

            done();
        });
    });

    describe("Send Feedback Tests", () => {
        it("Successful send returns 200", async (done) => {
            // todo : add createdat is admin and other stuff...

            const json = {
                email: "noAdmin@test.com",
                usersname: "FeedbackAdmin",
                password: cryptojs.SHA1("Test").toString(),
                isAdmin: true
            };

            const jsonLogin = {
              username : json.usersname,
              password : json.password
            };

            const logginedInToken = getToken(20);

            const jsonToken = {
              token: logginedInToken,
            };

            const conn = request(app.initRouters());
            spyOn(Models("User"), "create").and.returnValue(Promise.resolve(json));
            spyOn(Models("User"), "update").and.returnValue(Promise.resolve(logginedInToken));

            conn.post("/account/register")
                .send(json)
                .set("Content-Type", "application/json");

            const token = conn.post("/account/login")
                .send(jsonLogin)
                .set("Content-Type", "application/json")
                .expect(200);

            conn.get("/feedback/all/" + token)
                .expect(200);

            conn.post("/account/logout/token")
                .send(jsonToken)
                .expect(200);

            done();
        });
    });
});
