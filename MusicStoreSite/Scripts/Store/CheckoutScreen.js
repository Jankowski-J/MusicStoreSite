var CheckoutScreen = (function () {

    setupPage();

    function setupPage() {
        $("form").submit(function (e) {
            if ($("#PromoCode").val() == '') {
                e.preventDefault();
                
            } else if ($("#PromoCode").val() != 'FREE') {

            }
        });
    };
})