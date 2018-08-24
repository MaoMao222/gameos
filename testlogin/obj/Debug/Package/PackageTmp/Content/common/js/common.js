/**
 * Created by mm on 2016/8/26.
 */
(function ($) {
  $.getUrlParam = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
  };

  //加载
  var _get = $.get;
  $.get = function (url, data, success, dataType, isShow) {
    if (isShow == undefined || isShow == true) {
      $('.mark').show();
    }

    if (typeof data == 'function') {
      _get(url, function (response, status, xhr) {
        if (isShow == undefined || isShow == true) {
          $('.mark').hide();
        }
        data(response, status, xhr);
      }, success);
    } else {
      _get(url, data, function (response, status, xhr) {
        if (isShow == undefined || isShow == true) {
          $('.mark').hide();
        }
        success(response, status, xhr);
      }, dataType);
    }
  };
})(jQuery);


