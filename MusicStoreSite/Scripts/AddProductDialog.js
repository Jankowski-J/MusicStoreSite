var Store = Store || {};

Store.Product = (function() {

    var refresh = function () {
        console.log("refresh called");
        $.ajax({
            url: "/Store/GetCartMiniInfo",
            type: "post",
            success: function (data) {
                console.log("refresh recieved");
                $("#cartMenu").html(data.view);
            }
        });
    }

    var onModalReviewed = function(data) {
        var $container = $("#modalContainer");
        $container.html(data);
        $container.find("#myModal").modal('show');
        refresh();
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
})();