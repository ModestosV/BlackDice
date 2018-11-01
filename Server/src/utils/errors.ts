const errorMessages: IStatusMessages = {
  500: JSON.parse('{ "500" :"Update failed" }'),
  400: JSON.parse('{ "400" :"Bad Request" }'),
  200: JSON.parse('{ "200" : "Request was completed successfully" }')
};

interface IStatusMessages {
  "500": JSON;
  "400": JSON;
  "200": JSON;
  [key: string]: JSON;
}

function getStatus(statusCode: number): JSON {
  return errorMessages[statusCode.toString()];
}

export default getStatus;