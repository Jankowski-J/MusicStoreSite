var Store = Store || {};

Store.MiniCounter = (function () {

    console.log("refresh loaded");

    var refresh = function () {
        $.ajax({
            url: "/Store/GetCartMiniInfo",
            type: "GET",
            success: function (data) {
                $("#cartMenu").html(data);
            }
        });
    }

    refresh();

    return {
        Refresh : refresh
    }
})();