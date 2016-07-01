function SendInvitationToFriends(userId, userName) {
    $.ajax({
        url: '/Friends/SendInvitationToFriends',
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        data: { userId: userId },
        context: document.body,
        success: function () {           
            var element = $(".singleUserinformation > span:contains(" + userName + ")").parent();
            if (element != null) {
                element.css("margin", "0px");
                element.css("padding", "0px");

                element.load("/User/DisplayUserPartial", { userId: userId });
            }
        }
    });
}

function AcceptInvitationToFriends(invitationId)
{

}


