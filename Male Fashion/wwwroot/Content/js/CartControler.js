
class CartController {

    constructor() {
        this.renderCartsContent();
        this.allPrice();
        this.renderOrderDetailContent();

        $(".product__color__select label, .shop__sidebar__size label").on('click', function () {
            $(".product__color__select label, .shop__sidebar__size label ").removeClass('active');
            $(this).addClass('active');
        });
        $(".product__details__option__size label").on('click', function () {
            console.log("tren");
            $(".product__details__option__size label").removeClass('active');
            $(this).addClass('active');
            var rows = $('.product__details__option__size label.active').data('id');
            console.log("xong");
        });
        $(".product__details__option__size2 label").on('click', function () {
            console.log("duoi");
            $(".product__details__option__size2 label").removeClass('active');
            $(this).addClass('active');

            console.log("xong");
        });
        $('.size').on('click', function () {
            var click = $(this);
            var size = click.data('id');
            console.log(size);
            $.ajax({
                url: "/Events/checkbysize",
                data: { kich: size },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        var rows = "<span>Color:</span>";
                        console.log("ok")
                        console.log(response)
                        for (var i = 0; i < response.arr.length; i++) {
                            console.log(response.arr[i].id);
                            console.log(response.arr[i].mauSacSP);
                            rows += `
                                                <label for="${response.arr[i].mauSacSP}" class="colour" data-id="${response.arr[i].id}"">
                                                    ${response.arr[i].mauSacSP}
                                                    <input name="mau" type="radio" id="${response.arr[i].id}">
                                                </label>
                                        
                                    `
                        }
                        $('#ngu').html(rows);
                        $(".product__details__option__size2 label").on('click', function () {
                            console.log("duoi");
                            $(".product__details__option__size2 label").removeClass('active');
                            $(this).addClass('active');
                            var size = $('.product__details__option__size label.active').data('id');
                            var colour = $('.product__details__option__size2 label.active').data('id');
                            console.log(size);
                            console.log(colour);
                            var Id = $('.button_add_to_cart_new').data('id');
                            $.ajax({
                                url: "/Events/KiemTra",
                                data: { id: Id, mau: colour, kich: size },
                                dataType: "json",
                                type: "POST",
                                success: function (response) {
                                    if (response.status == false) {
                                        $('.button_add_to_cart_new').attr("disabled", true);
                                        $('.primary-btn').html("Đã hết hàng")
                                    }
                                    else {
                                        $('.button_add_to_cart_new').attr("disabled", false);
                                        $('.primary-btn').html("ADD TO CART")
                                    }
                                }
                            })
                        });
                    }
                }
            })
        });
        $('.button_abc').on('click', function () {
            console.log("ok");
            var click = $(this);
            var Id = click.data('id');
            var size = $('.product__details__option__size label.active').data('id');
            var colour = $('.product__details__option__size2 label.active').data('id');
            console.log(size);
            console.log(colour);
            if (size == null || colour == null) {
                alert("Hình như bạn quên chọn size hoặc màu !");
            }
        });
        $('#icolour').on('change', function () {
            console.log("heheheheh");
            var size = $('#isize').val();
            var colour = $('#icolour').val();
            var Id = $('.button_add_to_cart_new').data('id');
            $.ajax({
                url: "/Events/onCheck",
                data: {
                    id: Id,
                    mau: colour,
                    kich: size
                },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == false) {
                        $('.button_add_to_cart_new').attr("disabled", true);
                        $('.primary-btn').html("Đã hết hàng")
                    }
                    else {
                        $('.button_add_to_cart_new').attr("disabled", false);
                        $('.primary-btn').html("ADD TO CART")

                    }
                }
            })
        });
        $('#isize').on('change', function () {
            console.log("heheheheh");
            var size = $('#isize').val();
            var colour = $('#icolour').val();
            var Id = $('.button_add_to_cart_new').data('id');
            $.ajax({
                url: "/Events/onCheck2",
                data: { id: Id, mau: colour, kich: size },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == false) {
                        $('.button_add_to_cart_new').attr("disabled", true);
                        $('.primary-btn').html("Đã hết hàng")
                    }
                    else {
                        $('.button_add_to_cart_new').attr("disabled", false);
                        $('.primary-btn').html("ADD TO CART")
                    }
                }
            })
        });
        $('.change-price').on('change', function () {
            console.log("ok")
            var click = $(this);
            var Quanity = click.val();
            var Id = click.data('id');
            $.ajax({
                url: "/Carts/ChangePrice",
                data: { id: Id, sl: Quanity },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                        if (Carts.length > 0) {
                            for (var i = 0; i < Carts.length; i++) {
                                if (Id == Carts[i].Prime) {
                                    Carts[i].SoLuong = Number(Quanity);
                                    Carts[i].Tong = Carts[i].SoLuong * Carts[i].Gia
                                    $(`#total-price-${Id}`).html("$" + Carts[i].Tong);
                                    localStorage.setItem('Carts', JSON.stringify(Carts));
                                }
                            }
                            var tongtien = 0;
                            for (var i = 0; i < Carts.length; i++) {
                                if (Carts[i].TrangThai == true) {
                                    tongtien = tongtien + Carts[i].Tong;
                                }
                            }
                            localStorage.setItem('TT', tongtien)
                            localStorage.setItem('Carts', JSON.stringify(Carts));
                            let TT = localStorage.getItem('TT')
                            $('.total-checkout').html(tongtien)
                            localStorage.setItem('Carts', JSON.stringify(Carts));
                            var row = "";
                            $(`.mess_${Id}`).html(row);
                        }
                    }
                    else {
                        var row = " San pham ko the vuot qua " + response.max;
                        if (response.max == 0) {
                            console.log("ok");
                            row = "da het hang";
                            let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                            if (Carts.length > 0) {
                                for (var i = 0; i < Carts.length; i++) {
                                    if (Id == Carts[i].Prime) {
                                        Carts[i].TrangThai = false;
                                        Carts[i].SoLuong = 1;
                                        Carts[i].Tong = Carts[i].SoLuong * Carts[i].Gia
                                        $(`#total-price-${Id}`).html(Carts[i].Tong);
                                        localStorage.setItem('Carts', JSON.stringify(Carts));
                                        console.log("ok");
                                    }
                                }
                                var tongtien = 0;
                                for (var i = 0; i < Carts.length; i++) {
                                    if (Carts[i].TrangThai == true) {
                                        tongtien = tongtien + Carts[i].Tong;
                                    }
                                }
                                localStorage.setItem('TT', tongtien)
                                localStorage.setItem('Carts', JSON.stringify(Carts));
                                let TT = localStorage.getItem('TT')
                                $('.total-checkout').html(tongtien)
                                localStorage.setItem('Carts', JSON.stringify(Carts));
                                $(`.mess_${Id}`).html(row);
                            }

                        }
                        $(`.mess_${Id}`).html(row);
                    }
                }
            })
        });
        $('.button_add_to_cart_new').on('click', function () {
            let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
            var click = $(this);
            var Id = click.data('id');
            var size = $('.product__details__option__size label.active').data('id');
            var colour = $('.product__details__option__size2 label.active').data('id');
            if (size == null || colour == null) {
                alert("Hình như bạn quên chọn size hoặc màu !");
            }
            else {
                let Temp = [];
                Temp.push({
                    Id: Id,
                    Colour: colour,
                    Size: size,
                });
                $.ajax({
                    url: "/Carts/AddCart",
                    data: { cartTemp: JSON.stringify(Temp) },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        if (response.status == true) {
                            var a = 0;
                            let b = String(1);
                            if (Carts.length > 0) {
                                for (var i = 0; i < Carts.length; i++) {
                                    if (Carts[i].Prime == response.prime) {
                                        if (Carts[i].SoLuong < response.soLuong) {
                                            Carts[i].SoLuong = Carts[i].SoLuong + 1;
                                            Carts[i].Tong = Carts[i].Tong + Carts[i].Gia;
                                        }
                                        a = 1;
                                    }
                                }
                                if (a == 0) {
                                    Carts.push({
                                        Prime: response.prime,
                                        SoLuong: 1,
                                        Img: response.img,
                                        Gia: response.gia,
                                        Name: response.ten,
                                        Mau: response.mau,
                                        Kich: response.kich,
                                        Tong: 0 + response.gia,
                                        TrangThai: true,
                                    });
                                }
                            }
                            else {
                                Carts.push({
                                    Prime: response.prime,
                                    SoLuong: 1,
                                    Img: response.img,
                                    Gia: response.gia,
                                    Name: response.ten,
                                    Mau: response.mau,
                                    Kich: response.kich,
                                    Tong: 0 + response.gia,
                                    TrangThai: true,
                                });
                            }
                        }
                        localStorage.setItem('Carts', JSON.stringify(Carts));
                        var tongtien = 0;
                        for (var i = 0; i < Carts.length; i++) {
                            tongtien = tongtien + Carts[i].Tong;
                        }
                        localStorage.setItem('TT', tongtien)

                        let TT = localStorage.getItem('TT')
                        $('.total-checkout').html(TT);
                        /*window.location.href = "/Carts";*/
                        var tongSoLuong = 0;
                        var tongGia = 0;
                        var rows2 = "";
                        for (var i = 0; i < Carts.length; i++) {
                            var cart = Carts[i];
                            console.log(cart)
                            tongSoLuong = tongSoLuong + cart.SoLuong;
                            tongGia = tongGia + cart.Gia * cart.SoLuong;
                            rows2 +=
                                `
                                                                <div class="cart-sub">
                                                                    <img class="image-cart-sub" src="/Content/img/${cart.Img}" />
                                                                        <div>
                                                                            <span>${cart.Name}</span>
                                                                            <p>Số lượng : ${cart.SoLuong}</p>
                                                                            <button class="cart__close button-remove-cart-sub" data-id="${cart.prime}">
                                                                                Xóa
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                `
                        }
                        $('.cart-sub-render').html(rows2);
                        new CartController();
                        $('.conut-number').html(tongSoLuong);
                        $('.price').html(tongGia + "$");
                        $("#mini_cart_id").slideDown();
                        document.getElementById("get_blur").style.display = "block";
                    }
                })
            }


        });
        $('#paypal').off('click').on('click', function () {
            var check = true;
            var AddRess = $('#DC').val();
            var Phone = $('#SDT').val();
            var Email = $('#Gmail').val();
            var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
            if (AddRess == "") {
                $('#textAddress').html("Nhập địa chỉ");
                console.log("đã vào");
                check = false;
            }
            if (vnf_regex.test(Phone) == false) {
                $('#textPhone').html('Số điện thoại của bạn không đúng định dạng!');
                check = false;
            }
            if (Email == "") {
                $('#textEmail').html("Hãy nhập mail");
                check = false;
            }
            if (localStorage && check==true) {
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
                    var Carts = [];
                    var carts = JSON.parse(cartsDataAsJson);
                    var rows = "";
                    for (var i = 0; i < carts.length; i++) {
                        var cart = carts[i];
                        Carts.push(cart);
                    }
                }
                let Order = [];
                Order.push({
                    address: AddRess,
                    phone: Phone,
                    email: Email,
                });
                console.log(JSON.stringify(Carts))
                localStorage.setItem('Order', JSON.stringify(Order));
                $.ajax({
                    url: "/Order/PaymentWithPaypal",
                    data: { cartUser: JSON.stringify(Carts), addRess: AddRess, phone: Phone },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        if (response.status == true) {
                            var link = response.link;

                            /*alert("XONG!");*/
                            /*window.location.href = "";*/
                            location.href = link
                            /*window.location.href = "/Carts";*/
                        }

                        else {
                            window.location.href = "/Carts";
                        }
                    }
                })
            }
        });
        /*$('.make-order').off('click').on('click', function () {
            if (localStorage) {
                var TT = localStorage.getItem('TT')
                if (TT > 0) {
                    window.location.href = "/Order/Index";
                }
                else {
                    alert("Gio hang dang ko co gi ! ");
                }
            }
        });*/
        $('#make_order').off('click').on('click', function () {
            var check = true;
            var AddRess = $('#DC').val();
            var Phone = $('#SDT').val();
            var Email = $('#Gmail').val();
            var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
            if (AddRess == "") {
                $('#textAddress').html("Nhập địa chỉ");
                console.log("đã vào");
                check = false;
            }
            if (vnf_regex.test(Phone) == false) {
                $('#textPhone').html('Số điện thoại của bạn không đúng định dạng!');
                check = false;
            }
            if (Email == "") {
                $('#textEmail').html("Hãy nhập mail");
                check = false;
            }
            if (localStorage && check==true) {
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
                    var Carts = [];
                    var carts = JSON.parse(cartsDataAsJson);
                    var rows = "";
                    for (var i = 0; i < carts.length; i++) {
                        var cart = carts[i];
                        Carts.push(cart);
                    }
                }
                $.ajax({
                    url: "/Order/MakeOrder",
                    data: { cartUser: JSON.stringify(Carts), addRess: AddRess, phone: Phone },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        if (response.status == true) {
                            Carts = [];
                            localStorage.setItem('Carts', JSON.stringify(Carts));
                            localStorage.setItem('TT', 0);
                            alert("XONG!");
                            window.location.href = "/Events/Index";
                        }
                    }
                })
            }

        });
        $('.cart__close').off('click').on('click', function () {
            var tongtien = 0;
            var rows = "";
            var click = $(this);
            var Id = click.data('id');
            let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
            if (Carts.length > 0) {
                for (var i = 0; i < Carts.length; i++) {
                    if (Id == Carts[i].Prime) {
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
                                    rows += `<tr>
                                <td class="product__cart__item">
                                    <div class="product__cart__item__pic">
                                        <img src="/Content/img/${cart.img}" alt="">
                                    </div>
                                    <div class="product__cart__item__text">
                                        <h6>${cart.name}</h6>
                                        <h5>€${cart.gia}</h5>
                                        Size: ${cart.kich}
                                         <br>
                                        Color: ${cart.mau}
                                        <br>
                                    </div>
                                </td>
                                <td class="quantity__item">
                                    <div class="quantity">
                                        <div class="pro-qty-2">
                                            <input class="change-price" data-id="${cart.prime}" value="${cart.soLuong}" type="number" min="1" max="10">
                                        </div>
                                    </div>
                                </td>
                                <td class="cart__price" id="total-price-${cart.prime}">$ ${cart.tong}</td>
                                <td class="cart__close" data-id="${cart.prime}"><i class="fa fa-close"></i></td>
                            </tr>
                                        `
                                }
                                $('.js__cart-content').html(rows);
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
                new CartController()
            }
        });
        $('#checkout').off('click').on('click', function () {
            console.log("ok");
            if (localStorage) {
                var cartsDataAsJson = localStorage.getItem("Carts")
                var TT = localStorage.getItem('TT')
                var carts = JSON.parse(cartsDataAsJson);
                var rows = "";
                for (var i = 0; i < carts.length; i++) {
                    var cart = carts[i];
                    if (cart.TrangThai == true) {
                        rows += `
                        <li>${cart.soLuong} - ${cart.name} <span>$ ${cart.gia}</span></li>
                    `
                    }
                }
                $('.checkout__total__products').html(rows);
                console.log("okokok");
                if (TT > 0) {
                    window.location.href = "/Order/Index";

                }
                else {
                    alert("Gio hang dang ko co gi ! ");
                }
            }
        });
    }
    allPrice() {
        var TT = localStorage.getItem('TT')
        $('.all-money').html(TT);
        $('.total-checkout').html(TT);
    }
    renderOrderDetailContent() {
        if (localStorage) {
            var TT = localStorage.getItem('TT')
            $('.tongphu').html(TT);
            $('.tongchinh').html(TT);
            var cartsDataAsJson = localStorage.getItem("Carts")
            var carts = JSON.parse(cartsDataAsJson);
            if (cartsDataAsJson) {
                var rows = "";
                for (var i = 0; i < carts.length; i++) {
                    var cart = carts[i];
                    if (cart.TrangThai == true) {
                        rows += `
                        <li>${cart.SoLuong}   <span>$ ${cart.Gia}</span> <span style="margin-right: 18%">${cart.Name}</span></li>
                    `
                    }
                }
                $('.checkout__total__products').html(rows);
            }
        }
    }
    renderCartsContent() {
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
                console.log(carts);
                $.ajax({
                    url: "/Carts/RemakeRender",
                    data: { cartTemp: JSON.stringify(carts) },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        if (response.status == true) {
                            console.log(response.list);
                            carts = response.list;
                            var rows = "";
                            var rows2 = "";
                            var tongtien = 0;
                            for (var i = 0; i < carts.length; i++) {
                                var cart = carts[i];
                                if (cart.TrangThai == false) {
                                    rows +=
                                        `
                        <tr>
                                <td class="product__cart__item">
                                    <div class="product__cart__item__pic">
                                        <img style="height: 110px;width: 90px; " src="/Content/img/${cart.img}" alt="">
                                    </div>
                                    <div class="product__cart__item__text">
                                        <h6>${cart.name}</h6>
                                        <h5>€${cart.gia}</h5>
                                        Size: ${cart.kich}
                                         -
                                        Color: ${cart.mau}
                                        <br>
                                         <h5 class="mess_${cart.prime}" style="color:red">da het hang</h5>
                                    </div>
                                </td>
                                <td class="quantity__item">
                                    <div class="quantity">
                                        <div class="QUANTITY">
                                                <input class="change-price" id="key_${cart.prime}" data-id="${cart.prime}" value="${cart.soLuong}" type="number" min="1" max="10">
                                        </div>
                                    </div>
                                </td>
                                <td class="cart__price" id="total-price-${cart.prime}">$ ${cart.tong}</td>
                                <td class="cart__close" data-id="${cart.prime}"><i class="fa fa-close"></i></td>
                            </tr>
                    `
                                    rows2 +=
                                        `
                                                                <div class="cart-sub">
                                                                    <img class="image-cart-sub" src="/Content/img/${cart.img}" />
                                                                        <div>
                                                                            <span>${cart.name}</span>
                                                                            <p>Số lượng : ${cart.soLuong}</p>
                                                                            <button class="cart__close button-remove-cart-sub" data-id="${cart.prime}">
                                                                                Xóa
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                               
                                                            `
                                }
                                else {
                                    tongtien = tongtien + cart.tong;
                                    rows += `
                        <tr>
                                <td class="product__cart__item">
                                    <div class="product__cart__item__pic">
                                        <img style="height: 110px;width: 90px; " src="/Content/img/${cart.img}" alt="">
                                    </div>
                                    <div class="product__cart__item__text">
                                        <h6>${cart.name}</h6>
                                        <h5>€${cart.gia}</h5>
                                        Size: ${cart.kich}
                                         -
                                        Color: ${cart.mau}
                                        <br>
                                         <h5 class="mess_${cart.prime}" style="color:red"></h5>
                                    </div>
                                </td>
                                <td class="quantity__item">
                                    <div class="quantity">
                                        <div class="QUANTITY">
                                                <input class="change-price" data-id="${cart.prime}" value="${cart.soLuong}" type="number" min="1" max="10">
                                        </div>
                                    </div>
                                </td>
                                <td class="cart__price" id="total-price-${cart.prime}">$ ${cart.tong}</td>
                                <td class="cart__close" data-id="${cart.prime}"><i class="fa fa-close"></i></td>
                            </tr>
                    `
                                    rows2 +=
                                        `
                                                                <div class="cart-sub">
                                                                    <img class="image-cart-sub" src="/Content/img/${cart.img}" />
                                                                        <div>
                                                                            <span>${cart.name}</span>
                                                                            <p>Số lượng : ${cart.soLuong}</p>
                                                                            <button class="cart__close button-remove-cart-sub" data-id="${cart.prime}">
                                                                                Xóa
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                
                                                            `
                                }
                            }
                            localStorage.setItem('TT', tongtien);
                            $('.total-checkout').html(tongtien);
                            $('.js__cart-content').html(rows);
                            console.log(rows2)
                            $('.cart-sub-render').html(rows2);
                            $('.change-price').on('change', function () {
                                var click = $(this);
                                var Quanity = click.val();
                                var Id = click.data('id');
                                $.ajax({
                                    url: "/Carts/ChangePrice",
                                    data: { id: Id, sl: Quanity },
                                    dataType: "json",
                                    type: "POST",
                                    success: function (response) {
                                        if (response.status == true) {
                                            let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                                            if (Carts.length > 0) {
                                                for (var i = 0; i < Carts.length; i++) {
                                                    if (Id == Carts[i].Prime) {
                                                        Carts[i].SoLuong = Number(Quanity);
                                                        Carts[i].Tong = Carts[i].SoLuong * Carts[i].Gia
                                                        $(`#total-price-${Id}`).html("$" + Carts[i].Tong);
                                                        localStorage.setItem('Carts', JSON.stringify(Carts));
                                                    }
                                                }
                                                var tongtien = 0;
                                                for (var i = 0; i < Carts.length; i++) {
                                                    if (Carts[i].TrangThai == true) {
                                                        tongtien = tongtien + Carts[i].Tong;
                                                    }
                                                }
                                                localStorage.setItem('TT', tongtien)
                                                localStorage.setItem('Carts', JSON.stringify(Carts));
                                                let TT = localStorage.getItem('TT')
                                                $('.total-checkout').html(tongtien)
                                                localStorage.setItem('Carts', JSON.stringify(Carts));
                                                var row = "";
                                                $(`.mess_${Id}`).html(row);
                                            }
                                        }
                                        else {
                                            var row = " San pham ko the vuot qua " + response.max;
                                            if (response.max == 0) {
                                                console.log("ok");
                                                row = "da het hang";
                                                let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                                                if (Carts.length > 0) {
                                                    for (var i = 0; i < Carts.length; i++) {
                                                        if (Id == Carts[i].Prime) {
                                                            Carts[i].TrangThai = false;
                                                            Carts[i].SoLuong = 1;
                                                            Carts[i].Tong = Carts[i].SoLuong * Carts[i].Gia
                                                            $(`#total-price-${Id}`).html(Carts[i].Tong);
                                                            localStorage.setItem('Carts', JSON.stringify(Carts));
                                                            console.log("ok");
                                                        }
                                                    }
                                                    var tongtien = 0;
                                                    for (var i = 0; i < Carts.length; i++) {
                                                        if (Carts[i].TrangThai == true) {
                                                            tongtien = tongtien + Carts[i].Tong;
                                                        }
                                                    }
                                                    localStorage.setItem('TT', tongtien)
                                                    localStorage.setItem('Carts', JSON.stringify(Carts));
                                                    let TT = localStorage.getItem('TT')
                                                    $('.total-checkout').html(tongtien)
                                                    localStorage.setItem('Carts', JSON.stringify(Carts));
                                                    $(`.mess_${Id}`).html(row);
                                                }

                                            }
                                            $(`.mess_${Id}`).html(row);
                                        }
                                    }
                                })
                            });
                            $('.cart__close').off('click').on('click', function () {
                                var tongtien = 0;
                                var rows = "";
                                var rows2 = "";
                                var click = $(this);
                                var Id = click.data('id');
                                let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                                if (Carts.length > 0) {
                                    for (var i = 0; i < Carts.length; i++) {
                                        if (Id == Carts[i].Prime) {
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
                                                        rows += `<tr>
                                <td class="product__cart__item">
                                    <div class="product__cart__item__pic">
                                        <img src="/Content/img/${cart.img}" alt="">
                                    </div>
                                    <div class="product__cart__item__text">
                                        <h6>${cart.name}</h6>
                                        <h5>€${cart.gia}</h5>
                                        Size: ${cart.kich}
                                         <br>
                                        Color: ${cart.mau}
                                        <br>
                                    </div>
                                </td>
                                <td class="quantity__item">
                                    <div class="quantity">
                                        <div class="pro-qty-2">
                                            <input class="change-price" data-id="${cart.prime}" value="${cart.soLuong}" type="number" min="1" max="10">
                                        </div>
                                    </div>
                                </td>
                                <td class="cart__price" id="total-price-${cart.prime}">$ ${cart.tong}</td>
                                <td class="cart__close" data-id="${cart.prime}"><i class="fa fa-close"></i></td>
                            </tr>
                                        `
                                                        rows2 +=
                                                            `
                                                                <div class="cart-sub">
                                                                    <img class="image-cart-sub" src="~/Content/img/${cart.img}" />
                                                                        <div>
                                                                            <span>${cart.name}</span>
                                                                            <p>Số lượng : ${cart.soLuong}</p>
                                                                            <button class="cart__close button-remove-cart-sub" data-id="${cart.prime}">
                                                                                Xóa
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                
                                                            `
                                                    }
                                                    $('.js__cart-content').html(rows);
                                                    $('.cart-sub-render').html(rows2);
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
                                    new CartController()
                                }
                            });
                        }
                    }
                })
            }
        }

    }
}
new CartController();
