import bodyParser from "body-parser";
import express, { NextFunction, Request, Response } from "express";
import _ from "lodash";
import moment from "moment";
import mongoose from "mongoose";
import UserSchema from "../../models/User";
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
    
      const username = req.body.username;
      const passHash = req.body.password;
      const email = req.body.email;

      if (username && passHash && email) {
        const salt = moment();
        const finalHash = passHash;

        const userData = {
          createdAt: salt,
          email: email,
          username: username,
          loggedIn: false,
          passwordHash: finalHash
        };

        User.create(userData); // TODO: Catch duplicates
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
      global.console.log("login request going through");
      global.console.log(req.body);

      const passHash = req.body.password;
      const email = req.body.email;

      const loginQuery = {
        email: email
      };

      const userDoc = await User.findOne(loginQuery).exec();

      if (!userDoc) {
        res.status(400);
        return res.json("Email does not exist");
      } else {
        if (_.isEqual(passHash, userDoc.get("passwordHash"))) {
          userDoc.set("loggedIn", true);
          const updatedDoc = await userDoc.save();
          if (updatedDoc) {
            res.status(200);
            return res.json(updatedDoc);
          } else {
            res.status(500);
            return res.json("Server error");
          }
        } else {
          res.status(400);
          return res.json("Incorrect password");
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
      global.console.log("logout request going through");
      global.console.log(req.body);
      
      const passHash = req.body.password;
      const email = req.body.email;
      const loginQuery = {
        email: email
      };

      const userDoc = await User.findOne(loginQuery).exec();

      if (!userDoc) {
        res.status(400);
        return res.json("Email does not exist");
      } else {
        if (_.isEqual(passHash, userDoc.get("passwordHash"))) {
          userDoc.set("loggedIn", false);
          const updatedDoc = await userDoc.save();
          if (updatedDoc) {
            res.status(200);
            return res.json(updatedDoc);
          } else {
            res.status(500);
            return res.json("Server error");
          }
        } else {
          res.status(400);
          return res.json("Incorrect password");
        }
      }
    } catch (err) {
      return next(err);
    }
  },
  errorHandler
);

export default router;
