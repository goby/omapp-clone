var _dialog;
$(window).ready(function () {
    _dialog = $("#dialog-form");
});

$(function () {
    $("#txtStartDate").datepicker();
    $("#txtEndDate").datepicker();
});


function reset(o) {
    $('input:text').val('');
    return false;
}

function setdayte(o){
    $(o).datepicker({
			changeMonth: true,
			changeYear: true
		});
}

function selectAll() {
    $('input:checkbox:not([disabled])').attr('checked', true);
    return false;
}

function checkAll(o) {
    if ($('#tbPlans').length > 0) {
        $('#tbPlans').find('input:checkbox:not([disabled])').attr('checked', o.checked);
    }
}

function sendPlan() {
    //var chks = $('#tbPlans').find('input:checkbox:not([disabled])').filter('[checked=true]');
    var chks = $('#tbPlans').find('input:checkbox:[checked]');
    if (chks.length == 0) {
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
        _dialog.find('p.content').eq(0).html('请选择您要发送的轨道数据。');
        _dialog.dialog('open');
        return false;
    }

    var ids = chks.map(function () { return this.value; }).get().join(',');

    showSend(ids);
    return false;
}

function showEdit(planid,plantype) {
    //window.location.href = "/Views/PlanManage/PlanEdit.aspx?id=" + id;
    var feature1 = 'width=820px;height=600px,toolbar=no, menubar=no,scrollbars=yes,resizable=yes,location=no,status=yes,';
    var feature2 = 'width=500px;toolbar=no, menubar=no,scrollbars=no,resizable=yes,location=no,status=yes,';
    switch (plantype)
        {
            case "YJJH":
            //window.open("/Views/PlanManage/YJJHEdit.aspx?id=" + planid ,"",feature2);
            window.location.href = "/Views/PlanManage/YJJHEdit.aspx?id=" + planid;
                break;
            case "XXXQ":
            //window.open("/Views/PlanManage/XXXQEdit.aspx?id=" + planid ,"",feature1);
            window.location.href = "/Views/PlanManage/XXXQEdit.aspx?id=" + planid;
                break;
            case "MBXQ":
            //window.open("/Views/PlanManage/MBXQEdit.aspx?id=" + planid ,"",feature1);
            window.location.href = "/Views/PlanManage/MBXQEdit.aspx?id=" + planid;
                break;
            case "HJXQ":
            //window.open("/Views/PlanManage/HJXQEdit.aspx?id=" + planid ,"",feature1);
            window.location.href = "/Views/PlanManage/HJXQEdit.aspx?id=" + planid;
                break;
            case "DMJH":
            //window.open("/Views/PlanManage/DMJHEdit.aspx?id=" + planid ,"",feature1);
            window.location.href = "/Views/PlanManage/DMJHEdit.aspx?id=" + planid;
                break;
            case "ZXJH":
            //window.open("/Views/PlanManage/ZXJHEdit.aspx?id=" + planid ,"",feature1);
            window.location.href = "/Views/PlanManage/ZXJHEdit.aspx?id=" + planid;
                break;
            case "TYSJ":
            //window.open("/Views/PlanManage/TYSJEdit.aspx?id=" + planid ,"",feature2);
            window.location.href = "/Views/PlanManage/TYSJEdit.aspx?id=" + planid;
                break;
        }
  // window.open("/Views/PlanManage/PlanEdit.aspx?planid=" + planid + "&infotype="+plantype,"",feature1);
    return false;
}

//function showSend(id,planid,plantype) {
function showSend(ids) {
//    var divData = $('#divData');
//    divData.hide();
//    var indicator = $('#tartgetPanel');
//    indicator.show();
    var txtId = $('#txtId'); 
    txtId.val(ids);
//    var txtPlanID = $('#txtPlanID'); 
//    txtPlanID.val(planid);
//    var txtPlanType = $('#txtPlanType'); 
//    txtPlanType.val(plantype);
    var btn = $('#btnHidden');
    btn.click();
    return false;
}

function showPopSendForm() {
    var _dialog;
    _dialog = $("#tartgetPanel");
    _dialog.dialog({
        autoOpen: false,
        height: 350,
        width: 330,
        modal: true,
        buttons: {
            '关闭': function () {
                $(this).dialog("close");
            }
        }
    });
    _dialog.dialog('open');
    _dialog.parent().appendTo($("form:first"));
    return false;
}

function callSend() {
    var btn = $('#btnSubmit');
    btn.click();
    return false;
}

