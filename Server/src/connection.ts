import mongoose from 'mongoose';
import User from './models/User'

mongoose.connect('mongodb://localhost/blackdice');

/**
 * THIS IS WERE WE INSTANTIATE THE DB MODELS
 */
mongoose.model('User', User);


/**
 * THIS IS WERE THE CONNECTION GETS VALIDATED
 */
const db = mongoose.connection;

db.on('error', global.console.error.bind(console, 'connection error:'));
db.once('open', () => global.console.log('connected to blackdice'));