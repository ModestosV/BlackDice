import mongoose from "mongoose";
import FeedbackSchema from "../models/Feedback";
import UserSchema from "../models/User";

interface IModels {
    User: mongoose.Model<mongoose.Document>;
    Feedback: mongoose.Model<mongoose.Document>;
    [key: string]: mongoose.Model<mongoose.Document>;
}

const schemas: IModels = {
    User: mongoose.model("User", UserSchema),
    Feedback: mongoose.model("Feedback", FeedbackSchema)
};

function getModel(name: string) {
    return schemas[name];
}

export default getModel;