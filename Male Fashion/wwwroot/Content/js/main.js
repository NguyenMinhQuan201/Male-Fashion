/*  ---------------------------------------------------
    Template Name: Male Fashion
    Description: Male Fashion - ecommerce teplate
    Author: Colorib
    Author URI: https://www.colorib.com/
    Version: 1.0
    Created: Colorib
---------------------------------------------------------  */

'use strict';

(function ($) {

    /*------------------
        Preloader
    --------------------*/
    $(window).on('load', function () {
        const isLocalhost = window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1';
        var url = "";
        if (isLocalhost) {
            url = "https://localhost:7179/"
        }
        else {
            url = "http://192.168.1.16:9898/"
        }
        $(".loader").fadeOut();
        $("#preloder").delay(200).fadeOut("slow");

        /*------------------
            Gallery filter
        --------------------*/
        $('.filter__controls li').on('click', function () {
            $('.filter__controls li').removeClass('active');
            $(this).addClass('active');
        });
        if ($('.product__filter').length > 0) {
            var containerEl = document.querySelector('.product__filter');
            var mixer = mixitup(containerEl);
        }
        if (localStorage) {
            var cartsDataAsJson = localStorage.getItem("Carts")
            var carts = JSON.parse(cartsDataAsJson);
            var tongSoLuong = 0;
            var tongGia = 0;
            var rows = ""
            for (var i = 0; i < carts.length; i++) {
                var cart = carts[i];
                tongSoLuong = tongSoLuong + Number(cart.quantity);
                tongGia = tongGia + cart.price * cart.quantity;
                rows += `
                                                                <div class="cart-sub">
                                                                    <img class="image-cart-sub" src="${url}user_content/${cart.imagePath}" />
                                                                        <div>
                                                                            <span>${cart.name}</span>
                                                                            <p>Số lượng : ${cart.quantity}</p>
                                                                            <button class="button-remove-cart-sub" data-id="${cart.id}">
                                                                                Xóa
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                               
                                                            `
            }
            $('.cart-sub-render').html(rows);
            $('.conut-number').html(tongSoLuong);
            $('.price').html(tongGia + "đ");
            $(".button-remove-cart-sub").off('click').on('click', function () {
                console.log("YOLO")
                var tongtien = 0;
                var rows = "";
                var click = $(this);
                var Id = click.data('id');
                let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                if (Carts.length > 0) {
                    for (var i = 0; i < Carts.length; i++) {
                        if (Id == Carts[i].id) {
                            var tmp = Carts[i]
                            Carts[i] = Carts[0]
                            Carts[0] = tmp
                            Carts.splice(0, 1);
                            localStorage.setItem('Carts', JSON.stringify(Carts));
                            if (localStorage) {
                                var cartsDataAsJson = localStorage.getItem("Carts")
                                if (cartsDataAsJson) {
                                    /**
                                     * @type{ {
                                     *  Size: string;
                                     *  Colour: string;
                                     *  Img: string;
                                     *  
                                     * }[]}
                                     * */
                                    var carts = JSON.parse(cartsDataAsJson);

                                    for (var i = 0; i < carts.length; i++) {

                                        var cart = carts[i];
                                        tongtien = tongtien + cart.tong;
                                        rows += `
                                    <div class="cart-sub">
                                                                    <img class="image-cart-sub" src="${url}user_content/${cart.imagePath}" />
                                                                        <div>
                                                                            <span>${cart.name}</span>
                                                                            <p>Số lượng :${cart.quantity}</p>
                                                                            <button class="button-remove-cart-sub" data-id="${cart.id}">
                                                                                Xóa
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                        `
                                    }
                                    $('.cart-sub-render').html(rows);
                                    localStorage.setItem('TT', tongtien);
                                    $('.total-checkout').html(tongtien);
                                }
                            }
                        }
                    }
                }
                if (Carts.length == 0) {
                    $('.total-checkout').html("0");
                }
                if (Carts.length > 0) {
                    SetRemove();
                }
            });
            function SetRemove() {
                $(".button-remove-cart-sub").off('click').on('click', function () {
                    console.log("YOLO")
                    var tongtien = 0;
                    var rows = "";
                    var click = $(this);
                    var Id = click.data('id');
                    let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                    if (Carts.length > 0) {
                        for (var i = 0; i < Carts.length; i++) {
                            if (Id == Carts[i].id) {
                                var tmp = Carts[i]
                                Carts[i] = Carts[0]
                                Carts[0] = tmp
                                Carts.splice(0, 1);
                                localStorage.setItem('Carts', JSON.stringify(Carts));
                                if (localStorage) {
                                    var cartsDataAsJson = localStorage.getItem("Carts")
                                    if (cartsDataAsJson) {
                                        /**
                                         * @type{ {
                                         *  Size: string;
                                         *  Colour: string;
                                         *  Img: string;
                                         *  
                                         * }[]}
                                         * */
                                        var carts = JSON.parse(cartsDataAsJson);

                                        for (var i = 0; i < carts.length; i++) {

                                            var cart = carts[i];
                                            tongtien = tongtien + cart.tong;
                                            rows += `
                                    <div class="cart-sub">
                                                                    <img class="image-cart-sub" src="${url}user_content/${cart.imagePath}" />
                                                                        <div>
                                                                            <span>${cart.name}</span>
                                                                            <p>Số lượng :${cart.quantity}</p>
                                                                            <button class="button-remove-cart-sub" data-id="${cart.id}">
                                                                                Xóa
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                        `
                                        }
                                        $('.cart-sub-render').html(rows);
                                        localStorage.setItem('TT', tongtien);
                                        $('.total-checkout').html(tongtien);
                                    }
                                }
                            }
                        }
                    }
                    if (Carts.length == 0) {
                        $('.total-checkout').html("0");
                    }
                });
            }
            //new CartController();
        }
    });

    /*------------------
        Background Set
    --------------------*/
    $('.set-bg').each(function () {
        var bg = $(this).data('setbg');
        $(this).css('background-image', 'url(' + bg + ')');
    });

    //Search Switch
    $('.search-switch').on('click', function () {
        $('.search-model').fadeIn(400);
    });

    $('.search-close-switch').on('click', function () {
        $('.search-model').fadeOut(400, function () {
            $('#search-input').val('');
        });
    });

    /*------------------
        Navigation
    --------------------*/
    $(".mobile-menu").slicknav({
        prependTo: '#mobile-menu-wrap',
        allowParentLinks: true
    });
    /*------------------
        Accordin Active
    --------------------*/
    $('.collapse').on('shown.bs.collapse', function () {
        $(this).prev().addClass('active');
    });

    $('.collapse').on('hidden.bs.collapse', function () {
        $(this).prev().removeClass('active');
    });

    //Canvas Menu
    $(".canvas__open").on('click', function () {
        $(".offcanvas-menu-wrapper").addClass("active");
        $(".offcanvas-menu-overlay").addClass("active");
    });

    $(".offcanvas-menu-overlay").on('click', function () {
        $(".offcanvas-menu-wrapper").removeClass("active");
        $(".offcanvas-menu-overlay").removeClass("active");
    });

    /*-----------------------
        Hero Slider
    ------------------------*/
    $(".hero__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 1,
        dots: false,
        nav: true,
        navText: ["<span class='fa fa-arrow-left'><span/>", "<span class='fa fa-arrow-right'><span/>"],
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: false
    });

    /*--------------------------
        Select
    ----------------------------*/
    $("select").niceSelect();

    /*-------------------
        Radio Btn
    --------------------- */
    /*$(".product__color__select label, .shop__sidebar__size label, .product__details__option__size label").on('click', function () {
        $(".product__color__select label, .shop__sidebar__size label, .product__details__option__size label").removeClass('active');
        $(this).addClass('active');
    });
    $(".product__details__option__size2 label").on('click', function () {
        console.log("lala");
        $(".product__details__option__size2 label").removeClass('active');
        $(this).addClass('active');
    });*/

    /*-------------------
        Scroll
    --------------------- */
    $(".nice-scroll").niceScroll({
        cursorcolor: "#0d0d0d",
        cursorwidth: "5px",
        background: "#e5e5e5",
        cursorborder: "",
        autohidemode: true,
        horizrailenabled: false
    });

    /*------------------
        CountDown
    --------------------*/
    // For demo preview start
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    if (mm == 12) {
        mm = '01';
        yyyy = yyyy + 1;
    } else {
        mm = parseInt(mm) + 1;
        mm = String(mm).padStart(2, '0');
    }
    var timerdate = mm + '/' + dd + '/' + yyyy;
    // For demo preview end


    // Uncomment below and use your date //

    /* var timerdate = "2020/12/30" */

    $("#countdown").countdown(timerdate, function (event) {
        $(this).html(event.strftime("<div class='cd-item'><span>%D</span> <p>Days</p> </div>" + "<div class='cd-item'><span>%H</span> <p>Hours</p> </div>" + "<div class='cd-item'><span>%M</span> <p>Minutes</p> </div>" + "<div class='cd-item'><span>%S</span> <p>Seconds</p> </div>"));
    });

    /*------------------
        Magnific
    --------------------*/
    $('.video-popup').magnificPopup({
        type: 'iframe'
    });

    /*-------------------
        Quantity change
    --------------------- */
    var proQty = $('.pro-qty');
    proQty.prepend('<span class="fa fa-angle-up dec qtybtn"></span>');
    proQty.append('<span class="fa fa-angle-down inc qtybtn"></span>');
    proQty.on('click', '.qtybtn', function () {
        var $button = $(this);
        var oldValue = $button.parent().find('input').val();
        if ($button.hasClass('inc')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below zero
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
            }
        }
        $button.parent().find('input').val(newVal);
    });

    var proQty = $('.pro-qty-2');
    proQty.prepend('<span class="fa fa-angle-left dec qtybtn"></span>');
    proQty.append('<span class="fa fa-angle-right inc qtybtn"></span>');
    proQty.on('click', '.qtybtn', function () {
        var $button = $(this);
        var oldValue = $button.parent().find('input').val();
        if ($button.hasClass('inc')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below zero
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
            }
        }
        $button.parent().find('input').val(newVal);
    });

    /*------------------
        Achieve Counter
    --------------------*/
    $('.cn_num').each(function () {
        $(this).prop('Counter', 0).animate({
            Counter: $(this).text()
        }, {
            duration: 4000,
            easing: 'swing',
            step: function (now) {
                $(this).text(Math.ceil(now));
            }
        });
    });
    //class="conut-number"
    /*$('.conut-number').on("load", function () {
        console.log("number")

    });*/
    /*------------------
        MiniCart
    --------------------*/
    var status = false;
    $('.cart_icon').off('click').on('click', function () {
        if (status == false) {
            status = true;
            $("#mini_cart_id").slideDown();
            document.getElementById("get_blur").style.display = "block";
        }
        else {
            $("#mini_cart_id").slideUp();
            status = false;
        }
    });
    $('#cart_icon_close').off('click').on('click', function () {
        $("#mini_cart_id").slideUp();
        status = false;
        document.getElementById("get_blur").style.display = "none";
    });




    $('#get_blur').off('click').on('click', function () {
        $("#mini_cart_id").slideUp();
        status = false;
        document.getElementById("get_blur").style.display = "none";
    });
    /*------------------
        ScrollCartSub
    --------------------*/
    window.onscroll = function () { myFunction() };

    var cartsub = document.getElementById("mini_cart_id");
    var sticky = cartsub.offsetTop;

    function myFunction() {
        if (window.pageYOffset > sticky) {
            cartsub.classList.add("sticky");
        } else {
            cartsub.classList.remove("sticky");
        }
    }
})(jQuery);