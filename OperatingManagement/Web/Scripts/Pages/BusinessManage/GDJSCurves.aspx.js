var Request = { QueryString: function (key) {
    var svalue = window.location.search.match(new RegExp("[\?\&]" + key + "=([^\&]*)(\&?)", "i"));
    return svalue ? svalue[1] : svalue;
}
};

$(window).ready(function () {
    $.ajax({
        url: 'GDJSDataType.xml',
        dataType: 'xml',
        error: function (xml) {
            alert('Cant load xml: ' + xml.responseText);
        },
        success: function (xml) {
            // wrap datas.
            window.rvalues = window.rvalues || {};
            var keys = [];
            $('result', xml).each(function () {
                var self = $(this);
                keys.push(self.attr('name'));
                var children = self.children('data');
                var datas = [];
                for (var i = 0; i < children.length; i++) {
                    datas.push(children[i].getAttribute('name'));
                }
                //get values
                rvalues[self.attr('value')] = datas;
            });

            // init selects.
            window.selectResult = $('#resulttype');
            window.selectData = $('#dataname');

            var values = [];
            for (var key in window.rvalues) {
                values.push(key);
            }

            initOptions(selectResult, keys, values);

            selectOnChanged(values[0]);
            selectResult.bind('change', function (e) {
                selectOnChanged($(this).val());
            })

            selectData.bind('change', function (e) {
                dataSelectOnChanged();
            })
            //Get request value of filetype(ft)
            var ft = Request.QueryString("ft");
            if (ft != null) {
                if (rvalues[ft] != null) {
                    $('#resulttype').attr('value', ft);
                    selectOnChanged(ft);
                }
            }

        }
    })
});


function renderOptions(select, keys) {

    var html = [];
    for (var i = 0; i < keys.length; i++) {
        html.push('<option value="' + keys[i] + '" >' + keys[i] + '</option>');
    }
    select.html(html.join(''))

}

function initOptions(select, keys, values) {
    var html = [];
    for (var i = 0; i < keys.length; i++) {
        html.push('<option value="' + values[i] + '" >' + keys[i] + '</option>');
    }
    select.html(html.join(''))

}

function selectOnChanged(key) {
    var keys = rvalues[key];
    if (keys) {
        $('#hdResultType').attr('value', key);
        renderOptions(selectData, keys);
        $('#hdDataType').attr('value', keys[0]);
    }

    function dataSelectOnChanged() {
        $('#hdDataType').attr('value', $('#dataname').val());
    }

}