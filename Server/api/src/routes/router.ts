/**
 * IMPORTS
 */
import express from "express";
import apiRoutes from "./APIRoutes"
import websiteRoutes from "./WebsiteRoutes";
/**
 * CONSTANT PROPERTIES
 */
const router = express.Router();

router.use("/api", apiRoutes);
router.use("/", websiteRoutes)

export default router;
