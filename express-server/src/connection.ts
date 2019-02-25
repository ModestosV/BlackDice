import mongoose, { Mongoose } from "mongoose";
export class Connection {
  private mongoose: Mongoose;
  private connection: mongoose.Connection | undefined;

  constructor() {
    this.mongoose = mongoose;

  }

  public connect() {
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
      console.log("DB Dropped");
    }
  }

  private confirmConnect() {
    if (this.connection) {
      this.connection.on("error", console.error.bind(console, "Connection error:"));
      this.connection.once("open", () => console.log("Connected to the database named blackdice"));
    }
  }

}