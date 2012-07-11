
//$(function () {
//    $("#txtStartDate").datepicker();
//    $("#txtEndDate").datepicker();
//});


function clearField() {
    $('input:text').val('');
    return false;
}

function showDetail(id) {
    window.open("/Views/PlanManage/ExperimentPlanDetail.aspx?id=" + id);
    return false;
}

