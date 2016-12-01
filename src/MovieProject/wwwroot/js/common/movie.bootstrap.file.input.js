(function () {
    'use strict';

    $(document).on('ready', function () {
        var mainTemplate = '{preview}<div class="input-group {class}"><div class="input-group-btn">{browse}{upload}{remove}</div>{caption}</div>';

        $("#exportToXmlFileInput").fileinput({
            previewFileType: "text",
            allowedFileExtensions: ["csv"],
            previewClass: "bg-warning",
            showRemove: false,
            showPreview: false,
            layoutTemplates: {
                main1: mainTemplate
            }
        });

        $("#exportToXmlFileInput").on("filebrowse", function () {
            $("#exportToXmlFileInput").fileinput('clear');
        });
    });
})();