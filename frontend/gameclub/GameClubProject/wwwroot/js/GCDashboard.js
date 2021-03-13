$(document).ready(function () {
    // Popular Gme Effect-----
    $('.popularGameText').novacancy({

        'reblinkProbability': 0.1,
        'blinkMin': 0.2,
        'blinkMax': 0.6,
        'loopMin': 8,
        'loopMax': 10,
        'color': '#ffffff',
        'glow': ['0 0 80px #ffffff', '0 0 30px #008000', '0 0 6px #0000ff']
    });

    $('.gameText').novacancy({
        'blink': 1,
        'off': 1,
        'color': 'Red',
        'glow': ['0 0 80px Red', '0 0 30px FireBrick', '0 0 6px DarkRed']
    });

    // -----------


    // Ajax call for filters
    // -----Genres--------
    $(".filters").hide();
    $(".filterBox").on("click", ".genres", function (e) {
        e.preventDefault();
        $(".Platformsfilters").hide("slow");
        $(".Ratingfilters").hide("slow");

        if ($(".Genresfilters").attr("style") == "display: none;") {

            if ($(this).data("requested") == "no") {
                $.ajax({
                    method: "GET",
                    url: $(this).attr("href"),
                    success: function (response) {

                        for (let index = 0; index < response.length; index++) {
                            $(".Genresfilters").append("<a href='/fetchGameBaseOnGenres/" + response[index].slug + "' class='btn btn-sm btn-outline-danger mr-3 mt-2'> " + response[index].name + " </a>")


                        }
                    }
                });


            }
            $(this).data("requested", "yes");
            $(".Genresfilters").show();
        }
        else {
            $(".Genresfilters").hide("slow");
        }
        return false;
    })

    // -----Platforms--------
    $(".filterBox").on("click", ".platforms", function (e) {
        e.preventDefault();
        $(".Genresfilters").hide("slow");
        $(".Ratingfilters").hide("slow");
        if ($(".Platformsfilters").attr("style") == "display: none;") {

            if ($(this).data("requested") == "no") {
                $.ajax({
                    method: "GET",
                    url: $(this).attr("href"),
                    success: function (response) {
                        for (let index = 0; index < response.length; index++) {
                            $(".Platformsfilters").append("<a href='/fetchGameBaseOnPlatform/" + response[index] + "' class='btn btn-sm btn-outline-danger mr-3 mt-2'> " + response[index] + " </a>")
                        }
                    }
                });
            }
            $(this).data("requested", "yes");
            $(".Platformsfilters").show();

        }
        else {
            $(".Platformsfilters").hide("slow");
        }
        return false;
    })

    // -----Rating--------
    $(".filterBox").on("click", ".rating", function (e) {
        e.preventDefault();
        $(".Platformsfilters").hide("slow");
        $(".Genresfilters").hide("slow");
        $(".Ratingfilters").show();

        return false;
    })


    // ----Game Profile Rating Circle Progress bar animation-----
    $('svg.radial-progress').each(function (index, value) {
        $(this).find($('circle.complete')).removeAttr('style');
    });
    $(window).scroll(function () {
        $('svg.radial-progress').each(function (index, value) {
            // If svg.radial-progress is approximately 25% vertically into the window when scrolling from the top or the bottom
            if (
                $(window).scrollTop() > $(this).offset().top - ($(window).height() * 0.75) &&
                $(window).scrollTop() < $(this).offset().top + $(this).height() - ($(window).height() * 0.25)
            ) {
                // Get percentage of progress
                percent = $(value).data('percentage');
                // Get radius of the svg's circle.complete
                radius = $(this).find($('circle.complete')).attr('r');
                // Get circumference (2πr)
                circumference = 2 * Math.PI * radius;
                // Get stroke-dashoffset value based on the percentage of the circumference
                strokeDashOffset = circumference - ((percent * circumference) / 100);
                // Transition progress for 1.25 seconds
                $(this).find($('circle.complete')).animate({ 'stroke-dashoffset': strokeDashOffset }, 1250);
            }
        });
    }).trigger('scroll');



    // Expansion Sliders CoverEffect in Game Profile

    var swiper = new Swiper('.ExpansionSwiper-container', {

        effect: 'coverflow',
        grabCursor: true,
        centeredSlides: true,
        slidesPerView: 'auto',

        coverflowEffect: {
            rotate: 0,
            stretch: 0,
            depth: 100,
            modifier: 1,
            slideShadows: false,
        },
        autoplay: {
            delay: 3000,
        },
        mousewheel: {
            invert: true,
            forceToAxis: true,
            sensitivity: 200
        },
        pagination: {
            el: '.ExpansionSwiper-pagination',
            clickable: true
        },
    });
    swiper.on('autoplayStop', function () {
        swiper.autoplay.start();
    });



    // Slide for popular games showcase
    var swiper = new Swiper('.swiper-container', {

        effect: 'coverflow',
        grabCursor: true,
        centeredSlides: true,
        slidesPerView: 'auto',

        coverflowEffect: {
        rotate: 0,
        stretch: 0,
        depth: 100,
        modifier: 1,
        slideShadows : false,
        },
        autoplay: {
            delay: 3000,
        },
        mousewheel: {
            invert: true,
            forceToAxis:true,
            sensitivity:200
        },
        pagination: {
        el: '.swiper-pagination',
        clickable:true
        },
    });
    swiper.on('autoplayStop', function () {
        swiper.autoplay.start();
    });



    // Game Reviewed Sliders CoverEffect in User Profile
    var swiper = new Swiper('.ReviewedGamesSwiper-container', {

        effect: 'coverflow',
        grabCursor: true,
        centeredSlides: true,
        slidesPerView: 'auto',

        coverflowEffect: {
            rotate: 0,
            stretch: 0,
            depth: 100,
            modifier: 1,
            slideShadows: false,
        },
        autoplay: {
            delay: 4000,
        },
        mousewheel: {
            invert: true,
            forceToAxis: true,
            sensitivity: 200
        },
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        },
    });
    swiper.on('autoplayStop', function () {
        swiper.autoplay.start();
    });


    // Video Swiper in gAME pROFILE
    var swiper = new Swiper('.VideoSwiper-container', {

        loop: true,
        mousewheel: {
            invert: true,
            forceToAxis: true,
            sensitivity: 200
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
    });







    //-----scroll effect for User Profile-------
    window.sr = ScrollReveal();
    sr.reveal('.profileInfo .details', {
        duration: 2000,
        scale: 0.90,
        delay: 400

    });
    sr.reveal('.ProfilePic', {
        duration: 2000,
        origin: 'left',
        distance: '200px'

    });
    sr.reveal('.ProfileBg .gameReviewed', {
        duration: 2000,
        origin: 'bottom',
        distance: '200px'

    });


    // -----Scroll effect for Popular Game Section in Dashboard

    sr.reveal('.popularGameSection', {
        duration: 2000,
        origin: 'right',
        distance: '400px'

    });
    sr.reveal('.game', {
        duration: 2000,
        origin: 'bottom',
        distance: '100px'

    });

});