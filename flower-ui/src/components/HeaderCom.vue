<template>
  <div>
    <div class="Header-topToos">
      <div class="HTTview" @click="OpenLogin" v-if="store.state.NickName == undefined"><span>登录</span></div>
      <div class="HTTview" @click="OpenRegister" v-if="store.state.NickName == undefined"><span>注册</span></div>
      <div class="HTTview" v-if="store.state.NickName != undefined"><span>{{ store.state.NickName }}</span></div>
      <div class="HTTview" v-if="store.state.NickName != undefined" @click="OpenPersonCenter"><span>个人中心</span></div>
      <div class="HTTview" @click="LogOut" v-if="store.state.NickName != undefined"><span>注销</span></div>
    </div>
    <div class="Header-search">
      <div class="logo Hs-view"><img src="" alt="">logo</div>
      <div class="searchs Hs-view">
        <el-input v-model="input1" class="w-50 m-2 searchs" placeholder="商品搜索" :suffix-icon="Search" />
      </div>
      <div class="Cservice Hs-view" @click="OpenService"><span>biao</span>在线客服</div>
      <!-- <router-link to="/loveflower">dsadaf</router-link> -->

    </div>
    <div class="headerMenu">
      <el-menu class="el-menu-header" mode="horizontal" router>
        <el-menu-item index="/">首页</el-menu-item>
        <el-menu-item index="/loveflower">爱情鲜花</el-menu-item>
        <el-menu-item index="/bithdayflower">生日鲜花</el-menu-item>
        <el-menu-item index="/friendflower">友情鲜花</el-menu-item>
        <el-menu-item index="/weddingflower">婚庆鲜花</el-menu-item>
      </el-menu>
    </div>
  </div>
  <LoginCom />
  <RegisterCom />
  <ServiceCom />
</template>

<script lang="ts" setup>
import { Search } from '@element-plus/icons-vue'
import { ref } from 'vue'
import { useRouter } from "vue-router";

import { useStore } from 'vuex'
import LoginCom from './LoginCom.vue'
import RegisterCom from './RegisterCom.vue'
import ServiceCom from './ServiceCom.vue'
import { db } from '../http/dexie'

const input1 = ref('')

const store = useStore()
//登陆
const OpenLogin = () => {
  store.commit('OpenLogin')

}
//注册
const OpenRegister = () => {
  store.commit('OpenRegister')
}
//退出登陆
const LogOut = () => {
  localStorage.removeItem('NickName'); //清空本地缓存
  localStorage.removeItem('token');    //清空本地缓存
  store.commit('SettingNickName', undefined); //清空vuex中的设置昵称方法
  // 删除表中的所有数据
  db.delete().then(() => {
    return true;
  }).catch((ex:any) => {
    console.log("DB" + ex)
    return false;
  })

}
//跳转个人中心
const router = useRouter();
const OpenPersonCenter = async () => { 
  router.push({ path: "/personcenter" });
}

//打开客服窗口
const OpenService = () => {
  store.commit('OpenService')
}
</script>

<style lang="scss">
.Header-topToos {
  display: flex;
  flex-direction: row;
  justify-content: flex-end;

  .HTTview {
    cursor: pointer;
    width: 70px;
    height: 30px;
    margin: 0 8px;
    line-height: 30px;
    text-align: center;
  }

  .HTTview:hover {
    color: #409eff;
  }

}

.Header-search {
  width: 100%;
  height: 50px;
  margin: 10px 0;
  display: flex;
  line-height: 50px;
  justify-content: space-around;

  // background-color: #eee;
  .Hs-view {
    margin: 0 20px;
  }

  .searchs {
    width: 300px;
    height: 50px;

    input {
      width: 300px;
      height: 40px;
      padding: 0 10px;
      line-height: 40px;
      font-size: 18px;
      // border-radius: 30px;
    }
  }

  .Cservice {
    font-size: 16px;
    color: #77777783;
    line-height: 40px;
  }

  .Cservice:hover {
    color: #000;
    cursor: pointer;
  }
}

.headerMenu {
  display: flex;
  justify-content: center;

  .el-menu-header {
    width: 50%;
    font-size: 40px;
    display: flex;
    justify-content: center;
  }
}
</style>