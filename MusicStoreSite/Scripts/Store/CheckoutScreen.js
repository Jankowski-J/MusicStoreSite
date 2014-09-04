var CheckoutScreen = (function () {

    setupPage();

    function setupPage() {
        $("form").submit(function (e) {
            if ($("#PromoCode").val() == '') {
                $("#PromoCodeError").text('')
            } else if ($("#PromoCode").val() != 'FREE') {
                e.preventDefault();
                $("#PromoCodeError").text("Please make sure you have spelled the promo code properly");
            }
        });
    };

    return {
    };
})();