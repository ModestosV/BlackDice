import bcrypt from "bcrypt";
import bodyParser from "body-parser";
import express, { NextFunction, Request, Response } from "express";
import _ from "lodash";
import moment from "moment";
import mongoose from "mongoose";
import app from "../../app";
import { errorHandler } from "../../utils/middlewares";

const router = express.Router();
const User = mongoose.model("User");

// using the app to store local variables is a bad practice but fro local dev it is ok, until we get the database up...
// TODO remove app variables and install proper data checks with database.

router.post(
  "/register",
  async (req: Request, res: Response, next: NextFunction) => {
    try {
      global.console.log("register request going through");

      const passHash = req.query.password;
      const email = req.query.email;

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
          return res.json({ 200: "Request was completed successfully" });
        } else {
          throw new Error("A database error occured while registering");
        }
      }
      return res.json({ 400: "Bad Request" });
    } catch (err) {
      return next(err);
    }
  },
  errorHandler
);

router.post(
  "/login",
  async (req: Request, res: Response, next: NextFunction) => {
    try {
      const email = req.query.email;
      const passHash = req.query.password;

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
            return res.json({ 200: "Request was completed successfully" });
          } else {
            return res.send({ 500: "Update failed" });
          }
        } else {
          return res.send({ 400: "Bad Request" });
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
  async (req: Request, res: Response, next: NextFunction) => {
    // TODO finish the logout router.
    try {
      const email = req.query.email;
      const passHash = req.query.password;

      const loginQuery = {
        email
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
            return res.json({ 200: "Request was completed successfully" });
          } else {
            return res.send({ 500: "Update failed" });
          }
        } else {
          return res.send({ 400: "Bad Request" });
        }
      }
    } catch (err) {
      return next(err);
    }
    if (req.query.email === app.locals.loggedInUser) {
      app.locals.loggedInUser = null;
      res.send("true");
    } else {
      res.send("false");
    }
  },
  errorHandler
);

export default router;
