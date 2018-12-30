/**
 * IMPORTS FOR INTERFACES, CLASSES OR DEPENDENCIES
 */
import bodyParser from "body-parser";
import crypto from "crypto-js";
import express, { NextFunction, Request, Response, Router } from "express";
import _ from "lodash";
import moment from "moment";
import { Model } from "mongoose";
import { Document } from "mongoose";
/**
 * HELPER FUNCTIONS
 */
import getModel from "../app/models";
import { errorHandler } from "../utils/middlewares";
import { getToken } from "../utils/utils";

export class UserRoutes {
    public router: Router;
    private user: Model<Document>;

    constructor(router: Router, user: Model<Document>) {
        this.router = router;
        this.user = user;
    }

    public Node() {
      this.router.get(
        "/",
        (req: Request, res: Response, next: NextFunction) => {
            return res.json({message: "Hello from the account api server!"});
        },
        errorHandler
      );
    }

    public Register() {
        this.router.post(
            "/register",
            bodyParser.json(),
            async (req: Request, res: Response, next: NextFunction) => {
                try {

                    global.console.log("Registration request going through.");
                    const username = req.body.username;
                    const passHash = req.body.password;
                    const email = req.body.email;

                    if (username && passHash && email) {

                        const salt = moment();
                        const finalHash = crypto.SHA512(passHash, salt.toString()).toString();
                        const userData = new this.user({
                            createdAt: salt,
                            email,
                            loggedInToken: undefined,
                            passwordHash: finalHash,
                            username
                        });

                        await userData.save();
                        res.status(201);
                        return res.json(userData);

                    }
                    res.status(400);
                    return res.json("Request invalid");
                } catch (err) {
                    const isDuplicate = err.message.toString().indexOf("duplicate") !== -1 ? true : false;
                    if (isDuplicate) {
                        res.status(412);
                        return res.json("Duplicate Key");
                    }
                    return next(err);
                }
            },
            errorHandler
        );
    }

    public Login() {
        this.router.post(
            "/login",
            bodyParser.json(),
            async (req: Request, res: Response, next: NextFunction) => {
                try {

                    global.console.log("Login request going through.");

                    const passHash = req.body.password;
                    const email = req.body.email;

                    const loginQuery = {
                        email
                    };

                    const userDoc = await this.user.findOne(loginQuery).exec();

                    if (!userDoc) {

                        res.status(400);
                        return res.json("Email does not exist");

                    } else {

                        const finalHash = crypto.SHA512(passHash, userDoc.get("createdAt").toString()).toString();
                        const token = getToken(20); // Random token each time this request is made.

                        if (_.isEqual(finalHash, userDoc.get("passwordHash"))) {

                            userDoc.set("loggedInToken", token);
                            const updatedDoc = await userDoc.save();

                            if (updatedDoc) {
                                res.status(200);
                                return res.json(updatedDoc.get("loggedInToken"));
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

    public Logout() {
        this.router.post(
            "/logout",
            bodyParser.json(),
            async (req: Request, res: Response, next: NextFunction) => {
                try {

                    global.console.log("Logout request going through.");

                    const email = req.body.email;
                    const loginQuery = {
                        email
                    };

                    const userDoc = await this.user.findOne(loginQuery).exec();

                    if (!userDoc) {

                        res.status(400);
                        return res.json("Email does not exist");

                    } else {

                        if (userDoc.get("loggedInToken")) {

                            userDoc.set("loggedInToken", undefined);
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
                            return res.json("User is not logged in.");
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

const userRoutes = new UserRoutes(express.Router(), getModel("User"));

userRoutes.Node();
userRoutes.Register();
userRoutes.Login();
userRoutes.Logout();

export default userRoutes.router;
