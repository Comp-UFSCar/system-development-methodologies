(function ($) {

    console.log("Carousel loaded");
    
    $(".carousel-indicators")
        .children()
        .first()
        .addClass("active");

    $(".carousel-inner")
        .children()
        .first()
        .addClass("active");

})(jQuery);