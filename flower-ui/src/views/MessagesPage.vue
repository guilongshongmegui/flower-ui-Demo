<template>
    <div class="common-layout chatBox" style="margin-bottom: 10px;">
        <el-container style="position: relative;">
            <el-aside width="240px">
                <!-- 搜索款 -->
                <div class="add-box">
                    <input type="text" class="search-in" @focus="switchPage(1)" @keydown.enter="searchUser"
                        v-model="SearchData">
                    <input type="button" value="搜索" class="search-bt">
                </div>
                <div style="height: 60px;"></div>
                <!-- 搜索数据 -->
                <div class="search-friend" v-if="isShowSearch" style="background-color: #eee;">
                    <div class="search-friend-back">
                        <div @click="switchPage(0)"><b>《</b></div>
                    </div>
                    <div class="search-friend-item" v-if="isShowItem" @click="agreeFriend(friendItem.id.toString())">
                        <div class="item-img">
                            <img :src="imgUrl" alt="">
                        </div>
                        <div class="item-text">
                            <p>{{ friendItem.userName }}</p>
                        </div>
                    </div>
                </div>
                <!-- 好友列表 -->
                <div class="friend-list" v-if="!isShowSearch">
                    <!-- <div class="friend-list" v-if="true"> -->
                    <div class="friend-item" v-for="item in friendList" :key="item.id"
                        @click="SendaMessage(item.id, item.name)">
                        <div class="item-HP">
                            <img :src="imgUrl" alt="" style="background-color: aliceblue;">
                        </div>
                        <div class="item-right">
                            <p class="item-name">{{ item.name }}</p>
                        </div>
                    </div>
                </div>
                <!-- 好友请求 -->
                <div class="friend-req" v-if="Zindex">
                    <div v-if="friendReqList.length > 0">
                        <div class="friend-req-item" v-for="item in friendReqList" :key="item.id">
                            <div class="item-HP"><img :src="imgUrl" alt="" style="background-color: aliceblue;"></div>
                            <div class="item-message">
                                <p class="item-message-name">{{ item.userName }}</p>
                                <div class="item-wait" v-if="!item.isShow">
                                    <p class="item-wait-btu">等待同意</p>
                                </div>
                                <div class="item-req" v-else>
                                    <input class="item-req-btu" type="button" value="同意" @click="agreeReq(item.id)">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div v-else>
                        <p>Null</p>
                    </div>
                </div>
                <!-- 切换页面 -->
                <div class="cutList" @click="qie"><b>!</b></div>
            </el-aside>

            <el-main style="padding: 0; " ref="child">
                <el-header style="background-color: #ddd;">
                    <p>{{ Objname.name }}</p>
                </el-header>
                <el-main class="putMain">

                    <el-scrollbar ref="scrollbarRef" height="400px">
                        <div ref="innerRef">
                            <div :class="{ 'message-box': true, 'message-box-right': item.boolkey }"
                                v-for="item in messageObject" :key="item.Id">
                                <div class="message-HP">
                                    <img :src="imgUrl" alt="">
                                </div>
                                <div class="message-text">{{ item.messages }}</div>
                            </div>
                        </div>
                    </el-scrollbar>

                </el-main>
                <el-footer style="padding: 0;">
                    <el-input class="inputBox" v-model="textMessage" :rows="3" type="textarea" placeholder="Please input" />
                    <button class="putBut" @click="sendMessage">发送</button>
                </el-footer>
            </el-main>
        </el-container>
    </div>
    <!-- <div @click="addrFloorsClick">dddddddd</div> -->
</template>

<script lang="ts" setup>
import { ref, onMounted, nextTick } from 'vue';
import * as signalR from '@microsoft/signalr'
//import { SearchUser, AddFriendUser } from "../http/index";
// import { ElMessage, ElMessageBox } from 'element-plus'
import { ElMessage } from 'element-plus'
import { SearchUser } from "../http/index";
import { db } from '../http/dexie';
import { ElScrollbar } from 'element-plus'

const imgUrl = "./images/R-C.jpg"

const textMessage = ref();
const isShowSearch = ref(false);
const isShowItem = ref(false);
//const isShowBtu = ref(false);
const friendItem = ref( //搜索出来的对象
    {
        id: 0,
        userName: 'nnnn'
    }
)
const friendList = ref([  //好友列表
    { id: '0', name: '客服' }
])
const friendReqList = ref([ //列表对象
    { id: '0', userName: 'hh', isShow: '' }
])
//搜索的值
const SearchData = ref('');
const SearchDataObj = {
    id: 0
};


