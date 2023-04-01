class PhanTrangLoaiSanPham {
    constructor() {
        $('.pagechange').on('click', function () {
            console.log("heheheheh");
            var click = $(this);
            $.ajax({
                url: "https://localhost:44314/api/LoaiSanPhams/daubuoi",
                data: "",
                dataType: "json",
                method: "GET",
                contentType: "application/json",
                success: function (response) {

                    var pageSize = 1;
                    var pageCout = click.data('id');
                    console.log(pageCout);
                    var dem = 0;
                    var dempage = 0;
                    var all = 0;
                    console.log(response);
                    console.log("ok");
                    var rows = "";
                    for (var i = 0; i < response.length; i++) {
                        var LSP = response[i];

                        dem = dem + 1;
                        rows += `
                    <tr>
                        <td>
                            ${LSP.IDLoaiSanPham}
                        </td>
                        <td>
                            ${LSP.Ten}
                        </td>
                        <td>
                            ${LSP.TrangThai}
                        </td>
                            
                    </tr>
                    `
                        if (dem == pageSize) {
                            dem = 0;
                            dempage = dempage + 1;
                            var rows2 = rows;
                            rows = "";
                            if (pageCout == dempage) {
                                $('.js__Getall').html(rows2);
                                break
                            }
                        }
                    }
                    for (var i = 0; i < response.length; i++) {
                        all = all + 1;
                    }
                    var tongsoPage;
                    if (all % pageSize == 0) {
                        tongsoPage = all / pageSize;
                        console.log("tong so page  = " + tongsoPage);
                    }
                    else {
                        var tinhlaydu = all % pageSize;
                        var tinh = all / pageSize;
                        tongsoPage = tinhlaydu + 1;
                        if (tinh > tinhlaydu) {
                            console.log("tong so page 2  = " + tongsoPage);
                        }
                    }

                    if (rows != "") {
                        $('.js__Getall').html(rows);
                    }
                    if (tongsoPage > 2) {
                        rows = `
<div style="text-align:center;height:50px;padding-left:42%">
                        <div style="margin: 10px; float: left;color:deeppink " class="pagechange" data-id="1">
                                    1
                        </div>
                        <div style="margin: 10px; float: left;color:deeppink " class="pagechange" data-id="2">
                                    2
                        </div>
                        <div style="margin: 10px; float: left;color:deeppink " class="pagechange" data-id="${tongsoPage}">
                                    >>
                        </div>
</div>
`
                    }
                    else if (tongsoPage > 1) {
                        rows = `
<div style="text-align:center;height:50px;padding-left:42%">
                        <div style="margin: 10px; float: left;color:deeppink " class="pagechange" data-id="1">
                                    1
                        </div>
                        <div style="margin: 10px; float: left;color:deeppink " class="pagechange" data-id="2">
                                    2
                        </div>
</div>
`
                    }
                    else if (tongsoPage == 1) {
                        rows = `
<div style="text-align:center;height:50px;padding-left:42%">
                        <div style="margin: 10px; float: left;color:deeppink " class="pagechange" data-id="1">
                                    1
                        </div>
                        
</div>
`
                    }
                    console.log("lala");
                    $('.js_page').html(rows);
                }
            })
        });
    }

}
new PhanTrangLoaiSanPham();