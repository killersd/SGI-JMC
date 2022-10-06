window.setTimeout(function () {
    $(".alert").fadeTo(3000, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 3000);