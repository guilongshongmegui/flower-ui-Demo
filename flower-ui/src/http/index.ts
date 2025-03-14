import axios from "axios";
import { ref } from "vue";
//需要拦截的地方使用instance对象   有自定义返回逻辑的地方沿用axios，在组件内部处理返回结果即可
import instance from "./filter";

//获取轮播图
const json = ref("/json");
//const http = ref("/api");
//const http = ref("http://119.91.231.224:5136/api");
const http = ref("http://localhost:5136/api");


export const getBanners = () =>{          //获取本地的json中的数据
    return axios.get(json.value + "/banner.json");
}
//获取轮播图
export const getBanners2 = () => {        
    return axios.get(http.value + "/Image/GetImages");
}
//注册
export const Register = (parms: {}) => { 
    return axios.post(http.value + "/Login/Register",parms);
}
//登录
export const getToken = (parms: {}) => {  
    return axios.post(http.value + "/Login/Check",parms);
}

//获取列表数据
export const getFlowers = (parms: {}) => {
    return axios.post(http.value + "/Flowers/GetFlowerType",parms);
}
export const getFlowersDome = (parms: {}) => {
    return axios.post(http.value + "/Flowers/GetFlowers",parms);
}

//创建订单
export const CreateOrder = (parms: {}) => {
    //在header里携带token访问端接口
    axios.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return axios.post(http.value + "/Order/CreateOrder",parms);
}

//获取订单列表 
//将axios换成instance就可以是用axios的自己创建的拦截器了
export const GetOrder = () => {
    //在header里携带token访问端接口
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return instance.post(http.value + "/Order/GetOrder");
}
export const GetOrder2 = () => {
    //在header里携带token访问端接口
    axios.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return axios.post(http.value + "/Order/GetOrder");
}

//搜索用户
// export const SearchUser = (parms: {}) => {
//     //return axios.get(http.value + "/Friends/Getfriend",{ parms });
//     return axios.get(http.value+'/Friends/Getfriend',parms);
// }
export const SearchUser = (parms: {}) => {
    return axios.post(http.value + "/Friends/Getfriend",parms);
}

//添加用户
export const AddFriendUser = (parms: {}) => {
    axios.defaults.headers.common['Authorization'] = "Bearer " + localStorage["token"];
    return axios.post(http.value + "/Friends/AddFriend",parms);
}
//验证是否已经登陆或者登陆超时
export const PinUser = () => {
    axios.defaults.headers.common['Authorization'] = "Bearer" + localStorage["token"];
    return axios.post(http.value + "/Login/PinUser");
}