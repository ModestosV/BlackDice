import bodyParser from 'body-parser';
import express, { NextFunction, Request, Response } from 'express';
import mongoose from 'mongoose';
const router = express.Router();
const userAccount = mongoose.model('User');

router.get(
  '/account_info',
  async (req: Request, res: Response, next: NextFunction) => {
    const email: string = req.body.userID;
    if (!email) {
      global.console.log('This user ID is empty. Nice');
      global.console.log('Not really');
      return next(new Error('Not valide user ID'));
    }

    const user = await userAccount.find({ email: { email } }).exec();

    if (!user) {
      global.console.log('User needs to be created');
    }
  },
  errorHandler,
);

function errorHandler(err: Error, req: Request, res: Response, next: NextFunction) {
  global.console.error('Error:');
  global.console.error(err);
  return res.json(err);
}

export default router;
