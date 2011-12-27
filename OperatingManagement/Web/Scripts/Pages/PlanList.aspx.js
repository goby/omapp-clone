function reset(o) {
    $('input:text').val('');
    return false;
}

function showMsg(msg) {
    $.fn.modal({
        title: '提示信息',
        content: function (o, e) {
            o.content.html(msg);
        },
        cancelText: '关闭',
    });
}

function showDetail(planid,plantype) {
    //window.location.href = "/Views/PlanManage/PlanDetail.aspx?id=" + id;
    var feature1 = 'width=820px;height=700px,toolbar=no, menubar=no,scrollbars=yes,resizable=yes,location=no,status=yes,';
    var feature2 = 'width=500px;height=300px,toolbar=no, menubar=no,scrollbars=no,resizable=yes,location=no,status=yes,';
        switch (plantype)
        {
            case "YJJH":
            window.open("/Views/PlanManage/YJJHEdit.aspx?id=" + planid + "&op=detail","",feature2);
                break;
            case "XXXQ":
            window.open("/Views/PlanManage/XXXQEdit.aspx?id=" + planid + "&op=detail","",feature1);
                break;
            case "MBXQ":
            window.open("/Views/PlanManage/MBXQEdit.aspx?id=" + planid + "&op=detail","",feature1);
                break;
            case "HJXQ":
            window.open("/Views/PlanManage/HJXQEdit.aspx?id=" + planid + "&op=detail","",feature1);
                break;
            case "DMJH":
            window.open("/Views/PlanManage/DMJHEdit.aspx?id=" + planid + "&op=detail","",feature1);
                break;
            case "ZXJH":
            window.open("/Views/PlanManage/ZXJHEdit.aspx?id=" + planid + "&op=detail","",feature1);
                break;
            case "TYSJ":
            window.open("/Views/PlanManage/TYSJEdit.aspx?id=" + planid + "&op=detail","",feature2);
                break;
        }
    //window.open("/Views/PlanManage/PlanDetail.aspx?id=" + planid);
    return false;
}

function showEdit(planid,plantype) {
    //window.location.href = "/Views/PlanManage/PlanEdit.aspx?id=" + id;
    var feature1 = 'width=820px;height=600px,toolbar=no, menubar=no,scrollbars=yes,resizable=yes,location=no,status=yes,';
    var feature2 = 'width=500px;toolbar=no, menubar=no,scrollbars=no,resizable=yes,location=no,status=yes,';
    switch (plantype)
        {
            case "YJJH":
            window.open("/Views/PlanManage/YJJHEdit.aspx?id=" + planid ,"",feature2);
                break;
            case "XXXQ":
            window.open("/Views/PlanManage/XXXQEdit.aspx?id=" + planid ,"",feature1);
                break;
            case "MBXQ":
            window.open("/Views/PlanManage/MBXQEdit.aspx?id=" + planid ,"",feature1);
                break;
            case "HJXQ":
            window.open("/Views/PlanManage/HJXQEdit.aspx?id=" + planid ,"",feature1);
                break;
            case "DMJH":
            window.open("/Views/PlanManage/DMJHEdit.aspx?id=" + planid ,"",feature1);
                break;
            case "ZXJH":
            window.open("/Views/PlanManage/ZXJHEdit.aspx?id=" + planid ,"",feature1);
                break;
            case "TYSJ":
            window.open("/Views/PlanManage/TYSJEdit.aspx?id=" + planid ,"",feature2);
                break;
        }
  // window.open("/Views/PlanManage/PlanEdit.aspx?planid=" + planid + "&infotype="+plantype,"",feature1);
    return false;
}

function showSend(id,planid,plantype) {
//    var divData = $('#divData');
//    divData.hide();
//    var indicator = $('#tartgetPanel');
//    indicator.show();
    var txtId = $('#txtId'); 
    txtId.val(id);
    var txtPlanID = $('#txtPlanID'); 
    txtPlanID.val(planid);
    var txtPlanType = $('#txtPlanType'); 
    txtPlanType.val(plantype);
    var btn = $('#btnHidden');
    btn.click();
    return false;
}

