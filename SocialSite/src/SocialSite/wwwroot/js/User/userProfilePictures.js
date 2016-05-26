$(document).ready(function ()
{
    var uploadfiles = document.querySelector('#fileinput');

    uploadfiles.addEventListener('change', function () {
        var files = this.files;

        for (var i = 0; i < files.length; i++) {
            uploadFile(this.files[i]);
        }
    }, false);
});



function getPosition(el) {
    var xPos = 0;
    var yPos = 0;

    while (el) {
        if (el.tagName == "BODY") {
            // deal with browser quirks with body/window/document and page scroll
            var xScroll = el.scrollLeft || document.documentElement.scrollLeft;
            var yScroll = el.scrollTop || document.documentElement.scrollTop;

            xPos += (el.offsetLeft - xScroll + el.clientLeft);
            yPos += (el.offsetTop - yScroll + el.clientTop);
        } else {
            // for all other non-BODY elements
            xPos += (el.offsetLeft - el.scrollLeft + el.clientLeft);
            yPos += (el.offsetTop - el.scrollTop + el.clientTop);
        }

        el = el.offsetParent;
    }
    return {
        x: xPos,
        y: yPos
    };
}

function uploadFile(file) {
    var url = '/User/AddPicture';
    var xhr = new XMLHttpRequest();
    var fd = new FormData();
    xhr.open("Post", url, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            // Every thing ok, file uploaded
            console.log(xhr.responseText); // handle response.
        }
    };

    displayPictureInGalery(file);

    fd.append("file", file);
    xhr.send(fd);
}

function displayPictureInGalery(file) {
    var gallery = document.getElementById('galleryId');
    var img = document.createElement("img");
    img.file = file;

    gallery.appendChild(img);

    //addButtonToPictureInGalery(gallery, img);
    // Using FileReader to display the image content
    var reader = new FileReader();
    reader.onload = (function (aImg) { return function (e) { aImg.src = e.target.result; }; })(img);
    reader.readAsDataURL(file);
}

function addButtonToPictureInGalery(gallery, image)
{
    var button = document.createElement("button");
    button.textContent = "PRZYCISK";

    button.style.position = "absolute";
    button.offsetTop = getPosition(image).y;
    button.offsetLeft = getPosition(image).x;
    gallery.appendChild(button);
}
