<template>
  <HeaderComVue/>
  <router-view></router-view>
  <FooterCom/>
</template>

<script lang="ts" setup>

import FooterCom from './components/FooterCom.vue'
import HeaderComVue from './components/HeaderCom.vue'

</script>

<style lang="scss">
*{
  margin: 0;
  padding: 0;
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 30px;
  padding: 0 0%;
}
</style>
