var Store = Store || {};

Store.Product = (function() {

    var onModalReviewed = function(data) {
        var $container = $("#modalContainer");
        $container.html(data);
        $container.find("#myModal").modal('show');
    }

    var getModal = function () {
        console.log($("#htmlProductId").val());
        $.ajax({
            url: "/Store/AddProductDialog",
            type: "GET",
            data: { poductId: $("#htmlProductId").val() },
            success: onModalReviewed
        });
    };

    $("#AddToShoppingCart").click(function (e) {
        e.preventDefault();
        getModal();
    });

    //$(function() {
    //    $("#myModal").on('shown.bs.modal', function (e) {
    //        console.log("modal show", e);
    //    });
    //});
    //return {

    //};
})();