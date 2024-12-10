const PROXY_CONFIG = [
  {
    "context": [
      "/api"
    ],
    "logLevel": "debug",
    "target": process.env['services__api__https__0'] || process.env['services__api__http__0'] || 'https://127.0.0.1:7035',
    "secure": false,
    "pathRewrite": {
      "^/api": ""
    }

  },
];

module.exports = PROXY_CONFIG;
