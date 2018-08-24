(function ($) {
    "use strict";
    $(document).ready(function () {
        githubDownloadsCount();
    });

    function githubDownloadsCount() {
        $('.download-count').each(function () {
            var $this = $(this);
            var asset_id = $this.data('asset-id');
            if (asset_id != 0) {
                var repo = $this.data('repo');
                var apiuri = 'https://api.github.com/repos/gigagit/' + repo + '/releases/assets/' + asset_id;
                $.getJSON(apiuri)
                .done(function (data) {
                    $.each(data, function (key, item) {
                        if (key == 'download_count') {
                            $this.html(item);
                            return false;
                        }
                    });
                });
            } else {
                $this.parent().parent().parent().css({ 'display': 'none' });
            }
        });
    }
})(jQuery);