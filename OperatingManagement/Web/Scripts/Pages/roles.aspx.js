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
function editRole(id) {
    window.location.href = "/views/userandrole/roleedit.aspx?id=" + id;
    return false;
}
function deleteRoles() {
    var chks = $('#tbRoles').find('input:checkbox:not([disabled])').filter('[checked=true]');
    if (chks.length == 0) {
        showMsg('请选择您要删除的角色。');
        return false;
    }
    $.fn.modal({
        title: '警  告',
        content: function (o, e) {
            o.content.html('<span class="red">删除指定的角色后将不可恢复，且将会移除与用户的关联关系，确定要继续吗？</span>');
            e.setEnable();
        },
        okEvent: function (o, e) {
            //ajax begin
            var ids = chks.map(function () { return this.value; }).get().join(',');
            var indicator = $('#submitIndicator').attr('class', 'load');
            indicator.show();

            $.ajax({
                url: 'data.axd',
                dataType: 'text',
                data: {
                    ids: ids,
                    action: 'deleteRolesByIds',
                    t: Math.random()
                },
                error: function (resp) {
                    indicator.attr('class', 'error').html('数据提交过程中发生了异常。');
                },
                success: function (resp) {
                    var json = eval('(' + resp + ')');
                    if (json.suc) {
                        window.location.href = window.location.href;
                    } else {
                        indicator.attr('class', 'error').html('数据提交过程中发生了异常。');
                    }
                }
            });

            //ajax end
        }
    });
    return false;
}
