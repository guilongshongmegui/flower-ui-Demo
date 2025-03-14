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
              <el-image class="zhanShi" src="http://119.91.231.224/images/flower/theme/flower_bannerLove_190906.jpg"></el-image>
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
  type: 1,
}
onMounted(async () => {
  flowers.value = (await getFlowers(getData)).data.result;
});

const GoToDetail = (curryId: Number) =>{
  const sss = ref();
  sss.value = curryId;
  router.push({path: '/detail', query: {id:sss.value, type: getData.type } } );
}
</script>

<style lang="scss">
.navigetion {
  border: 1px solid #000;
  border-width: 0px 1px 0 1px;
  border-top: 1px dashed #000;
  padding: 0 5px 0 5px;

  .navigetion-item {
    width: 188px;
    height: 200px;
    border-bottom: 1px dashed #000;
  }
}
.main-header {
  height: 300px;

  .zhanShi {
    height: 300px;
  }
}

.main-main {
  
  display: flex;
  flex-wrap: wrap;
  flex-direction: row;
  justify-content: space-between;
  align-content: flex-start;

  .el-main-item {
    width: 175px;
    height: 230px;
    margin-bottom: 20px;
    box-sizing: border-box;
    a {
      text-decoration: none;
      color: #000;
    }

    .img-box {
      width: 175px;
      height: 175px;
      margin-bottom: 5px;
      overflow: hidden;
    }
    .item-content-title {
      font-size: 14px;
      margin-bottom: 5px;
    }
    .item-content-pirce {
      font-weight: 700;
    }
  }
}
</style>