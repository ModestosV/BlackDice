import mongoose, { Mongoose } from "mongoose";
export class Connection {
  private mongoose: Mongoose;
  private connection: mongoose.Connection | undefined;

  constructor() {
    this.mongoose = mongoose;
    this.connection = undefined;
  }
  /**
   * initModels: THIS IS WERE WE INSTANTIATE THE DB MODELS
   */
  public initModels() {
    // modelSchemas("User"); No sure if we actually need this
  }

  /**
   * connect: THIS IS WERE THE MONGOOSE CONNECTION GETS STARTED
   */
  public connect() {
    this.mongoose.connect(
      "mongodb://localhost/blackdice",
      { useNewUrlParser: true }
    );
    this.mongoose.set("useCreateIndex", true);
    /**
     * THIS IS WERE THE CONNECTION GETS VALIDATED
     */
    this.connection = mongoose.connection;
    this.confirmConnect();
  }

  /**
   * connectTest
   */
  public connectTest() {
    this.mongoose.connect(
      "mongodb://localhost/test",
      { useNewUrlParser: true }
    );
    this.mongoose.set("useCreateIndex", true);
    /**
     * THIS IS WERE THE CONNECTION GETS VALIDATED
     */
    this.connection = mongoose.connection;
    this.confirmConnect();
  }

  /**
   * dropDB
   */
  public dropDB() {
    if (this.connection) {
      this.connection.dropDatabase();
      global.console.log("DB Droped");
    }
  }

  /**
   * confirmConnection
   */
  private confirmConnect() {
    if (this.connection) {
      this.connection.on("error", global.console.error.bind(console, "Connection error:"));
      this.connection.once("open", () => global.console.log("Connected to the database named blackdice"));
    }
  }

}