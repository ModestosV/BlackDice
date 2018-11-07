import bcrypt from "bcrypt";
import bodyParser from "body-parser";
import express, { NextFunction, Request, Response } from "express";
import _ from "lodash";
import moment from "moment";
import mongoose from "mongoose";
import UserSchema from "../../models/User";
import getStatus from "../../utils/errors";
import { errorHandler } from "../../utils/middlewares";

const router = express.Router();
const User = mongoose.model("User", UserSchema);

router.post(
  "/register",
  bodyParser.json(),
  async (req: Request, res: Response, next: NextFunction) => {
    try {
      global.console.log("register request going through");
      global.console.log(req.body);

      const passHash = req.body.password;
      const email = req.body.email;
      const userName = req.body.username;

      if (passHash && email) {
        const salt = moment();
        const finalHash = passHash;

        const userData = {
          createdAt: salt,
          email,
          username: userName,
          loggedIn: false,
          passwordHash: finalHash
        };

        User.create(userData);
        res.status(200);
        return res.json(userData);
      }
      res.status(400);
      return res.json("Request invalid");
    } catch (err) {
      return next(new Error("A database error occured while registering"));
    }
  },
  errorHandler
);

router.post(
  "/login",
  bodyParser.json(),
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
  bodyParser.json(),
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
