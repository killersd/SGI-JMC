var button = document.getElementById("idbutton");

button.addEventListener("click", function () {

    var container = document.getElementById("idfrc");

    if (container.style.display === "none") {
        container.style.display = "block";
    }
    else
    {
        container.style.display = "none";
    }

});




