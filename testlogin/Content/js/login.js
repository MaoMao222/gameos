/**
 * Created by adim on 2016/8/22.
 */
$(function() {

    $('.page-container .submit').click(function(){
        var username = $(this).find('.username').val();
        var password = $(this).find('.password').val();
        if(username == '') {
            $(this).find('.error').fadeOut('fast', function(){
                $(this).css('top', '27px');
            });
            $(this).find('.error').fadeIn('fast', function(){
                $(this).parent().find('.username').focus();
            });
            return false;
        }
        if(password == '') {
            $(this).find('.error').fadeOut('fast', function(){
                $(this).css('top', '96px');
            });
            $(this).find('.error').fadeIn('fast', function(){
                $(this).parent().find('.password').focus();
            });
            return false;
        }
    });

    $('.page-container  .username, .page-container  .password').keyup(function(){
        $(this).parent().find('.error').fadeOut('fast');
    });

    $(".submit").click(function(){
        login();
    });
	
	$('.page-container').on('keypress',function(event){
		if(event.keyCode == 13){
        login();
			  return;
		}
	});
	
    function login(){
    	 var username = $('[name=username]').val();
       var password = $('[name=password]').val();

        var parm =
        {
            "module": "pluginsauth",
            "funtion": "logon",
            "session": "",
            "params":
            {
                "account": username,
                "password": password
            }
        };

        var get_url ="http://yp.bookse.cn/webservice.php";
        $.get(get_url,{
            send:JSON.stringify(parm)
        },function(res){
               if(res.code==0){
                 var data = JSON.stringify(res);
                 $.cookie('data',data);
                 location.href="index.html?data";
               }else{
                   window.wxc.xcConfirm(res.msg, "error");
               }
        },'json');
    }

});
/**
 * Created by adim on 2016/8/23.
 */
