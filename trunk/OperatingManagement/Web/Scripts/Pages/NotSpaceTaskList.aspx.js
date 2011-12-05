function reset(o) {
    $('input:text').val('');
}

function showDetail(id) {
    window.location.href = "/Views/PlanManage/YDSJDetail.aspx?id=" + id;
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

function sendYDSJ1() {
    var chks = $('#tbYDSJs').find('input:checkbox:not([disabled])').filter('[checked=true]');
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
