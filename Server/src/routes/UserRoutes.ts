import bodyParser from "body-parser";
import crypto from "crypto-js";
import express, { NextFunction, Request, Response, Router } from "express";
import _ from "lodash";
import moment from "moment";
import { Model } from "mongoose";
import { Document } from "mongoose";
import getModel from "../app/models";
import { errorHandler } from "../utils/middlewares";

export class UserRoutes {
    public router: Router;
    private user: Model<Document>;

    constructor(router: Router, user: Model<Document>) {
        this.router = router;
        this.user = user;
    }

    public Register() {
        this.router.post(
            "/register",
            bodyParser.json(),
            async (req: Request, res: Response, next: NextFunction) => {
                try {
                    global.console.log("register request going through");
                    const username = req.body.username;
                    const passHash = req.body.password;
                    const email = req.body.email;
                    if (username && passHash && email) {
                        const salt = moment();
                        const finalHash = crypto.SHA512(passHash, salt.toString()).toString();
                        const userData = new this.user({
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
                    res.status(400);
                    return res.json("Request invalid");
                } catch (err) {
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
                    global.console.log("login request going through");

                    // TODO: Token Validation & password validation

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

    public Logout() {
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

                    const userDoc = await this.user.findOne(loginQuery).exec();

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

const userRoutes = new UserRoutes(express.Router(), getModel("User"));

userRoutes.Register();
userRoutes.Login();
userRoutes.Logout();

export default userRoutes.router;
