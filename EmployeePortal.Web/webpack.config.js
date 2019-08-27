"use strict";

var path = require("path");
var WebpackNotifierPlugin = require("webpack-notifier");
var BrowserSyncPlugin = require("browser-sync-webpack-plugin");

module.exports = {
    entry: {
        overview: "../EmployeePortal.SkillManagement/Application/Skills/Overview.jsx",
        counter: "../EmployeePortal.SkillManagement/Application/Skills/Counter.tsx",
        appje: "../EmployeePortal.SkillManagement/Application/Skills/Appje.tsx"
    },
    output: {
        path: path.resolve(__dirname, "../EmployeePortal.SkillManagement/Application/Skills/"),
        filename: "[name].js"
    },
    resolve: {
        modules: [
            path.resolve("../EmployeePortal.Web/node_modules/")
        ],
        extensions: [".ts", ".tsx", ".js", ".jsx"]
    },
    module: {
        rules: [
            {
                test: /\.(ts|js)x?$/,
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