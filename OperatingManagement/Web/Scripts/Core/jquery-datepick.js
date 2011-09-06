///<reference path="../core/jquery-vsdoc.js" />
/*
    copyright @ jericho 2009
    http://www.ajaxplaza.net
*/
(function ($) {
    function formatDate(f, date) {
        f = (typeof f == 'undefined' ? 'yyyy-MM-dd' : f);
        var year = date.getFullYear().toString();
        var month = (date.getMonth() + 1).toString();
        var day = date.getDate().toString();
        var ym = f.replace(/[^y|Y]/g, '');
        if (ym.length == 2) year = year.substring(2, 4);

        var mm = f.replace(/[^m|M]/g, '');
        if (mm.length > 1) {
            if (month.length == 1) {
                month = "0" + month;
            }
        }
        var dm = f.replace(/[^d]/g, '');
        if (dm.length > 1) {
            if (day.length == 1) {
                day = "0" + day;
            }
        }
        return f.replace(ym, year).replace(mm, month).replace(dm, day);
    };
    function getMonthView(year, month) {
        var ds = new Array();
        var sdw = new Date(year, month, 1).getDay();
        var edw = new Date(year, month + 1, 0).getDate();
        var prev = (month - 1 < 0 ? { y: year - 1, m: 11} : { y: year, m: month - 1 });
        var pdw = new Date(prev.y, prev.m + 1, 0).getDate();
        if (sdw == 0) { sdw = 7; }
        for (var i = 0; i < sdw; i++) {
            ds.push({ a: 'n', v: pdw - (sdw - i) + 1 });
        }
        for (var i = 0; i < edw; i++) {
            ds.push({ a: 'c', v: i + 1 });
        }
        for (var i = 0; i < 42 - edw - sdw; i++) {
            ds.push({ a: 'n', v: i + 1 });
        }
        return ds;
    };

    $.fn.datepick = function (setting) {
        var opts = $.fn.extend({
            plugCss: {},
            weeks: ['日', '一', '二', '三', '四', '五', '六'],
            year: '-10:0',
            dateFormat: 'yyyy/MM/dd',
            speed: 'fast',
            target: null
        }, setting);

        var dp = this;
        dp.date = new Date();
        dp.year = this.date.getFullYear();
        dp.month = this.date.getMonth();
        dp.today = this.date.getDate();
        dp.week = this.date.getDay();

        var datepicker = $('#jquery-datepick');
        if (datepicker.length == 0) {
            var _sb = new stringBuilder();
            $.each(opts.weeks, function (i, n) {
                _sb.append('<span>' + n + '</span>');
            });
            datepicker = $('<div id="jquery-datepick">\
                                <div class="datepick-shadow"></div>\
                                <div class="datepick-ctnx">\
                                    <div class="datepick-title">\
                                        <a href="javascript:void(0)" class="datepick-prev" title="上个月"></a>\
                                        <div class="datepick-current"></div>\
                                        <a href="javascript:void(0)" class="datepick-next" title="下个月"></a>\
                                    </div>\
                                    <div class="datepick-body">\
                                        <div class="datepick-weeks">' +
                                            _sb.toString() +
                                        '</div>\
                                        <table class="datepick-main" cellspacing="0" cellpadding="0" ></table>\
                                    </div>\
                                </div>\
                            </div>').css(opts.plugCss).hide().appendTo('body:eq(0)');

        }
        var _dps = {
            prev: datepicker.find('.datepick-prev').eq(0),
            current: datepicker.find('.datepick-current').eq(0),
            next: datepicker.find('.datepick-next').eq(0),
            body: datepicker.find('.datepick-main').eq(0),
            shadow: datepicker.find('.datepick-shadow').eq(0)
        };
        _dps.shadow.hide();
        function hide() {
            if (datepicker.length > 0 && datepicker.is(':visible')) {
                _dps.shadow.hide();
                datepicker.fadeOut(opts.speed, function () {
                    $('select[id^=rpt]').show();
                });
            }
        }

        $(document).bind('mousedown', function (e) {
            if (typeof e.target != 'undefined' && (e.target.id == 'selDpYear' || e.target.id == 'selDpMonth'))
                return;
            if (e.pageX + document.documentElement.scrollLeft > datepicker.offset().left + datepicker.width() ||
                e.pageX + document.documentElement.scrollLeft < datepicker.offset().left ||
                e.pageY + document.documentElement.scrollTop > datepicker.offset().top + datepicker.height() ||
                e.pageY + document.documentElement.scrollTop < datepicker.offset().top
                ) {
                hide();
            }
        });
        function bindDate(a) {
            var _date = _dps.current.data('datepicker-date');
            var _ndate = { y: 1, m: 1 };
            switch (a) {
                case 'p':
                    if (_date.m - 1 <= 0) {
                        _ndate = { m: 12, y: _date.y - 1 };
                    }
                    else {
                        _ndate = { m: _date.m - 1, y: _date.y };
                    }
                    break;
                case 'n':
                    if (_date.m + 1 > 12) {
                        _ndate = { m: 1, y: _date.y + 1 };
                    }
                    else {
                        _ndate = { m: _date.m + 1, y: _date.y };
                    }
                    break;
                case 'c':
                    _ndate = _date;
                    break;
            }
            _dps.current.data('datepicker-date', _ndate);
            _dps.current.text('{0}年{1}月'.format(_ndate.y, _ndate.m));
            var _m = $(this);
            var _ds = getMonthView(_ndate.y, _ndate.m - 1);

            var _sb = new stringBuilder();
            for (var i = 0; i < _ds.length; i++) {
                if (i == 0 || i % 7 == 0) {
                    _sb.append('<tr>');
                }

                if (_ds[i].a == 'c') {
                    _sb.append('<td><a href="javascript:void(0);" title="' +
                        _ndate.y + '年' + _ndate.m + '月' + _ds[i].v + '日' + '  星期' + opts.weeks[i % 7]
                    + '" class="days' + (
                        (_ndate.y == dp.year && _ndate.m == dp.month + 1 && _ds[i].v == dp.today) ? ' today' : ''
                        ) + '">' + _ds[i].v + '</a></td>');
                } else {
                    _sb.append('<td><a class="otherDay" href="javascript:void(0);">' + _ds[i].v + '</a></td>');
                }
                if ((i + 1) % 7 == 0) {
                    _sb.append('</tr>');
                }
            }
            _dps.body.html(_sb.toString());
            _dps.body.find('a.days').click(function () {
                var _tt = $(this);
                hide();
                _m.val(formatDate(opts.dateFormat,
                                new Date(_ndate.y, _ndate.m - 1, parseInt(_tt.text()))));
                _tt.blur();
            });
        };
        return this.each(function () {
            var me = this;
            opts.target = (opts.target ? opts.target : me);
            var minusYear, plusYear;
            opts.year.replace(/\-(\d{0,2}):(\d{0,2})/g, function () {
                minusYear = parseFloat(arguments[1]);
                plusYear = parseFloat(arguments[2]);
            });

            this.startYear = dp.year - minusYear;
            this.endYear = dp.year + plusYear;

            $(this).click(function (e) {
                var _me = this, _ctrl = $(opts.target);
                _dps.prev.unbind('click').bind('click', function () {
                    bindDate.call(_ctrl, 'p');
                    this.blur();
                });
                _dps.next.unbind('click').bind('click', function () {
                    bindDate.call(_ctrl, 'n');
                    this.blur();
                });
                e.stopPropagation();
                var v = $.trim(_ctrl.val());
                var y = dp.year;
                var m = dp.month + 1;

                if (v != '') {
                    var r = opts.dateFormat.replace(/(dd)/g, '(\\d{2})', false)
                                           .replace(/(yyyy|yy)/g, '(\\d{2,4})', false)
                                           .replace(/(mm|MM|m|M)/g, '(\\d{1,2})', false)
                    var reg = new RegExp(r, 'g');

                    v.replace(reg, function () {
                        var a = arguments;
                        y = parseFloat(a[1]);
                        m = parseFloat(a[2]);
                    });
                }
                datepicker.css({
                    left: _ctrl.offset().left,
                    top: _ctrl.offset().top + _ctrl.innerHeight() + 5
                }).fadeIn(opts.speed, function () {
                    _dps.shadow.show();
                });
                $('select[id^=rpt]').hide();
                _dps.current.data('datepicker-date', { y: y, m: m });
                bindDate.call(_ctrl, 'c');
            });
        });
    }
})(jQuery);