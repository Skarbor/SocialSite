var userProfilePictureString;
var usersInformations = [];
var loggedUserId;

function setUpCommunicator(userId)
{
    getUserProfilePicture(userId);
    loggedUserId = userId;
}


function communicatorContactClick(userId)
{
    getUserProfilePicture(userId, function () {
        displayChatWindow(userId);
    });
    
}


function displayChatWindow(userId)
{
    if (CheckIfArrayContainsUserByUserId(usersInformations, userId)) {
        usersInformations[GetArrayElementIndexByUserId(usersInformations, userId)].IsChatWindowOpen = true;
    }
    else {
        usersInformations.push(
            {
                UserId: userId,
                IsChatWindowOpen: true
            });
    }

    var containerDiv = document.getElementById("conversationContainer");

    var window = document.createElement("div");
    window.className = "chatWindow";

    var belkaGorna = document.createElement("div");
    belkaGorna.className = "chatWindowBelkaGorna";
    belkaGorna.appendChild(document.createTextNode("Adam Wójcik"));
    belkaGorna.addEventListener("click", function () { hideChatWindow(userId, window); });

    var divRozmowy = document.createElement("div");
    divRozmowy.className = "chatWindowRozmowa";

    $(divRozmowy).scroll(function () {
        var pos = $(divRozmowy).scrollTop();
        if (pos == 0) {
            alert('top of the div');
        }
    });

    usersInformations[GetArrayElementIndexByUserId(usersInformations, userId)].DivRozmowy = divRozmowy;

    var textInput = document.createElement("input");
    textInput.type = "text";
    textInput.placeholder = "Wpisz wiadomość...";
    textInput.onkeypress = function () {if (event.keyCode == 13) sendMessage(divRozmowy, userId, textInput);};

    window.appendChild(belkaGorna);
    window.appendChild(divRozmowy);
    window.appendChild(textInput);
    containerDiv.appendChild(window);

    loadMessages(userId, divRozmowy);
}

function loadMessages(userId)
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
                displayMessage(data[i].Content, data[i].UserWhoReceivedId, data[i].UserWhoSendId, data[i].Id);
            }
        }
    });
}

function displayMessage(message, receiverUserId, senderUserId, messageId)
{
    var messageDiv = document.createElement("div");
    messageDiv.setAttribute("messageId", messageId);


    var profilePicture = document.createElement("img");
    if (usersInformations[GetArrayElementIndexByUserId(usersInformations, receiverUserId)].UserProfilePictureString != "") {
        profilePicture.src = usersInformations[GetArrayElementIndexByUserId(usersInformations, receiverUserId)].UserProfilePictureString;
    }
    else
        profilePicture.src = "images/anonim_men.jpg";

    var messageText = document.createElement("span");
    messageText.appendChild(document.createTextNode(message));

    messageDiv.appendChild(profilePicture);
    messageDiv.appendChild(messageText);

    if (loggedUserId == senderUserId) {
        messageDiv.className += "UserMessage";

        usersInformations[GetArrayElementIndexByUserId(usersInformations, receiverUserId)].DivRozmowy.appendChild(messageDiv);
        usersInformations[GetArrayElementIndexByUserId(usersInformations, receiverUserId)].DivRozmowy.scrollTop = usersInformations[GetArrayElementIndexByUserId(usersInformations, receiverUserId)].DivRozmowy.scrollHeight;
    }
    else {
        messageDiv.className += "NotUserMessage";

        usersInformations[GetArrayElementIndexByUserId(usersInformations, senderUserId)].DivRozmowy.appendChild(messageDiv);
        usersInformations[GetArrayElementIndexByUserId(usersInformations, senderUserId)].DivRozmowy.scrollTop = usersInformations[GetArrayElementIndexByUserId(usersInformations, senderUserId)].DivRozmowy.scrollHeight;
    }


    
}

function getUserProfilePicture(userId, callback)
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
           if (CheckIfArrayContainsUserByUserId(usersInformations, userId)) {
               usersInformations[GetArrayElementIndexByUserId(usersInformations, userId)].UserProfilePictureString = data;
           }
           else {
               usersInformations.push(
                   {
                       UserId: userId,
                       UserProfilePictureString: data
                   });
           }
           if (callback != undefined) {
               callback();
           }

       },
       error: function (data)
       {
           alert(data);
       }
   });
}

function sendMessage(divRozmowy, receiverUserId, textInput)
{
    var message = textInput.value;

    SocketSendMessage(receiverUserId, message);

    displayMessage(message,receiverUserId,  loggedUserId);
    textInput.value = "";
}

function hideChatWindow(userId, window) {
    usersInformations[GetArrayElementIndexByUserId(usersInformations, userId)].IsChatWindowOpen = false;
    window.remove();
}
