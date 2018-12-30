/**
 * IMPORTS
 */
import express from "express";
import userRoutes from "./UserRoutes";
import websiteRoutes from "./WebsiteRoutes";
/**
 * CONSTANT PROPERTIES
 */
const router = express.Router();

router.use("/account", userRoutes);
router.use("/", websiteRoutes);

export default router;
