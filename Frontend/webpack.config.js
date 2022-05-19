const HtmlWebpackPlugin = require('html-webpack-plugin');
const path = require("path");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {
    entry: ['babel-polyfill', "./src/index.tsx"],
    mode: "development",
    resolve: {
        extensions: [".ts", ".tsx", ".js", ",jsx"]
    },
    output: {
        filename: "./main.js",
        publicPath: "/"
    },
    plugins: [new HtmlWebpackPlugin({
        template: "./src/template.html"
    }),
        new MiniCssExtractPlugin({})],
    devServer: {
        historyApiFallback: true,
        static: {
            directory: path.join(__dirname, "src"),
        },
        compress: true,
        port: 9000,
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
            },
            {
                test: /\.m?js$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: "babel-loader"
                }
            },
            // {
            //   test:[ /react-datepicker.css/],
            //   use: [MiniCssExtractPlugin.loader, "css-loader"],
            // },
            {
                test: /\.css$/,
                use: [
                    //{ loader: "css-modules-typescript-loader"},
                    require.resolve("style-loader"),
                    {
                        loader: require.resolve("css-loader"),
                        //loader: require.resolve("typings-for-css-modules-loader"),
                        options: {
                            import: true,
                            modules: true,
                        }
                    }
                ]
            },
            {
                test: /\.(png|svg|jpg|gif)$/,
                use: ["file-loader"]
            }
        ]
    }
}