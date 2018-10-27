const express = require('express')
const app = express()
const port = 5500
const mongoose = require('mongoose')

mongoose.connect('mongodb://localhost:27017', { useNewUrlParser: true })

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
  res.send('Welcome to the BlackDice web app.')
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
            res.send('false')
          } else {
            res.send(user.email)
          }
        }
      });

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
          res.send('false')
        } else {
          if (!user) {
            res.send('false')
          } else {
            res.send(user.email)
          }
        }
      });

    }
	})

app.get('/logout', 
  (req, res, next) => {

    let logoutQuery = {
      'email' : req.query.email,
      'loggedIn' : true
    }

    User.findOneAndUpdate(logoutQuery, { 'loggedIn': false }, (err, user) => {
      if (err || !user) {
        res.send('false')
      } else {
        if (!user) {
          res.send('false')
        } else {
          res.send(user.email)
        }
      }
    }).exec()
  })

app.listen(port, () => console.log(`BlackDice web app listening on port ${port}!`))