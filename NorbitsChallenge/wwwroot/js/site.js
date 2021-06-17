// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function isEmptyOrNull(string) {
    return (!string || string.length === 0);
}

function onSuccess(data) {
    console.log(data);

    if (isEmptyOrNull(data.licensePlate)) {
        $('#carInfo').addClass('hidden');
        return;
    }

    var licensePlate = data.licensePlate;
    $('#licensePlate').text(licensePlate);
    $('#carInfo').removeClass('hidden');

    var tireCount = data.tireCount;
    $('#tireCount').text(tireCount);
    
    
    var model = data.model;
    $('#model').text(model);
    

    var brand = data.brand;
    $('#brand').text(brand);

    var description = data.description;
    $('#description').text(description);
}
function onSuccessfulDelete(data) {
    console.log(data);
}