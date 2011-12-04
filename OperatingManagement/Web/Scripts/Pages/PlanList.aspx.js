function reset(o) {
    $('input:text').val('');
}

function showDetail(id) {
    window.location.href = "/Views/PlanManage/PlanDetail.aspx?id=" + id;
    return false;
}

function showEdit(id) {
    window.location.href = "/Views/PlanManage/PlanEdit.aspx?id=" + id;
    return false;
}

function showSend() {
    var divData = $('#divData');
    divData.hide();
    var indicator = $('#tartgetPanel');
    indicator.show();

    return false;
}
