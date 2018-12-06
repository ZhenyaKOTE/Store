
$(function () {

    var IsCroping = false;
    let $MyCanvas = null;

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
                    $MyCanvas = $canvas;

                    $canvas.cropper('destroy').cropper();
                    IsCroping = true;
                }
                img.src = e.target.result;
            }
            reader.readAsDataURL(fileName);


        }

        $("#cropperClose").click(function ()
        {

            if (IsCroping == true)
            {
                //let $canvas = $("#canvas"),
                //    context = $canvas.get(0).getContext('2d');

                document.body.classList.remove("open");
                $(".containerCrop").hide();

                IsCroping = false;
            }

        });


        $("#crop").click(function () {

            if (IsCroping == true) {

                var myImage = $MyCanvas.cropper('getCroppedCanvas').toDataURL("image/jpg", 128, 128);


                console.log(myImage);

                $.ajax({
                    type: 'POST',
                    url: 'http://localhost:9828/api/AccountApi/UploadImage',
                    data: { "imgBase64": (myImage.replace(/^data:image\/(png|jpg);base64,/, "")) },
                    success: function (msg) {
                        console.log(msg.responseText);
                    }
                });


                IsCroping = false;
            }
        })

        $("#zoomMinus").click(function () {
            $MyCanvas.cropper('zoom', -0.1);
        })
        $("#zoomPlus").click(function () {
            $MyCanvas.cropper('zoom', 0.1);
        })
        $("#moveUp").click(function () {
            $MyCanvas.cropper('move', 0, 10);
        })
        $("#moveDown").click(function () {
            $MyCanvas.cropper('move', 0, -10);
        })
        $("#moveLeft").click(function () {
            $MyCanvas.cropper('move', -10, 0);
        })
        $("#moveRight").click(function () {
            $MyCanvas.cropper('move', 10, 0);
        })
        $("#rotateLeft").click(function () {
            $MyCanvas.cropper('rotate', 45);
        })
        $("#rotateRight").click(function () {
            $MyCanvas.cropper('rotate', -45);
        })
        $("#scaleX").click(function () {
            $MyCanvas.cropper('scaleX', X);
            X = X * -1;
        })
        $("#scaleY").click(function () {
            $MyCanvas.cropper('scaleY', Y);
            Y = Y * -1;
        })
        $("#dragModeMove").click(function () {
            $MyCanvas.cropper('setDragMode', 'move');
        })
        $("#dragModeMove").click(function () {
            $MyCanvas.cropper('dragModeCrop', 'crop');
        })

        //$("#cropperClose").click(function () {
        //    console.log("Close");
        //    $(".containerCrop").hide();
        //    $(".navbar").show();
        //})
    });
});