// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//
//calculate Total

calc_total();

function calc_total() {
    var sum = 0;
    $(".total").each(function () {
        sum += parseFloat($(this).text());
    });
    $('#sum').text(sum);
}

//
//TimePicker only every 15min steps
window.onload = function () {
    var timepicker = document.getElementById("deliveryTime");

    timepicker.addEventListener("change", function () {

        var [hours, minutes] = timepicker.value.split(":");

        minutes = (Math.ceil(minutes / 15) * 15);
        if (minutes == 0) minutes = "00";
        if (minutes == 60) { minutes = "00"; ++hours % 24; }

        var newTime = hours + ":" + minutes;

        timepicker.value = newTime;
    });
}

function search() {
    var input = document.getElementById("Search");
    var filter = input.value.toLowerCase();
    var nodes = document.getElementsByClassName('card mb-3');

    for (i = 0; i < nodes.length; i++) {
        if (nodes[i].innerText.toLowerCase().includes(filter)) {
            nodes[i].style.display = "block";
        } else {
            nodes[i].style.display = "none";
        }
    }
}

