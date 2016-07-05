var myWorker;
var lastMessageId = 0;
var userProfilePictureString;

function communicatorContactClick(userId)
{
    getUserProfilePicture(userId);
    displayChatWindow(userId);
}

function displayChatWindow(userId)
{
    lastMessageId = 0;

    var containerDiv = document.getElementById("conversationContainer");

    var window = document.createElement("div");
    window.className = "chatWindow";

    var belkaGorna = document.createElement("div");
    belkaGorna.className = "chatWindowBelkaGorna";
    belkaGorna.appendChild(document.createTextNode("Adam Wójcik"));
    belkaGorna.addEventListener("click", function () { hideChatWindow(window); });

    var divRozmowy = document.createElement("div");
    divRozmowy.className = "chatWindowRozmowa";

    var textInput = document.createElement("input");
    textInput.type = "text";
    textInput.placeholder = "Wpisz wiadomość...";
    textInput.onkeypress = function () {if (event.keyCode == 13) sendMessage(divRozmowy, userId, textInput);};

    window.appendChild(belkaGorna);
    window.appendChild(divRozmowy);
    window.appendChild(textInput);
    containerDiv.appendChild(window);

    //loadMessages(userId, divRozmowy);

    messagesWorker(userId, divRozmowy);
}

function messagesWorker(userId, divRozmowy)
{
    setTimeout(function ()
    {
        loadMessages(userId, divRozmowy)
        messagesWorker(userId, divRozmowy);
    }, 500);


    //myWorker = new Worker("js/Components/Communicator/communicatorMessageWorker.js");
    //myWorker.onmessage = function (event)
    //{
    //    displayMessage(event.data, divRozmowy)
    //}

    //myWorker.postMessage(userId);
}

function loadMessages(userId, divRozmowy)
{
    $.ajax(
    {
        url: "/Communicator/ReceiveLastXMessagesFromConversation",
        method: "GET",
        data: { "userWhoSendId": userId, "numberOfMessagesToReceive": 10 },
        dataType: "json",
        async: true,
        success: function (data)
        {
            for (var i = 0; i < data.length; i++) {
                if (data[i].Id > lastMessageId) {
                    lastMessageId = data[i].Id;
                    displayMessage(data[i].Content, divRozmowy);
                }

            }
        }
    });
}

function displayMessage(message, divRozmowy)
{
    var profilePicture = document.createElement("img");
    profilePicture.src = userProfilePictureString;


    var paragraph = document.createElement("p");
    paragraph.appendChild(document.createTextNode(message));

    divRozmowy.appendChild(profilePicture);
    divRozmowy.appendChild(paragraph);
    divRozmowy.scrollTop = divRozmowy.scrollHeight;
}

function getUserProfilePicture(userId)
{
    $.ajax(
   {
       url: "/Communicator/GetUserProfilPicture",
       method: "GET",
       data: { "userId": userId},
       dataType: "json",
       async: true,
       success: function (data)
       {
           userProfilePictureString = data;
       },
       error: function (data)
       {
           alert(data);
       }
   });
}

function sendMessage(divRozmowy, userId, textInput)
{
    var message = textInput.value;
    $.ajax(
    {
        url: "/Communicator/SendMessage",
        method: "GET",
        data: { "receivingUserId": userId, "message": message },
        dataType: "json",
        async: true
    });

    //displayMessage(message, divRozmowy);
    textInput.value = "";
}

function hideChatWindow(window) {
    window.remove();
}
