import mongoose from "mongoose";
import User from "./models/User";

mongoose.connect(
    "mongodb://192.168.99.100/blackdice",
    { useNewUrlParser: true }
);
mongoose.set("useCreateIndex", true);

/**
 * THIS IS WERE WE INSTANTIATE THE DB MODELS
 */
mongoose.model("User", User);

/**
 * THIS IS WERE THE CONNECTION GETS VALIDATED
 */
const db = mongoose.connection;

db.on("error", global.console.error.bind(console, "Connection error:"));
db.once("open", () => global.console.log("Connected to the database named blackdice"));
