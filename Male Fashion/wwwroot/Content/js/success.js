class success {
    constructor() {
        $(document).ready(function () {
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
                for (var i = 0; i < carts.length; i++) {
                    var cart = carts[i];
                    Carts.push(cart);
                }
            }
            var Orders = [];
            let orderDataAsJson = localStorage.getItem('Order');
            if (orderDataAsJson) {
                var orders = JSON.parse(orderDataAsJson);
                for (var i = 0; i < orders.length; i++) {
                    var order = orders[i];
                    Orders.push(order);
                }
            }
            $.ajax({
                url: "/Order/MakeOrderPayPal",
                data: { cartUser: JSON.stringify(Carts), addRess: Orders[0].address, phone: Orders[0].phone },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        console.log("FUCK")
                        Carts = [];
                        Orders = [];
                        localStorage.setItem('Carts', JSON.stringify(Carts));
                        localStorage.setItem('Order', JSON.stringify(Orders));
                        localStorage.setItem('TT', 0);
                        
                    }
                }
            })
            alert("Thanks !");
            window.location.href = "/Home/Index";
        });
    }
}
new success();