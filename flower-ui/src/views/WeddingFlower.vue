<template>
    <div class="common-layout">
    <el-container>
      <el-header>
        <el-breadcrumb separator="/" style="line-height: 60px;">
          <el-breadcrumb-item :to="{ path:'/' }">首页</el-breadcrumb-item>
          <el-breadcrumb-item>列表页</el-breadcrumb-item>
        </el-breadcrumb>
      </el-header>

      <el-main>
        <el-container>
          <el-aside width="200px" class="navigetion">
            <div class="navigetion-item"></div>
            <div class="navigetion-item"></div>
            <div class="navigetion-item"></div>
            <div class="navigetion-item"></div>
            <div class="navigetion-item"></div>
          </el-aside>
          <el-container>
            <el-header class="main-header">
              <el-image class="zhanShi" src="http://119.91.231.224/images/flower/theme/xianhua03_20190213.jpg"></el-image>
            </el-header>
            <el-main class="main-main">

              <div class="el-main-item" v-for="item in flowers" :key="item">
                <a @click="GoToDetail(item.id)">
                  <div class="img-box">
                    <el-image :src="htp+item.image" />
                  </div>
                  <div class="item-content">
                    <p class="item-content-title">{{item.title}}</p>
                    <p class="item-content-pirce">￥{{ item.price }}</p>
                  </div>
                </a>
              </div>

            </el-main>
          </el-container>
        </el-container>
      </el-main>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from "vue";
import { getFlowers } from "../http/index";
import router from "@/router";

const htp = ref("http://119.91.231.224");
const flowers = ref();

let getData = {
  id: 0,
  type: 4,
}
onMounted(async () => {
  flowers.value = (await getFlowers(getData)).data.result;
  console.log(flowers.value[0]);
});

const GoToDetail = (curryId: Number) =>{
  const sss = ref();
  sss.value = curryId;
  router.push({path: '/detail', query: {id:sss.value, type: getData.type } } );
}
</script>

