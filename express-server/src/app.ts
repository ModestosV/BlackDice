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
    this.app.use(router);
    this.app.use(this.logErrorHandler);
    return this.app;
  }

  public logErrorHandler(req: Request, res: Response, next: NextFunction) {
    // If an error gets here everything should explode because something stupid happened or forgot to catch an error.
    const err = new Error("404 - Not Found");

    err.name = "404";
    res.statusCode = 404;
    console.error("Error:");
    console.error(err.name);
    console.error(err.message);
    console.error(err.stack);

    console.error("This URL is not valid:");
    console.error(req.url);
    console.error("Request coming from this IP");
    console.error(req.ip);

    res.redirect("/");
  }
}