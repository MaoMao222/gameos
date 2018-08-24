//(function ($) {
//     $.login = {
  
//          formMessage: function (msg) {
//              $('.login_tips').find('.tips_msg').remove();
//              $('.login_tips').append('<div class="tips_msg"><i class=fa fa-question-circle></i>' + msg + '</div>');
//          },
  
//          loginClick: function () {
//             var $username = $("#username");
//             var $password = $("#password");
             
//             if ($username.val() == "") {
//                 $username.focus();
//                 $.login.formMessage('请输入用户名');
  
//                 return false;
//             }
//             else if ($password.val() == "") {
//                 $password.focus();
//                 $.login.formMessage('请输入登录密码');
  
//                 return false;
//             }             
//             else {
  
//                 $.login.formMessage('');
//                 $("#loginButton").attr('disabled', 'disabled').find('span').html("验证中...");
//                 $('form').submit();
// //                $.ajax({
// //                    url: "/Login/CheckLogin",
// //data: { username: $.trim($username.val()), password: $.trim($password.val()) },
// //                    type: "post",
// //                    dataType: "json",
// //                    success: function (data) {
// //                        if (data.state == "success") {
// //                            $("#loginButton").find('span').html("登录成功，正在跳转...");
// //                            window.setTimeout(function () {
// //                                window.location.href = "/Home/Index";
// //                            }, 200);
// //                        }
// //                        else {
// //                            $("#loginButton").removeAttr('disabled').find('span').html("登录");
// //                            //$code.val('');
// //                            $.login.formMessage(data.message);
// //                       }
// //                    }
// //                });
//             }
//         },
 
//         init: function () {             
//             $("#loginButton").click(function () {
//                $.login.loginClick();
//           });
//       }
 
//     };
//     $(function () {
//         $.login.init();
//     });
//})(jQuery);
(function ($) {
    var $username = $("#username");
    var $password = $("#password");

    if ($username.val() == "") {
        $username.focus();
        $.login.formMessage('请输入用户名');

        return false;
    }
    else if ($password.val() == "") {
        $password.focus();
        $.login.formMessage('请输入登录密码');

        return false;
    }
    else {

        $.login.formMessage('');
        $("#loginButton").attr('disabled', 'disabled').find('span').html("验证中...");
        $('form').submit();
    }
})(jQuery);

