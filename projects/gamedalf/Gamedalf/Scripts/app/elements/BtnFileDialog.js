
(function ($) {

    var $btnFileDialogs = $('.btn-file-dialog');

    var $inputs = $btnFileDialogs
        .children('input');

    var $btns = $btnFileDialogs
        .children('a');

    $inputs.hide();

    $btns.click(function () {
        $(this)
            .siblings('input')
            .click();
        return false;
    });

})(jQuery);