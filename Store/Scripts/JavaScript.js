
function CategoryLoadTo(Id) {
    $(function () {
        $.ajax({
            url: '/Home/GetCategory',
            type: "POST",
            success: function (msg) {
                //id_numbers = msg.split('|');
                var result = JSON.parse(msg);
                
                
                for (var a = 0; a < result.length; a++) {
                    $("#" + Id).append("<a href='#' class='nav-item'>" + result[a] + "</a>");
                }
                
                
            }
        })
    });
}