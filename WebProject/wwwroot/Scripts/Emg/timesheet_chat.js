$("#chat-message").on("keydown",
    function (e) {
        if (e.keyCode === 13) {
            sendMessage();
        }
    });


function checkNoReadMessage() {

    if (!$(".chat-sidebar").hasClass("chat-on")) {
        var estUserPair = new Object();

        $(".fa-comments").each(function () {
            var tr = $(this).closest("tr");
            var userId = tr.data("userId");
            var estId = tr.data("estId");

            estUserPair[estId] = userId;
        });

        if (Object.keys(estUserPair).length > 0) {
            var date = $("#date-arrow").val();

            $.ajax({
                type: 'POST',
                url: "/Projects/CheckNoReadMessage",
                data: {
                    estUserPair: estUserPair,
                    date: date
                },
                dataType: 'html'
            }).done(function (result) {
                var parse = $.parseJSON(result);
                $(parse).each(function () {
                    var chatPulse = this;
                    var rowEstId = chatPulse.EstId;
                    var rowUserId = chatPulse.UserId;
                    var rowIsNewMessage = chatPulse.IsNewMessages;

                    if (rowIsNewMessage) {
                        $('#row-' + rowUserId + '-' + rowEstId + ' .fa-comments').addClass('icon-pulse');
                    }
                });
            });
        }
    }

}

function checkNewMessage() {
    if ($(".chat-sidebar").hasClass("chat-on")) {
        var userId = $("#chat-user-id").val();
        var estId = $("#chat-est-id").val();
        var msgCount = $('.chat-messages li').length;
        var date = $("#date-arrow").val();

        $.ajax({
            type: 'POST',
            url: "/Projects/CheckNewMessage",
            data: {
                estId: estId,
                userId: userId,
                date: date,
                currMsgCount: msgCount
            },
            dataType: 'html'
        }).done(function (result) {
            var ul = $('.chat-messages');
            ul.append(result);
        });
    }
}

$(".ei-sent").on("click", function () {
    sendMessage();
});

function sendMessage() {
    var userId = $("#chat-user-id").val();
    var estId = $("#chat-est-id").val();
    var message = $("#chat-message").val();
    var date = $("#date-arrow").val();

    if (message !== "") {
        $.ajax({
            type: 'POST',
            url: "/Projects/CreateMessage",
            data: {
                estId: estId,
                userId: userId,
                message: message,
                date: date
            },
            dataType: 'html'
        }).done(function (result) {
            $("#chat-message").val("");
            $("#chat-message").outerHeight(80);

            var ul = $('.chat-messages');
            $.when(ul.append(result)).then(function () {
                setTimeout(chatScrollPosition(), 100);
            });
        });
    }
}

function chatScrollPosition() {
    var ul = $('.chat-messages');

    var ulHeight = ul.outerHeight();
    var containerHeight = $("#mCSB_2").height();
    var padding = 18;
    var scrollPosition = containerHeight - padding - ulHeight;

    $("#mCSB_2_container").css("top", scrollPosition + "px").hide().show(0);
}

$(document).on("click", ".fa-comments", function () {

    var tr = $(this).closest("tr");
    var userId = tr.data("userId");
    var estId = tr.data("estId");
    var date = $("#date-arrow").val();
    var estCode = tr.data("estCode");

    if (userId == $("#chat-user-id").val() &&
        estId == $("#chat-est-id").val() &&
        date == $("#chat-date").val()) {
        $(".chat-sidebar").toggleClass("chat-on");
        $(".page-content").toggleClass("righter");
        $("#topcontrol").toggleClass("chat");
        setTimeout(chatScrollPosition(), 100);
        setTimeout(function () { onResizeSticky(); }, 350);
    } else {
        $.ajax({
            type: 'POST',
            url: "/Projects/GetMessages",
            data: {
                estId: estId,
                userId: userId,
                date: date
            },
            dataType: 'html'
        }).done(function (result) {
            var div = $('.chat-messages');
            $.when(div.html(result)).then(function () {
                setTimeout(chatScrollPosition(), 100);
            });
            $("#chat-est-code").html("Смета: " + estCode);
            setTimeout(function () { onResizeSticky(); }, 250);
        });

        if (!$(".chat-sidebar").hasClass("chat-on")) {
            $(".chat-sidebar").toggleClass("chat-on");
            $(".page-content").toggleClass("righter");
            $("#topcontrol").toggleClass("chat");
        }
    }

    $(".fa-comments").css("color", "black");
    if ($(".chat-sidebar").hasClass("chat-on")) {
        $(this).css("color", "darkgoldenrod");
    }

    $("#chat-est-id").val(estId);
    $("#chat-user-id").val(userId);
    $("#chat-date").val(date);

    $(this).removeClass("icon-pulse");
});