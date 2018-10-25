import * as router from './routes/router';
import express from 'express';

const app = express();

app.use(router.default);

export = app;