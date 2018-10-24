const app = require('../server');
import request from 'supertest'

describe('Testing the inital call to the root location of the server', () => {
  test( 'Root Call', async () => { 
     const response = await request(app).get('/');
     expect(response.status).toBe(200);
  })
});

