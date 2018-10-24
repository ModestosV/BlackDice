// Starting the server code
import consoleStamp from 'console-stamp';
import express, { NextFunction, Request, Response } from 'express';
import * as httpServer from 'http';
import * as router from './routes/router';

const stamp = { pattern: "UTC:yyyy-mm-dd'T'HH:MM:ss".toString() };
const app = express();
let server = null;
const host = 'localhost';
const port = 5500;

consoleStamp(console, stamp);

app.use(router.default);
server = httpServer.createServer(app);
server.listen(port, host);
server.on('error', logError);
server.on('listening', listen);


function logError(err: Error, req: Request, res: Response, next: NextFunction) {
  // If an error gets here everything should explode because I did something stupid or forgot to do something.
  res.status(res.statusCode || 500);
  global.console.error('Error:');
  global.console.error(err.name);
  global.console.error(err.message);
  global.console.error(err.stack);

  throw err;
}

function listen() {
  let stringErr = 'Server is currently listing on: ';
  stringErr = stringErr.concat(host, ':', port.toString());
  global.console.log('');
  global.console.log(stringErr);
  global.console.log('');
}
