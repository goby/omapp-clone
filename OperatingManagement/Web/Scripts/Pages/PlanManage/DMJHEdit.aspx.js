$(window).ready(function () {
    $("#TaskList").change(
        function () {
            var checkValue = $("#TaskList").val();
            var obj = document.getElementById("ddlTask");
            for (var i = 0; i < obj.length; i++) {

                if (obj.options[i].text == checkValue) {
                    $("#txtSCID").val(obj.options[i].value);
                    break;
                }
            }

        });

    $("#TaskList").change();
});

function SetSBDH(o, cid , tid) {
    var index = o.selectedIndex;
    var selectedValue = o.options[index].value;
    var selectedText = o.options[index].text;

    var obj = document.getElementById(cid);
    if (selectedText == "喀什站") {
        showSBDHForm(cid,tid);
    }
    else {
        for (var i = 0; i < obj.length; i++) {

            if (obj.options[i].text == selectedText) {
                obj.options[i].selected = true;
                $("#"+tid).val(obj.options[i].value);
                break;
            }
        }
    }

}

//弹出喀什站设备代号选择窗口
function showSBDHForm(cid,tid) {
    var _dialog;
    _dialog = $("#dialog-sbdh");
    _dialog.dialog({
        autoOpen: false,
        height: 150,
        width: 300,
        modal: true,
        open:function(){$(".ui-dialog-titlebar-close").hide();},
        buttons: {
            '确定': function () {
                var selectedText;
                selectedText =  $('input:radio[name="kashi"]:checked').val();
                var obj = document.getElementById(cid);
                for (var i = 0; i < obj.length; i++) {

                    if (obj.options[i].text == selectedText) {
                        obj.options[i].selected = true;
                        $("#" + tid).val(obj.options[i].value);
                        break;
                    }
                }
                $(this).dialog("close");

            }
        }
    });

    _dialog.dialog('open');
    _dialog.parent().appendTo($("form:first"));
    return false;
}

function CheckClientValidate() {
    Page_ClientValidate();
    if (Page_IsValid) {
        var isvalid = true;
        var txtlist = $('#detailtable').find('input:text:not([disabled])');
        txtlist.each(function () {
            if ($(this).val() == '') {
                $(this).css({ "background-color": "#ffcccc" });
                isvalid = false;
            }
            else {
                $(this).css({ "background-color": "#f5f5f5" });
            }
        });
        if (!isvalid) {
            showMsg('您填写的计划信息不完整,请检查并填写完善后重新提交!');
        }

        return isvalid;
    }
}

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
//弹出选择测控资源使用计划窗口
function showSBJHForm() {
    var _dialog;
    _dialog = $("#dialog-sbjh");
    _dialog.dialog({
        autoOpen: false,
        height: 450,
        width: 600,
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
    btn.text("所选测控资源使用计划： " + filepath + "    (点击取消选择)");
    var btn = $('#btnHidden');
    btn.click();
    return false;
}


//弹出进出站及航捷数据统计文件内容窗口
function showFileContentForm() {
    var _dialog;
    _dialog = $("#dialog-station");
    _dialog.dialog({
        autoOpen: false,
        height: 550,
        width: 750,
        modal: true,
        buttons: {
            '确定': function () {
                var list = $('#tbStations').find('input:checkbox:[checked]');
                if (list.length == 0) {
                    showMsg('请选择进出站及航捷数据。');
                }
                else {
                    $(this).dialog("close");
                    var ids = list.map(function () { return this.value; }).get().join(',');
                    $('#txtIds').val(ids);
                    var btn = $('#btnGetStationData');
                    btn.click();
                }
            },
            '关闭': function () {
                $(this).dialog("close");
            }
        }
    });
    _dialog.dialog('open');
    _dialog.parent().appendTo($("form:first"));
    return false;
}

function checkAll(o) {
    if ($('#tbStations').length > 0) {
        $('#tbStations').find('input:checkbox:not([disabled])').attr('checked', o.checked);
    }
}