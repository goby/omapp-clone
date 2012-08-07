var _dialog;
$(window).ready(function () {
    _dialog = $("#dialog-form");
});


function clearField() {
    //$('input:text').val('');
    $('#txtStartDate').val("");
    $('#txtEndDate').val("");
    var sal = $('#ddlType');
    sal.val("YJJH");
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
    _dialog.dialog({
        autoOpen: false,
        height: 150,
        width: 400,
        modal: true,
        buttons: {
            '关闭': function () {
                $(this).dialog("close");
            }
        }
    });

    var ddlType = $('#ddlType');
    if (ddlType.val() == "SBJH") {
        _dialog.find('p.content').eq(0).html('不能发送设备工作计划。');
        _dialog.dialog('open');
        return false;
    }
    var chks = $('#tbPlans').find('input:checkbox:[checked]');
    if (chks.length == 0) {
        _dialog.find('p.content').eq(0).html('请选择您要发送的计划。');
        _dialog.dialog('open');
        return false;
    }
    if (chks.length > 1) {
        _dialog.find('p.content').eq(0).html('只能选择一个计划进行发送。');
        _dialog.dialog('open');
        return false;
    }

    var ids = chks.map(function () { return this.value; }).get().join(',');

    showSend(ids);
    return false;
}

function showEdit(planid, plantype) {
    //window.location.href = "/Views/PlanManage/PlanEdit.aspx?id=" + id;
    var feature1 = 'width=820px;height=600px,toolbar=no, menubar=no,scrollbars=yes,resizable=yes,location=no,status=yes,';
    var feature2 = 'width=500px;toolbar=no, menubar=no,scrollbars=no,resizable=yes,location=no,status=yes,';
    switch (plantype)
        {
            case "YJJH":
            //window.open("/Views/PlanManage/YJJHEdit.aspx?id=" + planid ,"",feature2);
                window.location.href = "/Views/PlanManage/YJJHEdit.aspx?id=" + planid + "&startDate=" + $('#txtStartDate').val() + "&endDate=" + $('#txtEndDate').val();
                break;
            case "XXXQ":
                window.location.href = "/Views/PlanManage/XXXQEdit.aspx?id=" + planid + "&startDate=" + $('#txtStartDate').val() + "&endDate=" + $('#txtEndDate').val();
                break;
            case "MBXQ":
                window.location.href = "/Views/PlanManage/MBXQEdit.aspx?id=" + planid + "&startDate=" + $('#txtStartDate').val() + "&endDate=" + $('#txtEndDate').val();
                break;
            case "HJXQ":
                window.location.href = "/Views/PlanManage/HJXQEdit.aspx?id=" + planid + "&startDate=" + $('#txtStartDate').val() + "&endDate=" + $('#txtEndDate').val();
                break;
            case "DJZYSQ":
                window.location.href = "/Views/PlanManage/DJZYSQEdit.aspx?id=" + planid + "&startDate=" + $('#txtStartDate').val() + "&endDate=" + $('#txtEndDate').val();
                break;
            case "GZJH":
                window.location.href = "/Views/PlanManage/GZJHEdit.aspx?id=" + planid + "&startDate=" + $('#txtStartDate').val() + "&endDate=" + $('#txtEndDate').val();
                break;
            case "ZXJH":
                window.location.href = "/Views/PlanManage/ZXJHEdit.aspx?id=" + planid + "&startDate=" + $('#txtStartDate').val() + "&endDate=" + $('#txtEndDate').val();
                break;
            case "TYSJ":
                window.location.href = "/Views/PlanManage/TYSJEdit.aspx?id=" + planid + "&startDate=" + $('#txtStartDate').val() + "&endDate=" + $('#txtEndDate').val();
                break;
        }
  // window.open("/Views/PlanManage/PlanEdit.aspx?planid=" + planid + "&infotype="+plantype,"",feature1);
    return false;
}

//function showSend(id,planid,plantype) {
function showSend(ids) {
    var txtId = $('#txtId'); 
    txtId.val(ids);

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
            '确定': function () {
                var list = $('#tartgetPanel').find('input:checkbox:[checked]');
                if (list.length == 0) {
                    $('#lblTargetMessage').show();
                }
                else {
                    $('#lblTargetMessage').hide();
                    $(this).dialog("close");
                    var btn = $('#btnSubmit');
                    btn.click();
                }
            },
            '取消': function () {
                $(this).dialog("close");
            }
    }
});
    _dialog.dialog('open');
    _dialog.parent().appendTo($("form:first"));
    return false;
}
