import request from "supertest";

describe("Testing the inital call to the root location of the server", () => {
    test("Root Call", async () => {
        const app = require("../app");
        const conn = request(app);
        const response = await conn.get("/");
        expect(response.status).toBe(200);
    });
});
