<template>
    <el-dialog v-model="IsShowService" title="在线客服" width="1000px" :before-close="handleClose">
        <div class="ServiceDiv">
            <div v-if="IsShow">等待对接客服</div>
            <div v-if="!IsShow">对接成功</div>
        </div>
        <div class="ServicePut">
            <el-input v-model="textarea" :autosize="{ minRows: 2, maxRows: 4 }" type="textarea" placeholder="Please input"
                width="100px" />
            <div class="PutServer">
                <el-button type="primary" round @click="PutIn">发送</el-button>
            </div>
        </div>
    </el-dialog>
</template>

<script lang="ts" setup>
// import { computed, ref } from "vue";
// import { useStore } from "vuex";

// const IsShow = ref(true)

// //WebSocket
// var server = 'ws://localhost:5136';                //如果开启了https则这里是wss
// //var server = 'ws://119.91.231.224:5136';
// var WEB_SOCKET = new WebSocket(server + '/ws?Id=123');

// WEB_SOCKET.onopen = function () {
//     console.log('打开连接 ...');                      //打开连接
//     const aa = ref({
//         textarea: "Hello Word",
//         Id: 1
//     })
//     WEB_SOCKET.send(JSON.stringify(aa.value));  //向服务器发送消息，以JSON格式
// };
// WEB_SOCKET.onmessage =async (evt) => {
//     //console.log('接收到来自服务器的消息: ');
//     var bb = evt.data.replace(/\0/g, '')             //去除空字符串
//     bb = bb.replace(/"/g,'')                         //去除引号
//     //添加到页面
//     AddMeg(1,bb)
// };
// WEB_SOCKET.onclose = function () {
//     console.log('连接关闭.请刷新页面');
// };
// //WebSocket

// const store = useStore();
// const IsShowService = computed(() => store.state.IsShowService);

// const textarea = ref("  ")
// const textObj = ref({
//     textarea: textarea,
//     Id: 1
// })
//                                                 //消息发送
// const PutIn = async () => {
//     //发送消息
//     console.log(textObj)
//     WEB_SOCKET.send(JSON.stringify(textObj.value));
//     AddMeg(0, textarea.value)
//     textarea.value = '';
// }

// //添加消息框
// const AddMeg = (a: number, text: any) => {
//     const KhMeg = document.createElement('div')
//     const KhMegDiv = document.createElement('div')
//     if (a == 0) {
//         KhMeg.className = "UserMeg"
//         KhMegDiv.className = "RightMeg"
//     }
//     else {
//         //console.log("客服的")
//         KhMeg.className = "SeaMeg"
//         KhMegDiv.className = "LeftMeg"
//     }
//     KhMeg.innerText = text
//     KhMegDiv.appendChild(KhMeg)
//     document.querySelector('.ServiceDiv')?.appendChild(KhMegDiv)
// }


//                                              //关闭组件时
// const handleClose = (done: () => void) => {
//     store.commit("CloseService");

//     WEB_SOCKET.close(); //关闭组件时，关闭连接

//     done();
// };

</script>

<style lang="scss">
.ServiceDiv {
    margin: auto;
    width: 95%;
    height: 500px;
    border: 1px solid #000;
    border-radius: 10px;
    display: flex;
    flex-direction: column;

    .RightMeg {
        width: 100%;
        display: flex;
        flex-direction: row-reverse;
    }

    .UserMeg {
        background-color: lightgrey;
        padding: 10px;
        margin: 10px 80px 0 0;
        border-radius: 5px;
    }

    .LeftMeg {
        width: 100%;
        display: flex;
        flex-direction: row;
    }

    .SeaMeg {
        background-color: lightgrey;
        padding: 10px;
        margin: 10px 0 0 80px;
        border-radius: 5px;
    }
}

.ServicePut {
    margin: auto;
    margin-top: 10px;
    width: 95%;
    height: 50px;
    //background-color: antiquewhite;
    display: flex;
    flex-wrap: nowrap;
    justify-content: space-between;

    .PutServer {
        width: 80px;
        height: 60px;
    }
}
</style>