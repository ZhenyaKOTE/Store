$(document).ready(function () {

    $.ajax({
        type: "POST",
        url: '@Url.Action("GetJ", "Account")',
        data: result,
        success: function (result) {
            console.Log("Alo");
        },
        error: function (xhr, status, p3) {
            alert(xhr.responseText);
        }
        
    
    });


});
