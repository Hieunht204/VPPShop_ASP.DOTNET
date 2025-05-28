$(document).ready(function () {
    // --- 1. Xử lý thêm vào giỏ hàng ---
    $(document).on('click', '.btn-add-to-cart', function (e) {
        e.preventDefault();

        var button = $(this);
        var productId = button.data('product-id');

        var quantityInput = button.closest('form').find('.quantity-input');
        var quantity = parseInt(quantityInput.val()) || 1;

        $.ajax({
            url: '/Cart/AddToCart',
            method: 'POST',
            data: { id: productId, quantity: quantity },
            success: function (res) {
                if (res.success) {
                    showPopup(res.message);
                    $('.cart-count').text(res.totalItems);
                } else {
                    alert("Thêm sản phẩm thất bại.");
                }
            },
            error: function () {
                alert("Có lỗi xảy ra khi thêm vào giỏ hàng.");
            }
        });
    });

    $(document).on('click', '.btn-plus, .btn-minus', function (e) {
        e.preventDefault();
        const btn = $(this);

        // Trường hợp: Trang giỏ hàng
        if (btn.closest('table.cart-table').length > 0) {
            const productId = btn.data('id');
            const $row = $('tr[data-id="' + productId + '"]');
            const input = $row.find('input.quantity-input');
            let quantity = parseInt(input.val());

            if (btn.hasClass('btn-plus')) {
                quantity += 1;
            } else if (btn.hasClass('btn-minus') && quantity > 1) {
                quantity -= 1;
            }

            $.ajax({
                url: '/Cart/UpdateQuantity',
                type: 'POST',
                data: { productId, quantity },
                success: function (res) {
                    if (res.success) {
                        input.val(res.quantity);
                        $row.find('.item-total').text(res.itemTotal + ' đ');
                        $('.cart-total').text(res.cartTotal + ' đ');
                    } else {
                        alert("Cập nhật thất bại.");
                    }
                },
                error: function () {
                    alert("Có lỗi khi gửi yêu cầu.");
                }
            });
        }
        // Trường hợp: Trang chi tiết sản phẩm
        else {
            const input = btn.closest('.quantity').find('input.quantity-input');
            let quantity = parseInt(input.val());

            if (btn.hasClass('btn-plus')) {
                quantity += 1;
            } else if (btn.hasClass('btn-minus') && quantity > 1) {
                quantity -= 1;
            }

            input.val(quantity);
        }
    });



    // --- 3. Hiển thị popup nhỏ ---
    function showPopup(message) {
        const popup = $('<div class="cart-popup alert alert-success"></div>').text(message);
        $('body').append(popup);
        popup.css({
            position: 'fixed',
            bottom: '20px',
            right: '20px',
            zIndex: 9999
        });

        setTimeout(() => popup.fadeOut(500, () => popup.remove()), 2000);
    }
});


