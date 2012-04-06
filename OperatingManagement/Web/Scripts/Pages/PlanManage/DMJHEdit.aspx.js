
$(function () {
    $("#txtPlanStartTime").datepicker();
    $("#txtPlanEndTime").datepicker();
});

function showMsg(msg) {
    var _dialog;
    _dialog = $("#dialog-form");
    _dialog.dialog({
        autoOpen: false,
        height: 150,
        width: 350,
        modal: true,
        buttons: {
            '关闭': function () {
                $(this).dialog("close");
            }
        }
    });
    _dialog.find('p.content').eq(0).html(msg);
    _dialog.dialog('open');
    return false;
}
//弹出选择设备计划窗口
function showSBJHForm() {
    var _dialog;
    _dialog = $("#dialog-sbjh");
    _dialog.dialog({
        autoOpen: false,
        height: 450,
        width: 550,
        modal: true,
        buttons: {
            '关闭': function () {
                $(this).dialog("close");
            }
        }
    });
    //_dialog.find('p.content').eq(0).html(msg);
    _dialog.dialog('open');
    _dialog.parent().appendTo($("form:first"));
    return false;
}

function hideAllButton() {
    $(":submit").css('display','none'); 
    return false;
}

function setdayte(o){
    $(o).datepicker({
			changeMonth: true,
			changeYear: true
		});
}

function SelectSBJH(id, fileindex) {
    var txtId = $('#hfSBJHID');
    txtId.val(id);
    var btn = $('#btnSBJH');
    var filepath = unescape(fileindex);
    btn.text("所选设备计划： "+filepath + "    (点击取消选择)");
    var btn = $('#btnHidden');
    btn.click();
    return false;
}