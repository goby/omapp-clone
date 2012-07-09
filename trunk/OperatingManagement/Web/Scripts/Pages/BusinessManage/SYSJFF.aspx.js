function SendFile() {
    //return true;
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
                '确定': function () {
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
        $('#hfycids').val(ycids);
    }
    else
        $('#hfycids').val("");
    var ufids;
    if (chks_uf != null) {
        ufids = chks_uf.map(function () { return this.value; }).get().join(',');
        $('#hfufids').val(ufids);
    }
    else
        $('#hfufids').val("");
    var fzids;
    if (chks_fz != null) {
        fzids = chks_fz.map(function () { return this.value; }).get().join(',');
        $('#hffzids').val(fzids);
    }
    else
        $('#hffzids').val("");

    //return true;
    var _Senddialog;
    _Senddialog = $("#SendPanel");
    _Senddialog.dialog({
        autoOpen: false,
        height: 180,
        width: 350,
        modal: true,
        buttons: {
            '确定': function () {
                var _radios = $('#rblProtocol').find('input');
                var selVal = _radios.filter('[checked=checked]');
                var sendway = selVal.attr('value');
                $('#hfsendway').val(sendway);
                $(this).dialog("close");
                var btn = $('#btnHidden');
                btn.click();
            },
                '取消': function () {
                    $(this).dialog("close");
                }
        }
    });
    _Senddialog.dialog('open');
    return false;
}
