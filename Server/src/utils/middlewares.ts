import express, { NextFunction, Request, Response } from "express";
import getStatus from "./errors";

export function errorHandler(err: Error, req: Request, res: Response, next: NextFunction) {
  global.console.error("Error:");
  global.console.error(err.message);
  global.console.error(err.stack);
  return res.json(getStatus(500));
}
