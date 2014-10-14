
var initializeFileUpload = function (selector) {
    var idSelector = selector;
    var $box = $(idSelector + " .fileBox");

    var supportAjaxUploadWithProgress = function () {
        var supportFileAPI = function () {
            var fi = document.createElement('INPUT');
            fi.type = 'file';
            return 'files' in fi;
        };

        var supportAjaxUploadProgressEvents = function () {
            var xhr = createHTTPRequestObject();
            return !!(xhr && ('upload' in xhr) && ('onprogress' in xhr.upload));
        };

        var supportFormData = function () {
            return !!window.FormData;
        };

        return supportFileAPI() && supportAjaxUploadProgressEvents() && supportFormData();
    };

    var sendXHRequest = function (formData, uri) {
        var xhr = createHTTPRequestObject();

        xhr.upload.addEventListener('loadstart', onloadstartHandler, false);
        xhr.upload.addEventListener('progress', onprogressHandler, false);
        xhr.upload.addEventListener('load', onloadHandler, false);
        xhr.addEventListener('readystatechange', onreadystatechangeHandler, false);

        xhr.open('POST', uri, true);

        xhr.setRequestHeader("Accept", "application/json");

        // Fire!
        xhr.send(formData);
    };

    // Handle the start of the transmission
    var onloadstartHandler = function () {
        $(idSelector + "_NewFile").val('');
        $(idSelector + ' .errorInput').hide();
        $(idSelector + ' .dropFile').hide();
        $(idSelector + ' .progressStatus').show();
        $(idSelector + ' .uploadStatus').html('Subiendo...');
        $(idSelector + ' .bar').css('width', 0 + '%');
    };

    // Handle the end of the transmission
    var onloadHandler = function () {
        $(idSelector + ' .uploadStatus').html('Finalizado');
        $(idSelector + " .progressInfo").html('Progreso: 100%');
        $(idSelector + ' .bar').css('width', 100 + '%');
    };

    // Handle the progress
    var onprogressHandler = function (ev) {
        var percent = Math.round(ev.loaded / ev.total * 100);
        $(idSelector + " .bar").css('width', percent + '%');
        $(idSelector + ' .progressInfo').html('Progreso: ' + percent + '%');
    };

    // Handle the response from the server
    var onreadystatechangeHandler = function (ev) {
        $(idSelector + " .dropFile").show();
        $(idSelector + " .progressStatus").hide();

        var status = null;

        try {
            status = ev.target.status;
        } catch (e) {
            return;
        }

        if (status != '200') {
            $(idSelector + " .errorInput").show();
        }

        if (status == '200' && ev.target.response) {
            var resp = JSON.parse(ev.target.response);
            $(idSelector + " .newImageHolder").attr('src', resp[0].Path);
            $(idSelector + " .newImagePreview").show();
            $(idSelector + " .fileBox").hide();

            $(idSelector + "_NewFile").val(resp[0].Path);
        }
    };


    var dragOver = function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
    };

    var drop = function (ev) {
        ev.stopPropagation();
        ev.preventDefault();

        var files = ev.originalEvent.dataTransfer.files;

        if (files.length > 0) {
            uploadFile(files[0]);
        }
    };

    var uploadFile = function (file) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            //Solo permito subir un archivo
            //            for (var i = 0; i < files.length; i++) {
            //                //console.log(files[i]);
            //                data.append("file" + i, files[i]);
            //            }

            data.append("file0", file);

            submitFiles(data);
        } else {
            alert("su navegador no soporta HTML5");
        }
    };

    var submitFiles = function (data) {
        var action = "/api/upload";

        // Code common to both variants
        sendXHRequest(data, action);

        // Avoid normal form submission
        return false;
    };

    $box.bind("dragover", dragOver);
    $box.bind("drop", drop);

    $(idSelector + 'Input').hide();

    $(idSelector + ' .fileBoxTrigger').click(function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $(idSelector + 'Input').trigger('click');
    });

    $(idSelector + 'Input').change(function (ev) {
        ev.preventDefault();

        var files = ev.originalEvent.currentTarget.files;

        if (files.length > 0) {
            uploadFile(files[0]);
        }
    });

    $(idSelector + ' .newImage').click(function (ev) {
        ev.preventDefault();
        $(idSelector + ' .preview').hide();
        $(idSelector + ' .fileBox').show();
        $(idSelector + "_NewFile").val('');
    });

    //TODO: hacer fallback de file input para browsers que no soportan html5
    if (!supportAjaxUploadWithProgress()) {
    }
};
