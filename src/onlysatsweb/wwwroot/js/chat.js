"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("newMessage").disabled = true;

connection.on("ReceiveMessage", function (user, message, origin_server_ts, event_id) {
    var messages = [
        { "sender": user, "content": { "formatted_body": message }, "origin_server_ts": origin_server_ts, "event_id": event_id }
    ];

    displayMessages(messages, true);
});

connection.start().then(function () {
    document.getElementById("newMessage").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// TODO: Rewrite to use jQuery
document.getElementById("newMessage").addEventListener("click", function (event) {

    var roomId = document.getElementById("RoomId").value;
    var message = document.getElementById("messageText").value;
    sendMessage(roomId, message);

    event.preventDefault();
});


function loadPreviousMessages() {
    let from = $('#End').val();

    if (from === '') {
        return;
    }

    let roomId = $('#RoomId').val();

    $('#loadPrevious').hide();

    $.get('/api/chat/messages', { roomId: roomId, from: from })
        .done(function (data) {
            displayMessages(data.messages, false);
        })
        .always(function (data) {
            if (from === data.end) {
                $('#End').val('');
                $('#noMore').show();
            } else {
                $('#noMore').hide();
                $('#End').val(data.end);
                $('#loadPrevious').show();
            }
        });
}

function displayMessages(messages, append) {
    if (messages.length > 0) {
        let newHtml = '';
        for (var i = 0; i < messages.length; i++) {
            let message = messages[i];
            newHtml += `
                <div class="chatMessage" id="msg-${message.event_id.replace('$', '')}">
                    <b>${message.sender} (at ${message.origin_server_ts})</b>: ${message.content.formatted_body}
                </div>`;
        }
        let existingHtml = $('#chatMessages').html();
        if (append) {
            $('#chatMessages').html(existingHtml + newHtml);
        } else {
            $('#chatMessages').html(newHtml + existingHtml);
        }
    } else {
        // alert('no new messages');
    }
}

function sendMessage(roomId, message) {
    // TODO: POST to api/chat/messages and on done, use signalR to send 
    // the message realtime to the participants in the room
    $.ajax({
        url: '/api/chat/messages',
        dataType: 'json',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ "RoomId": roomId, "Message": message }),
        processData: false,
        success: function (data, textStatus, jQxhr) {
            if (!$(`#msg-${data.event_id.replace('$', '')}`).length) {
                connection.invoke("SendMessage", data.sender, data.content.formatted_body, data.origin_server_ts, data.event_id).catch(function (err) {
                    return console.error(err.toString());
                });
            }

            $('#messageText').val('');
        },
        error: function (jqXhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function sendPaidMessage(roomId, assetPackageId, unlockedContent, costSatoshis) {
    // TODO: Creator only. Creator can send a paid message where they select 
    // a previously created AssetPackage of content (images, video, etc.) or some
    // other service, a message to display to the Patron in chat while the 
    // message is locked (usually a description of the content), and how much
    // it costs to unlock the message
}