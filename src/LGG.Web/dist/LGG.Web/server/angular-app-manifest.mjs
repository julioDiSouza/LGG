
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
    'index.csr.html': {size: 54693, hash: '2193c0a7285f4d26e45ef8dac85108b7fa283c98c598b0ef609ce1992eff581e', text: () => import('./assets-chunks/index_csr_html.mjs').then(m => m.default)},
    'index.server.html': {size: 1050, hash: '9d59211f44be75c8f71edc1d6fc78c527e3889475e634869292c3d673fd5626c', text: () => import('./assets-chunks/index_server_html.mjs').then(m => m.default)},
    'Email/cadastro_confirmacao/index.html': {size: 58862, hash: 'e298568523751cc3885fb3696390211118fe855655cb010d3b24a49d1d4c5e8d', text: () => import('./assets-chunks/Email_cadastro_confirmacao_index_html.mjs').then(m => m.default)},
    'index.html': {size: 58096, hash: 'df33202c41323b79dd2f247d3cbdc62aeb5cdacee7299cd5bb7eb12c7552ebe7', text: () => import('./assets-chunks/index_html.mjs').then(m => m.default)},
    'styles-4OCKFFVR.css': {size: 103897, hash: 't9xTACKfKVI', text: () => import('./assets-chunks/styles-4OCKFFVR_css.mjs').then(m => m.default)}
  },
};
