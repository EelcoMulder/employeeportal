"use strict";

var path = require("path");
var WebpackNotifierPlugin = require("webpack-notifier");
var BrowserSyncPlugin = require("browser-sync-webpack-plugin");

module.exports = {
    entry: "../EmployeePortal.SkillManagement/Application/Skills/Overview.jsx",
    output: {
        path: path.resolve(__dirname, "../EmployeePortal.SkillManagement/Application/Skills/"),
        filename: "Overview.js"
    },
    resolve: {
        modules: [
            path.resolve("../EmployeePortal.Web/node_modules/")
        ],
        extensions: [".js", ".jsx"]
    },
    module: {
        rules: [
            {
                test: /\.jsx$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader",
                    options: {
                        presets: ["@babel/preset-react", "@babel/preset-env"],
                        plugins: ["@babel/proposal-class-properties"]
                    }
                }
            }
        ]
    },
    devtool: "inline-source-map",
    plugins: [new WebpackNotifierPlugin(), new BrowserSyncPlugin()]
};