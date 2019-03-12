import bodyParser from "body-parser";
import express, { NextFunction, Request, Response, Router } from "express";
import { errorHandler } from "../utils/middlewares";

export class WebsiteRoutes {

    public router: Router;

    constructor(router: Router) {
        this.router = router;
    }

    public Node() {

        const path = require("path");

        // Serve React web app static files to "/"
        const staticAssets = path.join(__dirname,  "../../../../react-app/build");
        this.router.use("/", express.static(staticAssets));

        this.router.get(
            "/",
            (req: Request, res: Response, next: NextFunction) => {
                return res.sendFile(`${staticAssets}/index.html)`);
            },
            errorHandler
        );
    }
}

const websiteRoutes = new WebsiteRoutes(express.Router());

websiteRoutes.Node();

export default websiteRoutes.router;
