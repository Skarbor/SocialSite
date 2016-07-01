function SendInvitationToFriends(userId) {
    $.ajax({
        url: '/Friends/SendInvitationToFriends',
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        data: { userId: userId },
        context: document.body,
        success: function () {
            $('#singleUserinformation').load("/User/DisplayUserPartial", { userId: userId });
        }
    });
}

function AcceptInvitationToFriends(invitationId)
{

}


