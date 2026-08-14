
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
    'index.csr.html': {size: 431, hash: 'bc412bc78dca7df285be36437f36752db5a1871075464298c8a225ae558e1653', text: () => import('./assets-chunks/index_csr_html.mjs').then(m => m.default)},
    'index.server.html': {size: 944, hash: '29ddd1d93750a91176689229d68b73a2fc1b1b2f9d37dc77feebe6664072a688', text: () => import('./assets-chunks/index_server_html.mjs').then(m => m.default)},
    'index.html': {size: 963, hash: 'bcbd23e65436d069b0c7236715a20e693ef26f4c57f88b3e34b510eac7bb4925', text: () => import('./assets-chunks/index_html.mjs').then(m => m.default)},
    'Email/cadastro_confirmacao/index.html': {size: 1735, hash: '7b45fb6427657d0b5430083018b294ece558cd4f10eb1e415910a30f85abffe2', text: () => import('./assets-chunks/Email_cadastro_confirmacao_index_html.mjs').then(m => m.default)},
    'styles-5INURTSO.css': {size: 0, hash: 'menYUTfbRu8', text: () => import('./assets-chunks/styles-5INURTSO_css.mjs').then(m => m.default)}
  },
};
