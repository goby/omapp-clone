function reset(o) {
    $('input:text').val('');
}

function showDetail(id) {
    window.location.href = "/PlanManage/ExperimentProgramDetail.aspx?id=" + id;
    return false;
}

function generatePlans(id) {
    return true;
}