'use strict';
omp.controller('wishListCtrl', ['$scope', 'productService', 'toaster', function ($scope, productService, toaster) {
    productService.getWishList().then(function (res) {
        $scope.wishList = res.data;
    });

    $scope.delete = function (productID, index) {
        productService.deleteWishList(productID).then(function (res) {
            console.log(res);
            if (res.status === 200) {
                toaster.pop('success', "Product Wishlist", "This product is successfully removed from your wishlist");
                $scope.wishList.splice(index, 1);
            } else {
                toaster.pop('error', "Server Error", "Please contact with site administrator");
            }
        });
    }
}]);