
export default {
  basePath: '/',
  allowedHosts: [
  "localhost",
  "127.0.0.1",
  "localhost:4000",
  "localhost:4200"
],
  supportedLocales: {
  "en-US": ""
},
  entryPoints: {
    '': () => import('./main.server.mjs')
  },
};
