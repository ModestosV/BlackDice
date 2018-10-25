import * as router from './routes/router';
import express, { NextFunction, Request, Response } from 'express';

const app = express();

app.use(router.default);
app.use(logErrorHandler)

export = app;

function logErrorHandler(err: Error, req: Request, res: Response, next: NextFunction) {
  // If an error gets here everything should explode because I did something stupid or forgot to do something.
  if(err) {
    res.status(res.statusCode || 500);
    global.console.error('Error:');
    global.console.error(err.name);
    global.console.error(err.message);
    global.console.error(err.stack);

    throw err;
  }
}