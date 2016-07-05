importScripts("/js/site.js");

var lastMessageId = 0;
var userId;

this.addEventListener("message", function (e) {
    userId = e.data;
})

function Work() {

    //setTimeout("loadMessages(1)", 500);
    postMessage("AAA");
}

Work();


function loadMessages(userId) {
    $.ajax(
    {
        url: "/Communicator/ReceiveLastXMessagesFromConversation",
        method: "GET",
        data: { "userWhoSendId": userId, "numberOfMessagesToReceive": 10 },
        dataType: "json",
        async: true,
        success: function (data) {
            for (var i = 0; i < data.length; i++) {              
                if (data[i].Id > lastMessageId) {
                    lastMessageId = data[i].Id;
                    postMessage(i); (data[i].Content);
                }               
            }
        }
    });

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            cfunc(xhttp);
        }
    };
    xhttp.open("GET", "/Communicator/ReceiveLastXMessagesFromConversation", true);
    xhttp.send();

}