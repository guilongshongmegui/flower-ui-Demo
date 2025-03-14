<template>
  <div class="common-layout">
    <el-container>
      <el-header>
        <div class="header-left">
          <h3 style="width: 110px; font-size: 25px">爱情鲜花</h3>
          <span class="EheaderDiv"></span>送·让你怦然心动的人
        </div>
        <div class="header-right"><a href="">更多爱情鲜花</a></div>
      </el-header>

      <el-container class="common-main">

        <el-aside style="width: 200px">
          <el-image src="http://119.91.231.224/images/content/home_floor_elder.png" />
        </el-aside>

        <el-main>
          <div class="el-main-item">
            <a href="">
              <div class="img-box">
                <el-image src="http://119.91.231.224/images/content/y3.jpg" />
              </div>
              <div class="item-content">
                <p class="item-content-title">鲜花的标题</p>
                <p class="item-content-pirce">￥200</p>
              </div>
            </a>
          </div>
          <div class="el-main-item">
            <a href="">
              <div class="img-box">
                <el-image src="http://119.91.231.224/images/content/y7.jpg" />
              </div>
              <div class="item-content">
                <p class="item-content-title">鲜花的标题</p>
                <p class="item-content-pirce">￥200</p>
              </div>
            </a>
          </div>
          <div class="el-main-item">
            <a href="">
              <div class="img-box">
                <el-image src="http://119.91.231.224/images/content/s1.jpg" />
              </div>
              <div class="item-content">
                <p class="item-content-title">鲜花的标题</p>
                <p class="item-content-pirce">￥200</p>
              </div>
            </a>
          </div>
          <div class="el-main-item">
            <a href="">
              <div class="img-box">
                <el-image src="http://119.91.231.224/images/content/a5.jpg" />
              </div>
              <div class="item-content">
                <p class="item-content-title">鲜花的标题</p>
                <p class="item-content-pirce">￥200</p>
              </div>
            </a>
          </div>
          <div class="el-main-item">
            <a href="">
              <div class="img-box">
                <el-image src="http://119.91.231.224/images/content/a1.jpg" />
              </div>
              <div class="item-content">
                <p class="item-content-title">鲜花的标题</p>
                <p class="item-content-pirce">￥200</p>
              </div>
            </a>
          </div>
          <div class="el-main-item">
            <a href="">
              <div class="img-box">
                <el-image src="http://119.91.231.224/images/content/s5.jpg" />
              </div>
              <div class="item-content">
                <p class="item-content-title">鲜花的标题</p>
                <p class="item-content-pirce">￥200</p>
              </div>
            </a>
          </div>
          <div class="el-main-item">
            <a href="">
              <div class="img-box">
                <el-image src="http://119.91.231.224/images/content/y1.jpg" />
              </div>
              <div class="item-content">
                <p class="item-content-title">鲜花的标题</p>
                <p class="item-content-pirce">￥200</p>
              </div>
            </a>
          </div>
          <div class="el-main-item">
            <a href="">
              <div class="img-box">
                <el-image src="http://119.91.231.224/images/content/s2.jpg" />
              </div>
              <div class="item-content">
                <p class="item-content-title">鲜花的标题</p>
                <p class="item-content-pirce">￥200</p>
              </div>
            </a>
          </div>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<style lang="scss">
.common-layout {
  width: 1000px;
  margin: auto;

  .el-header {
    line-height: 60px;
    display: flex;
    justify-content: space-between;

    .header-left {
      width: 350px;
      display: flex;
      flex-direction: row;
      .EheaderDiv {
        height: 30px;
        border: 1px solid #000;
        margin: 15px 10px;
      }
    }
    .header-right {
      color: #777;
      a{
          text-decoration: none;
          color: #000;
      }
    }
  }

  .common-main{
    height: 500px;

    .el-aside{
      .el-image{
        width: 200px;
        height: 480px;
      }
    }

    .el-main{
      padding: 0;
      padding-left: 20px;
      display: flex;
      flex-direction: row;
      flex-wrap: wrap;

      .el-main-item{
        width: 175px;
        height: 230px;
        margin-right: 20px;
        margin-bottom: 20px;
        a{
          text-decoration: none;
          color: #000;
        }

        .img-box{
          width: 175px;
          height: 175px;
          margin-bottom: 5px;
          overflow: hidden;
        }
        .item-content-title{
          font-size: 14px;
          margin-bottom: 5px;
        }
        .item-content-pirce{
          font-weight: 700;
        }
      }
    }
  }
}
</style>