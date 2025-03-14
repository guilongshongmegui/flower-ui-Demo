<template>
  <el-dialog
    v-model="IsShowLogin"
    title="登录"
    width="30%"
    :before-close="handleClose"
  >
    <el-form :model="form" label-width="120px">
      <el-form-item label="用户名">
        <el-input v-model="form.UserName" />
      </el-form-item>
      <el-form-item label="密码">
        <el-input v-model="form.Password" type="password" show-password />
      </el-form-item>

      <!-- <el-form-item label="验证码">
        <el-input v-model="form.ValidateCode" />
        <el-image
          style="width: 200px; height: 100px"
          :src="vaildImages"
          @click="ChangeImage"
        />
      </el-form-item> -->

      <el-form-item>
        <el-button type="primary" @click="Submit">确定</el-button>
        <el-button @click="CloseLogin">取消</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script lang="ts" setup>
import { computed,ref } from "vue";
import { useStore } from "vuex";
import { getToken } from "../http/index"
import { ElMessage } from "element-plus";
import { FormatToken } from "@/global"; 

//vuex
const store = useStore();
const IsShowLogin = computed(() => store.state.IsShowLogin);
//:before-close是ElementPro自带的事件
const handleClose = (done: () => void) => {
  store.commit("CloseLogin");
  done();
};

const form = ref();
form.value = {
  UserName:"",
  Password:"",
}

const Submit =async () => {
  var data = {
    UserName: form.value.UserName,
    Password: form.value.Password,
  };
  var res = (await getToken(data)).data;
  if (res.isSuccess) {
    ElMessage({
      message: "登录成功！",
      type: "success",
    });
    let user = JSON.parse(FormatToken(res.result))
    console.log(user)
    localStorage["token"] = res.result;
    //设置全局变量的值
    store.commit("SettingNickName", user.NickName);
    //设置localStorage，保证页面刷新后vuex的值可以从里面读，避免刷新后状态丢失
    localStorage["NickName"] = user.NickName;
    //登录成功后隐藏登录框
    store.commit("CloseLogin");
  } else {
    ElMessage.error(res.msg);
  }

}

const CloseLogin = () => {
  store.commit("CloseLogin");
}
</script>