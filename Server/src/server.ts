// Starting the server code
import consoleStamp from "console-stamp";
import dotenv from "dotenv";
import { NextFunction, Request, Response } from "express";
import * as httpServer from "http";
import app from "./app";

const stamp = { pattern: "UTC:yyyy-mm-dd'T'HH:MM:ss" };

dotenv.config();

let server = null;
const host = process.env.HOST || "localhost";
const port = parseInt(process.env.PORT ? process.env.PORT : "5500", 10);

consoleStamp(console, stamp);

server = httpServer.createServer(app);
server.listen(port, host);
server.on("error", handleError);
server.on("listening", listen);

function handleError(err: Error, req: Request, res: Response, next: NextFunction) {

    const error = {
        code: res.statusCode || 500,
        message: err.message,
        name: err.name
    };

    return res.json(error);
}

function listen() {
    let stringMessage = "Server is currently listing on: ";
    stringMessage = stringMessage.concat(host, " ", port.toString());
    global.console.log("");
    global.console.log(stringMessage);
    global.console.log("");
}
