$(document).ready(function () {

    $('.carousel-moving').slick({
        infinite: false,
        slidesToShow: 4,
        slidesToScroll: 2,
        nextArrow: $('.right-button'),
        prevArrow: $('.left-button'),
        responsive: [
            {
                breakpoint: 2560,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 676,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
            // You can unslick at a given breakpoint now by adding:
            // settings: "unslick"
            // instead of a settings object
        ]
    });

});
