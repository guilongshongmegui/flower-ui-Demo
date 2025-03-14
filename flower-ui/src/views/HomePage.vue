<template>
    <div>
        <el-carousel style="height: 450px;" v-if="isShow">
            <el-carousel-item v-for="item in images" :key="item">
                <el-link :href="item.courseUrl" target="_blank">
                    <el-image :src="item.imageUrl"></el-image>
                </el-link>
            </el-carousel-item>
        </el-carousel>
    </div>
    <HomeContent/>
</template>

<script setup>
    import { ref,onMounted } from 'vue'
    import { getBanners2 } from '../http/index'
    import HomeContent from '../components/HomeContent.vue'

    const isShow= ref(false);
    const images = ref();
    onMounted(async()=>{
        images.value = (await getBanners2()).data
        isShow.value=true;
    })
</script>

<style lang="scss">
.el-carousel__container {
    height: 480px;
    margin-top: 10px;
}
</style>