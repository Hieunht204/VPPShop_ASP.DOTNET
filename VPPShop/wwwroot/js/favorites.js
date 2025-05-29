$(document).ready(function () {
    // Xử lý sự kiện thêm vào yêu thích
    $('.btn-add-to-favorites').click(function (e) {
        e.preventDefault();
        var productId = $(this).data('product-id');

        $.ajax({
            url: '/Favorites/AddToFavorites',
            type: 'GET',
            data: {
                id: productId
            },
            success: function (data) {
                if (data.success) {
                    // Cập nhật số lượng sản phẩm yêu thích
                    $('.favorite-count').text(data.totalItems);

                }
            }
        });
    });
});