$(document).ready(function ()
{
    var uploadfiles = document.querySelector('#fileinput');

    if (uploadfiles != null) {
        uploadfiles.addEventListener('change', function () {
            var files = this.files;

            for (var i = 0; i < files.length; i++) {
                uploadFile(this.files[i]);
            }
        }, false);
    }


    var uploadProfilePictures = document.querySelector('#profileFileinput');

    if (uploadProfilePictures != null) {
        uploadProfilePictures.addEventListener('change', function () {
            var files = this.files;

            for (var i = 0; i < files.length; i++) {
                uploadProfilePicture(this.files[i]);
            }
        }, false);
    }

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

function uploadProfilePicture(file) {
    var url = '/User/AddProfilePicture';
    var xhr = new XMLHttpRequest();
    var fd = new FormData();
    xhr.open("Post", url, true);

    var pictureId;

    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            // Every thing ok, file uploaded
            pictureId = xhr.response;

            displayProfilePicture(file, pictureId);
        }
    };

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

function displayProfilePicture(file, pictureId)
{
    var profilePictureDiv = document.getElementById("profilePicture");

    deleteChildren(profilePictureDiv);

    var img = document.createElement("img");
    img.className = "img-rounded";
    img.file = file;


    var a = document.createElement("a");
    a.href = "/User/DisplaySinglePicture?pictureId=" + pictureId;

    a.appendChild(img);

    profilePictureDiv.appendChild(a);
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


function deleteChildren(node)
{
    if (node.hasChildNodes) {
        for (var i = node.childNodes.length - 1; i >= 0; i--) {
            node.removeChild(node.childNodes[i]);
        }
    }
}