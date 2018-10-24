const app = require('../server');
import request from 'supertest'
let server: request.SuperTest<request.Test>;

beforeAll(() => {
  server = request(app);
})

describe('Testing the inital call to the root location of the server', () => {
  test( 'Root Call', async () => { 
     const response = await server.get('/');
     expect(response.status).toBe(200);
  })
});

