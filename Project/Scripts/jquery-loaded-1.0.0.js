//This handles the loaded
$(document).ready(function () {
    var $header = $('#header');
    var $footer = $('#footer');
    var $content = $('#container');

    $.ajaxSetup({ cache: false });

    $(window).on('resize', function () {
        var reduce = 0;

        var height = $(window).height() - $header.height() - $footer.height() - reduce;
        $content.height(height);
    }).trigger('resize'); //on page load

    $('#toast-container').click(function (e) {

        if ($(this.innerHTML).find(':contains("success")').length > 0) {
            alert('success');
        } else if ($(this.innerHTML).find(':contains("info")').length > 0) {
            alert('info');
        } else if ($(this.innerHTML).find(':contains("warning")').length > 0) {
            alert('warning');
        } else if ($(this.innerHTML).find(':contains("error")').length > 0) {
            alert('error');
        }
    });

    waiting(false);
});