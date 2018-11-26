$(function () {

    //console.log("Allalfjskdfjksdl");

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

        inputFile.on('change', function ()
        {
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

        function uploadFileCropper(fileName)
        {
            //console.log("Upload file -----> ", fileName);
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
                    
                    $canvas.cropper('destroy').cropper();
                }
                img.src = e.target.result;
            }
            reader.readAsDataURL(fileName);
        }
    });
});