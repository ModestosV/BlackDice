import request from 'supertest'
import * as app from '../server';

describe('Testing the inital call to the root location of the server', () => {
  test( 'Root Call', async () => { 
    const app = require('../server');
    const conn = request(app);
    const response = await conn.get('/');
    expect(response.status).toBe(200);
    conn.
  })
});

