
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

Date.prototype.Format = function (fmt) { //author: meizz   
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}  

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

//任务准备开始时间输入后，跟踪开始时间、任务开始时间、任务结束时间、跟踪结束时间
//默认分别依次累加30分钟、30秒、10分钟、30秒。
function SetDateTime(o, startid, trackstartid,waveonid,waveoffid,trackendid,endid) {
    if (o.value != "") {
        predate = o.value;
        $("#" + startid).val(GetDateTimeFormat(predate, 30*60+30));
        $("#" + trackstartid).val(GetDateTimeFormat(predate, 30*60));
        //$("#" + waveonid).val(GetDateTimeFormat(predate, 30));
        //$("#" + waveoffid).val(GetDateTimeFormat(predate, 30));
        $("#" + trackendid).val(GetDateTimeFormat(predate, 41 * 60));
        $("#" + endid).val(GetDateTimeFormat(predate, 40 * 60 + 30));
    }
}
//格式化时间
function GetDateTimeFormat(curr, second) {
    var formatDate = new Date(curr.substr(0, 4), curr.substr(4, 2), curr.substr(6, 2), curr.substr(8, 2), curr.substr(10, 2), curr.substr(12, 2));
    formatDate.setTime(formatDate.getTime() + second * 1000);

    var yyyy = formatDate.getFullYear();
    //var yy = yyyy.toString().substring(2);
    var m = formatDate.getMonth();
    var mm = m < 10 ? "0" + m : m;
    var d = formatDate.getDate();
    var dd = d < 10 ? "0" + d : d;

    var h = formatDate.getHours();
    var hh = h < 10 ? "0" + h : h;
    var n = formatDate.getMinutes();
    var nn = n < 10 ? "0" + n : n;
    var s = formatDate.getSeconds();
    var ss = s < 10 ? "0" + s : s;

    var str = "" +yyyy + mm + dd + hh + nn + ss;
    return str;
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