$.extend($.fn.dataTableExt.oStdClasses, {
    sSortAsc: "header headerSortDown",
    sSortDesc: "header headerSortUp",
    sSortable: "header"
});
$.fn.dataTableExt.oApi.fnPagingInfo = function (a) {
    return {
        iStart: a._iDisplayStart,
        iEnd: a.fnDisplayEnd(),
        iLength: a._iDisplayLength,
        iTotal: a.fnRecordsTotal(),
        iFilteredTotal: a.fnRecordsDisplay(),
        iPage: Math.ceil(a._iDisplayStart / a._iDisplayLength),
        iTotalPages: Math.ceil(a.fnRecordsDisplay() / a._iDisplayLength)
    };
};

$.extend($.fn.dataTableExt.oPagination, {
    bootstrap: {
        fnInit: function (e, b, d) {
            var a = e.oLanguage.oPaginate;
            var f = function (g) {
                g.preventDefault();
                if (e.oApi._fnPageChange(e, g.data.action)) {
                    d(e);
                }
            };

            $(b).addClass("pagination").append('<ul><li class="prev disabled"><a href="#">&larr;</a></li><li class="next disabled"><a href="#">&rarr; </a></li></ul>');
            var c = $("a", b);
            $(c[0]).bind("click.DT", {
                action: "previous"
            }, f);
            $(c[1]).bind("click.DT", {
                action: "next"
            }, f);
        },
        fnUpdate: function (c, k) {
            var l = 5;
            var e = c.oInstance.fnPagingInfo();
            var h = c.aanFeatures.p;
            var g, f, d, a, m, b = Math.floor(l / 2);
            if (e.iTotalPages < l) {
                a = 1;
                m = e.iTotalPages;
            } else {
                if (e.iPage <= b) {
                    a = 1;
                    m = l;
                } else {
                    if (e.iPage >= (e.iTotalPages - b)) {
                        a = e.iTotalPages - l + 1;
                        m = e.iTotalPages
                    } else {
                        a = e.iPage - b + 1;
                        m = a + l - 1
                    }
                }
            }
            for (g = 0, iLen = h.length; g < iLen; g++) {
                $("li:gt(0)", h[g]).filter(":not(:last)").remove();
                for (f = a; f <= m; f++) {
                    d = (f == e.iPage + 1) ? 'class="active"' : "";
                    $("<li " + d + '><a href="#">' + f + "</a></li>").insertBefore($("li:last", h[g])[0]).bind("click", function (i) {
                        i.preventDefault();
                        c._iDisplayStart = (parseInt($("a", this).text(), 10) - 1) * e.iLength;
                        k(c)
                    })
                }
                if (e.iPage === 0) {
                    $("li:first", h[g]).addClass("disabled")
                } else {
                    $("li:first", h[g]).removeClass("disabled")
                }
                if (e.iPage === e.iTotalPages - 1 || e.iTotalPages === 0) {
                    $("li:last", h[g]).addClass("disabled")
                } else {
                    $("li:last", h[g]).removeClass("disabled")
                }
            }
        }
    }
});

$(".utopia-check-all").click(function () {
    $(this).closest(".table").find("input:checkbox").attr("checked", $(this).is(":checked"))
});