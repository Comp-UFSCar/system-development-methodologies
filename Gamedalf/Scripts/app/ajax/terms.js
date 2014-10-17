(function () {

    // hides out confirm button
    document.getElementById('btn-confirm').style.display = 'none';

    console.log(document.getElementById('btn-confirm').style.display);

    var $content,
        $confirm;

    function load() {
        $.ajax({
            url: '/api/Terms/Developers',
            type: 'GET',
        })
        .done(function (data) {
            $content.append(data);
        })
        .fail(function (data) {
            $content.html(data);
            //$confirm.fadeOut(500);
        });

        return false;
    }

    $(document).ready(function () {
        $content = $('#terms-content');
        $confirm = $('#btn-confirm');
        load();
    });
}());
