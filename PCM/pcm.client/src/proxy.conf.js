const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/session"
    ],
    target: "https://localhost:7028",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
