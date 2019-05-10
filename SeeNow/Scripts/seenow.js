//"use strict";
//var audio = new Audio('../music/gone_fishin_by_memoraphile_CC0.mp3');
//audio.play();

//$(function () {
//    // Reference the auto-generated proxy for the hub.
//    var chat = $.connection.gameHub;
//    // Create a function that the hub can call back to display messages.
//    chat.client.addNewMessageToPage = function (name, message) {
//        // Add the message to the page.
//        $('#discussion').append('<li><strong>' + htmlEncode(name)
//            + '</strong>: ' + htmlEncode(message) + '</li>');
//    };
//    // Get the user name and store it to prepend to messages.
//    $('#displayname').val(prompt('Enter your name:', ''));
//    // Set initial focus to message input box.
//    $('#message').focus();
//    // Start the connection.
//    $.connection.hub.start().done(function () {
//        $('#sendmessage').click(function () {
//            // Call the Send method on the hub.
//            chat.server.chatSend($('#displayname').val(), $('#message').val());
//            // Clear text box and reset focus for next comment.
//            $('#message').val('').focus();
//        });
//    });
//});

var Timer_1;
var timerT = function (c) {
    $('#audio')[0].play();
    $('#countTime').html(c);
    $('#countTime').fadeOut(850).fadeIn(10);
    c--;
    Timer_1 = setTimeout(function () {
        if (c <= 0) {
            clearTimeout(Timer_1);
            $('#countTime').html("0");
        }
        else {
            timerT(c);
        }
    }, 1000);
};

var hubCon = $.connection.gameHub;

$('#sendGroupBtn').attr("disabled", true);

//Start the connection.
$.connection.hub.start().done(function () {
    $('#sendGroupBtn').attr("disabled", false);
}).catch(function (err) {
    return console.error(err.toString());
});

//加入群組
$('#addGroupBtn').click(function (event) {
    var user = $('#name').val();
    var group = $('#group').val();
    $('#autoBtn').attr("disabled", false);
    hubCon.server.addGroup(group, user).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

//顯示加入訊息
hubCon.client.recAddGroupMsg = function (msg) {
    //var li = document.createElement("li");
    //li.textContent = msg+"&#13;&#10;";
    //$('#message').append(li);
    var msgTxtarea = $('#message');
    msgTxtarea.val(msgTxtarea.val() + msg + "\n");
    $('#message').scrollTop($('#message')[0].scrollHeight);
};

//送訊息給群組
$('#sendGroupBtn').click(function () {
    var user = $('#name').val();
    var group = $('#group').val();
    var message = $('#msg').val();
    var msg2grp = { "group": group, "name": user, "message": message };
    var msgJSON = JSON.stringify(msg2grp);
    hubCon.server.msgToGroup(group, msgJSON).catch(function (err) {
        return console.error(err.toString());
    });
});

//接收訊息
hubCon.client.fromGroupMsg = function (msgGroup) {
    //var li = document.createElement("li");
    //li.textContent = msg+"&#13;&#10;";
    //$('#message').append(li);
    var msgTxtarea = $('#message');
    msgTxtarea.val(msgTxtarea.val() + msgGroup + "\n");
    $('#message').scrollTop($('#message')[0].scrollHeight);
};

//接收題目
hubCon.client.receiveGroupQuiz = (function (groupName, user, message, msgJSON) {
    var mJson = JSON.parse(msgJSON);
    var msg = '[群組(${groupName})]${user}：${message}';
    //var li = document.createElement("li");
    //li.textContent = msg;
    //document.getElementById("msgDiv").appendChild(li);
    $('#quizDiv').html(mJson.message);
    $('#countTime').html(mJson.interval_Time);//countTime顯示interval_Time
    timerT(parseInt($('#countTime').html()));//timerT(c),c=countTime
    $('#message').html(msg);
    $('#redBtn').val(mJson.redBtn);
    $('#blueBtn').attr('value', mJson.blueBtn);
    $('#greenBtn').attr('value', mJson.greenBtn);
    $('#yellowBtn').attr('value', mJson.yellowBtn);
});

function autoQuiz() {
    var tbRows = $('#Tbquiz').find('tbody').find('tr');
    var group = $('#group').val();
    var user = $('#name').val();
    var redBtn = 0, blueBtn = 0, greenBtn = 0, yellowBtn = 0;
    var qtimer;
    var i = 0;
    function q_display() {
        if (i <= tbRows.length - 1) {
            var tbRowsIndexVal = $(tbRows[i]).find('td:eq(0)').html();
            //從QuizIndex取id="interval_Time"題目間隔時間
            //var interval_Time = $('#interval_Time').val();
            var interval_Time = $(tbRows[i]).find('td:eq(1)').html();
            redBtn = $(tbRows[i]).find('td:eq(2)').html();
            blueBtn = $(tbRows[i]).find('td:eq(3)').html();
            greenBtn = $(tbRows[i]).find('td:eq(4)').html();
            yellowBtn = $(tbRows[i]).find('td:eq(5)').html();
            var msg2grp = { "group": group, "name": user, "message": tbRowsIndexVal, "interval_Time": interval_Time, "redBtn": redBtn, "blueBtn": blueBtn, "greenBtn": greenBtn, "yellowBtn": yellowBtn };
            var msgJSON = JSON.stringify(msg2grp);
            hubCon.server.sendQuizToGroup(group, user, "quiz:" + (i + 1), msgJSON).catch(function (err) {
                return console.error(err.toString());
            });
            i++;
            qtimer = setTimeout(q_display, interval_Time * 1100);
        }   //*1100 多0.1秒讓計時器timerT有足夠顯示0的時間

        else {
            clearTimeout(qtimer);
            $('#audio')[0].pause();
        }
    }
    q_display();
}

//接收4個答案鈕
function choicBtn(choic) {
    var user = $('#name').val();
    var group = $('#group').val();
    var message = choic.name;
    var msg2grp = { "group": group, "name": user, "message": message };
    var msgJSON = JSON.stringify(msg2grp);
    hubCon.server.msgToGroup(group, msgJSON).catch(function (err) {
        return console.error(err.toString());
    });
}

