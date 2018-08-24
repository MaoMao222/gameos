/**
 * Created by sjj1 on 2016/10/17.
 */
//左侧导航
$(function () {
  //tooltip
  $('.gg-examples1 a').ggtooltip();

  var menuList = $.cookie('data');
  menuList = JSON.parse(menuList);
  html = "";
  for (var i = 0; i < menuList.data.right.length; i++) {
    html += "<li  class='gg-examples1' data-href=\"" + menuList.data.right[i].url + "\"><a href = \"javascript:void(0);\" data-href=\"" + menuList.data.right[i].url + "\" data-placement=\"right\" data-backcolor=\"#fff\" data-textcolor=\"#333333\" data-bordercolor=\"#ccc\" data-title=\"\" ><i class=\"nav-icon\"  ></i><span>" + menuList.data.right[i].desc + "</span></a></li>";

  }
  $(".slide ul").append(html);
  $(".slide ul li:nth-child(1) a i").prepend("<i class=\"icon-android\"></i>");
  $(".slide ul li:nth-child(2) a i").prepend("<i class=\"icon-account-2\"></i>");
  $(".slide ul li:nth-child(3) a i").prepend("<i class=\"icon-game\"></i>");
  $(".slide ul li:nth-child(1) a ").attr("data-title", "游戏安卓包管理");
  $(".slide ul li:nth-child(2) a ").attr("data-title", "账号管理");
  $(".slide ul li:nth-child(3) a ").attr("data-title", "游戏资源管理插件");

  $('#userName').html(menuList.data.account);
  $(".slide ul li").click(function () {
    $(this).addClass("click").siblings().removeClass("click");
  });

  function initGameScorll() {
    var min = $("#j-iframe").contents().find('#table_box').width();
    //剩余宽度
    var resdueWidth = min - 60;
    //列数
    var cols = $("#j-iframe").contents().find("#table_scroll .tab-head>thead>tr>th").length - 1;
    //渠道宽度
    var channelWidth = resdueWidth / cols;
    var current = channelWidth * cols;
    if (min > current) {
      $("#j-iframe").contents().find('#table_scroll').css('width', min);
    } else {
      $("#j-iframe").contents().find('#table_scroll').css('width', current);
    }
  }

  // 左侧切换
  $(".viewer-slidebar").click(function () {
    if (
      $("body").hasClass("sidebar-collapse")) {
      $("body").removeClass("sidebar-collapse");
      $(".slide>ul>li>a>span").removeClass("sidebar-hide");
      $(".viewer-slidebar").removeClass("viewer-slidebar-min");
      $("#j-iframe").contents().find(".slide-right").removeClass("slide-right-min");
      $(".slide").removeClass("slide-min");
      $(".slide ul li ").css({"padding-left": "16px", "text-align": "left"});
      $(".slide ul li a").css({"width": "180", "padding-left": "16px", "text-align": "left"});
      $(".icon-unfold").removeClass("icon-fold");
      $('.gg-examples1 a').ggtooltip('destroy');
      window.isFill = true;
      return;
    }
    $("body").addClass("sidebar-collapse");
    $(".slide>ul>li>a>span").addClass("sidebar-hide");
    $(".viewer-slidebar").addClass("viewer-slidebar-min");
    $("#j-iframe").contents().find(".slide-right").addClass("slide-right-min");
    $(".slide").addClass("slide-min");
    $(".slide ul li ").css({"padding-left": "2px", "text-align": "center"});
    $(".slide ul li a").css({"width": "60", "padding-left": "2px", "text-align": "center"});
    $(".icon-unfold").addClass("icon-fold");
    $('.gg-examples1 a').ggtooltip();
    initGameScorll();
    window.isFill = false;
  });


  $('.slide ul').on('click', 'li', function () {
    var index = $('.slide ul li').index(this);
    var data_href = $(this).data();
    switch (index) {
      case 0:
        top.$('#j-iframe').attr('src', 'packagetools.html');
        break;
      case 1:
        top.$('#j-iframe').attr('src', 'login.html');
        break;
      case 2:
        top.$('#j-iframe').attr('src', 'packagetools.html');
        break;
      default:
        top.$('#j-iframe').attr('src', 'packagetools.html');
        break;
    }
  });


});