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

// Routes (Weren't working properly in router.ts, investigate...)

app.get('/register', 
  (req: Request, res: Response, next: NextFunction) => {

    app.locals.email = req.query.email;
    app.locals.password = req.query.password;

    return res.send('true');
  }
);

app.get('/login', 
  (req: Request, res: Response, next: NextFunction) => {
    if (req.query.email == app.locals.email && req.query.password == app.locals.password)
    {
      app.locals.loggedInUser = req.query.email;
      res.send('true')
    } else 
    {
      res.send('false');
    }
  }
);

app.get('/logout', 
  (req: Request, res: Response, next: NextFunction) => {
    if (req.query.email == app.locals.loggedInUser)
    {
      app.locals.loggedInUser = null;
      res.send('true')
    } else 
    {
      res.send('false');
    }
  }
);

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