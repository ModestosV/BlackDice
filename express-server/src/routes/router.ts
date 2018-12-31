import express from "express";
import feedbackRoutes from "./FeedbackRoutes";
import userRoutes from "./UserRoutes";
import websiteRoutes from "./WebsiteRoutes";

/**
 * CONSTANT PROPERTIES
 */
const router = express.Router();

router.use("/account", userRoutes);
router.use("/feedback", feedbackRoutes);
router.use("/", websiteRoutes);

export default router;
