function sendFiles() {
    var _dialog;
    _dialog = $("#dialog-form");

    var datatype = $('#ddlDataType').val();
    var chks_yc;
    var chks_uf;
    var chks_fz;
    //TJMB_Data
    if (datatype == "0") {
        chks_yc = $('#tbYCData').find('input:checkbox:not([disabled])').filter('[checked=checked]');
        chks_uf = $('#tbUFData').find('input:checkbox:not([disabled])').filter('[checked=checked]');
    }
    //KJJD_Data
    if (datatype == '1')
        chks_yc = $('#tbYCData').find('input:checkbox:not([disabled])').filter('[checked=checked]');
    //FZTY_Data
    if (datatype == '2')
        chks_fz = $('#tbFZData').find('input:checkbox:not([disabled])').filter('[checked=checked]');

    if ((chks_yc == null || chks_yc.length == 0) && (chks_fz == null ||chks_fz.length == 0) && (chks_uf == null || chks_uf.length == 0)) {
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
        _dialog.find('p.content').eq(0).html('请选择需要发送的试验数据。');
        _dialog.dialog('open');
        return false;
    }

    var ycids;
    if (chks_yc != null) {
        ycids = chks_yc.map(function () { return this.value; }).get().join(',');
        $('#hfycids').attr("value", "");
    }
    else
        $('#hfycids').val = "";
    var ufids;
    if (chks_uf != null) {
        ufids = chks_uf.map(function () { return this.value; }).get().join(',');
        $('#hfufids').val = ufids;
    }
    else
        $('#hfufids').attr("value", "");
    var fzids;
    if (chks_fz != null) {
        fzids = chks_fz.map(function () { return this.value; }).get().join(',');
        $('#hffzids').attr("value", fzids);
    }
    else
        $('#hffzids').attr("value", "");
    return true;
}