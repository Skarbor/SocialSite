var WebSocket;

function StartWebSocket() {
    if ("WebSocket" in window) {
        // Let us open a web socket
        WebSocket = new WebSocket("ws://localhost:55371");

        //ws.onopen = function () {
        //    // Web Socket is connected, send data using send()
        //    ws.send("Message to send");
        //    alert("Message is sent...");
        //};

        WebSocket.onmessage = function (evt) {
            var received_msg = evt.data;
            SocketReceiveMessage(received_msg);
        };

        WebSocket.onclose = function () {
            // websocket is closed.
            alert("Connection is closed...");
        };
    }

    else {
        // The browser doesn't support WebSocket
        alert("WebSocket NOT supported by your Browser!");
    }
}

function SocketSendMessage(receiverId, messageText)
{
    var message = receiverId + ":" + messageText;
    WebSocket.send(message);
}

function SocketReceiveMessage(message)
{

    // senderId : receiverId : messageId : messageText 

    var indexOfFirstSeperator = message.indexOf(':');
    var indexOfSecondSeperator = message.indexOf(':', indexOfFirstSeperator + 1);
    var indexOfThirdSeperator = message.indexOf(':', indexOfSecondSeperator + 1);



    var sendinguserId = message.substring(0, indexOfFirstSeperator);
    var receivingUserId = message.substring(indexOfFirstSeperator + 1, indexOfSecondSeperator);
    var messageId = message.substring(indexOfSecondSeperator + 1, indexOfThirdSeperator);
    var message = message.substring(indexOfThirdSeperator + 1);
    
    //alert(sendinguserId);
    //alert(receivingUserId);
    //alert(messageId);
    //alert(message);

    if (usersInformations[GetArrayElementIndexByUserId(usersInformations, sendinguserId)].IsChatWindowOpen) {
        displayMessage(message, receivingUserId, sendinguserId, messageId);
    }
}