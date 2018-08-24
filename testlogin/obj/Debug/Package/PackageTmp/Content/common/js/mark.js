/**
 * Created by sjj1 on 2016/10/17.
 */
(function($){
  $.fn.extend({
    mark:function(params){
      var funs = {
        init:function(options){

        },
        show:function(target){
          $(target).find('.mark').remove();
          var mark = ['<div class="mark" data-type="mark">',
            '<center class="vertical-center" style="position: absolute;top: 50%;left: 50%;transform: translate(-50%, -50%);">',
            '<div class=" line-spin-fade-loader">',
            '<div></div>',
            '<div></div>',
            '<div></div>',
            '<div></div>',
            '<div></div>',
            '<div></div>',
            '<div></div>',
            '<div></div>',
            '</div>',
            '</center>',
            '</div>'].join('');
          var tempNode = $('<div><div>').append(mark).find('.mark');
          /*$(tempNode).css('position','relative');*/
          $(target).append($(tempNode));
          return target;
        },
        hide:function(target){
          $(target).find('[data-type=mark]').remove();
        }
      };
      if(params) var fn = $.type(params) == 'string' ? funs[params](this) : funs['init'](params);
    }
  });
})(jQuery);

