const express = require('express')
const app = express()
const port = 5500
const mongoose = require('mongoose')

let mongooseOptions = { 
  useNewUrlParser: true, 
  useCreateIndex: true, 
  useFindAndModify: false 
}
mongoose.connect('mongodb://localhost:27017', mongooseOptions)

const Types = mongoose.Schema.Types;

const UserSchema = new mongoose.Schema({
  email: {
    type: Types.String,
    required: true,
    unique: true,
    trim: true
  },
  password: { 
    type: Types.String, 
    required: true 
  },
  loggedIn: { 
    type: Types.Boolean, 
    required: true 
  }
})

mongoose.model('User', UserSchema)

const User = mongoose.model('User')

app.get('/', (req, res, next) => {
  res.status(200).send('Welcome to the BlackDice web app.')
})

app.get('/register', (req, res, next) => {

    if (req.query.email && req.query.password) {
      
      var userData = {
        email: req.query.email,
        password: req.query.password,
        loggedIn: false
      }

      User.create(userData, function (err, user) {
        if (err) {
          return next(err)
        } else {
          if (!user) {
            res.status(200).send('false')
          } else {
            res.status(200).send(user.email)
          }
        }
      });

    } else {
      res.status(200).send('false')
    }
	})

app.get('/login',
	(req, res, next) => {
    if (req.query.email && req.query.password) {

      var loginQuery = {
        email: req.query.email,
        password: req.query.password
      }

      User.findOneAndUpdate(loginQuery, { loggedIn: true }, function (err, user) {
        if (err) {
          res.status(200).send('false')
        } else {
          if (!user) {
            res.status(200).send('false')
          } else {
            res.status(200).send(user.email)
          }
        }
      });

    } else {
      res.status(200).send('false')
    }
	})

app.get('/logout', 
  (req, res, next) => {
    if (req.query.email) {

      let logoutQuery = {
        'email' : req.query.email,
        'loggedIn' : true
      }

      User.findOneAndUpdate(logoutQuery, { 'loggedIn': false }, (err, user) => {
        if (err || !user) {
          res.status(200).send('false')
        } else {
          if (!user) {
            res.status(200).send('false')
          } else {
            res.status(200).send(user.email)
          }
        }
      })

    } else {
      res.status(200).send('false')
    }
  })

var server = app.listen(port, () => {
  console.log(`BlackDice web app listening on port ${port}!`)
})

module.exports = server
