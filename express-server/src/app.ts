import express, { Express, NextFunction, Request, Response } from "express";
import router from "./routes/router";
export class Application {
  private app: Express;

  constructor() {
    this.app = express();
  }

  /**
   * initRouters
   */
  public initRouters() {

    const cors = require("cors");
    this.app.use(cors());

    this.app.use(router);
    this.app.use(this.logErrorHandler);
    return this.app;
  }

  public logErrorHandler(req: Request, res: Response, next: NextFunction) {
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
}