
function CategoryLoadTo(Id, Url) {
    var u = Url;
    $(function () {
        $.ajax({
            url: u,
            type: "POST",
            success: function (msg) {
                //id_numbers = msg.split('|');
                var result = JSON.parse(msg);
                      
                for (var a = 0; a < result.length; a++) {
                    $("#" + Id).append("<a href='#' class='list-group-item list-group-item-action text-center'>" + result[a] + "</li>");
                }
            }
        })
    });
}

function CreateCategory(Id, Url) {
    var u = Url;
    $(function () {
        $.ajax({
            url: u,
            type: "POST",
            success: function (msg) {
                //id_numbers = msg.split('|');
                var result = JSON.parse(msg);

                for (var a = 0; a < result.length; a++) {
                    $("#" + Id).append("<a href='#' class='list-group-item list-group-item-action text-center'>" + result[a] + "</li>");
                }
            }
        })
    });
}
