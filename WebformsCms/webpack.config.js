const path = require("path")

module.exports = {
  entry: [
      "./Scripts/Cms-Admin/Es6/index.js"
  ],
  output: {
    path: path.resolve(__dirname, "./Scripts/Cms-Admin"),
    filename: "bundle.js"
  },
  module: {
    rules: [
        {
            test: /\.jsx?$/,
            exclude: /node_modules/,
            loader: "babel-loader"
        }
      ]
  }
}