import axios from "axios";
import bodyParser = require("body-parser");
import btoa from "btoa";
import express, { NextFunction, Request, Response, Router } from "express";
import { Document, Model } from "mongoose";
import getModel from "../app/models";
import getStatus from "../utils/errors";
import { errorHandler } from "../utils/middlewares";

class UserRoutes {
    public router: Router;
    private feedback: Model<Document>;
    private user: Model<Document>;

    constructor(router: Router, feedback: Model<Document>, user: Model<Document>) {
        this.router = router;
        this.feedback = feedback;
        this.user = user;
    }

    public FetchFeedback() {
        this.router.get("/all/:token", async (req: Request, res: Response, next: NextFunction) => {
            try {
                const token = req.params.token;

                console.log("Feedback listing request going through.");

                const loggedInUser = await this.user
                    .findOne({
                        $and: [{ loggedInToken: token }, { isAdmin: true }]
                    })
                    .lean()
                    .exec();

                if (!loggedInUser) {
                    return res.json(getStatus(400));
                }

                const feedback = await this.feedback
                    .find({})
                    .lean()
                    .exec();

                return res.json({ everything: feedback });
            } catch (err) {
                console.error("An error occured");
                console.error(err.message);
                return res.json(getStatus(400));
            }
        });
    }

    public SendFeedback() {
        this.router.post(
            "/send",
            bodyParser.json(),
            async (req: Request, res: Response, next: NextFunction) => {
                try {
                    console.log("Send feedback request going through.");
                    const timestamp = new Date()
                        .toISOString()
                        .slice(0, 19)
                        .replace("T", " ");
                    const email = req.body.email;
                    const message = req.body.message;

                    if (email && message) {
                        const feedbackData = new this.feedback({
                            timestamp,
                            email,
                            message
                        });

                        await feedbackData.save();
                        res.status(200);
                        return res.json(feedbackData);
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

    public Clicks() {
        this.router.get(
            "/clicks",
            async (req: Request, res: Response, next: NextFunction) => {
                try {
                    console.log("Click request going through");
                    const token = await axios(process.env.BASIC_URL + "/oauth/access_token", {
                        method: "post",
                        headers: {
                            "Accept": "application/json",
                            "Content-Type": "application/x-www-form-urlencoded",
                            "Authorization" : "Basic " + process.env.AUTHORIZATION
                        },
                        data: {
                            client_id: process.env.CLIENT_ID,
                            client_secret: process.env.CLIENT_SECRET
                        }
                    });
                    console.log("Fetched the key.");
                    const { data } = await axios(process.env.BASIC_URL + "/v4/bitlinks/" + req.query.link + "/clicks/summary", {
                        method: "get",
                        headers: {
                          Accept: "application/json",
                          Authorization : "Bearer " + token.data
                        }
                    });
                    console.log("Got Data");
                    return res.json({ total_clicks: data.total_clicks });
                } catch (e) {
                    return res.json(getStatus(400));
                }
            },
            errorHandler
        );
    }
}

const feedbackRoutes = new UserRoutes(express.Router(), getModel("Feedback"), getModel("User"));

feedbackRoutes.SendFeedback();
feedbackRoutes.FetchFeedback();
feedbackRoutes.Clicks();

export default feedbackRoutes.router;
