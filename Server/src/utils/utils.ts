import base58 from "bs58";
import crypto from "crypto";

export function getToken(bytes: number) {
  bytes = bytes || 16;
  const buf = crypto.randomBytes(bytes);
  return base58.encode(buf);
}