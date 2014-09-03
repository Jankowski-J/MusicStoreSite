var Store = Store || {};

Store.Product = (function() {

    var getModal = function() {
        $.ajax({
            url: "/Store/AddProductDialog",
            type: "GET",
            data: { poductId: 1 },
            success: function (data) {
                console.log(data);
                var $container = $("#modalContainer");
                $container.html(data);
                $container.find("#myModal").modal('show');
            }
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