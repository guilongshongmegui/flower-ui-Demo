<template>
    <div class="search-friend" v-if="isShowSearch" style="background-color: #eee;">
        <div class="search-friend-back">
            <div @click=""><b>《</b></div>
        </div>
        <div class="search-friend-item" v-if="isShowItem" @click="">
            <div class="item-img">
                <img src="" alt="">
            </div>
            <div class="item-text">
                <p>{ friendItem.userName }}</p>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue';

const isShowSearch = ref(false)
const isShowItem = ref(false)

</script>

<style lang="scss">
.search-friend {
    width: 100%;
    height: 580px;
    overflow: hidden;

    .search-friend-back {
        div {
            width: 30px;
            height: 30px;
            line-height: 30px
        }
    }

    .search-friend-item {
        width: 100%;
        height: 75px;
        padding-top: 5px;
        display: flex;
        justify-content: start;

        .item-img {
            width: 60px;
            height: 60px;
            margin-left: 10px;
            border-radius: 50%;
            background-color: #fff;

            img {
                width: 100%;
            }
        }

        .item-text {
            width: 150px;
            height: 80px;
            padding-left: 20px;
            line-height: 80px;
            text-align: left;
        }
    }
}
</style>