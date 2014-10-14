// Convert divs to queue widgets when the DOM is ready

$(function () {

    uploader = $("#uploader").pluploadQueue({
        // General settings
        runtimes: 'html5,html4',
        url: 'Upload/Upload',
        max_file_size: '5mb',
        chunk_size: '1mb',
        unique_names: true,
        rename: true,
        multiple_queues: true,
        browse_button: 'pickfiles',

        // Specify what files to browse for
        filters: [
            { title: "Image files", extensions: "jpg,gif,png,jpeg" },
            { title: "Zip files", extensions: "zip"}],
        preinit: {
            Init: function (up, info) {
                fileManager.SetUrl("Upload/Complete");
            } 
        },

        // Post init events, bound after the internal events.
        init: {
            BeforeUpload :function () {
            },
            UploadComplete: function (up, file, info) {
                // Called when a file has finished uploading
                console.log('[FileUploaded]');
                var uploader = $('#uploader').pluploadQueue();
                uploader.refresh();

                fileManager.Complete(up, file, info);

            },
            QueueChanged: function (up) {
            },
            Error: function (up, args) {
                // Called when a error has occured
                console.log('[error] ');
            }
        }
    });

    // Client side form validation
    $('#formUploadFile').submit(function (e) {
        var uploader = $('#uploader').pluploadQueue();

        // Files in queue upload them first
        if (uploader.files.length > 0) {
            // When all files are uploaded submit form
            uploader.bind('StateChanged', function () {
                if (uploader.files.length === (uploader.total.uploaded + uploader.total.failed)) {
                    $('form')[0].uploader.
                    $('form')[0].submit();
                }
            });

            uploader.start();
        } else {
            alert('You must queue at least one file.');
        }
        return false;
    });

});

    
        

