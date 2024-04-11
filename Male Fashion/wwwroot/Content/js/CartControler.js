const isLocalhost = window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1';
var url = "";
if (isLocalhost) {
    url = "https://localhost:7179/"
}
else {
    $.ajax({
        url: "/Home/GetString",
        type: "GET",
        success: function (result) {
            console.log("result", result);
            url = result + "/"
        },
        error: function (error) {
            console.log(error);
        }
    });

}
class CartController {
    constructor() {

        this.renderCartsContent();
        this.allPrice();
        this.renderOrderDetailContent();
        $(".button-remove-cart-sub").on('click', function () {
            console.log("YOLO")
            var tongtienremove = 0;
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
                new CartController()
            }
        });
        $(".product__color__select label, .shop__sidebar__size label").on('click', function () {
            $(".product__color__select label, .shop__sidebar__size label ").removeClass('active');
            $(this).addClass('active');
        });
        $(".product__details__option__size label").on('click', function () {
            $(".product__details__option__size label").removeClass('active');
            $(this).addClass('active');
            var rows = $('.product__details__option__size label.active').data('id');
        });
        $(".product__details__option__size2 label").on('click', function () {
            $(".product__details__option__size2 label").removeClass('active');
            $(this).addClass('active');
        });
        $('.size').on('click', function () {
            var click = $(this);
            var size = click.data('id');
            var id = click.data('id2');
            $.ajax({
                url: "/Events/Checkbysize",
                data: {
                    size: size,
                    id: id
                },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        var rows = "<span>Màu :</span>";
                        for (var i = 0; i < response.arr.length; i++) {
                            rows += `
                                                <label for="${response.arr[i]}" class="colour" data-id="${response.arr[i]}"">
                                                    ${response.arr[i]}
                                                    <input name="mau" type="radio" id="${response.arr[i]}">
                                                </label>
                                        
                                    `
                        }
                        $('#ngu').html(rows);
                        $(".product__details__option__size2 label").on('click', function () {
                            $(".product__details__option__size2 label").removeClass('active');
                            $(this).addClass('active');
                            var size = $('.product__details__option__size label.active').data('id');
                            var colour = $('.product__details__option__size2 label.active').data('id');
                            var Id = $('.button_add_to_cart_new').data('id');
                            $.ajax({
                                url: "/Events/CheckProduct",
                                data: { id: Id, color: colour, size: size },
                                dataType: "json",
                                type: "POST",
                                success: function (response) {
                                    if (response.status == false) {
                                        $('.button_add_to_cart_new').attr("disabled", true);
                                        $('.primary-btn').html("Đã hết hàng")
                                    }
                                    else {
                                        $('.button_add_to_cart_new').attr("disabled", false);
                                        $('.primary-btn').html("THÊM VÀO GIỎ")
                                    }
                                }
                            })
                        });
                    }
                }
            })
        });
        $('.button_abc').on('click', function () {
            var click = $(this);
            var Id = click.data('id');
            var size = $('.product__details__option__size label.active').data('id');
            var colour = $('.product__details__option__size2 label.active').data('id');
            if (size == null || colour == null) {
                alert("Hình như bạn quên chọn size hoặc Màu !");
            }
        });
        $('#icolour').on('change', function () {
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
                        $('.primary-btn').html("THÊM VÀO GIỎ")

                    }
                }
            })
        });
        $('#isize').on('change', function () {
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
                        $('.primary-btn').html("THÊM VÀO GIỎ")
                    }
                }
            })
        });
        $('.change-price').on('change', function () {
            var click = $(this);
            var Quanity = click.val();
            var Id = click.data('id');
            $.ajax({
                url: "/Carts/ChangePrice",
                data: { id: Id, quantity: Quanity },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                        if (Carts.length > 0) {
                            for (var i = 0; i < Carts.length; i++) {
                                if (Id == Carts[i].id) {
                                    Carts[i].quantity = Quanity;
                                    Carts[i].totalPrice = Carts[i].quantity * Carts[i].price
                                    $(`#total-price-${Id}`).html("$" + Carts[i].totalPrice);
                                    localStorage.setItem('Carts', JSON.stringify(Carts));
                                }
                            }
                            var tongtien = 0;
                            for (var i = 0; i < Carts.length; i++) {
                                if (Carts[i].Status == true) {
                                    tongtien = tongtien + Carts[i].totalPrice;
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
                        var row = " Product can not reach " + response.max;
                        if (response.max == 0) {
                            row = "Out of stock";
                            let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                            if (Carts.length > 0) {
                                for (var i = 0; i < Carts.length; i++) {
                                    if (Id == Carts[i].id) {
                                        Carts[i].Status = false;
                                        Carts[i].quantity = 1;
                                        Carts[i].totalPrice = Carts[i].quantity * Carts[i].price
                                        $(`#total-price-${Id}`).html(Carts[i].totalPrice);
                                        localStorage.setItem('Carts', JSON.stringify(Carts));
                                    }
                                }
                                var tongtien = 0;
                                for (var i = 0; i < Carts.length; i++) {
                                    if (Carts[i].Status == true) {
                                        tongtien = tongtien + Carts[i].totalPrice;
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
            console.log("Thêm ọk")
            let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
            var click = $(this);
            var Id = click.data('id');
            var size = $('.product__details__option__size label.active').data('id');
            var color = $('.product__details__option__size2 label.active').data('id');
            if (size == null || color == null) {
                alert("Hình như bạn quên chọn size và Màu !");
            }
            else {
                $.ajax({
                    url: "/Carts/AddCart",
                    data: {
                        id: Id,
                        size: size,
                        color: color
                    },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        if (response.status == true) {
                            var a = 0;
                            let b = String(1);
                            if (Carts.length > 0) {
                                for (var i = 0; i < Carts.length; i++) {
                                    if (Carts[i].id == response.id) {
                                        if (Carts[i].quantity < response.quantity) {
                                            Carts[i].quantity = Carts[i].quantity + 1;
                                            Carts[i].totalPrice = Carts[i].totalPrice + Carts[i].price;
                                        }
                                        a = 1;
                                    }
                                }
                                if (a == 0) {
                                    Carts.push({
                                        id: response.id,
                                        quantity: 1,
                                        imagePath: response.imagePath,
                                        price: response.price,
                                        name: response.name,
                                        color: response.color,
                                        size: response.size,
                                        totalPrice: 0 + response.price,
                                        Status: true,
                                    });
                                }
                            }
                            else {
                                Carts.push({
                                    id: response.id,
                                    quantity: 1,
                                    imagePath: response.imagePath,
                                    price: response.price,
                                    name: response.name,
                                    color: response.color,
                                    size: response.size,
                                    totalPrice: 0 + response.price,
                                    Status: true,
                                });
                            }
                        }
                        localStorage.setItem('Carts', JSON.stringify(Carts));
                        var tongtien = 0;
                        for (var i = 0; i < Carts.length; i++) {
                            tongtien = tongtien + Carts[i].totalPrice;
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
                            tongSoLuong = tongSoLuong + cart.quantity;
                            tongGia = tongGia + cart.price * cart.quantity;
                            rows2 +=
                                `
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
            var Name = $('#Name').val();
            var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
            if (AddRess == "") {
                $('#textAddress').html("Nhập địa chỉ");
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
            if (localStorage && check == true) {
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
                    name: Name
                });
                localStorage.setItem('Order', JSON.stringify(Order));
                $.ajax({
                    url: "/Order/PaymentWithPaypal",
                    data: { cartUser: JSON.stringify(Carts), addRess: AddRess, phone: Phone, name: Name },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        if (response.status == true) {
                            var link = response.link;
                            location.href = link
                        }
                        else {
                            window.location.href = "/Carts";
                        }
                    }
                })
            }
        });
        $('#vnpay').off('click').on('click', function () {
            var check = true;
            var AddRess = $('#DC').val();
            var Phone = $('#SDT').val();
            var Email = $('#Gmail').val();
            var Name = $('#Name').val();
            var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
            if (AddRess == "") {
                $('#textAddress').html("Nhập địa chỉ");
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
            if (localStorage && check == true) {
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
                    name: Name
                });
                localStorage.setItem('Order', JSON.stringify(Order));
                $.ajax({
                    url: "/Order/Payment",
                    data: { cartUser: JSON.stringify(Carts), addRess: AddRess, phone: Phone, name: Name },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        if (response.status == true) {
                            var link = response.link;
                            location.href = link
                        }
                        else {
                            window.location.href = "/Carts";
                        }
                    }
                })
            }
        });
        $('#make_order').off('click').on('click', function () {
            var check = true;
            var AddRess = $('#DC').val();
            var Phone = $('#SDT').val();
            var Email = $('#Gmail').val();
            var Name = $('#Name').val();
            var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
            if (AddRess == "") {
                $('#textAddress').html("Nhập địa chỉ");
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
            if (localStorage && check == true) {
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
                    name: Name
                });
                localStorage.setItem('Order', JSON.stringify(Order));
                $.ajax({
                    url: "/Order/MakeOrder",
                    data: { cartUser: JSON.stringify(Carts), addRess: AddRess, phone: Phone, name: Name, email: Email },
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
                                        Màu : ${cart.mau}
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
            if (localStorage) {
                var cartsDataAsJson = localStorage.getItem("Carts")
                var TT = localStorage.getItem('TT')
                var carts = JSON.parse(cartsDataAsJson);
                var rows = "";
                for (var i = 0; i < carts.length; i++) {
                    var cart = carts[i];
                    if (cart.Status == true) {
                        rows += `
                        <li> <div style="background: url(${url}user_content/${cart.imagePath}) no-repeat center center;
    background-size: contain; display: inline-block;
    height: 64px;
    width: 64px; "> </div>
<span class="span-for-total"> ${cart.quantity}</span> ${cart.name} <span> ${cart.price}</span></li>
                    `
                    }
                }
                $('.checkout__total__products').html(rows);
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
                    if (cart.Status == true) {
                        rows += `
                        <li> <div style="background: url(${url}user_content/${cart.imagePath}) no-repeat center center;
    background-size: contain; display: inline-block;
    height: 64px;
    width: 64px; ">
</div>
<span class="span-for-total"> ${cart.quantity}</span>   <span> ${cart.price}</span> <span style="margin-right: 2%">${cart.name}</span></li>
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
                $.ajax({
                    url: "/Carts/RemakeRender",
                    data: { cartTemp: JSON.stringify(carts) },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        if (response.status == true) {
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
                                        <img style="height: 110px;width: 90px; " src="${url}user_content/${cart.imagePath}" alt="">
                                    </div>
                                    <div class="product__cart__item__text">
                                        <h6>${cart.name}</h6>
                                        <h5>€${cart.price}</h5>
                                        Size: ${cart.size}
                                         -
                                        Màu: ${cart.color}
                                        <br>
                                         <h5 class="mess_${cart.id}" style="color:red">da het hang</h5>
                                    </div>
                                </td>
                                <td class="quantity__item">
                                    <div class="quantity">
                                        <div class="QUANTITY">
                                                <input class="change-price" id="key_${cart.id}" data-id="${cart.id}" value="${cart.quantity}" type="number" min="1" max="10">
                                        </div>
                                    </div>
                                </td>
                                <td class="cart__price" id="total-price-${cart.id}">$ ${cart.totalPrice}</td>
                                <td class="cart__close" data-id="${cart.id}"><i class="fa fa-close"></i></td>
                            </tr>
                    `
                                    rows2 +=
                                        `
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
                                else {
                                    tongtien = tongtien + cart.totalPrice;
                                    rows += `
                        <tr>
                                <td class="product__cart__item">
                                    <div class="product__cart__item__pic">
                                        <img style="height: 110px;width: 90px; " src="${url}user_content/${cart.imagePath}" alt="">
                                    </div>
                                    <div class="product__cart__item__text">
                                        <h6>${cart.name}</h6>
                                        <h5>€${cart.price}</h5>
                                        Size: ${cart.size}
                                         -
                                        Màu: ${cart.color}
                                        <br>
                                         <h5 class="mess_${cart.id}" style="color:red"></h5>
                                    </div>
                                </td>
                                <td class="quantity__item">
                                    <div class="quantity">
                                        <div class="QUANTITY">
                                                <input class="change-price" data-id="${cart.id}" value="${cart.quantity}" type="number" min="1" max="10">
                                        </div>
                                    </div>
                                </td>
                                <td class="cart__price" id="total-price-${cart.id}">$ ${cart.totalPrice}</td>
                                <td class="cart__close" data-id="${cart.id}"><i class="fa fa-close"></i></td>
                            </tr>
                    `
                                    rows2 +=
                                        `
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
                            }
                            localStorage.setItem('TT', tongtien);
                            $('.total-checkout').html(tongtien);
                            $('.js__cart-content').html(rows);
                            $('.cart-sub-render').html(rows2);
                            $('.change-price').on('change', function () {
                                var click = $(this);
                                var Quanity = click.val();
                                var Id = click.data('id');
                                $.ajax({
                                    url: "/Carts/ChangePrice",
                                    data: { id: Id, quantity: Quanity },
                                    dataType: "json",
                                    type: "POST",
                                    success: function (response) {
                                        if (response.status == true) {
                                            let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                                            if (Carts.length > 0) {
                                                for (var i = 0; i < Carts.length; i++) {
                                                    if (Id == Carts[i].id) {
                                                        Carts[i].quantity = Quanity;
                                                        Carts[i].totalPrice = Carts[i].quantity * Carts[i].price
                                                        $(`#total-price-${Id}`).html("$" + Carts[i].totalPrice);
                                                        localStorage.setItem('Carts', JSON.stringify(Carts));
                                                    }
                                                }
                                                var tongtien = 0;
                                                for (var i = 0; i < Carts.length; i++) {
                                                    if (Carts[i].Status == true) {
                                                        tongtien = tongtien + Carts[i].totalPrice;
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
                                            var row = " Product can not reach " + response.max;
                                            if (response.max == 0) {
                                                row = "Out of stock";
                                                let Carts = localStorage.getItem('Carts') ? JSON.parse(localStorage.getItem('Carts')) : [];
                                                if (Carts.length > 0) {
                                                    for (var i = 0; i < Carts.length; i++) {
                                                        if (Id == Carts[i].id) {
                                                            Carts[i].Status = false;
                                                            Carts[i].quantity = 1;
                                                            Carts[i].totalPrice = Carts[i].quantity * Carts[i].price
                                                            $(`#total-price-${Id}`).html(Carts[i].totalPrice);
                                                            localStorage.setItem('Carts', JSON.stringify(Carts));
                                                        }
                                                    }
                                                    var tongtien = 0;
                                                    for (var i = 0; i < Carts.length; i++) {
                                                        if (Carts[i].Status == true) {
                                                            tongtien = tongtien + Carts[i].totalPrice;
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
                                                        tongtien = tongtien + cart.totalPrice;
                                                        rows += `<tr>
                                <td class="product__cart__item">
                                    <div class="product__cart__item__pic">
                                        <img src="${url}user_content/${cart.imagePath}" alt="">
                                    </div>
                                    <div class="product__cart__item__text">
                                        <h6>${cart.name}</h6>
                                        <h5>€${cart.price}</h5>
                                        Size: ${cart.size}
                                         <br>
                                        Color: ${cart.color}
                                        <br>
                                    </div>
                                </td>
                                <td class="quantity__item">
                                    <div class="quantity">
                                        <div class="pro-qty-2">
                                            <input class="change-price" data-id="${cart.id}" value="${cart.quantity}" type="number" min="1" max="10">
                                        </div>
                                    </div>
                                </td>
                                <td class="cart__price" id="total-price-${cart.id}">$ ${cart.totalPrice}</td>
                                <td class="cart__close" data-id="${cart.id}"><i class="fa fa-close"></i></td>
                            </tr>
                                        `
                                                        rows2 +=
                                                            `
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
