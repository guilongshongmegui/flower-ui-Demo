<template>
  <div class="common-layout">
    <div class="mge-header">
      <li class="hdr-item" @click="onMessage()">消息</li>
      <li class="hdr-item Now-hdr-item">订单</li>
    </div>
    <el-container>
      <el-header class="header-top"> 订单列表 </el-header>
      <el-main>
        <el-table :data="list" height="400px" stripe style="width: 100%">
          <el-table-column prop="orderDate" label="订单日期" width="200" />
          <el-table-column prop="orderNumber" label="订单号" width="200" />
          <el-table-column prop="flowerTitle" label="商品标题" />
          <el-table-column prop="price" label="价格" />
        </el-table>
      </el-main>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import { ref,onMounted} from 'vue'
import { GetOrder2 } from '@/http'
import { useRouter } from 'vue-router';

const list = ref();
// onMounted(async() => {
//     list.value = (await GetOrder())
// })
onMounted(async() => {
    list.value = ((await GetOrder2()).data.result)
})

const router = useRouter();
const onMessage = () =>{
  router.push({path:'/messagepage'})
}

</script>

<style lang="scss">
.header-top {
  margin: auto;
}
.mge-header{
  width: 100%;
  height: 60px;
  display: flex;
  justify-content: flex-end;

  .hdr-item{
    list-style: none;
    display: inline-block;
    width: 80px;
    height: 59px;
    line-height: 60px;
    margin: 0 10px;
  transition: background-color 0.25s ease, color 0.25s ease;
  }
  .hdr-item:hover{
    background-color: #409eff;
    color: #fff;
  }
  .Now-hdr-item{
    border-bottom: 1px solid #000;
  }
}
</style>