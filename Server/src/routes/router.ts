import express, { NextFunction, Request, Response } from "express";
import { errorHandler } from "../utils/middlewares";
import userRoutes from "./UserRoutes";
const router = express.Router();

router.get("/", (req: Request, res: Response, next: NextFunction) => {
	const response = {
		greeting: "Hello World"
	};

	return res.json(response);
});

router.use("/account", userRoutes);

router.get(
	"/chracterInfo",
	(req: Request, res: Response, next: NextFunction) => {
		if (!req.params.characterID) {
			global.console.log("This character ID is empty. Nice");
			global.console.log("Not really");
			return next(new Error("Not valide Chracter ID"));
		}
	},
	errorHandler
);

export default router;
