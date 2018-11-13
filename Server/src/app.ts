import express, { NextFunction, Request, Response } from "express";

const app = express();

import "./connection";
import router from "./routes/router";

app.use(router);
app.use(logErrorHandler);

export = app;

function logErrorHandler(req: Request, res: Response, next: NextFunction) {
    // If an error gets here everything should explode because something stupid happened or forgot to catch an error.
    const err = new Error("404 - Not Found");
    err.name = "404";
    res.statusCode = 404;
    global.console.error("Error:");
    global.console.error(err.name);
    global.console.error(err.message);
    global.console.error(err.stack);

    throw err;
}
