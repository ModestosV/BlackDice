import mongoose, { Mongoose } from "mongoose";
export class Connection {
  private mongoose: Mongoose;
  private connection: mongoose.Connection;

  constructor() {
    this.mongoose = mongoose;
    this.mongoose.connect(
      "mongodb://localhost/blackdice",
      { useNewUrlParser: true }
    );
    this.mongoose.set("useCreateIndex", true);

    this.connection = mongoose.connection;
    this.confirmConnect();
  }

  public connectTest() {
    this.mongoose.connect(
      "mongodb://localhost/test",
      { useNewUrlParser: true }
    );
    this.mongoose.set("useCreateIndex", true);

    this.connection = mongoose.connection;
    this.confirmConnect();
  }

  public dropDB() {
    if (this.connection) {
      this.connection.dropDatabase();
      global.console.log("DB Dropped");
    }
  }

  private confirmConnect() {
    if (this.connection) {
      this.connection.on("error", global.console.error.bind(console, "Connection error:"));
      this.connection.once("open", () => global.console.log("Connected to the database named blackdice"));
    }
  }

}