var request = require('supertest')
let server = require('./app')

describe('Testing route responses', function() {

  after(function(done) {
    server.close()
    done()
  })

  it('responds to /', function(done) {
    request(server)
      .get('/')
      .expect(200, done)
  })

  it('responds to /register', function(done) {
    request(server)
      .get('/register')
      .expect(200, done)
  })

  it('responds to /login', function(done) {
    request(server)
      .get('/login')
      .expect(200, done)
  })

  it('responds to /logout', function(done) {
    request(server)
      .get('/logout')
      .expect(200, done)
  })

})