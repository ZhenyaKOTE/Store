﻿
function CategoryLoadTo(Id, UrlByCategory) {
    let u = UrlByCategory;
    $(function () {
        $.ajax({
            url: u,
            type: "POST",
            success: function (msg) {
                console.log(u);
                let result = JSON.parse(msg);
                console.log(result);
                for (var a = 0; a < result.length; a++)
                {
                    $("#" + Id).append("<a href=" + result[a].UrlToMove + " class='list-group-item list-group-item-action text-center'>" + result[a].Name + "</li>");
                }
            }
        })
    });
}

function LoadNavigation(Url) {
    let u = Url;
    $(function () {
        $.ajax({
            url: u,
            type: "POST",
            success: function (msg) {
                $("#Navigation").replaceWith(msg);
                
            }
        })
    });
}
//function GetFilters(Id, Url) {
//    let u = Url;
//    $(function () {
//        $.ajax({
//            url: u,
//            type: "GET",
//            success: function (msg) {
//                let result1 = JSON.parse(msg);
//                for (let i = 0; i < result1.length; i++)
//                {
//                    let str =
//                        "<div class='custom-control custom-checkbox'>" +
//                        "<input type='checkbox' class='custom-control-input' id=" + result1[i].Id + ">" +
//                            "<label class='custom-control-label' for=" + i +">" + result1[i].Value +"</label>" +
//                        "</div>";
//                    $("#" + Id).append(str);
//                }
//            }
//        })
//    });
//}





