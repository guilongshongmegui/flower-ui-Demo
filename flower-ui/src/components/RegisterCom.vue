<template>
  <el-dialog
    v-model="IsShowRegister"
    title="注册"
    width="30%"
    :before-close="handleClose"
  >
    <el-form :model="form" label-width="120px">
      <el-form-item label="昵称">
        <el-input v-model="form.NickName" />
      </el-form-item>
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
        <el-button @click="CloseRegister">取消</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script lang="ts" setup>
import { computed, ref } from "vue";
import { useStore } from "vuex";
import { Register } from "../http/index";
import { ElMessage } from "element-plus";
import { FormatToken } from "@/global";

//vuex
const store = useStore();
const IsShowRegister = computed(() => store.state.IsShowRegister);
const handleClose = (done: () => void) => {
  store.commit("CloseRegister");
  done();
};

const form = ref();
form.value = {
  NickName: "",
  UserName: "",
  Password: "",
};
const Submit = async () => {
  var data = {
    UserName: form.value.UserName,
    NickName: form.value.NickName,
    Password: form.value.Password,
  };
  var res = (await Register(data)).data;
  if (res.isSuccess) {
    ElMessage({
      message: "注册成功！",
      type: "success",
    });
    let user = JSON.parse(FormatToken(res.result))
    localStorage["token"] = res.result;
    //设置全局变量的值
    store.commit("SettingNickName", user.NickName);
    //设置localStorage，保证页面刷新后vuex的值可以从里面读，避免刷新后状态丢失
    localStorage["NickName"] = user.NickName;
    //登录成功后隐藏登录框
    store.commit("CloseRegister");
  } else {
    ElMessage.error(res.msg);
  }
};
const CloseRegister = () => {
  store.commit("CloseRegister");
};
</script>