//切换页面
const switchPage = (a: number) => {
    if (a) {
        if (isShowSearch.value != true) {
            isShowSearch.value = !isShowSearch.value;
        }
    } else {
        isShowSearch.value = !isShowSearch.value;
        SearchData.value = ''
    }
}
//切换列表
const Zindex = ref(false)
const qie = () => {
    Zindex.value = !Zindex.value
}
//搜索框回车搜索时
const searchUser = async () => {
    friendItem.value = { id: 0, userName: '' }
    SearchDataObj.id = parseInt(SearchData.value);
    var aa = (await SearchUser(SearchDataObj)).data.result;
    if (aa.userName != null) {
        friendItem.value.id = aa.id
        friendItem.value.userName = aa.userName
        isShowItem.value = true
    }
    else {
        ElMessage({
            message: '用户不存在！',
            type: 'warning',
        })
    }
}
//添加用户
// const addFriend = async (id: number) => {
//     ElMessageBox.confirm(
//         '确认添加此用户',
//         '提示',
//         {
//             confirmButtonText: 'OK',
//             cancelButtonText: 'Cancel',
//         }
//     )
//         .then(async () => {
//             var aa = (await AddFriendUser(SearchDataObj)).data.result;
//             ElMessage({
//                 type: 'success',
//                 message: '添加成功',
//             })
//         })
//         .catch(() => {
//             ElMessage({
//                 type: 'info',
//                 message: 'canceled',
//             })
//         })
// }

const Objname = ref({//当前聊天对象
    id: '0',
    name: '...'
})
//切换聊天对象
const SendaMessage = (a: string, b: string) => {
    messageObject.value = messageObject.value.filter(() => false);
    Objname.value.id = a;
    Objname.value.name = b;
    addMessageBox(b);
}
//SignalR连接对象
//const httpUrl = "http://localhost:5136/chathubs"
const httpUrl = "http://119.91.231.224:5136/chathubs"
const connection = new signalR.HubConnectionBuilder().withUrl(httpUrl, {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets,
    //accessTokenFactory: () => "Bearer " + localStorage["token"]
}).build();

var tableS = null;

const userPin = async () => {
    
}

//钩子，挂载是触发
onMounted(async () => {
    tableS = db.createTable<Item>('tableS');

    await userPin();

    connection.start().then(async () => {
        //执行查询关系请求
        if (connection.state == 'Connected') {
            //显示页面时，加载已有好友方法 & 创建连接关系
            console.log("连接成功");
            await connection.invoke('ReaderFriends', localStorage["token"]);
        }
    });
    //接收消息
    connection.on('ReceiveMessage', async (acceptName, message) => {
        //存储在本地DB
        await addIndexDB(acceptName, message, false)
        //渲染到页面
        if (Objname.value.name == acceptName) {
            addMessageBox(acceptName);
        } else {
            ElMessage("接收到来自" + acceptName + "的消息")
        }
    })
    //接收列表
    connection.on('ReceiveFriendList', (message) => {
        console.log(message)
        //加载已有用户
        for (var h = 0; h < message.haveBecome.length; h++) {
            friendList.value.push({
                id: message.haveBecome[h].id,
                name: message.haveBecome[h].name
            })
        }

        //追加等待 pyydetty
        friendReqList.value = friendReqList.value.filter(() => false);
        for (var i = 0; i < message.toBeAgreed.length; i++) {
            friendReqList.value.push({
                id: message.toBeAgreed[i].id,
                userName: message.toBeAgreed[i].name,
                isShow: ""
            })
        }
        //追加回应
        for (var j = 0; j < message.pyydetty.length; j++) {
            friendReqList.value.push({
                id: message.pyydetty[j].id,
                userName: message.pyydetty[j].name,
                isShow: "true"
            })
        }
    })
});

interface Item {  //存储到数据库的对象
    id?: number;
    name: string;
    messages: string;
    boolkey: boolean;
}
const messageObject = ref([ //消息框对象
    {
        Id: 0,
        name: '客服',
        messages: 'Hello Word!',
        boolkey: false
    }
])

//发送消息
const sendMessage = async () => {
    // try {
    //发送消息
    await connection.invoke('SendMessage', localStorage["token"], Objname.value.id, textMessage.value);

    //添加到本地数据库
    await addIndexDB(Objname.value.name, textMessage.value, true)
    //添加到消息框
    addMessageBox(Objname.value.name)

    //清除对话框
    textMessage.value = null
};
//添加好友
const agreeFriend = async (a: string) => {
    try {
        await connection.invoke('AgreeFriend', localStorage["token"], a);
    } catch (error) {
        ElMessage({
            message: '添加错误，请稍后尝试',
            type: 'warning',
        })
    }
}
//同意添加
const agreeReq = async (a: string) => {
    await connection.invoke('agreeReq', localStorage["token"], a);
}


//渲染到消息框
const addMessageBox = (Name: string) => {
    messageObject.value = messageObject.value.filter(() => false)
    tableS!.where('name')
        .equals(Name)
        .toArray()
        .then((results: Item[]) => {
            // 处理查询结果
            results.forEach(element => {
                messageObject.value.push({
                    Id: element.id,
                    name: element.name,
                    messages: element.messages,
                    boolkey: element.boolkey

                })
            });
        })
        .catch((error: string) => {
            console.error("Error querying data: " + error);
            ElMessage({
                message: '出错了！请稍后尝试或者联系管理员',
                type: 'warning',
            })
        });

    nextTick(() => {
        setTimeout(() => {
            const scrollHeight = innerRef.value!.scrollHeight
            //使用组件的setScrollTop方法滚动到内容的高度就是最底部
            scrollbarRef.value!.setScrollTop(scrollHeight)
        }, 50);
    });
}

