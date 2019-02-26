// Starting the server code
import consoleStamp from "console-stamp";
import dotenv from "dotenv";
import { NextFunction, Request, Response } from "express";
import * as httpServer from "http";
import { Application } from "./app";
import { Connection } from "./connection";

const stamp = { pattern: "UTC:'Date: 'yyyy-mm-dd' Time: 'HH:MM:ss" };

dotenv.config();

let server = null;
const host = process.env.HOST || "localhost";
const port = parseInt(process.env.PORT ? process.env.PORT : "5500", 10);

consoleStamp(console, stamp);

const app = new Application();
const connection = new Connection();
connection.connect();

server = httpServer.createServer(app.initRouters());
server.listen(port, host);
server.on("error", handleError);
server.on("listening", listen);

function handleError(err: Error) {

    console.error(err.message);
    console.error(err.name);
    console.error(err.stack);
}

function listen() {
    let stringMessage = "Server is currently listing on: ";
    stringMessage = stringMessage.concat(host, " ", port.toString());
    console.log("");
    console.log(stringMessage);
    console.log("");
}
