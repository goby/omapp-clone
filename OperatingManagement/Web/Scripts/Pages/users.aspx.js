///<reference path="../jquery-1.4.1-vsdoc.js" />
function showMsg(msg) {
    $.fn.modal({
        title: '提示信息',
        content: function (o, e) {
            o.content.html(msg);
        },
        cancelText: '关闭'
    });
}
function selectAll() {
    $('input:checkbox:not([disabled])').attr('checked', true);
    return false;
}
function checkAll(o) {
    $('#tbUsers').find('input:checkbox:not([disabled])').attr('checked', o.checked);
}
function editUser(id) {
    window.location.href = "/views/userandrole/useredit.aspx?id=" + id;
    return false;
}
function deleteUsers() {
    var chks = $('#tbUsers').find('input:checkbox:not([disabled])').filter('[checked=true]');
    if (chks.length == 0) {
        showMsg('请选择您要删除的用户。');
        return false;
    }
    $.fn.modal({
        title: '警  告',
        content: function (o, e) {
            o.content.html('<span class="red">删除指定的用户后将不可恢复，确定要继续吗？</span>');
            e.setEnable();
        },
        okEvent: function (o, e) {
            //ajax begin
            var ids = chks.map(function () { return this.value; }).get().join(',');
            var indicator = $('#submitIndicator');
            indicator.show();
            /*
            $.ajax({
                url: 'data.axd',
                dataType: 'text',
                data: {
                    ids: ids,
                    action: 'userdelete',
                    t: Math.random()
                },
                error: function (resp) {
                    showMsg('数据提交过程中发生了异常。');
                    indicator.hide();
                },
                success: function (resp) {
                    var json = eval('(' + resp + ')');
                    showMsg(json.msg);
                    if (json.suc) {
                        window.location.href = window.location.href;
                    }
                    indicator.hide();
                }
            });
            */
            //ajax end
        }
    });
    return false;
}
