import bcrypt from "bcrypt";
import bodyParser from "body-parser";
import express, { NextFunction, Request, Response } from "express";
import _ from "lodash";
import moment from "moment";
import mongoose from "mongoose";
import getStatus from "../../utils/errors";
import { errorHandler } from "../../utils/middlewares";

const router = express.Router();
const User = mongoose.model("User");

router.post(
  "/register",
  bodyParser.json,
  async (req: Request, res: Response, next: NextFunction) => {
    try {
      global.console.log("register request going through");

      const passHash = req.body.password;
      const email = req.body.email;

      if (passHash && email) {
        const salt = moment();
        const finalHash = await bcrypt.hash(passHash, moment.toString());

        const userData = {
          createdAt: salt,
          email: { email },
          loggedIn: false,
          password: finalHash
        };

        const userDoc = await User.create(userData);

        if (userDoc) {
          return res.json(getStatus(200));
        } else {
          throw new Error("A database error occured while registering");
        }
      }
      return res.json(getStatus(400));
    } catch (err) {
      return next(err);
    }
  },
  errorHandler
);

router.post(
  "/login",
  bodyParser.json,
  async (req: Request, res: Response, next: NextFunction) => {
    try {
      const passHash = req.body.password;
      const email = req.body.email;

      const loginQuery = {
        email: { email }
      };

      const userDoc = await User.findOne(loginQuery).exec();

      if (!userDoc) {
        throw new Error("A database error occured while logging in");
      } else {
        const hash = bcrypt.hash(passHash, userDoc.get("createdAt"));
        if (_.isEqual(hash, userDoc.get("password"))) {
          userDoc.set("loggedIn", true);
          const updatedDoc = await userDoc.save();
          if (updatedDoc) {
            return res.json(getStatus(200));
          } else {
            throw new Error("A database error occured while logging");
          }
        } else {
          return res.send(getStatus(400));
        }
      }
    } catch (err) {
      return next(err);
    }
  },
  errorHandler
);

router.post(
  "/logout",
  bodyParser.json,
  async (req: Request, res: Response, next: NextFunction) => {
    try {
      const passHash = req.body.password;
      const email = req.body.email;
      const loginQuery = {
        email: { email }
      };

      const userDoc = await User.findOne(loginQuery).exec();

      if (!userDoc) {
        throw new Error("A database error occured while logging out");
      } else {
        const hash = bcrypt.hash(passHash, userDoc.get("createdAt"));
        if (_.isEqual(hash, userDoc.get("password"))) {
          userDoc.set("loggedIn", false);
          const updatedDoc = await userDoc.save();
          if (updatedDoc) {
            return res.json(getStatus(200));
          } else {
            throw new Error("A database error occured while logging out");
          }
        } else {
          return res.send(getStatus(400));
        }
      }
    } catch (err) {
      return next(err);
    }
  },
  errorHandler
);

export default router;
