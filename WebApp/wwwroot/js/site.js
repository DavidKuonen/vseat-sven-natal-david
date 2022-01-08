// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//
//Calculate the total of the shoppingcart function
total();
function total() {
    var sum = 0;
    //Calculates the sum from the table row with the class total
    $(".total").each(function () {
        sum += parseFloat($(this).text());
    });
    //Stores the sum in the table row with the id Sum
    $('#sum').text(sum);
}

//
//TimePicker only every 15min steps function
window.onload = function () {
    //Access to the Id attribute deliveryTime and store in variable timepicker
    var timepicker = document.getElementById("deliveryTime");

    timepicker.addEventListener("change", function () {
        var [hours, minutes] = timepicker.value.split(":");

        //Round up
        minutes = (Math.ceil(minutes / 15) * 15);
        if (minutes == 0) minutes = "00";
        if (minutes == 60) { minutes = "00"; ++hours % 24; }

        var newTime = hours + ":" + minutes;

        //Returns the new rounded up value
        timepicker.value = newTime;
    });
}

//
//Restaurant search function
function search() {
    //Access to the Id attribute search and store in variable
    var input = document.getElementById("search");
    //Input in search field lowercase, so that there is no longer upper and lower case
    var filter = input.value.toLowerCase();
    //Access the element via the class name and store it in an array
    var nodes = document.getElementsByClassName('card mb-3');

    for (i = 0; i < nodes.length; i++) {
        //Lowercase the text of the array elements
        if (nodes[i].innerText.toLowerCase().includes(filter)) {
            //if array element contains the content of the search bar, show the element
            nodes[i].style.display = "block";
        } else {
            //if array element not contains the content of the search bar, don't show the element
            nodes[i].style.display = "none";
        }
    }
}
