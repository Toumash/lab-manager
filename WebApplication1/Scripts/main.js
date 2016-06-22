$(document)
    .ready(function() {
        $(".row-link")
            .click(function () {
                window.document.location = $(this).data("href");
            });
    });