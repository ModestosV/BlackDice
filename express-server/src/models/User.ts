import mongoose from "mongoose";
const Types = mongoose.Schema.Types;

const UserSchema = new mongoose.Schema({
    createdAt: { type: Types.Date, required: true },
    email: { type: Types.String, required: true, unique: true },
    loggedInToken: { type: Types.String },
    passwordHash: { type: Types.String, required: true },
    username: { type: Types.String, required: true, unique: true },
    isAdmin: { type: Types.Boolean }
});

export default UserSchema;
