import mongoose from "mongoose";
import UserSchema from "../models/User";

interface IModels {
    User: mongoose.Model<mongoose.Document>;
    [key: string]: mongoose.Model<mongoose.Document>;
}

const schemas: IModels = {
    User: mongoose.model("User", UserSchema)
};

function getModel(name: string) {
    return schemas[name];
}

export default getModel;