$(function () {
  window.taskchecked = {};
  //tooltip
  $('.gg-examples2 a').ggtooltip();

//下拉按钮向上弹出
  $("#tab-body").on("click", "tbody>tr", function (e) {
    $("#pageY").text(e.pageY);
    var pageY = $("#pageY").text();
    if (pageY > 330) {
      $(this).find(".btn-group").addClass("dropup").parent().parent().siblings().find(".btn-group").removeClass("dropup");

    } else {
      $(this).find(".btn-group").removeClass("dropup");
    }
  });

  $('input[data-requi=true]').keyup(function () {
    if ($(this).val()) {
      $(this).parent().find(".error").hide();
    }
  });

  function checkNull() {
    var isSuccess = true;
    $(".pack-table").find("input[data-requi=true]").each(function (a, b) {
      var txt_val = $(this).val();
      if (!txt_val) {
        $(this).parent().find('.error').remove();
        $(this).parent().append('<span class="error">字段不能为空</span>');
        isSuccess = false;
      }
    });
    return isSuccess;
  }

  function checkNullValue(select) {
    var isSuccess = true;
    $(select).find("input[data-requi=true]").each(function (a, b) {
      var txt_val = $(this).val();
      if (!txt_val) {
        $(this).parent().find('.error').remove();
        $(this).parent().append('<span class="error">字段不能为空</span>');
        isSuccess = false;
      }
    });
    return isSuccess;
  }

  //验证版本号是数字
  function checkNum(e) {
    var isSuccess = true;
    var reg = /^\d+$/;//用来验证数字
    var this_val = $(e).val();
    if (!reg.test(this_val)) {
      $(e).parent().find('.error').remove();
      $(e).parent().append('<span class="error">字段必须是数字</span>');
      isSuccess = false;
    }
    return isSuccess;
  }

  //全选;
  function allCheck(obj, item) {
    if (obj.checked == true) {
      $.each(item, function (i, n) {
        $(n).prop('checked', true);
      });
    } else {
      $.each(item, function (i, n) {
        $(n).prop('checked', false);
      });
    }
  }

  //横向滚动条
  window.initGameScorll = function initGameScorll() {
    var min = $('#table_box').width();
    //剩余宽度
    var resdueWidth = min - 80;
    //列数
    var cols = $("#table_scroll .tab-head>thead>tr>th").length-1;
    //渠道宽度
    var channelWidth = cols*10;
    var current = channelWidth * cols;
    if (min > current) {
      $('#table_scroll').css('width', min);
    } else {
      $('#table_scroll').css('width', current);
    }
  };

  //隐藏/显示
  $(".btn-icon").click(function () {
    var show = $(".none").attr('data-show');
    if (show && show == "show") {
      $(".none").hide();
      $(".none").attr('data-show', "hide");
      $(this).removeClass("btn-show");
      $('.gg-examples2 a').ggtooltip('destroy');
      $('.gg-examples2 a').data('title', "开启修改包信息");
      $('.gg-examples2 a').ggtooltip();
    } else if (show && show == "hide") {
      $(".none").show();
      $(".none").attr('data-show', "show");
      $(this).addClass("btn-show");
      $('.gg-examples2 a').ggtooltip('destroy');
      $('.gg-examples2 a').data('title', "关闭修改包信息");
      $('.gg-examples2 a').ggtooltip();
    }

  });

  //新增游戏 子资源

  $(".child-box").click(function () {
    if ($(".child-sel").css("display") == "none") {
      $(".child-sel").css("display", "block")
    } else {
      $(".child-sel").css("display", "none")
    }
  });

  //包结构信息切换
  function initPackage() {
    var index = 0;
    selectPackageTag(index);
  }

  function selectPackageTag(index) {
    var $li = $(".package-info .package-menu ul li");
    $($li[index]).addClass('on').siblings().removeClass('on');
    $(".tab-content > div").eq(index).show().siblings().hide();
  }

  //下一步
  $('#btn_last_1').click(function () {
    var isSuccess = true;
    $(".channel-info").find("input[data-requi=true]").each(function (a, b) {
      var txt_val = $(this).val();
      if (!txt_val) {
        $(this).parent().find('.error').remove();
        $(this).parent().append('<span class="error">字段不能为空</span>');
        isSuccess = false;
      }
    });
    if (!isSuccess) {
      return false;
    }
    selectPackageTag(1);
    $(".package-menu ul li.first span ").addClass("color").addClass("color-gray");
    $(".package-menu ul li.second span ").addClass("color").removeClass("color-gray");
  });

  //上一步
  $('#btn_first_2').click(function () {
    $(".package-menu ul li.first span ").removeClass("color-gray").addClass("color");
    $(".package-menu ul li.second span ").removeClass("color").addClass("color-gray");
    selectPackageTag(0);
  });

  //下一步
  $('#btn_last_2').click(function () {

    var isSuccess = true;
    $(".config").find("input[data-requi=true]").each(function (a, b) {
      var txt_val = $(this).val();
      if (!txt_val) {
        $(this).parent().find('.error').remove();
        $(this).parent().append('<span class="error">字段不能为空</span>');
        isSuccess = false;
      }
    });

    if (isSuccess) {
      $(".package-menu ul li.second span ").addClass("color-gray").addClass("color");
      selectPackageTag(2);
    }
    return false;

  });
  //上一步
  $("#btn_first_3").click(function () {
    $(".package-menu ul li.second span ").removeClass("color-gray").addClass("color");
    selectPackageTag(1);
  });

  /**
   * 为table指定行添加一行
   *
   * tab 表id
   * row 行数，如：0->第一行 1->第二行 -2->倒数第二行 -1->最后一行
   * trHtml 添加行的html代码
   *
   */
  function addTr(tab, row, trHtml) {
    //获取table最后一行 $("#tab tr:last")
    //获取table第一行 $("#tab tr").eq(0)
    //获取table倒数第二行 $("#tab tr").eq(-2)
    if ($("#" + tab + " tr").size() == 0) {
      $("#" + tab).html(trHtml);
    } else {
      var $tr = $("#" + tab + " tr").eq(row);
      $tr.after(trHtml);
    }
  }
  //检查暂时是否为空
  function checkValue() {
    var isSuccess = true;
    $(".game-table").find("input[data-requi=true]").each(function (a, b) {
      var txt_val = $(this).val();
      if (!txt_val) {
        $(this).parent().find('.error').remove();
        $(this).parent().append('<span class="error">字段不能为空</span>');
        isSuccess = false;
      }
    });
    return isSuccess;
  }
  var dataList = $.cookie('data');
  dataList = JSON.parse(dataList);
  //游戏查询列表
  var baseApi = "http://yp.bookse.cn/webservice.php";
  var sessionID = dataList.data.session ;
  var defaultParams = {
    module: "pluginspackage",
    session: sessionID
  };
  var funs = {
    /**
     * 初始化
     */
    init: function () {
      $("#table_scroll").mark("show");
      funs['getGameInfo'](function (_gameInfoList) {
        window.gameInfoList = _gameInfoList;
        funs['getChannelInfo'](function (__channelInfoList) {
          window.channelInfoList = __channelInfoList;
          //渲染
          funs['renderGameInfo'](gameInfoList);
          funs['renderChannelInfo'](channelInfoList);
          $("#table_scroll").mark("hide");
        })
      });
      $(window).resize(function () {
        initGameScorll();
      });
    },
    /**
     * 查询游戏信息
     * @param {Function} success
     * @param {Function}  error
     */
    getGameInfo: function (success, error) {
      var params = {
        funtion: "select_game_info",
        params: {
          'id': 0
        }
      };
      params = $.extend({}, defaultParams, params);
      $.get(baseApi, {
        send: JSON.stringify(params)
      }, function (res) {
        if (res.code == 0) {
          if (success)success(res.data);
        } else {
          if (error) {
            if (error)error(res.data);
          }
        }
      }, 'json')
    },
    /**
     * 获取渠道信息
     * @param {Function} success
     * @param {Function} error
     */
    getChannelInfo: function (success, error) {
      var params = {
        funtion: 'select_channel_info',
        params: {
          "id": 0
        }
      };
      params = $.extend({}, defaultParams, params);
      $.get(baseApi, {
        send: JSON.stringify(params)
      }, function (res) {
        if (res.code == 0) {
          if (success)success(res.data);
        } else {
          if (error)error(res.data);
        }
      }, 'json')
    },
    renderGameInfo: function (data) {
      var html = "";
      for (var i = 0; i < data.length; i++) {
        html += "<tr  data-id=\"" + data[i].id + "\" ><td  style=\"width:80px;\"><span class=\"btn-group menu-select\"><span class=\"dropdown-toggle btn-xs dropdown-btn\" data-toggle=\"dropdown\"><span class=\"name\">" + data[i].name + "<input class=\"data-opt\" type='hidden' value='" + JSON.stringify(data[i]) + "' /></span><span class=\"caret\"></span></span><ul class=\"dropdown-menu icon-select\" role=\"menu\" ><li class=\"all\" data-toggle=\"modal\">全选</li><li class=\"revise\" data-toggle=\"modal\" data-target=\"#myModal\">修改</li><li class=\"del-row\" data-toggle=\"modal\">删除</li><li class=\"cancel-all\" data-toggle=\"modal\">取消全选</li></ul></span></td></tr>";
      }
      $("#tab-body tbody").html('');
      $("#tab-body tbody").append(html);

      $('#tab-body').on('click', '.all', function () {
        var checked = $(this).parent().parent().parent().parent().find('[name=checkbox]').attr('checked', true);
      });
      $('#tab-body').on('click', '.cancel-all', function () {
        $(this).parent().parent().parent().parent().find('[name=checkbox]').attr('checked', false);
      });
      initGameScorll();
    },
    renderChannelInfo: function (data) {
      window.channel_list = data;
      var html = "";
      for (var i = 0; i < data.length; i++) {
        html += "<td style=\"min-width:100px;\">";
        if (data[i].c_apk != "0") {
          html += "<p style=\"margin:0px;\"><label><input type=\"checkbox\" data-type='c_apk' name =\"checkbox\" /><span class='package'>渠道包</span><span class=\"tab-btn none pakage-update\" data-type=\"c_apk\" data-show=\"hide\" data-toggle=\"modal\" data-target=\"#myModal4\">修改<input class=\"data-am\" type='hidden' value='" + JSON.stringify(data[i]) + "' /></span></label></p>";

        }
        if (data[i].f_apk != "0") {
          html += "<p style=\"margin:0px;\"><label><input type=\"checkbox\" data-type='f_apk' name =\"checkbox\" /><span class='package'>完整包</span><span class=\"tab-btn none pakage-update\" data-type=\"f_apk\" data-show=\"hide\" data-toggle=\"modal\" data-target=\"#myModal4\">修改<input class=\"data-am\" type='hidden' value='" + JSON.stringify(data[i]) + "' /></span></label></p>";
        }
        html += "</td>";
      }
      $("#tab-body tr").append(html);

      $(".tab-head thead tr>th").remove();
      $(".tab-head thead tr").append("<th style=\"width:80px;\">名称</th>");
      var result = "";
      for (var i = 0; i < data.length; i++) {
        result += "<th style=\"min-width:100px;\" data-id=\"" + data[i].id + "\" ><span class=\"btn-group menu-select\" style='min-width: 80px;'><span class=\"dropdown-toggle btn-xs\" data-toggle=\"dropdown\"><span>" + data[i].name + "<input class=\"data-op\" type='hidden' value='" + JSON.stringify(data[i]) + "' /></span><span class=\"caret\"></span></span><ul class=\"dropdown-menu icon-select dropdown-con\"><li class=\"channel-all\">全选</li><li class=\"amend-channel\" data-toggle=\"modal\" data-target=\"#myModal6\">修改</li><li class=\"del-col\">删除</li><li class=\"cancel-channel-all\">取消全选</li></ul></span></th>";
      }
      $(".tab-head thead tr").append(result);

      //全选
      $(".tab-head").on("click", ".channel-all", function () {
        var index = $(this).parent().parent().parent().index();
        channelSelect(index, true);
      });

      //渠道全选或取消全选
      function channelSelect(index, isChecked) {
        var trs = $('#tab-body > tbody > tr');
        $.each(trs, function (i, item) {
          var td = $(item).find('td')[index];
          $(td).find('[name=checkbox]').attr('checked', isChecked);
        });
      }

      //取消全选
      $(".tab-head").on("click", ".cancel-channel-all", function () {
        var index = $(this).parent().parent().parent().index();
        channelSelect(index, false);
      });
      //删除渠道包
      $("#tab tbody").on("click", 'tr td .delete', function () {
        $(this).parent().parent().remove();
      });
      $("#tab1 tbody").on("click", 'tr td .delete', function () {
        $(this).parent().parent().remove();
      });

      initGameScorll();
    }

  };
  funs['init']();

  //新增游戏初始化
  $('#myModal').on('show.bs.modal', function (data) {
    if (!$($(data.relatedTarget)).hasClass('revise')) {
      $(".game-table tr td .id").val("");
      $(".game-table tr td .key").val("");
      $(".game-table tr td .game-name").val("");
      $(".game-table tr td .resource").val("");
      $(".game-table tr td .plate-work").val("");
      $(".game-table tr td .plate-licene").val("");
      $(".game-table tr td .plate-file").val("");
      $(".game-table tr td .error").text("");
      $(".child-game .child-box").children().remove();
      $(".game-table tr td .id").attr("disabled", false);
    }

    var isAddGame = $(data.relatedTarget).hasClass('add-game');
    if (isAddGame) {
      $('#myModal>div>div>div>h4').html('新增游戏');
      $('.game-btn').attr('btn-type', 'add');
      initChildGames([]);
    } else {
      $('#myModal>div>div>div>h4').html('修改游戏');
      $('.game-btn').attr('btn-type', 'update');
      $(".game-table tbody tr td .id").attr("disabled", true);
    }

  });

  //子资源下拉框
  $('.child_games').on('click', 'span>.label-close', function () {
    $(this).parent().remove();
    var option_data = $(this).parent().attr('option-data');
    $.each($('.child-sel>ul>li'), function (i, item) {
      var id = $(item).attr('option-data');
      if (option_data == id) {
        $(item).css('display', 'block');
        return;
      }
    });
  });


  $('.child-sel>ul').on('click', 'li', function () {
    var lable_title = $(this).text();
    var id = $(this).attr('option-data');
    var lable = "<span class=\"label label-primary child_game\" option-data=\"" + id + "\" >" + lable_title + "<span class=\"label-close\">&times;</span></span>";
    $(".child-box").append(lable);
    $(this).hide();
  });


  //验证是否新增游戏
  $(".game-btn").on('click', function () {
    var id = $(".game-table tr td .id").val();
    if (!checkValue()) {
      return;
    }
    if ($(this).attr('btn-type') == 'add') {
      for (var i = 0; i < gameInfoList.length; i++) {
        if (id == gameInfoList[i].id) {
          window.wxc.xcConfirm("id已存在!", "warning");
          return false;
        }
      }
      window.wxc.xcConfirm("是否新增游戏？", "custom", {
        title: "提示信息",
        icon: "0 0",//蓝色i
        btn: window.wxc.xcConfirm.btnEnum.okcancel,
        onOk: function () {
          addGame();
        }
      });

    } else if ($(this).attr('btn-type') == 'update') {
      window.wxc.xcConfirm("是否修改游戏？", "custom", {
        title: "提示信息",
        icon: "0 0",//蓝色i
        btn: window.wxc.xcConfirm.btnEnum.okcancel,
        onOk: function () {
          return updateGameInfo();
        }
      });
    }
  });
  //新增游戏
  function addGame() {
    var id = $(".game-table tr td .id").val();
    var key = $(".game-table tr td .key").val();
    var game_name = $(".game-table tr td .game-name").val();
    var resource = $(".game-table tr td .resource").val();
    var plate_work = $(".game-table tr td .plate-work").val();
    var plate_licene = $(".game-table tr td .plate-licene").val();
    var plate_file = $(".game-table tr td .plate-file").val();

    var child_games = new Array();

    $('.child_games>.child_game').each(function (number, element) {
      child_games.push($(this).attr('option-data'));
    });
    var parm = {
      "funtion": "insert_game_info",
      "params": {
        "id": parseInt(id),
        "key_word": key,
        "child": child_games,
        "name": game_name,
        "res": resource,
        "vs_file": plate_file,
        "vs_licene": plate_licene,
        "vs_work": plate_work
      }
    };
    parm = $.extend({}, defaultParams, parm);
    $.get(baseApi, {
      send: JSON.stringify(parm)
    }, function (res) {
      if ((res.code * 1) == 0) {
        window.wxc.xcConfirm("新增游戏成功！", "success", {
          onOk: function () {
            $('#myModal .close').click();
            funs['init']();
          }
        });
      } else {
        window.wxc.xcConfirm(res.msg, "error");
      }
    }, 'json');

  }

  //删除游戏

  $("#tab-body tbody").find("tr>td .del-row").live("click", function () {
    var row = $(this).parent().parent().parent().parent().index();
    var data = $(this).parent().parent().find(".data-opt:input").val();
    window.wxc.xcConfirm("是否删除游戏？", "custom", {
      title: "提示信息",
      icon: "0 0",//蓝色i
      btn: window.wxc.xcConfirm.btnEnum.okcancel,
      onOk: function () {
        $("#tab-body tbody tr").eq(row).remove();
        //将json字符串转换成json对象
        data = eval('(' + data + ')');
        var id = data.id;
        var parm = {
          "funtion": "delete_game_info",
          "params": {
            "id": parseInt(id)
          }
        };
        parm = $.extend({}, defaultParams, parm);
        $.get(baseApi, {
          send: JSON.stringify(parm)
        }, function (rel) {
          if ((rel.code * 1) == 0) {
            //提示删除游戏
            window.wxc.xcConfirm("删除成功！", "success");
          } else {
            window.wxc.xcConfirm(rel.msg, "error");
          }
        }, 'json')
      }
    });
  });

  //修改游戏
  $("#tab-body tbody").find("tr>td .revise").live("click", function () {
    var game_id = $(this).parent().parent().parent().parent().attr('data-id');
    var gameInfo = {};
    $.each(gameInfoList, function (id, item) {
      gameInfo[item.id] = item;
    });
    var data = gameInfo[game_id];
    $(".game-table tbody tr td .id").val(data.id);
    $(".game-table tr td .key").val(data.keyword);
    $(".game-table tr td .game-name").val(data.name);
    $(".game-table tr td .resource").val(data.res);
    $(".game-table tr td .plate-work").val(data.vs_work);
    $(".game-table tr td .plate-licene").val(data.vs_lecene);
    $(".game-table tr td .plate-file").val(data.vs_file);

    initChildBoxInfo(data.child, data.id);
    var children = data.child;
    children.push(data.id);

    initChildGames(children);
  });


  //初始化所属子游戏信息
  function initChildBoxInfo(children, hide_id) {
    //初始化所属子游戏信息
    try {
      $(".child-box").html("");
      $.each(children, function (j, child) {
        var gameName = "";
        $.each(gameInfoList, function (i, item) {
          if (item['id'] == child) {
            gameName = item['name'];
          }
        });
        var game_name = gameName;
        if (hide_id != child) {
          var lable = "<span class=\"label label-primary child_game\" option-data=\"" + child + "\" >" + game_name + "<span class=\"label-close\">&times;</span></span>";
          $(".child-box").append(lable);
        }
      });
    } catch (e) {
      console.error(e);
    }
  }

  //Load游戏子资源
  function initChildGames(hide_children) {
    try {
      var html = "";
      for (var i = 0; i < gameInfoList.length; i++) {
        var isShow = true;
        $.each(hide_children, function (j, hide_child) {
          if (hide_child == gameInfoList[i].id) {
            isShow = false;
          }
        });
        if (isShow) {
          html += "<li option-data=\"" + gameInfoList[i].id + "\">" + gameInfoList[i].name + "</li>";
        } else {
          html += "<li option-data=\"" + gameInfoList[i].id + "\" style=\"display: none;\">" + gameInfoList[i].name + "</li>";
        }
      }
      $("#myModal .child-sel ul").html(html);

    } catch (e) {
      console.error(e);
    }
  }

  /**
   * 更新游戏信息
   */
  function updateGameInfo() {
    var amend_id = $(".game-table tr td .id").val();
    var amend_key = $(".game-table tr td .key").val();
    var amend_name = $(".game-table tr td .game-name").val();
    var amend_resource = $(".game-table tr td .resource").val();
    var amend_plate_work = $(".game-table tr td .plate-work").val();
    var amend_plate_licene = $(".game-table tr td .plate-licene").val();
    var amend_plate_file = $(".game-table tr td .plate-file").val();

    //获得child-box中的tag
    var child_game_tags = $(".child-box.child_games > span.child_game");
    var child = [];
    //将tag中的id属性逐个遍历取出来放到child变量中
    $.each(child_game_tags, function (i, data) {
      child[i] = parseInt($(data).attr('option-data'));
    });
    var parm = {
      "funtion": "update_game_info",
      "params": {
        "id": parseInt(amend_id),
        "key_word": amend_key,
        "child": child,
        "name": amend_name,
        "res": amend_resource,
        "vs_file": amend_plate_file,
        "vs_licene": amend_plate_licene,
        "vs_work": amend_plate_work
      }
    };
    parm = $.extend({}, defaultParams, parm);
    $.get(baseApi, {
      send: JSON.stringify(parm)
    }, function (rel) {
      //将code强制转换成数值直接与数值0对比
      if ((rel.code * 1) == 0) {
        window.wxc.xcConfirm("修改游戏成功！", "success", {
          onOk: function () {
            $('#myModal .close').click();
            funs['init']();
          }
        });

      } else {
        window.wxc.xcConfirm(rel.message, "error");
      }
    }, 'json')
  }

  //增加渠道初始化
  $('#myModal2').on('show.bs.modal', function () {
    $(".add-channel tr td .channel-id").val("");
    $(".add-channel tr td .channel-key").val("");
    $(".add-channel tr td .channel-name").val("");
    /*    $(".add-channel tr td .channel-sid").val("");*/
    $("#tab tbody tr").remove();
    $('#tab tbody tr').each(function (index) {
      if (index != 0) {
        $(this).remove();
      }
    });
  });

  //验证增加渠道
  $(".channel-btn").click(function () {
    var channel_id = $(".add-channel tr td .channel-id").val();
    var channel_sid = $(".add-channel tbody >tr >td>.channel-sid").val();
    if (!checkNullValue($('.add-channel'))) {
      return;
    }
    for (var i = 0; i < channelInfoList.length; i++) {
      if (channel_id == channelInfoList[i].id) {
        if(channel_sid==channelInfoList[i].channel_sid){
          window.wxc.xcConfirm("子渠道id已存在!", "warning");
          return false;
        }
      }
    }
    var channels = $('#tab tr td .channel-package');
    if (channels.length == 2) {
      if ($(channels[0]).val() == $(channels[1]).val()) {
        window.wxc.xcConfirm("两个渠道不能一样!", 'warning');
        return false;
      }
    }
    window.wxc.xcConfirm("是否新增渠道？", "custom", {
      title: "提示信息",
      icon: "0 0",//蓝色i
      btn: window.wxc.xcConfirm.btnEnum.okcancel,
      onOk: function () {
        addChannel();
      }
    });
  });
  //增加渠道
  function addChannel() {
    var channel_id = $(".add-channel tr td .channel-id").val(),
      channel_key = $(".add-channel tr td .channel-key").val(),
      channel_name = $(".add-channel tr td .channel-name").val(),
      channel_package1 = $($('#tab tr td .channel-package')[0]).val(),
      channel_value1 = $($('#tab tr td .select')[0]).val(),
      channel_value12 = $($('#tab tr td .select')[1]).val();
    var channel_sid = $(".add-channel tbody >tr >td>.channel-sid").val();

    var channe_value, complete_value;
    if (channel_package1 == "c_apk") {
      channe_value = channel_value1;
      complete_value = channel_value12;
    } else {
      complete_value = channel_value1;
      channe_value = channel_value12;
    }
    var val = $("[value=c_apk]");
    if (val) {

    }
    $($('#tab tr td .channel-package')[0]).val();

    var parm = {
      "funtion": "insert_channel_info",
      "params": {
        "id": parseInt(channel_id),
        "key_word": channel_key,
        "channel_sid":parseInt(channel_sid),
        "name": channel_name,
        "c_apk": parseInt(channe_value),
        "f_apk": parseInt(complete_value)
      }
    };
    parm = $.extend({}, defaultParams, parm);
    $.get(baseApi, {
      send: JSON.stringify(parm)
    }, function (res) {
      if ((res.code * 1) == 0) {
        window.wxc.xcConfirm('新增渠道成功！', "success", {
          onOk: function () {
            $('#myModal2 .close').click();
            funs['init']();
          }
        });
      } else {
        window.wxc.xcConfirm(res.msg, "error");
      }
    }, 'json')
  }

  //新增渠道  增加母包FF
  $("#myModal2").on("click", ".ditch-table .add-pack", function () {
    var parm = {
      "funtion": "select_parentpkg_info",
      "params": {
        "id": 0
      }
    };
    parm = $.extend({}, defaultParams, parm);
    $.get(baseApi,{send: JSON.stringify(parm)},function (res) {
      if ((res.code * 1) != 0) {
        window.wxc.xcConfirm(res.msg, "error");
        return;
      }

      var options = "";
      for (i = 0; i < res.data.length; i++) {
        options += "<option value=" + res.data[i].id + ">" + res.data[i].name + "</option>";
      }
      var trHtml = "<tr><td><select class=\"channel-package\"><option value=\"c_apk\">渠道包</option><option value=\"f_apk\">完整包</option></select></td><td><select class='select'>" + options + "</select></td><td><span class='delete'>删除</span></td></tr>";
      var rows = $("#tab tbody tr").length;
      if (rows > 1) {
        window.wxc.xcConfirm('添加的数量不能大与二！', "warning");
        return;
      } else {
        $("#tab tbody").append(trHtml);
        //默认一个完整包，一个渠道包

        if ($('#myModal2 .channel-package').length <= 1) {
          return;
        }

        var channel = $('#myModal2 .channel-package')[0].selectedIndex;
        if (0 == channel) {
          $('#myModal2 .channel-package')[1].selectedIndex = 1;
        } else {
          $('#myModal2 .channel-package')[1].selectedIndex = 0;
        }
      }
    }, 'json')
  });

  updatechannelInfo();

  //修改渠道
  function updatechannelInfo() {
    $(".tab-head thead").on('click', 'th .menu-select ul .amend-channel', function () {
      var data = $(this).parent().parent().parent().find('.data-op').val();
      //将json字符串转换成json对象
      data = eval('(' + data + ')');
      $(".amChannel tbody tr td .amChannel-id").val(data.channel_id);
      $(".amChannel tbody tr td .amChannel-key").val(data.keyword);
      $(".amChannel tbody tr td .amChannel-name").val(data.name);
      window.channelSid = $(".amChannel tbody >tr >td>.amChannel-sid").val(data.channel_sid);

      $('#tab1>tbody>tr').remove();
      $(".amChannel tbody tr td .amChannel-id").attr("disabled", true);
      window.channelId=$(this).parent().parent().parent().attr("data-id");

      var parm = {
        "funtion": "select_parentpkg_info",
        "params": {
          "id": 0
        }
      };
      parm = $.extend({}, defaultParams, parm);
      $.get(baseApi,{send: JSON.stringify(parm)},function (res) {
        if ((res.code * 1) != 0) {
          window.wxc.xcConfirm(res.msg, "error");
          return;
        }
        var options = "";
        for (i = 0; i < res.data.length; i++) {
          options += "<option value=" + res.data[i].id + ">" + res.data[i].name + "</option>";
        }
        var trHtml = "<tr><td><select class=\"channel-package\"><option value=\"c_apk\">渠道包</option><option value=\"f_apk\">完整包</option></select></td><td><select class='select'>" + options + "</select></td><td><span class='delete'>删除</span></td></tr>";

        var count = 0;
        if (data.f_apk != 0) {
          addTr("tab1>tbody", "-1", trHtml);
          count++;
          $($("#tab1 .channel-package")[(count - 1)]).val("f_apk");
          $($("#tab1 .select")[(count - 1)]).val(data.f_apk);
        }
        if (data.c_apk != 0) {
          addTr("tab1>tbody", "-1", trHtml);
          count++;
          $($("#tab1 .channel-package")[(count - 1)]).val("c_apk");
          $($("#tab1 .select")[(count - 1)]).val(data.c_apk);
        }
        var parm = {
          "funtion": "select_channel_info",
          "params": {
            "id": 0
          }
        };
        parm = $.extend({}, defaultParams, parm);
        $.get(baseApi,{send: JSON.stringify(parm)},function (res) {
        });
      }, 'json');
    });
  }

  //修改渠道  增加母包
  $("#myModal6 .add-pack1").click(function () {
    var parm = {
      "funtion": "select_parentpkg_info",
      "params": {
        "id": 0
      }
    };
    parm = $.extend({}, defaultParams, parm);
    $.get(baseApi,{send: JSON.stringify(parm)},function (res) {
      if ((res.code * 1) != 0) {
        window.wxc.xcConfirm(res.msg, "error");
        return;
      }
      var options = "";
      for (i = 0; i < res.data.length; i++) {
        options += "<option value=" + res.data[i].id + ">" + res.data[i].name + "</option>";
      }
      var trHtml = "<tr><td><select class=\"channel-package\"><option value=\"c_apk\">渠道包</option><option value=\"f_apk\">完整包</option></select></td><td><select class='select'>" + options + "</select></td><td><span class='delete'>删除</span></td></tr>";
      var rows = $("#tab1 tbody tr").length;
      if (rows > 1) {
        window.wxc.xcConfirm('添加的数量不能大与二！', "warning");
        return false;
      } else {
        addTr("tab1 tbody", "-1", trHtml);
        //默认一个完整包，一个渠道包
        if ($('#myModal6 .channel-package').length <= 1) {
          return;
        }

        var channel = $('#myModal6 .channel-package')[0].selectedIndex;
        if (0 == channel) {
          $('#myModal6 .channel-package')[1].selectedIndex = 1;
        } else {
          $('#myModal6 .channel-package')[1].selectedIndex = 0;
        }
      }
    }, 'json')
  });

  //验证是否修改渠道
  $(".amdChannel-btn").on("click", function () {
    var channels = $('#tab1 tr td .channel-package');
    var amChannel_id = $(".amChannel tbody tr td .amChannel-id").val();
    var amChannel_sid = $(".amChannel tbody >tr>td>.amChannel-sid").val();
    for (var i = 0; i < channelInfoList.length; i++) {
      if (amChannel_id == channelInfoList[i].id) {
        if(amChannel_sid !=channelSid){
        }
        else if(amChannel_sid==channelInfoList[i].channel_sid ){
          window.wxc.xcConfirm("子渠道id已存在!", "warning");
          return false;
        }
      }
    }
    if (channels.length == 2) {
      if ($(channels[0]).val() == $(channels[1]).val()) {
        window.wxc.xcConfirm("两个渠道不能一样", 'warning');
        return false;
      }
    }
    window.wxc.xcConfirm("是否修改渠道？", "custom", {
      title: "提示信息",
      icon: "0 0",//蓝色i
      btn: window.wxc.xcConfirm.btnEnum.okcancel,
      onOk: function () {
        updateChannel();
      }
    });
  });
  //更新渠道
  function updateChannel() {
    var amChannel_id = $(".amChannel tbody tr td .amChannel-id").val();
    var amChannel_key = $(".amChannel tbody tr td .amChannel-key").val();
    var amChannel_name = $(".amChannel tbody tr td .amChannel-name").val();
    var amChannel_sid = $(".amChannel tbody >tr>td>.amChannel-sid").val();
    $($('#tab1 tr td .channel-package')[0]).val();

    var channel_value = 0;
    var complete_value = 0;

    //渠道包赋值
    var channels = $('#tab1 tr td .channel-package');
    if (channels.length > 0) {
      if ($(channels[0]).val() == "c_apk") {
        channel_value = $($('#tab1 tr td .select')[0]).val();
      } else {
        complete_value = $($('#tab1 tr td .select')[0]).val();
      }

      if (channels[1]) {
        if ($(channels[1]).val() == "c_apk") {
          channel_value = $($('#tab1 tr td .select')[1]).val();
        } else {
          complete_value = $($('#tab1 tr td .select')[1]).val();
        }
      }
    }

    var parm = {
      "funtion": "update_channel_info",
      "params": {
        "id": parseInt(channelId),
        "channel_id": parseInt(amChannel_id),
        "key_word": amChannel_key,
        "name": amChannel_name,
        "channel_sid":parseInt(amChannel_sid),
        "c_apk": parseInt(channel_value),
        "f_apk": parseInt(complete_value)
      }
    };
    parm = $.extend({}, defaultParams, parm);
    $.get(baseApi, {
      send: JSON.stringify(parm)
    }, function (res) {
      if ((res.code * 1) == 0) {
        window.wxc.xcConfirm('修改渠道成功！', "success", {
          onOk: function () {
            $('#myModal6 .close').click();
            funs['init']();
          }
        });
      } else {
        window.wxc.xcConfirm(res.msg, "error");
      }
    }, 'json')
  }

  //删除渠道
  $(".tab-head thead").on('click', 'th .menu-select ul .del-col', function () {
    var index = $(this).parent().parent().parent().index();
    var id = $(this).parent().parent().parent().attr("data-id");
    window.wxc.xcConfirm("是否删除渠道？", "custom", {
      title: "提示信息",
      icon: "0 0",//蓝色i
      btn: window.wxc.xcConfirm.btnEnum.okcancel,
      onOk: function () {
        delcol(index);
        var parm = {
          "funtion": "delete_channel_info",
          "params": {
            "id": parseInt(id)
          }
        };
        parm = $.extend({}, defaultParams, parm);
        $.get(baseApi, {
          send: JSON.stringify(parm)
        }, function (res) {
          if ((res.code * 1) == 0) {
            //提示删除渠道
            window.wxc.xcConfirm("删除成功！", "success");
          } else {
            window.wxc.xcConfirm(res.msg, "error");
          }
        }, 'json')
      }
    });

  });

  function delcol(col) {
    var l = $("table tr").length;
    for (var i = 0; i < l; i++) {
      $("#tab-body tr").eq(i).find("td").eq(col).remove();
      $(".tab-head tr").eq(i).find("th").eq(col).remove();
    }
  }

  $('.package-manager').click(function () {
    showPackageList(function () {
      $('#myModal3').modal('show');
    });
  });

  function showPackageList(fun) {
    //母包查询
    var parm = {
      "funtion": "select_parentpkg_info",
      "params": {
        "id": 0
      }
    };
    parm = $.extend({}, defaultParams, parm);
    $.get(baseApi,{send: JSON.stringify(parm)},function (res) {
      if ((res.code * 1) == 0) {
        var html = "";
        for (var i = 0; i < res.data.length; i++) {
          var is_upload = res.data[i].url ? true : false;
          var file_url = res.data[i].url ? res.data[i].url.replace("http://cn-bookse-yunpai.oss-cn-hangzhou.aliyuncs.com/package/", "") : "";
          html += "<tr><td><input type=\"text\" name=\"pck\" class='pck' value=\"" + res.data[i].name + "\" ><input class=\"data-hide\" type='hidden' value='" + JSON.stringify(res.data[i]) + "' /></td><td><input type=\"file\"  opt-value= '" + res.data[i].url + "' name=\"url\" class=\"xFile\" data-id='" + JSON.stringify(parseInt(res.data[i].id)) + "' \"><input type='text' name='text' class='upload-url' value='" + file_url + "' /><span class='bag-span'><span class=\"delete badge-del\">删除</span><span class=\"delete " + (is_upload ? 'badge-update' : 'save-btn') + "\">" + (is_upload ? "修改" : "保存") + "</span></span></td></tr>";
        }
        $("#pack-manage tbody").html("");
        $("#pack-manage tbody tr").remove();
        $("#pack-manage tbody").append(html);
        $(".hint").remove();
        updata();
      } else {
        alert(res.msg)
      }
      fun();
    }, 'json');
  }

  //初始化母包列表
  function InitPackageManageList() {
    //母包查询
    var parm = {
      "funtion": "select_parentpkg_info",
      "params": {
        "id": 0
      }
    };
    parm = $.extend({}, defaultParams, parm);
    $.get(baseApi,{send: JSON.stringify(parm)},function (res) {
      if ((res.code * 1) == 0) {
        var html = "";
        for (var i = 0; i < res.data.length; i++) {
          var is_upload = res.data[i].url ? true : false;
          var file_url = res.data[i].url ? res.data[i].url.replace("http://cn-bookse-yunpai.oss-cn-hangzhou.aliyuncs.com/package/", "") : "";
          html += "<tr><td><input type=\"text\" name=\"pck\" class='pck' value=\"" + res.data[i].name + "\" ><input class=\"data-hide\" type='hidden' value='" + JSON.stringify(res.data[i]) + "' /></td><td><input type=\"file\"  opt-value= '" + res.data[i].url + "' name=\"url\" class=\"xFile\" data-id='" + JSON.stringify(parseInt(res.data[i].id)) + "' \"><input type='text' name='text' class='upload-url' value='" + file_url + "' /><span class='bag-span'><span class=\"delete badge-del\">删除</span><span class=\"delete " + (is_upload ? "badge-update" : "save-btn") + "\">" + (is_upload ? "修改" : "保存") + "</span></span></td></tr>";
        }
        $("#pack-manage tbody").html("");
        $("#pack-manage tbody tr").remove();
        $("#pack-manage tbody").append(html);
        $(".hint").remove();
        updata();
      } else {
        alert(res.msg)
      }
    }, 'json');
  }

  $(document).on('change', '.xFile', function (e) {
    try {
      $(e).attr("disable", true);
      package_manage_change_status(e);
      var name = e.currentTarget.files[0].name;
      var fileType = name.substring(name.lastIndexOf("."), name.length).replace('.', '');
      if (fileType != 'apk') {
        window.wxc.xcConfirm("文件不是apk格式的!", "warning");
        $(e).attr("disable", false);
        return false;
      }
      $(e.currentTarget).parent().find('input.upload-url').val(name);
      $(e).attr("disable", false);
      return false;
    } catch (e) {
      console.log(e);
    }

  });

  function updata() {
    /*$('.xFile').on("change", function(e) {
     package_manage_change_status(e);
     var name = e.currentTarget.files[0].name;
     var fileType = name.substring(name.lastIndexOf("."), name.length).replace('.', '');
     if(fileType != 'apk') {
     window.wxc.xcConfirm("文件不是apk格式的!", "custom", {
     title: "提示信息",
     });
     return false;
     }
     $(e.currentTarget).parent().find('input.upload-url').val(name);
     return false;
     });*/

  }

  //增加母包
  $("#myModal3 .add-pck").click(function () {
    var name = $("#pack-manage >tbody >tr:last-child").find(".pck").val();
    var file_url = $("#pack-manage >tbody >tr:last-child").find(".upload-url").val();
    if (!name || !file_url) {
      $(this).parent().find('.hint').remove();
      $(this).parent().append("<span class='hint'>* 请完善并保存未完成的母包信息</span>");
      return;
    } else {
      $(this).parent().find('.hint').remove();
    }
    //母包管理 增加母包
    var trHtml = "<tr><td><input type=\"text\" name=\"pck\" class='pck'><input class=\"data-hide\" type='hidden' value='' /></td><td><input type=\"file\"  name=\"url\" class=\"xFile\" \"><input type='text' name='text' class='upload-url' /><span class='bag-span'><span class=\"delete badge-del\">删除</span><span class=\"delete save-btn\">保存</span></span></td></tr>";
    addTr("pack-manage", "-1", trHtml);
  });

  window.package_manage_change_status = function (event) {
    $(event.target).parent().parent().find('.bag-span>.badge-update,.save-btn').attr('change_status', 1);
  };


  $(document).on('change', 'input.pck', package_manage_change_status);

  //更新母包
  $("#pack-manage").find('.badge-update,.save-btn').live("click", function () {
    var change_status = $(this).attr('change_status');
    if (!change_status || change_status != 1) { //如果没有改变就不调用接口
      return;
    }
    var pkg_id, file_url;
    var data = $(this).parent().parent().siblings().find('.data-hide').val();
    if (data) {
      data = JSON.parse(data);
      pkg_id = data.id;
      file_url = data.url;
    }
    var name = $(this).parent().parent().parent().find('[name=pck]:input').val();
    //上传文件
    var files = $(this).parent().siblings('.xFile')[0].files;
    if (files.length > 0) {
      var file_name = files[0].name;
      var fileType = file_name.substring(file_name.lastIndexOf("."), file_name.length).replace('.', '');
      if (fileType != 'apk') {
        window.wxc.xcConfirm("文件不是apk格式的!", "warning");
        return false;
      }
      //上传文件
      file_url = "package/" + file_name;
      //调用上传文件方法
      uploadOssFile(files[0], file_url, {
        uploadComplete: function () {
          //上传成功
          file_url = "http://cn-bookse-yunpai.oss-cn-hangzhou.aliyuncs.com/" + file_url;
          saveParentPkg(name, file_url, pkg_id);
        }
      });

    } else {
      //调取接口保存数据
      saveParentPkg(name, file_url, pkg_id);
    }

  });


  function saveParentPkg(pkg_name, file_url, id) {
    var parm = {
      "module": "pluginspackage",
      "session": sessionID,
      "params": {
        "url": file_url,
        "name": pkg_name
      }
    };
    if (id) {
      parm["funtion"] = "update_parentpkg_info";
      parm["params"]["id"] = id;
    } else {
      parm["funtion"] = "insert_parentpkg_info";
    }
    $.get(baseApi, {
      send: JSON.stringify(parm)
    }, function (res) {
      if ((res.code * 1) == 0) {
        window.wxc.xcConfirm("更新上传成功！", "success");
        showPackageList(function () {
        });
      } else {
        window.wxc.xcConfirm(res.msg, "error");
      }
    }, 'json')
  }

  //删除母包
  $("#pack-manage").find('.badge-del').live("click", function () {
    var data = $(this).parent().parent().siblings().find('.data-hide').val();
    $(this).parent().parent().parent().remove();
    $('.hint').remove();
    try {
      data = eval("(" + data + ")");
    } catch (e) {
      return;
    }


    window.wxc.xcConfirm("是否删除母包？", "custom", {
      title: "提示信息",
      icon: "0 0",//蓝色i
      btn: window.wxc.xcConfirm.btnEnum.okcancel,
      onOk: function () {
        var parm = {
          "funtion": "delete_parentpkg_info",
          "params": {
            "id": parseInt(data.id)
          }
        };
        parm = $.extend({}, defaultParams, parm);
        $.get(baseApi, {
          send: JSON.stringify(parm)
        }, function (res) {
          if ((res.code * 1) == 0) {
            showPackageList(function () {
            });
            window.wxc.xcConfirm("删除成功！", "success");
          } else {
            window.wxc.xcConfirm(res.msg, "error");
          }
        }, "json");
        $(this).parent().parent().parent().remove();
      }
    });
  });

  //具体包结构
  $('#myModal4').on('show.bs.modal', function (e, o) {
    initPackage();
    $('.package-menu>ul>li').removeClass('on').find('.color').removeClass('color');
    $('.package-menu>ul>li').eq(0).addClass('on').find('span').removeClass('color-gray').addClass('color');
    selectPackageTag(0);
    $('.package-gameId').val("");
    $('.package-gameKey').val("");
    $('.package-gameName').val("");
    $('.package-channelId').val("");
    $('.package-channelKey').val("");
    $('.package-channelName').val("");
    $(".um-channel").val("");
    $(".we-id").val(""); //微信id
    $(".we-key").val(""); //微信关键字
    $(".we-mchid").val(""); //商户号
    $(".version").val(""); //版本号
    $(".version").attr("disabled", false);
    $(".version_f").attr("checked", false); //是否跟随母包
    $(".pkg-name").val(""); //包名
    $(".um-key").val(""); //友盟app_key
    $(".um-secret").val(""); //友盟secret
    //渠道包 完整包配置
    $(".channel-appid").val(""); //渠道app_id
    $(".channel-appkey").val(""); //渠道app_id
    $(".channel-payid").val(""); //渠道pay_id
    $(".channel-paykey").val(""); //渠道pay_id
    $(".file-name").val(""); //输出文件名
    $(".down-other-url").val(""); //其他上传文件
    $(".replace-file tbody").html("");//替换文本
    $(".can-pay").attr("checked", false); //是否支付
    $("#game-icon-file").val("");
    $("#game-package-file").val("");
    $('.error').remove();
    $("#upload-img").attr("src", "");
    $("#upload-pic").attr("src", "");
    $("#gameIconUpload").text('未上传');
    $("#gameIconUpload").attr({
      "enable": "enable"
    });
    $(".upload-btn").text('未上传');
    $(".upload-btn").attr({
      "enable": "enable"
    });
    $("#myModal4 .else-deploy > tbody").html("");
    $("#game-package-file").removeData('url');

    //获取单击元素
    var source_btn = e.relatedTarget;
    //通过源目标元素得到游戏信息
    var game_id = $(source_btn).parent().parent().parent().parent().attr('data-id');
    var info = {};
    $.each(window.gameInfoList, function (id, item) {
      info[item.id] = item
    });
    var game_info = info[game_id];
    window.game_info = game_info;
    //通过源目标元素得到列索引
    var col_index = $(source_btn).parent().parent().parent().parent().find('td').index($(source_btn).parent().parent().parent());

    //得到渠道信息
    channel_info = $($('.tab-head tr th')[col_index]).find('.data-op:input').val();
    channel_info = JSON.parse(channel_info);

    //包类型
    var package_type = $(source_btn).attr('data-type');

    //是否完整包
    var pkg_id = ('c_apk' == package_type) ? 0 : 1;
    channel_info['pkg_id'] = pkg_id;

    var package_id = channel_info.channel_sid*1000000 + game_info.id * 1000 + channel_info.channel_id * 10 + pkg_id;
    //得到的结构信息
    var structInfo;
    var parm = {
      "funtion": "select_package_info",
      "params": {
        "id": parseInt(package_id)
      }
    };
    parm = $.extend({}, defaultParams, parm);
    var parms = JSON.stringify(parm);
    var url ="http://yp.bookse.cn/webservice.php?send=" + parms;
    $.ajax({
      type: "get",
      url: url,
      async: false,
      success: function (res) {
        res = JSON.parse(res);
        if ((res.code * 1) == 0 && res.data) {
          //包结构数据
          structInfo = res.data[0];
        }
      }
    });
    if (structInfo) {
      $('#myModal4').attr('operation-type', 1);
    } else {
      $('#myModal4').attr('operation-type', 0);
    }
    window.gameIconFileKey = game_info.keyword + '/' +
      channel_info.channel_id + '/' +
      pkg_id + '/' +
      'Icon-' + 256 + '.png';

    window.downOtherUrl = game_info.keyword + '/' +
      channel_info.channel_id + '/' +
      pkg_id + '/' +
      'ext' + '.zip';
    $('.package-gameId').val(game_info.id);
    $('.package-gameKey').val(game_info.keyword);
    $('.package-gameName').val(game_info.name);
    $('.package-channelId').val(channel_info.channel_id);
    $('.package-channelKey').val(channel_info.keyword);
    $('.package-channelName').val(channel_info.name);
    $(".package-Channel-sid").val(channel_info.channel_sid);
    $(".um-channel").val(channel_info.keyword); //友盟channel;

    if (structInfo) {
      $(".we-id").val(structInfo.we_id); //微信id
      $(".we-key").val(structInfo.we_key); //微信关键字
      $(".we-mchid").val(structInfo.we_mchid); //商户号
      $(".pkg-name").val(structInfo.pkg_name); //包名
      $(".um-key").val(structInfo.um_key); //友盟app_key
      $(".um-secret").val(structInfo.um_secret); //友盟secret
      //渠道包 完整包配置
      $(".channel-appid").val(structInfo.channel_appid); //渠道app_id
      $(".channel-appkey").val(structInfo.channel_appkey); //渠道app_id
      $(".channel-payid").val(structInfo.channel_payid); //渠道pay_id
      $(".channel-paykey").val(structInfo.channel_paykey); //渠道pay_id
      $(".file-name").val(structInfo.file_name); //输出文件名
      $(".version").val(structInfo.version); //版本号

      $(".can-pay").attr("checked", structInfo.can_pay == 1); //是否支付
      $(".version_f").attr('checked', structInfo.version_f == 1); //是否跟随母包

      //判断图片是否存在
      var icon_key="";
      if(structInfo.down_icon_url){
        var icon_json=$.parseJSON(structInfo.down_icon_url);
        icon_key=icon_json.value;
      }
      var upload_img_url = "http://cn-bookse-yunpai.oss-cn-hangzhou.aliyuncs.com/" + icon_key;
      var ImgObj = new Image();

      ImgObj.onload = function () {
        $("#upload-img").attr("src", upload_img_url + "?v=" + new Date().getTime());
        $("#gameIconUpload").text('已上传');
        /*  $("#game-icon-file").data('url',structInfo.down_icon_url);*/
        $("#gameIconUpload").attr({
          "disabled": "disabled"
        });
      };

      ImgObj.onerror = function () {
        $("#upload-img").attr("src", "");
        $("#gameIconUpload").text('未上传');
        $("#gameIconUpload").attr({
          "enable": "enable"
        });
      };

      ImgObj.src = upload_img_url;

      //判断文件是否存在
      if (structInfo.down_other_url) {
        //判断文件是否存在
        var down_img_url = "img/zip7.png";

        $("#upload-pic").attr("src", down_img_url);
        $("#game-package-file").data('url',structInfo.down_other_url);
        $(".upload-btn").text('已上传');
        $(".upload-btn").attr({
          "disabled": "disabled"
        });

      } else {
        $("#upload-pic").attr("src", "");
        $(".upload-btn").text('未上传');
        $(".upload-btn").attr({
          "enable": "enable"
        });
      }

      if ($(".checked").is(":checked")) {
        $(".version").attr("disabled", true)
      } else {
        $(".version").attr("disabled", false);
      }
    }

    //替换文本初始化
    var rep_string = [{key: "", value: ""}, {key: "", value: ""}, {key: "", value: ""}];
    if (structInfo) {
      rep_string = structInfo.rep_string;
      if (rep_string) {
        if(JSON.parse(rep_string)){
          rep_string = JSON.parse(rep_string);
          $.each(rep_string, function (i, item) {
            var replaceBefore = item['key'];
            var replaceAfter = item['value'];
            var html = "<tr>";
            html += "<td>";
            html += '将&nbsp;<input  type="text" name="replace" class="replace-before" value="' + replaceBefore + '" />';
            html += '&nbsp;&nbsp;替换为&nbsp;&nbsp;<input  type="text" name="replace" class="replace-after" value="' + replaceAfter + '" />';
            html += '<span class="delete deleteReplace">&nbsp;&nbsp;&nbsp;删除</span>';
            html += "</td>";
            html += "</tr>";
            $(".tab-content .replace-file > tbody").append(html);
          });
        }
      }
    }
    // 其他配置 初始化
    var other_string = [{key: "", value: ""}, {key: "", value: ""}, {key: "", value: ""}];
    if (structInfo) {
      other_string = structInfo.other_string;
      if (other_string) {
        if (JSON.parse(other_string)) {
          other_string = JSON.parse(other_string);
          $.each(other_string, function (i, item) {
            var add_key = item['key'];
            var add_value = item['value'];
            var html = "<tr>";
            html += "<td>";
            html += '&nbsp;字段名：&nbsp;<input type="text" name="add-text" class="add-key" value="' + add_key + '"/>';
            html += "</td>";
            html += "<td>";
            html += '值：&nbsp;<input type=type="text" name="add-text" class="add-value" value="' + add_value + '" /><span class="else-delete  delete">删除</span>';
            html += "</td>";
            $("#myModal4 .else-deploy > tbody").append(html);
          })
        }
      }
    }

    //是否跟随母包
    $(".checked").click(function () {
      if ($(".checked").is(":checked")) {
        $(".version").attr("disabled", true);
        if ($(".version"))
          $(".version").removeAttr('data-requi');
        $(".version").parent().find('.error').remove();
      } else {
        $(".version").attr("disabled", false);
        $(".version").attr('data-requi', "true");
      }

    });
  });
  //其他配置 添加
  $("#addInfo").on("click",function () {
    html = "";
    html = "<tr><td>&nbsp;字段名：&nbsp;<input type=\"text\" name=\"add-text\" class='add-key'/></td><td>值：&nbsp;<input type=\"text\"  name=\"add-text\" class='add-value'/><span class='else-delete  delete'>删除</span></td></tr>";
    $("#myModal4 .else-deploy tbody").append(html);
  });
  $(document).on('click', '.else-delete ', function () {
    $(this).parent().parent().remove();
  });
  //增加/删除替换
  $("#myModal4 .add-replace").click(function () {
    var trHtml = "<tr><td>将&nbsp;<input  type=\"text\" name=\"replace\" class=\"replace-before\" />&nbsp;&nbsp;替换为&nbsp;&nbsp;<input  type=\"text\" name=\"replace\" class=\"replace-after\" /><span class=\"delete deleteReplace\">&nbsp;&nbsp;&nbsp;删除</span></td></tr>";
    addTr("replaceFile tbody", "-1", trHtml);
  });
  $(document).on('click', '.deleteReplace', function () {
    $(this).parent().parent().remove();
  });

  //保存包信息
  $('#btn_submit').on('click', function (event) {
    var version_p_isSelected = $('#version_p').attr('checked') == 'checked' ? 1 : 0; //是否跟随母包
    if (version_p_isSelected != 1) {
      if (!checkNum('.pack-table .version')) {
        return;
      }
    }
    if (!checkNull()) {
      return;
    }
    var src = $("#upload-img").attr("src");
    if ($("#game-icon-file").attr("data-upload") == 'true' || !src) {
      window.wxc.xcConfirm("请上传图片!", "warning");
      return false;
    }
    //如果包含此游戏的包结构，者为更新状态，isCreate设置为false
    if ($('#myModal4').attr('operation-type') == 1) {
      updatePackageStructInfo();
    } else {
      insertPackageStructInfo();
    }
  });

  /**
   * 新增包结构信息
   */
  function insertPackageStructInfo() {
    window.wxc.xcConfirm("是否增加包结构？", "custom", {
      title: "提示信息",
      icon: "0 0",//蓝色i
      btn: window.wxc.xcConfirm.btnEnum.okcancel,
      onOk: function () {
        var game_info = window.game_info;
        var we_id = $(".we-id").val(), //微信id
          we_key = $(".we-key").val(), //微信关键字
          we_mchid = $(".we-mchid").val(), //商户号
          pkg_name = $(".pkg-name").val(), //包名
          um_key = $(".um-key").val(), //友盟app_key
          um_secret = $(".um-secret").val(), //友盟secret
          //渠道包 完整包配置
          channel_appid = $(".channel-appid").val(), //渠道app_id
          channel_appkey = $(".channel-appkey").val(), //渠道app_key
          channel_payid = $(".channel-payid").val(), //渠道的支付id
          channel_paykey = $(".channel-paykey").val(), //渠道的支付key
          file_name = $(".file-name").val(), //输出文件名
          version = $(".version").val(); //版本号
        $(".um-channel").val(channel_info.keyword); //友盟channel

        var version_p_isSelected = $('#version_p').attr('checked') == 'checked' ? 1 : 0; //是否跟随母包
        var isClosePay = $('#isClosePay').attr('checked') == 'checked' ? 1 : 0; //是否支付
        var version = version_p_isSelected == 1 ? 0 : version; //禁用时为0

        //替换
        var replace_beforeList = $('.replace-before');
        var replace_afterList = $('.replace-after');
        var rep_obj = new Array();
        $.each(replace_beforeList, function (i, before) {
          var obj = {
            key: $(before).val(),
            value: $(replace_afterList[i]).val()
          };
          rep_obj.push(obj);
        });
        //其他配置添加
        var key_list = $(".add-key");
        var value_list = $(".add-value");
        var other = new Array();
        $.each(key_list, function (i, key) {
          var obj = {
            key: $(key).val(),
            value: $(value_list[i]).val()
          };
          other.push(obj);
        });
        /*var temp_down_icon_url = $('#game-icon-file').data('url');*/
        var temp_downOtherUrl = $('#game-package-file').data('url');
        var parm = {
          "funtion": "insert_package_info",
          "params": {
            "can_pay": parseInt(isClosePay),
            "channel_appid": channel_appid,
            "channel_appkey": channel_appkey,
            "channel_id": parseInt(channel_info.id),
            "channel_payid": channel_payid,
            "channel_paykey": channel_paykey,
            "down_icon_url": {
              "key": 256,
              "value":gameIconFileKey
            },
            "down_other_url": temp_downOtherUrl ? temp_downOtherUrl : '',
            "other_string": other,
            "file_name": file_name,
            "game_id": parseInt(game_info.id),
            "pkg_id": channel_info.pkg_id,
            "pkg_name": pkg_name,
            "rep_string": rep_obj,
            "um_key": um_key,
            "um_secret": um_secret,
            "version": parseInt(version),
            "version_f": parseInt(version_p_isSelected),
            "we_id": we_id,
            "we_key": we_key,
            "we_mchid": we_mchid
          }
        };
        parm = $.extend({}, defaultParams, parm);
        $.post(baseApi, {
          send: JSON.stringify(parm)
        }, function (res) {
          if ((res.code * 1) == 0) {
            window.wxc.xcConfirm("新增包结构成功！", "success", {
              onOk: function () {
                $("#myModal4 .close").click();
              }
            });
          } else {
            window.wxc.xcConfirm(res.msg, "error");
          }
        }, 'json');
      }
    });
  }

  /**
   * 修改包结构信息
   */
  function updatePackageStructInfo() {
    window.wxc.xcConfirm("是否修改包结构？", "custom", {
      title: "提示信息",
      icon: "0 0",//蓝色i
      btn: window.wxc.xcConfirm.btnEnum.okcancel,
      onOk: function () {
        var game_info = window.game_info;
        var we_id = $(".we-id").val(), //微信id
          we_key = $(".we-key").val(), //微信关键字
          we_mchid = $(".we-mchid").val(), //商户号
          version = $(".version").val(), //版本号
          version_f = $(".version_f").val(), //是否跟随母包
          pkg_name = $(".pkg-name").val(), //包名
          um_key = $(".um-key").val(), //友盟app_key
          um_secret = $(".um-secret").val(), //友盟secret
          //渠道包 完整包配置
          channel_appid = $(".channel-appid").val(), //渠道app_id
          channel_appkey = $(".channel-appkey").val(), //渠道app_key
          channel_payid = $(".channel-payid").val(), //渠道pay_id
          channel_paykey = $(".channel-paykey").val(), //渠道pay_id
          file_name = $(".file-name").val(); //输出文件名
        var version_p_isSelected = $('#version_p').attr('checked') == 'checked' ? 1 : 0; //是否跟随母包
        var isClosePay = $('#isClosePay').attr('checked') == 'checked' ? 1 : 0; //是否支付
        var version = version_p_isSelected == 1 ? 0 : version; //禁用时为0

        var package_id = channel_info.channel_sid*1000000 + game_info.id * 1000 + channel_info.channel_id * 10 + channel_info.pkg_id;
        //替换
        var replace_beforeList = $('.replace-before');
        var replace_afterList = $('.replace-after');
        var rep_obj = new Array();
        $.each(replace_beforeList, function (i, before) {
          var obj = {
            key: $(before).val(),
            value: $(replace_afterList[i]).val()
          };
          rep_obj.push(obj);
        });
        //其他配置添加
        var key_list = $(".add-key");
        var value_list = $(".add-value");
        var other = new Array();
        $.each(key_list, function (i, key) {
          var obj = {
            key: $(key).val(),
            value: $(value_list[i]).val()
          };
          other.push(obj);
        });

        var temp_downOtherUrl = $('#game-package-file').data('url');
        var parm = {
          "funtion": "update_package_info",
          "params": {
            "can_pay": parseInt(isClosePay),
            "channel_appid": channel_appid,
            "channel_appkey": channel_appkey,
            "channel_id": parseInt(channel_info.id),
            "channel_payid": channel_payid,
            "channel_paykey": channel_paykey,
            "down_icon_url": {
              "key": 256,
              "value": gameIconFileKey
            },
            "down_other_url": temp_downOtherUrl ? temp_downOtherUrl : '',
            "other_string": other,
            "file_name": file_name,
            "game_id": parseInt(game_info.id),
            "id": package_id,
            "pkg_id": channel_info.pkg_id,
            "pkg_name": pkg_name,
            "rep_string": rep_obj,
            "um_key": um_key,
            "um_secret": um_secret,
            "version": parseInt(version),
            "version_f": parseInt(version_p_isSelected),
            "we_id": we_id,
            "we_key": we_key,
            "we_mchid": we_mchid
          }
        };
        console.log(parm)
        parm = $.extend({}, defaultParams, parm);
        $.post(baseApi, {
          send: JSON.stringify(parm)
        }, function (res) {
          if ((res.code * 1) == 0) {
            window.wxc.xcConfirm("成功修改包结构！", "success", {
              onOk: function () {
                $("#myModal4 .close").click();
              }
            });
          } else {
            window.wxc.xcConfirm(res.msg, "error")
          }
        }, 'json');
      }
    });
  }

  //取消
  $(document).on('click', '#btn_cancel', function () {

  });


  var get_pakcing_info;

  //开始打包
  $(".start-pack ").click(function () {
    //获得选中的信息(通过data-type筛选出选中的渠道包的对象)
    var checked = $("#tab-body tr td input[name=checkbox]:checked[data-type]");
    if ($(checked).attr("checked") != "checked") {
      window.wxc.xcConfirm('请选择打包项', 'warning');
      return;
    }

    var channel_data = [];
    var gameInfo_data = [];
    var is_C_F = []; //是否完整包列表

    $.each(checked, function (i, data) {
      //这个就是每一个复选框的渠道信息了
      channel_data[i] = JSON.parse($(data).siblings().find('.data-am').val());
      gameInfo_data[i] = JSON.parse($(data).parent().parent().parent().siblings().find(".data-opt").val());
      is_C_F[i] = $(data).attr('data-type');
    });

    var params = [];
    $.each(gameInfo_data, function (i, data) {
      params[i] = channel_data[i].channel_sid *1000000 + gameInfo_data[i].id * 1000 + channel_data[i].channel_id * 10 + (('c_apk' == is_C_F[i]) ? 0 : 1);
    });
    //开始打包
    var parm = {
      "funtion": "start_packing",
      "params": params
    };
    parm = $.extend({}, defaultParams, parm);
    $.get(baseApi, {
      send: JSON.stringify(parm)
    }, function (res) {
      if ((res.code * 1) !== 0) {
        window.wxc.xcConfirm(res.msg, "error");
        return
      }
      $(".noneInfoMessage").hide();
      queryPackageList();
    }, 'json');
  });

  //暂停按钮事件
  $("#list-content").on('click', " .pack-pause", function () {
    var selected = $(this).parent().parent();
    var status = $(this).parent().parent().find(".pack-status").text();
    if (status != "未开始") {
      $(this).attr("disabled", "disabled");
      window.wxc.xcConfirm('无法暂停！', 'warning');
      return;
    } else {
      $(this).removeAttr("disabled");
    }
    window.wxc.xcConfirm("是否暂停打包？", "custom", {
      title: "提示信息",
      icon: "0 0",//蓝色i
      btn: window.wxc.xcConfirm.btnEnum.okcancel,
      onOk: function () {
        var taskList = new Array();
        $.each(selected, function (i, data) {
          taskList.push(parseInt($(data).find(".package_item").attr('data-opt')));
        });
        var parm = {
          "funtion": "pause_pack_task",
          "params": taskList //任务列表的id 从查询打包中获取
        };
        parm = $.extend({}, defaultParams, parm);
        $.get(baseApi, {
          send: JSON.stringify(parm)
        }, function (res) {
          if ((res.code * 1) != 0) {
            window.wxc.xcConfirm(res.msg, "error");
            return;
          }
          queryPackageList();
        }, 'json');
      }
    });
  });

  $('.button-group').on('click', ".btn-pause-pack-right", function (e) {
    var checked = $('#list-content>tbody>tr>td').find('[type=checkbox]:checked');
    if ($(checked).attr("checked") == "checked") {
      var status = $(checked).parent().parent().find(".pack-status").text();
      if (status == "未开始") {
        $(".btn-pause-pack-right").removeAttr("disabled");
        window.wxc.xcConfirm("是否暂停打包？", "custom", {
          title: "提示信息",
          icon: "0 0",//蓝色i
          btn: window.wxc.xcConfirm.btnEnum.okcancel,
          onOk: function () {
            var taskList = new Array();
            $.each(checked, function (i, data) {
              taskList.push(parseInt($(data).attr('data-opt')));
            });
            var parm = {
              "funtion": "pause_pack_task",
              "params": taskList //任务列表的id 从查询打包中获取
            };
            parm = $.extend({}, defaultParams, parm);
            $.get(baseApi, {
              send: JSON.stringify(parm)
            }, function (res) {
              if ((res.code * 1) == 0) {
                queryPackageList();
              } else {
                window.wxc.xcConfirm(res.msg, "error");
              }
            }, 'json');
          }
        });
      } else {
        $(".btn-pause-pack-right").attr("disabled", "disabled");
        window.wxc.xcConfirm('无法暂停！', 'warning');
      }
    } else {
      window.wxc.xcConfirm('请选择要暂停项', 'warning');
    }
  });

  //清除按钮单击事件

  $("#list-content").on('click', " .pack-clear", function () {
    var selected = $(this).parent().parent();
    window.wxc.xcConfirm("是否删除包？", "custom", {
      title: "提示信息",
      icon: "0 0",//蓝色i
      btn: window.wxc.xcConfirm.btnEnum.okcancel,
      onOk: function () {
        var taskList = new Array();
        $.each(selected, function (i, data) {
          taskList.push(parseInt($(data).find(".package_item").attr('data-opt')));
        });
        var parm = {
          "funtion": "clear_pack_task",
          "params": taskList //任务列表的id 从查询打包中获取
        };
        parm = $.extend({}, defaultParams, parm);
        $.get(baseApi, {
          send: JSON.stringify(parm)
        }, function (res) {
          if ((res.code * 1) != 0) {
            window.wxc.xcConfirm(res.msg, "error");
            return;
          }
          queryPackageList();
          window.wxc.xcConfirm("清除成功!", "success", {
            onOk: function () {
              if ($("#list-content tr").length == 0) {
                $(".noneInfoMessage").show();
              } else {
                $(".noneInfoMessage").hide();
              }
            }
          });
        }, 'json');
      }
    });
  });

  $('.clear').on('click', function () {
    var checked = $('#list-content>tbody>tr').find('[type=checkbox]:checked');
    if ($(checked).attr("checked") != "checked") {
      window.wxc.xcConfirm('请选择要清除包', 'warning');
      return;
    }
    window.wxc.xcConfirm("是否删除打包？", "custom", {
      title: "提示信息",
      icon: "0 0",//蓝色i
      btn: window.wxc.xcConfirm.btnEnum.okcancel,
      onOk: function () {
        var taskList = new Array();
        $.each(checked, function (i, data) {
          taskList.push(parseInt($(data).attr('data-opt')));
        });
        var parm = {
          "funtion": "clear_pack_task",
          "params": taskList //任务列表的id 从查询打包中获取
        };
        parm = $.extend({}, defaultParams, parm);
        $.get(baseApi, {
          send: JSON.stringify(parm)
        }, function (res) {
          if ((res.code * 1) != 0) {
            window.wxc.xcConfirm(res.msg, "error");
            return;
          }
          window.wxc.xcConfirm("清除成功!", "success", {
            onOk: function () {
              if ($("#list-content tr").length == 0) {
                $(".noneInfoMessage").show();
              } else {
                $(".noneInfoMessage").hide();
              }
            }
          });
          queryPackageList();
        }, 'json');
      }
    });
  });

  $('#list-content').find('.package_item').live('click', function () {
    if ($(this).is(":checked")) {
      window.taskchecked[$(this).attr("data-opt")] = 1;
    } else {
      window.taskchecked[$(this).attr("data-opt")] = 0;
    }
  });

  var packageStatus = {
    '0': '失败',
    '1': '未开始',
    '2': '正在打包',
    '3': '成功',
    '4': '取消',
    '5': '完成'
  };
  queryPackageList();

  var timer = setInterval(queryPackageList, 2000);


  function queryPackageList() {
    var parm = {
      "funtion": "get_pakcing_info",
      "params": {} //任务列表的id 从查询打包中获取
    };
    parm = $.extend({}, defaultParams, parm);
    $.get(baseApi,{send: JSON.stringify(parm)}, function (res) {
      if (res.code != 0) {
        clearInterval(timer);
        alert('刷新打包信息失败!(' + res.msg + ')');
        return;
      }
      window.data = res.data;
      $('#list-content > tbody').children().remove();
      if (data != null) {
        $.each(data, function (i, param) {
          var tr = '';
          tr += ('<tr>');
          if (window.taskchecked[param['list_id']] == 1) {
            tr += '<td style="width: 100px"><input class="package_item" type="checkbox" data-opt="' + param['list_id'] + '" name="checkbox" checked /></td>';
          } else {
            tr += '<td style="width: 100px"><input class="package_item" type="checkbox" data-opt="' + param['list_id'] + '" name="checkbox" /></td>';
          }
          tr += ('<td style="width: 100px">');
          tr += (i + 1);
          tr += ('</td>');
          tr += ('<td>');
          tr += ('<span class="package-fileName">' + param['file_name'] + '</span>');
          tr += ('</td>');
          tr += ('<td style="width: 200px">');
          var color;
          if (packageStatus[param['status'] + ''] == '失败') {
            color = "color : red";
          }
          tr += ('<span  class="pack-status" style ="' + color + '">' + packageStatus[param['status'] + ''] + '</span>');
          tr += ('</td>');
          tr += ('<td style="width: 200px">');
          tr += (param['all_count'] + '/');
          tr += (param['success'] + '/');
          tr += (param['fail']);
          tr += ('</td>');
          tr += ('<td style="width: 300px">');
          tr += ('<span class="pack-delete pack-clear">删除</span>');
          tr += ('<span class="line">|</span>');
          tr += ('<span class="pause pack-pause">暂停</span>');
          if (param['status'] == 3) {
            tr += ('<span class="line">|</span>');
            tr += ('<a href="' + param['url'] + '" target="_blank" class="download"><span>下载</span><a>');
          }
          tr += ('</td>');
          tr += ('</tr>');
          $('#list-content > tbody').append(tr);
        });
        $("#allCkb").change(function () {
          allCheck(document.getElementById('allCkb'), $('.package_item'));
        });
        $("#allCkb").attr("checked", false);
        $(".noneInfoMessage").hide();
      }
    }, 'json', false);
  }

  //图片预览
  $("#game-icon-file").change(function (e) {
    $("#game-icon-file").attr("data-upload", true);
    var file = this.files[0];
    //图片尺寸检测
    var image = new Image();
    image.onload = function () {
      if (image.width != 256 || image.height != 256) {
        window.wxc.xcConfirm("图片尺寸不符合要求,必须是256*256!", "warning");
        $("#gameIconUpload").attr("disable", false);
        return false;
      } else {
        var objUrl = getObjectURL(file);
        if (objUrl) {
          $("#upload-img").attr("src", objUrl);
          $("#gameIconUpload").text('开始上传');
          $("#gameIconUpload").removeAttr("disabled");
        }
      }
    };
    var fr = new FileReader();
    fr.onload = function () {
      image.src = fr.result;
    };
    fr.readAsDataURL(file);
  });


  $("#game-package-file").change(function (e) {
    var objUrl = getObjectURL(this.files[0]);
    var name = e.currentTarget.files[0].name;
    var fileType = name.substring(name.lastIndexOf("."), name.length).replace('.', '');
    if (fileType != 'zip') {
      window.wxc.xcConfirm("文件不是zip格式的!", "warning");
      $(e).attr("disable", false);
      return false;
    } else {
      if (objUrl) {
        $("#upload-pic").attr("src", "img/zip7.png");
        $(".upload-btn").text('开始上传');
        $(".upload-btn").removeAttr("disabled");
      }
    }
  });


  //上传图片
  $("#gameIconUpload").click(function () {
    var file = document.getElementById('game-icon-file').files[0];
    //调用上传文件方法
    uploadOssFile(file, gameIconFileKey, {
      uploadComplete: function () {
        $("#gameIconUpload").text('已上传');
        $("#gameIconUpload").attr({
          "disabled": "disabled"
        });
        /*  $('#game-icon-file').data('url',gameIconFileKey);*/
        window.wxc.xcConfirm("上传成功!", "success");
        $("#game-icon-file").removeAttr("data-upload")
      }
    });

  });

  $(".upload-btn").click(function () {
    var file = document.getElementById('game-package-file').files[0];
    //调用上传文件方法
    uploadOssFile(file, downOtherUrl, {
      uploadComplete: function () {
        $(".upload-btn").text('已上传');
        $(".upload-btn").attr({
          "disabled": "disabled"
        });
        $('#game-package-file').data('url',downOtherUrl);
        window.wxc.xcConfirm("上传成功!", "success");
      }
    });
  });

});

