import app from '../../app'
import bodyParser from 'body-parser';
import express, { NextFunction, Request, Response } from 'express';
import { errorHandler } from '../../utils/middlewares'
const router = express.Router();

// using the app to store local variables is a bad practice but fro local dev it is ok, until we get the database up...
// TODO remove app variables and install proper data checks with database.

router.post(
	'/register',
	(req: Request, res: Response, next: NextFunction) => {
    global.console.log('register request going through');
    app.locals = {};
    app.locals.email = req.params.email;
    app.locals.password = req.params.password;

    let response1 = {
			password: req.params.password,
			email: req.params.email
		};
		return res.json(response1);
	},
	errorHandler
);

router.get('/login', 
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

router.get('/logout', 
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

export default router