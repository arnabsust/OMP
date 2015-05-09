'use strict';

omp.controller('productDetailsCtrl', ['$scope',
    '$rootScope',
    '$stateParams',
    'toaster',
    'productDetailsService',
    'productUploadService',
    'productService',
    'cartService',
    'profileService',
    function ($scope, $rootScope, $stateParams, toaster, productDetailsService, productUploadService, productService, cartService, profileService) {
        $scope.interval = 5000;
        $scope.cart = cartService.restoreCart();

        if (!$scope.cart)
            $scope.cart = [];

        $scope.rate = 0;
        $scope.max = 5;
        $scope.hoveringOver = function (value) {
            $scope.overStar = value;
            profileService.ratingUser($scope.productVM.userName, value).then(function (res) {
                if (res.status == 200) {
                    toaster.pop('success', 'Rating Successfull', 'Seller Profile Ranking is Successfull');
                }
            });
            $scope.percent = 100 * (value / $scope.max);
        };

        productService.getProductSummaryByID($stateParams.productID).then(function (res) {
            $scope.productVM = res.data;
            productService.getRelatedProducts($stateParams.productID, $scope.productVM.categoryName).then(function (res) {
                $scope.relatedProductsList = res.data;
            });

            //////////////////////////////////////////////
            profileService.getUserSummary($scope.productVM.userName).then(function (res) {
                $scope.rate = res.data.rating;
                $scope.userProfile = res.data;
            });
            //////////////////////////////////////////////
        });

        productUploadService.getProductImageByProductID($stateParams.productID).then(function (res) {
            $scope.imageList = res.data;
        });

        $scope.addToCart = function (productID, quantity) {
            
            if (!productID)
                productID = '';
            if (!quantity)
                quantity = 1;

            if ($scope.quantity > $scope.productVM.stockQuantity) {
                toaster.pop('error', "Validation Error", "Stock quantity is less than order quantity");
                return;
            }

            if (!$scope.quantity) {
                toaster.pop('error', "Validation Error", "Please insert Quantity");
                return;
            }

            $scope.cart.push({
                product: {
                    id: $scope.productVM.productID,
                    name: $scope.productVM.productTitle,
                    imageUrl: $scope.imageList[0] ? $scope.imageList[0].imageUrl : '',
                    qty: $scope.quantity,
                    unitPrice: $scope.productVM.unitPrice,
                    totalPrice: function () { return parseFloat(this.qty) * parseFloat(this.unitPrice); },
                    description: $scope.productVM.productShortDescription
                }
            });
            cartService.saveCart($scope.cart);
            $rootScope.totalCartItem = $scope.cart.length;

            toaster.pop('success', "Product Cart", "Your Cart is Updated Successfully");
        };

        $scope.createWishlist = function (productID) {
            productService.wishList(productID).then(function (res) {
                if (res.status === 201) {
                    toaster.pop('success', "Product Wishlist", "This product is successfully add to your wishlist");
                } else {
                    toaster.pop('error', "Server Error", "Please contact with site administrator");
                }
            });
        };

    }
]);
