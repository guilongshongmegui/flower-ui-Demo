<template>
  <div class="common-layout" v-if="isShow">
    <el-container>
      <el-header>
        <el-breadcrumb separator="/" style="line-height: 60px">
          <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
          <el-breadcrumb-item :to="{ path: path }">列表页</el-breadcrumb-item>
          <el-breadcrumb-item>详情页</el-breadcrumb-item>
        </el-breadcrumb>
      </el-header>
      <el-container>
        <el-aside width="450px">
          <el-image class="bigImage" :src="htp+infoer.image"></el-image>
        </el-aside>
        <el-main>
          <div class="main-title">
            <h2>{{ infoer.title }}</h2>
            <p>{{ infoer.description }}</p>
          </div>
          <div class="main-item">
            <p>
              <span class="item-title">售价</span
              ><span class="item-price">￥{{ infoer.price }}</span>
            </p>
          </div>
          <div class="main-item">
            <span class="item-title">花语</span
            ><span class="item-p">{{ infoer.language }}</span>
          </div>
          <div class="main-item">
            <p>
              <span class="item-title">材料</span
              ><span class="item-p">{{ infoer.material }}</span>
            </p>
          </div>
          <div class="main-item">
            <p>
              <span class="item-title">包装</span
              ><span class="item-p">{{ infoer.packing }}</span>
            </p>
          </div>
          <div class="main-item">
            <p>
              <span class="item-title">配送说明</span
              ><span class="item-p">{{ infoer.deliveryRemarks }}</span>
            </p>
          </div>
          <div class="main-item buys">
            <button class="buys-on" @click="gotoPay">立即购买</button>
          </div>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import { getFlowersDome } from "../http/index";

const htp = ref("http://119.91.231.224");
const path = ref("/loveflower");
//解析URL地址参数,解析出type的值
let type = window.location.href.split("type=")[1];
switch (type) {
  case "1":
    path.value = "/loveflower";
    break;
  case "2":
    path.value = "/bithdayflower";
    break;
  case "3":
    path.value = "/friendflower";
    break;
  case "4":
    path.value = "/weddingflower";
}

const isShow = ref(false); //设置一个变量，当数据请求完毕后再加载页面
const infoer = ref();
// infoer.value = {     //就可以不必写需要哪些变量了
//   tiltle:"",
//   description:"",
//   price:"",
//   language:"",
//   packing:"",
//   material:"",
//   deliveryRemarks:""

// }
let parms = {
  Id: 0,
  Type: type,
};
onMounted(async () => {
  //解析出id的值
  let urls = window.location.href.match(/id=(\S*)&type=/);
  if (urls != null) {
    parms.Id = parseInt(urls[1]);
  }
  infoer.value = (await getFlowersDome(parms)).data.result[0];
  isShow.value = true;
});

const router = useRouter();
const gotoPay = async () => {
  router.push({ path: "/pay", query: { id: parms.Id, type: parms.Type } });
};
</script>

<style lang="scss" scoped>
.el-aside {
  height: 500px;

  .bigImage {
    height: 450px;
  }
}
.el-main {
  text-align: left;

  .main-item {
    width: 100%;
    height: 55px;
    display: flex;
    flex-direction: row;

    .item-title {
      display: inline-block;
      width: 70px;
      height: 50px;
      vertical-align: top;
    }
    .item-price {
      font-size: 25px;
      color: rgb(255, 0, 0);
    }
    .item-p {
      display: inline-block;
      font-size: 13px;
      width: 430px;
      height: 50px;
      word-wrap: break-word;
    }
  }

  .buys {
    .buys-on {
      width: 100px;
      height: 40px;
      font-size: 18px;
      margin-left: 400px;
    }
  }
    .main-title {
      height: 70px;
      h2 {
        margin-bottom: 0px;
    }
  }
}
</style>