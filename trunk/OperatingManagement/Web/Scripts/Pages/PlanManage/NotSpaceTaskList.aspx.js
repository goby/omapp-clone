$(function () {
    $("#txtStartDate").datepicker();
    $("#txtEndDate").datepicker();
});

function reset(o) {
    $('input:text').val('');
}

function setdayte(o) {
    $(o).datepicker({
        changeMonth: true,
        changeYear: true
    });
}

function showDetail(id) {
    var feature2 = 'width=700px;height=200px,toolbar=no, menubar=no,scrollbars=no,resizable=yes,location=no,status=yes,';
    //window.location.href = "/Views/PlanManage/OribitalQuantityDetail.aspx?id=" + id;
    window.open("/Views/PlanManage/OribitalQuantityDetail.aspx?id=" + id, "", feature2);
    return false;
}

function selectAll() {
    $('input:checkbox:not([disabled])').attr('checked', true);
    return false;
}

function checkAll(o) {
    if ($('#tbYDSJs').length > 0) {
        $('#tbYDSJs').find('input:checkbox:not([disabled])').attr('checked', o.checked);
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

function sendYDSJ1() {
    //var chks = $('#tbYDSJs').find('input:checkbox:not([disabled])').filter('[checked=true]');
    var chks = $('#tbYDSJs').find('input:checkbox:[checked]');
    if (chks.length == 0) {
        showMsg('请选择您要发送的引导数据。');
        return false;
    }

    var divData = $('#divData');
    divData.hide();
    var indicator = $('#tartgetPanel');
    indicator.show();

    return false;
}

function showSelectAll() {
    var divselectAll1 = $('#selectAll1');
    divselectAll1.show();
    var divselectAll12 = $('#selectAll2');
    divselectAll12.show();

    return false;
}

function hideSelectAll() {
    var divselectAll1 = $('#selectAll1');
    divselectAll1.hide();
    var divselectAll12 = $('#selectAll2');
    divselectAll12.hide();

    return false;
}
