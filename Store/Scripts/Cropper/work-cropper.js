
$(function () {

    var IsCroping = false;

    $("#imageContainerPlus").on('click', function () {
        var inputFile = $('<input/>')
            .attr('type', 'file')
            .attr('name', 'img_file')
            .attr('id', 'img_file')
            .attr('class', 'hide');


        var fileUploadContainer = $("#fileUploadContainer");

        fileUploadContainer.html("");

        fileUploadContainer.html(inputFile);

        inputFile.click();

        inputFile.on('change', function () {
            if (this.files && this.files[0]) {
                if (this.files[0].type.match(/^image\//)) {
                    uploadFileCropper(this.files[0]);
                }
                else {
                    alert("invalid image type");
                }
            }
            else {
                alert("Upload File please");
            }
        });

        function uploadFileCropper(fileName) {

            let $canvas = $("#canvas"),
                context = $canvas.get(0).getContext('2d');

            let reader = new FileReader();
            reader.onload = function (e) {
                let img = new Image();
                img.onload = function () {
                    context.canvas.width = img.width;
                    context.canvas.height = img.height;

                    document.body.classList.toggle("open");
                    $(".containerCrop").show();

                    context.drawImage(img, 0, 0);
                    //$MyCropper = $canvas;

                    $canvas.cropper('destroy').cropper();
                    IsCroping = true;
                }
                img.src = e.target.result;
            }
            reader.readAsDataURL(fileName);


        }

        $("#cropperClose").click(function () {


            if (IsCroping == true) {
                let $canvas = $("#canvas"),
                    context = $canvas.get(0).getContext('2d');

                console.log($canvas);
                document.body.classList.remove("open");
                $(".containerCrop").hide();
                IsCroping = false;
            }


        });


        $("#crop").click(function () {

            //console.log(myImage.replace(/^data:image\/(png|jpg);base64,/, ""));
            if (IsCroping == true) {

                let $canvas = $("#canvas"),
                    context = $canvas.get(0).getContext('2d');

                var myImage = $canvas.cropper('getCroppedCanvas').toDataURL("image/jpg");


                console.log(myImage);
                
                $.ajax({
                    type: 'POST',
                    url: 'http://localhost:9828/api/AccountApi/UploadImage',
                    data: '{"imageBase64": "' + myImage.replace(/^data:image\/(png|jpg);base64,/, "") + '"}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        alert(msg.responseText);
                    }
                });


                IsCroping = false;
            }
        })
    });
});