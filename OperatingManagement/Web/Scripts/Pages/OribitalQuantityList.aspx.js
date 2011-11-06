function reset(o) {
    $('input:text').val('');
}

function showDetail(id) {
    window.location.href = "/PlanManage/OribitalQuantityDetail.aspx?id=" + id;
    return false;
}

function selectAll() {
    $('input:checkbox:not([disabled])').attr('checked', true);
    return false;
}

function sendGD1() {
    var chks = $('#tbGDs').find('input:checkbox:not([disabled])').filter('[checked=true]');
    if (chks.length == 0) {
        showMsg('请选择您要发送的轨道数据。');
        return false;
    }

    var divData = $('#divData');
    divData.hide();
    var indicator = $('#tartgetPanel');
    indicator.show();

    return false;
}