//添加到数据库
const addIndexDB = async (Name: string, Messages: string, Boolkey: boolean) => {
    if (Boolkey) {
        await tableS!.add({ name: Name, messages: Messages, boolkey: true });
    } else {
        await tableS!.add({ name: Name, messages: Messages, boolkey: false });
    }
}

//更新数据后跳转到最底部
const innerRef = ref<HTMLDivElement>()   //滚动内容绑定
const scrollbarRef = ref<InstanceType<typeof ElScrollbar>>() //Element Plus绑定组件
// const addrFloorsClick = () => {
//     //将滚动内容的高度提取出来
//     const scrollHeight = innerRef.value!.scrollHeight
//     //使用组件的setScrollTop方法滚动到内容的高度就是最底部
//     scrollbarRef.value!.setScrollTop(scrollHeight)
// };


</script>

<style lang="scss">
.chatBox {
    border: 1px solid #000;
    border-radius: 20px;
    overflow: hidden;
}

.el-aside {

    /* 隐藏主要滚动条 */
    -ms-overflow-style: none;
    /* IE and Edge */
    scrollbar-width: none;
    /* Firefox */

    /* 使用伪元素添加滚动条样式 */
    &::-webkit-scrollbar {
        display: none;
        /* Safari and Chrome */
    }
}

.add-box {
    position: absolute;
    top: 0;
    left: 0;
    width: 240px;
    height: 60px;
    background-color: #ddd;

    .search-in {
        width: 170px;
        height: 36px;
        padding: 10xp;
        border-radius: 10px;
        margin: 10px 10px 0 0;
    }

    .search-bt {
        width: 40px;
        height: 40px;
        border-radius: 20px;
        border-width: 1px;
    }
}

.friend-list {
    width: 100%;
    height: 580px;
    overflow: hidden;
    background-color: #f0f0f0;

    .friend-item {
        width: 100%;
        height: 70px;
        padding: 5px 0;
        display: flex;
        justify-content: start;

        .item-HP {
            width: 70px;
            height: 70px;
            border-radius: 50%;
            margin-left: 10px;
            background-color: #eee;
            overflow: hidden;

            img {
                width: 100%;
            }
        }

        .item-right {
            width: 150px;
            height: 70px;
            margin-left: 10px;
            line-height: 70px;
            text-align: left;
        }

    }
}

.friend-req {
    width: 240px;
    height: 580px;
    overflow: hidden;
    background-color: #f0f0f0;
    position: absolute;
    top: 60px;
    left: 0;

    .friend-req-item {
        width: 100%;
        height: 70px;
        padding: 5px 0;
        display: flex;
        justify-content: start;

        .item-HP {
            width: 70px;
            height: 70px;
            border-radius: 50%;
            margin-left: 10px;
            background-color: #eee;
            overflow: hidden;

            img {
                width: 100%;
            }
        }

        .item-message {
            width: 160px;
            height: 50px;
            margin-top: 20px;

            .item-wait-btu {
                font-size: 14px;
                color: #77777783;
            }

            .item-req {
                display: flex;
                justify-content: end;

                .item-req-btu {
                    margin-right: 20px;
                    background-color: #fff;
                    color: #67c23a;
                    border: 0px;
                }
            }
        }
    }
}

.cutList {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    background-color: #2c3e50;
    position: absolute;
    bottom: -26px;
    left: 60px;
    color: #fff;
    cursor: pointer;
}

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



.putMain {
    height: 500px;
    /* 隐藏主要滚动条 */
    -ms-overflow-style: none;
    /* IE and Edge */
    scrollbar-width: none;
    /* Firefox */

    /* 使用伪元素添加滚动条样式 */
    &::-webkit-scrollbar {
        display: none;
        /* Safari and Chrome */
    }

    .message-box {
        width: 100%;
        margin-bottom: 10px;
        display: flex;

        .message-HP {
            width: 40px;
            height: 40px;
            border-radius: 20px;
            margin: 0 5px;
            background-color: #eee;
            overflow: hidden;

            img {
                width: 100%;
            }
        }

        .message-text {
            background-color: #eee;
            padding: 10px;
            margin: 10;
            border-radius: 10px;
        }
    }

    .message-box-right {
        flex-direction: row-reverse;
    }
}

.el-footer {
    width: 760px;
    height: 80px;
    margin-bottom: 0px;
    display: flex;
    border-top: 1px solid #000;
    background-color: rgb(245, 245, 245);
    justify-content: space-around;

    .inputBox {
        height: 65px;
        width: 90%;
        border-width: 0;
        background-color: rgb(245, 245, 245);
    }

    .putBut {
        height: 100%;
        width: 9%;
        border-width: 0;
    }
}
</style>
