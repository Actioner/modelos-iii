$(function () {
    var alertVideoNotFound = function () {
        jAlert("No se pudo encontrar el trailer", "Alerta");
    };

    $("#showPreview").click(function (ev) {
        ev.preventDefault();
        var videoUrl = $(".trailerInput").val();
        var videoId = getYoutubeVideoId(videoUrl);

        if (videoId === '') {
            alertVideoNotFound();
            return false;
        }

        $.ajax({
            url: "http://gdata.youtube.com/feeds/api/videos/" + videoId,
            error: function (eve) {
                alertVideoNotFound();
                return false;
            },
            success: function () {
                loadMediaBox(videoUrl);
            }
        });

        return false;
    });

    initializeFileUpload('#NewPoster');
    initializeFileUpload('#NewSmallPoster');

    initializeSelect('#RatingId', {
        placeholder: "Seleccione una Califiación"
    });

    initializeSelect('#GenreIds', {
        placeholder: "Seleccione los Géneros"
    });

    $('.previewPoster').fancybox();
});