import crypto from "crypto-js";
import bodyParser from "body-parser";
import express, { NextFunction, Request, Response, Router } from "express";
import _ from "lodash";
import moment from "moment";
import getModel from "../../app/models";
import { Model } from "mongoose";
import {Document} from "mongoose";
import { errorHandler } from "../../utils/middlewares";

export class UserRoutersClass {
    router: Router;
    User: Model<Document, {}>;

    constructor(r:Router){
        this.router = r;
        this.User = getModel("User");
    }

    public Register(){
        this.router.post(
            "/register",
            bodyParser.json(),
            async (req: Request, res: Response, next: NextFunction) => {
              try {
                global.console.log("register request going through");
                global.console.log("BOI");
                const username = req.body.username;
                const passHash = req.body.password;
                const email = req.body.email;
                global.console.log("BOI1");
                if (username && passHash && email) {
                    global.console.log("BOI2");
                  const salt = moment();
                  const finalHash = crypto.SHA512(passHash, salt.toString()).toString();
                  global.console.log("BOI3");
                  const userData = new this.User({
                    createdAt: salt,
                    email,
                    loggedIn: false,
                    passwordHash: finalHash,
                    username
                  });
                  await userData.save();
                  res.status(200);
                  return res.json(userData); // TODO: Add the passwordless token generation
                }
                global.console.log("BOI4");
                res.status(400);
                return res.json("Request invalid");
              } catch (err) {
                global.console.log("BOI5 >:(");
                return next(err);
              }
            },
            errorHandler
          );
    }

    public Login(){     
        this.router.post(
            "/login",
            bodyParser.json(),
            async (req: Request, res: Response, next: NextFunction) => {
            try {
                global.console.log("login request going through");
                global.console.log(req.body);
        
        
                // TODO: Token Validation & password validation
        
                const passHash = req.body.password;
                const email = req.body.email;
        
                const loginQuery = {
                email
                };
        
                const userDoc = await this.User.findOne(loginQuery).exec();
        
                if (!userDoc) {
                res.status(400);
                return res.json("Email does not exist");
                } else {
        
                const finalHash = crypto.SHA512(passHash, userDoc.get("createdAt").toString()).toString();
        
                if (_.isEqual(finalHash, userDoc.get("passwordHash"))) {
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
    }

    public Logout(){
        this.router.post(
            "/logout",
            bodyParser.json(),
            async (req: Request, res: Response, next: NextFunction) => {
              try {
                global.console.log("logout request going through");
                global.console.log(req.body);
                // TODO: Token Validation needs to be done
                const passHash = req.body.password;
                const email = req.body.email;
                const loginQuery = {
                  email
                };
          
                const userDoc = await this.User.findOne(loginQuery).exec();
          
                if (!userDoc) {
                  res.status(400);
                  return res.json("Email does not exist");
                } else {
                  // TODO: Needs to be changed to use token validation and check if the user is logged in.
                  const finalHash = crypto.SHA512(passHash, userDoc.get("createdAt").toString()).toString();
          
                  if (_.isEqual(finalHash, userDoc.get("passwordHash"))) {
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
    }    
}