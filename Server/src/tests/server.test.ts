import request from "supertest";
import app from "../app";
import User from "../app/models";

describe("Testing the inital call to the root location of the server", () => {
    test("Route that doesn't exist", async () => {
        const conn = request(app);
        const response = await conn.get("/");
        expect(response.status).toBe(404);
    });

    test("Test Register Router Improperly", () => {
        const conn = request(app);
        conn.post("/account/register").expect(400);
    });

    test("Test Register Router", async (done) => {
        const json = {
            email: "test@marc2.com",
            password: "Thiscleartextisbad",
            username: "marc2"
        };
        const conn = request(app);
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

    test("Test Register Router", async (done) => {
        const json = {
            email: "test@marc2.com",
            password: "Thiscleartextisbad",
            username: "marc2"
        };
        const conn = request(app);
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
});
