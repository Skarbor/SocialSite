function SendInvitationToFriends(userId) {
    $.ajax({
        url: '/Friends/SendInvitationToFriends',
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        data: { userId: userId },
        context: document.body,
        success: function () {
            $('#singleUserinformatio').load("/User/DisplayUserPartial", { userId: userId });
        }
    });
}


var uploadfiles = document.querySelector('#fileinput');

uploadfiles.addEventListener('change', function () {
    var files = this.files;
    
    for (var i = 0; i < files.length; i++) {   
        uploadFile(this.files[i]);
    }
}, false);


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
    fd.append("file", file);
    xhr.send(fd);
}

