//1,將連線打開
(function () {
    openCon();
})();
//2,建立與Server端的Hub的物件，注意Hub的開頭字母一定要為小寫
var hubCon = $.connection.gameHub;
function openCon () {
   //將連線打開
    $.connection.hub.start().done(function () {
        //3,加入群組
        addGroupBtn();
    }).fail(function () {
        alert("發生錯誤");
    });
}

//3,加入群組
function addGroupBtn(){
    var user = $('#nickname').val();
    var group = $('#group').val();
    $('#autoBtn').attr("disabled", false);
    var msg2grp = { "group": group, "name": user };
    var msgJSON = JSON.stringify(msg2grp);
    hubCon.server.addGroup(msgJSON).catch(function (err) {
        //hubCon.server.addGroup(group, user).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}
//4,顯示加入訊息
hubCon.client.recAddGroupMsg = function (msgJSON) {
    var mJson = JSON.parse(msgJSON);
    //var msgTxtarea = $('#message');
    //msgTxtarea.val(msgTxtarea.val() + msg + "\n");
    $('#message').val($('#message').val() + mJson.name + "\n");
    //$('#message').val($('#message').val() + mJson.msg + "\n");
    $('#message').scrollTop($('#message')[0].scrollHeight);
};

//送訊息給群組
$('#sendGroupBtn').click(function () {
    var user = $('#nickname').val();
    var group = $('#group').val();
    var message = $('#msg').val();
    var msg2grp = { "group": group, "name": user, "message": message };
    var msgJSON = JSON.stringify(msg2grp);
    hubCon.server.msgToGroup(msgJSON).catch(function (err) {
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