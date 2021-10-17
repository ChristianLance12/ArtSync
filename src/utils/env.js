const { NODE_ENV } = process.env;

export const serverHostname = NODE_ENV === 'production' ? "" : "http://localhost:5000"
export const serverStaticPath = `${serverHostname}`