"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const router = express_1.default.Router();
/*
router.get('/register',
  (req: Request, res: Response, next: NextFunction) => {

    app.locals.email = req.query.email;
    app.locals.password = req.query.password;

    return res.send('true');
  }
);

router.get('/login',
  (req: Request, res: Response, next: NextFunction) => {
    if (req.query.email == app.locals.email && req.query.password == app.locals.password)
    {
      app.locals.loggedInUser = req.query.email;
      res.send('true')
    } else
    {
      res.send('false');
    }
  }
);

router.get('/logout',
  (req: Request, res: Response, next: NextFunction) => {
    if (req.query.email == app.locals.loggedInUser)
    {
      app.locals.loggedInUser = null;
      res.send('true')
    } else
    {
      res.send('false');
    }
  }
);
*/
/*
router.get(
  '/chracterInfo',
  (req: Request, res: Response, next: NextFunction) => {
    if (!req.params.characterID) {
      global.console.log('This character ID is empty. Nice');
      global.console.log('Not really');
      return next(new Error('Not valide Chracter ID'));
    }
  },
  errorHandler,
);

function validateRequest(req: Request, res: Response, next: NextFunction) {
  // Add a form of validation that way we know if the the request is coming from a user
  if (!req.body.userID) {
    return next(new Error('Not a valide Request. Good luck Next Time'));
  }

  return next();
}
*/
function errorHandler(err, req, res, next) {
    global.console.error(`Error: ${err}`);
    return res.json(err);
}
exports.default = router;
//# sourceMappingURL=router.js.map