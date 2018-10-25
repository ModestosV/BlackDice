// Starting the server code
import consoleStamp from 'console-stamp';
import { NextFunction, Request, Response } from 'express';
import * as httpServer from 'http';
import app from './app';


const stamp = { pattern: "UTC:yyyy-mm-dd'T'HH:MM:ss".toString() };

let server = null;
const host = 'localhost';
const port = 5500;

consoleStamp(console, stamp);

server = httpServer.createServer(app);
server.listen(port, host);
server.on('error', handleError);
server.on('listening', listen);


function handleError(err: Error, req: Request, res: Response, next: NextFunction) {
  // If an error gets here everything should explode because I did something stupid or forgot to do something.

  let error = {
    code: res.statusCode || 500,
    name: err.name,
    message: err.message
  };

  return res.json(error);
}

function listen() {
  let stringMessage = 'Server is currently listing on: ';
  stringMessage = stringMessage.concat(host, ' ', port.toString());
  global.console.log('');
  global.console.log(stringMessage);
  global.console.log('');
}