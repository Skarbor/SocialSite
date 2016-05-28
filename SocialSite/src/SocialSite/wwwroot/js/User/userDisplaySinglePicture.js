//$("#singleUserPictureDiv input[type='text']").keyup(function (event) {
//    if (event.keyCode == 13) {
        
//    }
//});

$(document).ready(function ()
{
    $("#singleUserPictureDiv input[type='text']").keypress('keyup', function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
        }
    });

});

function sendComment(pictureId)
{
    var commentText = $("#singleUserPictureDiv input[type='text']").val();

    if (commentText == "") {
        alert("Wprowadź tekst komentarza");
        return;
    }

    $.ajax(
        {
            url: "/User/AddCommentToPicture",
            method: "GET",
            data: { "pictureId": pictureId, "commentText": commentText },
            dataType: "json"
        })
        .done(function (data)
        {
            displaySendedComment(data);
        });
}

function displaySendedComment(data)
{
    var singleUserPictureDiv = document.getElementById("singleUserPictureDiv");

    var commentDiv = document.createElement("div");
    commentDiv.className = "comment";
   
    var em = document.createElement("em");

    var em_link = document.createElement("a");
    em_link.textContent = data.User.FirstName + " " + data.User.LastName;
    em_link.href = "/User/DisplayUserProfile?userId=" + data.User.Id;

    em.appendChild(em_link);

    var napisal = document.createTextNode(" napisał:")

    var article = document.createElement("article");
    article.appendChild(document.createTextNode("\u201E" + " " + data.Text + " " + "\u201D"));

    var em_data = document.createElement("em");
    em_data.appendChild(document.createTextNode(parseDataFromJSONdataObject(data.Date)));

    commentDiv.appendChild(em);
    commentDiv.appendChild(napisal);
    commentDiv.appendChild(article);
    commentDiv.appendChild(em_data);

    singleUserPictureDiv.appendChild(commentDiv);

    //$("#singleUserPictureDiv").add("div").addClass("comment").add("p").append("AAAAAAA");

}

function parseDataFromJSONdataObject(jsonData)
{
    var time = jsonData.slice(0, 10) + " " + jsonData.slice(11, 16);
    return time;
}