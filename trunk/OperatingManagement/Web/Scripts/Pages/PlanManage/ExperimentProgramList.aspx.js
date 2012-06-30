//$(function () {
//    $("#txtStartDate").datepicker();
//    $("#txtEndDate").datepicker();
//});

function reset(o) {
    $('input:text').val('');
    return false;
}

function showDetail(id) {
    //window.location.href = "/Views/PlanManage/ExperimentProgramDetail.aspx?id=" + id;
    window.open("/Views/PlanManage/ExperimentProgramDetail.aspx?id=" + id);
    return false;
}

function generatePlans(id) {
    //window.location.href = "/Views/PlanManage/GenaratedPlanList.aspx?id=" + id;
    window.open("/Views/PlanManage/GenaratedPlanList.aspx?id=" + id);
    return false;
}