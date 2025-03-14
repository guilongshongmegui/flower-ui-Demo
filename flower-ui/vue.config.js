const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  // devServer:{
  //   proxy:{
  //     '/api':{
  //       target: 'http://119.91.231.224:5136/api', 
  //       changeOrigin: true,
  //       ws: true,
  //       pathRewrite:{
  //         '^/api':''
  //       }
  //     }
  //   }
  // }
})

// target: 'http://119.91.231.224:5136/api'
//target: 'http://localhost:5136/api', 