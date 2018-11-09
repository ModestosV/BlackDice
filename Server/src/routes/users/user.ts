import express, { NextFunction, Request, Response } from "express";
import _ from "lodash";
import getModel from "../../app/models";
import { UserRoutersClass } from "./UserRoutersClass";

const userRouter = new UserRoutersClass(express.Router());
const User = getModel("User");

userRouter.Register();
userRouter.Login();
userRouter.Logout();

export default userRouter.router;
