
function CategoryLoadTo(Id, UrlByCategory) {
    let u = UrlByCategory;
    $(function () {
        $.ajax({
            url: u,
            type: "POST",
            success: function (msg) {
                //id_numbers = msg.split('|');
                let result = JSON.parse(msg);

                for (var a = 0; a < result.length; a++) {
                    $("#" + Id).append("<a href='#' class='list-group-item list-group-item-action text-center'>" + result[a] + "</li>");
                }
            }
        })
    });
}

function GetFilters(Id, Url) {
    let u = Url;
    $(function () {
        $.ajax({
            url: u,
            type: "POST",
            success: function (msg) {
                let result1 = JSON.parse(msg);
                for (let i = 0; i < result1.length; i++)
                {
                    let str =
                        "<div class='custom-control custom-checkbox'>" +
                            "<input type='checkbox' class='custom-control-input' id="+ i +">" +
                            "<label class='custom-control-label' for=" + i +">" + result1[i].Value +"</label>" +
                        "</div>";
                    $("#" + Id).append(str);
                    //console.log(result1[i].Value);
                }

                //let str =
                //    "<div class='custom-control custom-checkbox'>" +
                //    "<input type='checkbox' class='custom-control-input' id='customCheck1'>" +
                //    "<label class='custom-control-label' for='customCheck1'>" + result1[i].Value + "</label>" +
                //    "</div>";
            }
        })
    });
}
