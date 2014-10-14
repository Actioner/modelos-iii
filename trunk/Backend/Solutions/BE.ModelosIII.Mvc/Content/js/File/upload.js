var FileManager = function (uploadUrl, uploadCompleteUrl) {

    //Privates
    var _uploadUrl,
        _uploadCompleteUrl,
        _idEntity,
        _context,
        _name;

    _uploadUrl = uploadUrl;
    _uploadCompleteUrl = uploadCompleteUrl;

    var _submit = function (file) {

        var currentTime = new Date();
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        var date = day + "/" + month + "/" + year;

        var dataFile = [];

        for (var i = 0; i < file.length; i++) {

            var f = new Object();

            f.IdEntity = _idEntity;
            f.name = _name;

            f.loaded = file[i].loaded;
            f.size = file[i].size;
            f.status = file[i].status;
            f.targetname = file[i].target_name;
            f.percent = file[i].percent;
            f.dateupload = date;

            if (file[i].status == plupload.DONE)
                dataFile.push(f);
        }
        $.ajax({
            url: _uploadCompleteUrl,
            type: "POST",
            context: _context,
            data: JSON.stringify(dataFile),
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (data) {

                $(this).html(data);

            }
        });
    };

    var _uploader = $("#uploader").pluploadQueue({
        // General settings
        runtimes: 'html5,html4',
        url: _uploadUrl,
        max_file_size: '5mb',
        chunk_size: '1mb',
        unique_names: true,
        rename: true,
        multiple_queues: true,
        browse_button: 'pickfiles',
        // Specify what files to browse for
        filters: [{ title: "Image files", extensions: "jpg,gif,png,jpeg" }],
        // Post init events, bound after the internal events.
        init: {
            UploadComplete: function (up, file, info) {
                var uploader = $('#uploader').pluploadQueue();
                uploader.refresh();
                _submit(file);
            },
            Error: function (up, args) {
                // Called when a error has occured
                console.log('[error] ');
            }
        }
    });

    // Client side form validation
    $('#formUploadFile').submit(function () {
        //var uploader = $('#uploader').pluploadQueue();
        // Files in queue upload them first
        if (_uploader.files.length > 0) {
            // When all files are uploaded submit form
            _uploader.bind('StateChanged', function () {
                if (_uploader.files.length === (_uploader.total.uploaded + _uploader.total.failed)) {

                    $('form')[0].uploader.
                        $('form')[0].submit();
                }
            });
            _uploader.start();
        } else {
            alert('Debe haber al menos una imagen.');
        }
        return false;
    });

    // Return an object exposed to the public
    return {
        SetContext: function (contextElement) {
            _context = contextElement;
        },
        SetIdEntity: function (id) {
            _idEntity = id;
        },
        SetName: function (sName) {
            _name = sName;
        }
    };
};