function reset(o) {
    $('input:text').val('');
}

function showDetail(id) {
    window.location.href = "/Views/PlanManage/ExperimentProgramDetail.aspx?id=" + id;
    return false;
}

function generatePlans(id) {
    return true;
}