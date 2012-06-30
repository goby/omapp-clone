
//$(function () {
//    $("#txtPlanStartTime").datepicker();
//    $("#txtPlanEndTime").datepicker();

//    $("#txtMBTimeSection1").datepicker({
//            dateFormat: 'yymmdd'
//		});
//    $("#txtMBTimeSection2").datepicker({
//            dateFormat: 'yymmdd'
//		});

//    $("#txtHJTimeSection1").datepicker({
//            dateFormat: 'yymmdd'
//		});
//    $("#txtHJTimeSection2").datepicker({
//            dateFormat: 'yymmdd'
//		});
//});

    function showMsg(msg) {
        var _dialog;
        _dialog = $("#dialog-form");
        _dialog.dialog({
            autoOpen: false,
            height: 150,
            width: 350,
            modal: true,
            buttons: {
                '关闭': function () {
                    $(this).dialog("close");
                }
            }
        });
        _dialog.find('p.content').eq(0).html(msg);
        _dialog.dialog('open');
        return false;
    }

function hideAllButton() {
    $(":submit").css('display','none'); 
    return false;
}

function setdayte(o){
    $(o).datepicker({
			changeMonth: true,
			changeYear: true
		});
}