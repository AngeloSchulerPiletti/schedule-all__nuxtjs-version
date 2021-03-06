
export default {
  publicRuntimeConfig: {
    contractAddress: process.env.CONTRACT_ADDRESS,
    contractJsonPath: process.env.CONTRACT_JSON_PATH,
    networkAddress: process.env.NETWORK_ADDRESS,
  },
  
  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    titleTemplate: '%s | SCHEDULE',
    title: 'Inicio',
    htmlAttrs: {
      lang: 'en'
    },
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' },
      { name: 'format-detection', content: 'telephone=no' }
    ],
    link: [
      { rel: 'preconnect', href: 'https://fonts.googleapis.com' },
      { rel: 'preconnect', href: 'https://fonts.gstatic.com', crossorigin: "" },
      { rel: 'stylesheet', href: "https://fonts.googleapis.com/css2?family=Roboto+Slab:wght@200;300;400;500;700;800&family=Roboto:ital,wght@0,300;0,400;0,500;0,700;1,300;1,400&display=swap" }
    ]
  },

  router: {
    middleware: 'wallet_connection',
  },

  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [
    '@/assets/sass/reset.scss',
    '@/assets/sass/default.scss',
    '@/assets/sass/_index.scss',
    '@/assets/sass/abstract/_colors.scss'
  ],

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: [
    '~/plugins/axios.js',
    '~/plugins/user-web3.client.js',
    '~/plugins/node-web3.client.js',
    '~/plugins/smart-contract.client.js',
    '~/plugins/wallet_connection.client.js',
    '~/plugins/unauthAxios.client.js',
  ],

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: true,

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules: [
    // https://go.nuxtjs.dev/vuetify
    '@nuxtjs/vuetify',
  ],

  ssr: true,
  target: 'server',
  // Modules: https://go.nuxtjs.dev/config-modules
  modules: [
    // https://go.nuxtjs.dev/axios
    '@nuxtjs/axios',
    'cookie-universal-nuxt',
    '@nuxtjs/style-resources',
    'web3',
    // '@nuxtjs/auth-next'
  ],

  styleResources: {
    scss: ['./assets/sass/abstract/*.scss']
  },

  // Axios module configuration: https://go.nuxtjs.dev/config-axios
  axios: {
    baseUrl: "https://localhost:44335/api/",
  },

  // Build Configuration: https://go.nuxtjs.dev/config-build
  build: {
  }
}
