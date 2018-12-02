import bodyParser from "body-parser"
import express, { NextFunction, Request, Response, Router } from "express"
import { errorHandler } from "../utils/middlewares"

export class WebsiteRoutes {

    public router: Router;

    constructor(router: Router) {
        this.router = router;
    }

    public Node() {
        
        const path = require('path')
        
        // Serve React web app static files to "/"
        this.router.use(express.static(path.join(__dirname, '../../../../react-app/build')))
    }
}

const websiteRoutes = new WebsiteRoutes(express.Router())

websiteRoutes.Node()

export default websiteRoutes.router
