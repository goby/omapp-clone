function reset(o) {
    $('input:text').val('');
}

function showDetail(id) {
    window.location.href = "/PlanManage/YDSJDetail.aspx?id=" + id;
    return false;
}

function selectAll() {
    $('input:checkbox:not([disabled])').attr('checked', true);
    return false;
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
