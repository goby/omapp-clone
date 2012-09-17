$(function () {

    $("#rblType").change(function () {
        //var rbvalue = $("input[name=rblType]:radio:checked").val();
        var rbvalue = $('#rblType input:checked').val();
        var scope = $('#txtScope');


        if (rbvalue == "4") {
            scope.val('是,否');
        } else {

        }
    });

});

