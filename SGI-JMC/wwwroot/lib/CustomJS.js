window.setTimeout(function () {
    $(".alert").fadeTo(5000, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 5000);