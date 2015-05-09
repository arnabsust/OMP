'use strict';

omp.controller('productCtrl', [
    '$scope',
    '$state',
    '$stateParams',
    'productService',
    'productCategoryService',
    'toaster', function ($scope, $state, $stateParams, productService, productCategoryService, toaster) {

        if ($stateParams.own) {
            $scope.sidebar = false;
            $scope.buttons = false;
        } else {
            $scope.sidebar = true;
            $scope.buttons = true;
        }

        $scope.searchCriteria = {
            pageNumber: 1,
            pageSize: 5,
            categoryName: '',
            filterByUserName: '',
            lowestPrice: 0,
            highestPrice: 0,
            sortByMostSold: false,
            sortByPriceLowToHigh: false,
            sortbyPriceHighToLow: false,
            keyword: '',
        };

        productCategoryService.getProductCategoryHierarchicalList().then(function (res) {
            $scope.categoryList = res.data;
        });

        $scope.getAllProducts = function (searchCriteria) {
            if ($stateParams.own) {
                $scope.searchCriteria.filterByUserName = true;
                productService.myProducts(searchCriteria).then(function (res) {
                    $scope.productList = res.data;
                });
            } else {
                productService.getAllProducts(searchCriteria).then(function (res) {
                    $scope.productList = res.data;
                });
            }
            
        };

        $scope.getAllProducts($scope.searchCriteria);

        $scope.loadMore = function () {
            if ($stateParams.own) {
                $scope.searchCriteria.pageNumber++;
                $scope.searchCriteria.filterByUserName = true;
                productService.myProducts($scope.searchCriteria).then(function (res) {
                    if (res.data.length > 0) {
                        $scope.productList = res.data;
                    } else {
                        $scope.searchCriteria.pageNumber--;
                    }
                });
            } else {
                $scope.searchCriteria.pageNumber++;
                productService.getAllProducts($scope.searchCriteria).then(function (res) {
                    if (res.data.length > 0) {
                        $scope.productList = res.data;
                    } else {
                        --$scope.searchCriteria.pageNumber;
                    }
                });
            }
        }

        $scope.getAllProducByCategoryName = function (categoryName) {
            productService.getAllProducByCategoryName(categoryName, 1), then(function (res) {
                $scope.productList = res.data;
            });
        };

        $scope.search = function () {
            $scope.searchCriteria.categoryName = '';
            $scope.searchCriteria.pageNumber = 1;
            if ($scope.sortByPrice == 1) {
                $scope.searchCriteria.sortByPriceLowToHigh = true;
                $scope.searchCriteria.sortbyPriceHighToLow = false;
            }
            if ($scope.sortByPrice == 2) {
                $scope.searchCriteria.sortbyPriceHighToLow = true;
                $scope.searchCriteria.sortByPriceLowToHigh = false;
            }
            $scope.getAllProducts($scope.searchCriteria);
        };

        $scope.searchByCategory = function (categoryName) {
            $scope.searchCriteria.pageNumber = 1;
            $scope.searchCriteria.categoryName = categoryName;
            $scope.getAllProducts($scope.searchCriteria);
        };

        $scope.recomendedProducts = function () {
            productService.getRecommendedProducts().then(function (res) {
                $scope.productList = res.data;
            });
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
    }]);