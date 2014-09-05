var CheckoutScreen = (function () {

    setupPage();

    function setupPage() {
        $("form").submit(function (e) {
            if ($("#PromoCode").val() != 'FREE') {
                e.preventDefault();
                $("#PromoCodeError").text("Please make sure you have spelled the promo code properly");
            }
            else {
                $("#PromoCodeError").text('');
            }
        });
    };

    return {
    };
})();