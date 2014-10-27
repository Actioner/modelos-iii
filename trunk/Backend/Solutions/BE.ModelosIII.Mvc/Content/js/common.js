$(function () {
    Globalize.culture("es-AR");

    jQuery.ajaxSettings.traditional = true;
//    $(document).ajaxError(function (event, jqXHR) {
//        if (jqXHR.readyState == 0 || jqXHR.status == 0) {
//            return; //Skip this error  
//        }
//        if (401 === jqXHR.status) {
//            window.location.reload();
//        }
//        else {
//            alert("Se ha producido un error. Contacte al administrador. Error: " + jqXHR.statusText);
//        }
//    });
});

var showErrorsStyled = function () {
    $('.field-validation-error').parents('.control-group').addClass('error');
    $('.field-validation-valid').parents('.control-group').removeClass('error');
};

var initializeForms = function () {
    $('form').each(function () {
        if (typeof ($(this).data("validator")) == "undefined") {
            $.validator.unobtrusive.parse(this);
        }
        $(this).data("validator").settings.showErrors = function (errorMap, errorList) {
            this.defaultShowErrors();
            showErrorsStyled();
        };
    });
    showErrorsStyled();
};

var initializeSelect = function (jQuerySelector, options) {
    var defaults = { allowClear: true };
    
    /* merge defaults and options, without modifying defaults */
    var settings = $.extend({}, defaults, options);

    $(jQuerySelector).select2(settings);
};

var initializeEditor = function (jQuerySelector, options) {
    var defaults = { width: 600 };

    /* merge defaults and options, without modifying defaults */
    var settings = $.extend({}, defaults, options);
    
    $(jQuerySelector).cleditor(settings);
};

var initializeDataTableOnLoad = function (jQuerySelector, options, eventdelete) {
    var defaults = { "aaSorting": [] };
    /* merge defaults and options, without modifying defaults */
    var settings = $.extend({}, defaults, options);

    $(jQuerySelector).dataTable({
        sDom: "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
        bJQueryUI: true,
        bRetrieve: true,
        bDestroy: true,
        aaSorting: settings["aaSorting"],
        aoColumnDefs: settings["aoColumnDefs"],
        fnDrawCallback: function (oSettings) {

            $(".actions a").tooltip("hide");

            if (eventdelete == null || eventdelete == undefined)
                $(".actions .delete").click(function (ev) {
                    var self = this;
                    ev.preventDefault();
                    jConfirm("¿Seguro que desea borrar?", "Confirmación de borrado", function (result) {
                        if (result) {
                            window.location = $(self).attr('href');
                        }
                    });
                    return false;
                });
                if (settings["fnDrawCallback"] != undefined) {
                    settings["fnDrawCallback"]();
                }
        },
        sPaginationType: "bootstrap",
        oLanguage: {
            sUrl: '/Content/datatable/datatable.es-AR.txt'
        }
    });
};

var initializeBindings = function () {
    $(".fancyLauncher").unbind('click');
    $(".fancyLauncher").click(function (ev) {
        ev.preventDefault();
        loadBox($(this).attr('href'));
    });

    $(".fancyLauncherMedia").unbind('click');
    $(".fancyLauncherMedia").click(function (ev) {
        ev.preventDefault();
        loadMediaBox($(this).attr('href'));
    });

    $(".fancyClose").unbind('click');
    $('.fancyClose').click(function () {
        $.fancybox.close();
    });
};

var loadBox = function (url) {
    $.fancybox({
        type: 'ajax',
        href: url,
        afterShow: function (current, previous) {
            initializeBindings();
            initializeForms();
            return true;
        }
    });
};

var loadMediaBox = function (url) {
    $.fancybox({
        height: 405,
        margin:100,
        padding:10,
        href: url,
        helpers: {
            media: {}
        },
        youtube: {
            wmode: 'opaque'
        }
    });
};

var getYoutubeVideoId = function (url) {
    var regExp = /^.*((youtu.be\/)|(v\/)|(\/u\/\w\/)|(embed\/)|(watch\?))\??v?=?([^#\&\?]*).*/;
    var match = url.match(regExp);
    if (match && match[7].length == 11) {
        return match[7];
    } else {
        return '';
    }
};

var ajaxFormPostSuccess = function (result) {
    if (result.url) {
        window.location = result.url;
    }

    initializeForms();
    initializeBindings();
};

var createHTTPRequestObject = function () {
    // although IE supports the XMLHttpRequest object, but it does not work on local files.
    var forceActiveX = (window.ActiveXObject && location.protocol === "file:");
    if (window.XMLHttpRequest && !forceActiveX) {
        return new XMLHttpRequest();
    } else {
        try {
            return new ActiveXObject("Microsoft.XMLHTTP");
        } catch (e) {
        }
    }
    alert("Your browser doesn't support XML handling!");
    return null;
};

var Cache = function () {
    var items = {};

    this.get = function (key) {
        return items[key];
    };

    this.set = function (key, value) {
        items[key] = value;
    };

    this.remove = function (key) {
        var item = this.get(key);
        delete items[key];
        return item;
    };

    this.keys = function () {
        var ret = [], p;
        for (p in this.items) {
            ret.push(p);
        }
        return ret;
    };

    this.clear = function () {
        var keys = this.keys();
        for (var i = 0; i < keys.length; i++) {
            this.remove(keys[i]);
        }
    };
};

var submitDynamicForm = function (url, params) {
    var form = ['<form method="POST" action="', url, '">'];
    for (var key in params) {
        form.push('<input type="hidden" name="', key, '" value="', params[key], '"/>');
    }

    form.push('</form>');
    jQuery(form.join('')).appendTo('body')[0].submit();
};

function isFunction(functionToCheck) {
    var getType = {};
    return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
}


