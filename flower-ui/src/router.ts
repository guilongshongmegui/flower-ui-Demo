import { createRouter,createWebHashHistory } from "vue-router";
import HomePage from './views/HomePage.vue'
import LoveFlower from './views/LoveFlower.vue'
import BirthdayFlower from './views/BirthdayFlower.vue'
import FriendFlower from './views/FriendFlower.vue'
import WeddingFlower from './views/WeddingFlower.vue'
import FlowerDetail from './views/FlowerDetail.vue'
import FlowerPay from './views/FlowerPay.vue' 
import Personcenter from './views/PersonCenter.vue'
import MessagePage from './views/MessagesPage.vue'

const router = createRouter({
    history: createWebHashHistory(),
    routes:[
        {path:'/',component:HomePage},
        {path:'/loveflower',component:LoveFlower},
        {path:'/bithdayflower',component:BirthdayFlower},
        {path:'/friendflower',component:FriendFlower},
        {path:'/weddingflower',component:WeddingFlower},
        {path:'/detail',component:FlowerDetail},
        {path:'/pay',component:FlowerPay},
        {path:'/personcenter',component:Personcenter},
        {path:'/messagepage',component:MessagePage}
    ]
})
export default router

