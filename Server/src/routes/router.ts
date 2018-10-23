import bodyParser from 'body-parser';
import express, { NextFunction, Request, Response } from 'express';
const router = express.Router();

//router.use('/', bodyParser.json, validateRequest, errorHandler);

router.get('/', 
  (req: Request, res: Response, next: NextFunction) => {
    let response = {
      greeting: 'Hello World'
    };

    return res.json(response);
  }
);

router.get(
  '/chracterInfo',
  (req: Request, res: Response, next: NextFunction) => {
    if (!req.params.characterID) {
      global.console.log('This character ID is empty. Nice');
      global.console.log('Not really');
      return next(new Error('Not valide Chracter ID'));
    }
  },
  errorHandler,
);

function validateRequest(req: Request, res: Response, next: NextFunction) {
  // Add a form of validation that way we know if the the request is coming from a user
  if (!req.body.userID) {
    return next(new Error('Not a valide Request. Good luck Next Time'));
  }

  return next();
}

function errorHandler(err: Error, req: Request, res: Response, next: NextFunction) {
  global.console.error('Error:');
  global.console.error(err);
  return res.json(err);
}

export default router;
