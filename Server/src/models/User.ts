import mongoose from "mongoose";
const Types = mongoose.Schema.Types;

const userSchema = new mongoose.Schema({
  createdAt: { type: Types.Date, required: true },
  email: { type: Types.String, required: true, unique: true },
  givenname: { type: Types.String },
  loggedIn: { type: Types.Boolean, required: true },
  passwordHash: { type: Types.String, required: true },
  surname: { type: Types.String },
  username: { type: Types.String, unique: true }
});

export default userSchema;
