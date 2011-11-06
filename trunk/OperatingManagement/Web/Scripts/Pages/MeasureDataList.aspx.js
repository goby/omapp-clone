function reset(o) {
    $('input:text').val('');
}

function showDetail(id) {
    window.location.href = "/PlanManage/MeasureDataDetail.aspx?id=" + id;
    return false;
}