import mongoose from 'mongoose';
const Types = mongoose.Schema.Types;

const userSchema = new mongoose.Schema({
  createdAt: { type: Types.Date, required: true },
  email: { type: Types.String, required: true, unique: true },
  username: { type: Types.String, required: true, unique: true },
  givenname: { type: Types.String, required: true },
  passwordHash: { type: Types.String, required: true },
  surname: { type: Types.String, required: true },
});

export default userSchema;
