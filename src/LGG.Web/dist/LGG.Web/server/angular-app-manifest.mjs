
export default {
  bootstrap: () => import('./main.server.mjs').then(m => m.default),
  inlineCriticalCss: true,
  baseHref: '/',
  locale: undefined,
  routes: [
  {
    "renderMode": 2,
    "route": "/"
  },
  {
    "renderMode": 2,
    "route": "/Email/cadastro_confirmacao"
  }
],
  entryPointToBrowserMapping: undefined,
  assets: {
    'index.csr.html': {size: 54699, hash: 'beae06a72cc80790315981e4536151348b77bb5aa93e6719c9b842e403268ad1', text: () => import('./assets-chunks/index_csr_html.mjs').then(m => m.default)},
    'index.server.html': {size: 1050, hash: 'b285719a36a48d8013e5d581368b3efe2cba680af0c24d8eeab613e40d82da08', text: () => import('./assets-chunks/index_server_html.mjs').then(m => m.default)},
    'index.html': {size: 76666, hash: '7509735a4841616f3c5ec993510991d453c4a923370410ab85f084aecd15c51d', text: () => import('./assets-chunks/index_html.mjs').then(m => m.default)},
    'Email/cadastro_confirmacao/index.html': {size: 58868, hash: '557e4421701934fdeb4e5318c7f03d477d0932502d03e6a81a789c4038024b59', text: () => import('./assets-chunks/Email_cadastro_confirmacao_index_html.mjs').then(m => m.default)},
    'styles-ZFGC6FVF.css': {size: 103945, hash: 'L1igRuWqJKE', text: () => import('./assets-chunks/styles-ZFGC6FVF_css.mjs').then(m => m.default)}
  },
};
