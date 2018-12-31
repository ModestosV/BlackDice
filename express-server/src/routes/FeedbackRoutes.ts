import bodyParser = require("body-parser");
import express, { NextFunction, Request, Response, Router } from "express";
import { Document, Model } from "mongoose";
import getModel from "../app/models";
import { errorHandler } from "../utils/middlewares";

class UserRoutes {
    public router: Router;
    private feedback: Model<Document>;

    constructor(router: Router, feedback: Model<Document>) {
        this.router = router;
        this.feedback = feedback;
    }

    public SendFeedback() {
        this.router.post(
            "/send",
            bodyParser.json(),
            async (req: Request, res: Response, next: NextFunction) => {
                try {
                    global.console.log("Send feedback request going through.");
                    const timestamp = new Date().toISOString().slice(0, 19).replace("T", " ");
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
}

const feedbackRoutes = new UserRoutes(express.Router(), getModel("Feedback"));

feedbackRoutes.SendFeedback();

export default feedbackRoutes.router;