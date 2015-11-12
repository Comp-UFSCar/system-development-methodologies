(function ($) {

    var $container,
        $loading,
        $title,
        $dateCreated,
        $content,
        $form,
        $error;

    function load() {
        console.log("Loading Developers terms and conditions...");
        $.ajax({
            url: '/api/Terms?title=Developers',
            type: 'GET',
        })
        .always(function () {
            $loading.hide();
        })
        .done(function (terms) {
            console     .log("Load successful.");
            
            $title      .append(terms.Title);
            $dateCreated.append(terms.DateCreated);
            $content    .append(terms.Content);

            $container
                .hide().removeClass("hidden")
                .fadeIn(500);

            $form
                .hide().removeClass("hidden")
                .fadeIn(500);
        })
        .fail(function (data) {
            console.log("Load unsuccessful. Server's answer: ");
            console.log(data);

            $container.hide();

            $error
                .hide().removeClass("hidden")
                .fadeIn(500);
        });

        return false;
    }

    $(document).ready(function () {
        $container   = $('#terms-container');
        $loading     = $('#terms-loading');
        $title       = $('#terms-title');
        $dateCreated = $('#terms-dateCreated');
        $content     = $('#terms-content');

        $form    = $('#terms-form');
        $error   = $('#terms-error');

        load();
    });
}(window.jQuery));
