"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const mongoose_1 = __importDefault(require("mongoose"));
class Connection {
    constructor() {
        this.mongoose = mongoose_1.default;
        this.connection = undefined;
    }
    /**
     * initModels: THIS IS WERE WE INSTANTIATE THE DB MODELS
     */
    initModels() {
        // modelSchemas("User"); No sure if we actually need this
    }
    /**
     * connect: THIS IS WERE THE MONGOOSE CONNECTION GETS STARTED
     */
    connect() {
        this.mongoose.connect("mongodb://localhost/blackdice", { useNewUrlParser: true });
        this.mongoose.set("useCreateIndex", true);
        /**
         * THIS IS WERE THE CONNECTION GETS VALIDATED
         */
        this.connection = mongoose_1.default.connection;
        this.confirmConnect();
    }
    /**
     * connectTest
     */
    connectTest() {
        this.mongoose.connect("mongodb://localhost/test", { useNewUrlParser: true });
        this.mongoose.set("useCreateIndex", true);
        /**
         * THIS IS WERE THE CONNECTION GETS VALIDATED
         */
        this.connection = mongoose_1.default.connection;
        this.confirmConnect();
    }
    /**
     * dropDB
     */
    dropDB() {
        if (this.connection) {
            this.connection.dropDatabase();
            global.console.log("DB Droped");
        }
    }
    /**
     * confirmConnection
     */
    confirmConnect() {
        if (this.connection) {
            this.connection.on("error", global.console.error.bind(console, "Connection error:"));
            this.connection.once("open", () => global.console.log("Connected to the database named blackdice"));
        }
    }
}
exports.Connection = Connection;
//# sourceMappingURL=connection.js.map