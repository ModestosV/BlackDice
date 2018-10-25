"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const router = express_1.default.Router();
/*
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

function validateRequest(req: Request, res: Response, next: NextFunction) {
  // Add a form of validation that way we know if the the request is coming from a user
  if (!req.body.userID) {
    return next(new Error('Not a valide Request. Good luck Next Time'));
  }

  return next();
}
*/
router.get('/', (req, res, next) => {
    let response = {
        greeting: 'Hello World'
    };
    return res.json(response);
});
router.get('/chracterInfo', (req, res, next) => {
    if (!req.params.characterID) {
        global.console.log('This character ID is empty. Nice');
        global.console.log('Not really');
        return next(new Error('Not valide Chracter ID'));
    }
}, errorHandler);
router.post('/register', (req, res, next) => {
    global.console.log('register request going through');
    let response1 = {
        password: req.param("password", "super top secret password"),
        email: req.param("email", "email")
    };
    return res.json(response1);
}, errorHandler);
function validateRequest(req, res, next) {
    // Add a form of validation that way we know if the the request is coming from a user
    if (!req.body.userID) {
        return next(new Error('Not a valide Request. Good luck Next Time'));
    }
    return next();
}
function errorHandler(err, req, res, next) {
    global.console.error('Error:');
    global.console.error(err);
    return res.json(err);
}
exports.default = router;
//# sourceMappingURL=router.js.map