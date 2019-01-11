import mongoose from "mongoose";

const Types = mongoose.Schema.Types;

const FeedbackSchema = new mongoose.Schema({
    timestamp: { type: Types.Date, required: true },
    email: { type: Types.String, required: true },
    message: { type: Types.String, required: true, maxLength: 1000 }
});

export default FeedbackSchema;
