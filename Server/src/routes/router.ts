/**
 * IMPORTS
 */
import express from "express";
import userRoutes from "./UserRoutes";
/**
 * CONSTANT PROPERTIES
 */
const router = express.Router();

router.use("/account", userRoutes);

export default router;
