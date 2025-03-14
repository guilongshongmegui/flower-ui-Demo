//引入Vuex的模块
import { createStore } from 'vuex'



//创建需要的状态信息
const store = createStore({
  state() {
    return {
      IsShowLogin: false,             //是否显示登录弹窗
      IsShowRegister: false,          //是否显示注册弹窗
      IsShowService: false,
      NickName: localStorage["NickName"]//昵称，登录成功后赋值
    }
  },
  mutations: {                        //需要的方法
    OpenLogin(state) {                //打开登录框
      state.IsShowLogin = true;
    },
    CloseLogin(state) {               //关闭登录框
      state.IsShowLogin = false;
    },
    OpenRegister(state) {             //打开注册框
      state.IsShowRegister = true;
    },
    CloseRegister(state) {            //关闭注册框
      state.IsShowRegister = false;
    },
    SettingNickName(state, NickName) { //设置昵称的方法
      state.NickName = NickName;
    },
    OpenService(state) {
      state.IsShowService = true;     //打开客服框
    },
    CloseService(state) {
      state.IsShowService = false;    //关闭客户框
    }
  }
})
export default store;