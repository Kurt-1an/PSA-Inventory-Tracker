
function Print() {
    $(".hideWhenPrint").hide();
    window.print();
    $(".hideWhenPrint").show();
}

function closeModal() {
    $("#addSupplyModal").modal("hide");
}

window.reloadPage = function () {
    location.reload(true);
}
