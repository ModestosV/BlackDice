import userRoutes from "./UserRoutes";
import express, { NextFunction, Request, Response, Router } from "express"
import { errorHandler } from "../utils/middlewares"

export class APIRoutes {

    public router: Router;

    constructor(router: Router) {
        this.router = router;
    }
    
    public Node() {
      this.router.get(
        "/",
        (req: Request, res: Response, next: NextFunction) => {
            return res.json({"message": "Hello from the api server!"})
        },
        errorHandler
      )
    }
}

const router = express.Router()

const apiRoutes = new APIRoutes(router)
apiRoutes.Node()

router.use("/account", userRoutes)

export default apiRoutes.router
