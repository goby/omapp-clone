
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

//弹出选择试验计划窗口
function showSYJHForm() {
    var _dialog;
    _dialog = $("#dialog-sbjh");
    _dialog.dialog({
        autoOpen: false,
        height: 550,
        width: 800,
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

function SelectSYJH(satname, starttime, endtime, sysname, systask) {
    var uctask = $('#ucTask1');
    uctask.val(satname);
    var ddlSysName = $('#ddlSysName');
    ddlSysName.val(sysname);

    $('#txtStartTime').val(starttime);
    $('#txtEndTime').val(endtime);
    $('#txtTask').val(systask);
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