const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/session",
      "/Encounter",
      "/CampaignInfo"
    ],
    target: "https://localhost:7028",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
