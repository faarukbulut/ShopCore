$(document).ready(function () {
    $('.addcartbtn').click(function () {
        var token = $('input[name="__RequestVerificationToken"]').val();
        var productId = $(this).data("productid");
        var quantity = $(this).data("quantity");

        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart/",
            data: {
                __RequestVerificationToken: token,
                productID: productId,
                quantity: quantity
            },
        });
    });
});

