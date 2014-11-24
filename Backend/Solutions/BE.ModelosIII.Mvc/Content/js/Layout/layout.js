/// <reference path="../common.js" />

$(function () {
    initializeForms();

    $(".utcToLocaleDate").each(function () {
        var dateVal = moment.utc($(this).data("utc"));
        var output = dateVal.local().format("DD/MM/YYYY HH:mm:ss");
        $(this).text(output)
    });
});
