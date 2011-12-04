function resetAll() {
    $('input:text').val('');
}

function showDetail(id) {
    window.location.href = "/Views/PlanManage/ExperimentPlanDetail.aspx?id=" + id;
    return false;
}