const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function (app) {
  app.use(
    ['/weatherforecast'],
    createProxyMiddleware({
      target: 'http://localhost:5145',
      changeOrigin: true,
    })
  );
};
