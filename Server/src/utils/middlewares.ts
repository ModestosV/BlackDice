import express, { NextFunction, Request, Response } from 'express';

export function errorHandler(err: Error, req: Request, res: Response, next: NextFunction) {
  global.console.error('Error:');
  global.console.error(err);
  return res.json(err);
}

