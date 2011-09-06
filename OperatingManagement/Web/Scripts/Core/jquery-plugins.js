///<reference path="../jquery-1.4.1-vsdoc.js" />
$.extend($.fn, {
    modal: function (settings) {
        var ps = $.extend({
            width: 400,
            height: 150,
            title: '',
            borderWidth: 1,
            okText: '确定',
            cancelText: '取消',
            content: function (obj, arg) { },
            okEvent: false,
            shadowRadiu: { x: 10, y: 8 }
        }, settings);
        var me = this;
        me.hideModal = function () {
            me.cache.modal.fadeOut(500, function () {
                me.cache.layer.hide();
                $('body').css('overflow', '');
                $('select').show();
            });
            me.cache.shadow.hide();
        };
        if (!me.cache || !me.cache.modal) {
            var html = '<div class="jrbx_layer" />\
                        <div class="jrbx_modal">\
                            <div class="jrbx_modal_title" />\
                            <div class="jrbx_modal_content" />\
                            <div class="jrbx_modal_footer">\
                                <button disabled></button>&nbsp;&nbsp;<button></button>\
                            </div>\
                        </div>\
                        <div class="jrbx_modal_shadow" />';
            me.doms = $(html);
            me.doms.appendTo('body:eq(0)');
            me.cache = {
                layer: me.doms.filter('.jrbx_layer').eq(0).css('opacity', .1),
                modal: me.doms.filter('.jrbx_modal').eq(0).fadeOut(),
                shadow: me.doms.filter('.jrbx_modal_shadow').eq(0).css('opacity', .2).hide()
            };
            me.cache.modal.title = me.cache.modal.find('.jrbx_modal_title').eq(0);
            me.cache.modal.content = me.cache.modal.find('.jrbx_modal_content').eq(0);
            me.cache.modal.footer = me.cache.modal.find('.jrbx_modal_footer').eq(0);
            me.cache.modal.btn1 = me.cache.modal.footer.find('button:eq(0)');
            me.cache.modal.btn2 = me.cache.modal.footer.find('button:eq(1)').click(function () { me.hideModal() });
        }
        me.settings = { width: document.body.scrollWidth, height: document.body.scrollHeight };
        ps.width = ps.width < 1 ? ps.width * me.settings.width : ps.width;
        ps.height = ps.height < 1 ? ps.height * me.settings.height : ps.height;
        var modal = me.cache.modal,
            l = (me.settings.width - ps.width - ps.borderWidth) / 2,
            t = (me.settings.height - ps.height - ps.borderWidth) / 3,
            arg = {
                close: function () {
                    me.hideModal();
                },
                setEnable: function () {
                    modal.btn1.removeAttr('disabled');
                },
                isCancel: false
            };

        modal.btn2.text(ps.cancelText);
        if (ps.okEvent) {
            modal.btn1.unbind('click').click(function () {
                ps.okEvent(modal, arg);
                if (!arg.isCancel) {
                    me.hideModal();
                }
            }).text(ps.okText).show();
        } else {
            modal.btn1.hide();
        }
        modal.title.html(ps.title);
        me.cache.layer.css({
            width: me.settings.width,
            height: me.settings.height
        }).show();
        var mdCss = {
            width: ps.width,
            height: ps.height,
            borderWidth: ps.borderWidth,
            left: l,
            top: t
        };
        modal.css(mdCss).fadeIn(500, function () {
            me.cache.shadow.css(mdCss).show().animate({
                left: l + ps.shadowRadiu.x,
                top: t + ps.shadowRadiu.y
            });
        });
        $('select').hide();
        $('body').css('overflow', 'hidden');
        modal.content.css({ width: ps.width - 22, height: ps.height - 77 });
        modal.content.html('');
        ps.content(modal, arg);
    }
});