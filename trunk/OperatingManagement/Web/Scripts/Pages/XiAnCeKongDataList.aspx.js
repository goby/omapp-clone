function reset(o) {
    $('input:text').val('');
}

function showDetail(id) {
    window.location.href = "/PlanManage/XiAnCeKongDataDetail.aspx?id=" + id;
    return false;
}