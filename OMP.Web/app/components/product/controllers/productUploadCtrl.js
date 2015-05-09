'use strict';
omp.controller('productUploadCtrl', [
    '$scope',
    '$state',
    '$stateParams',
    'toaster',
    'productCategoryService',
    'productUploadService',
    function ($scope, $state, $stateParams, toaster, productCategoryService, productUploadService) {

        productCategoryService.getProductCategoryList().then(function (res) {
            $scope.producCategoryList = res.data;
        });

        $scope.getProductAttributeByCategory = function () {
            productCategoryService.getProductAttributeByCategory($scope.productVM.categoryID).then(function (res) {
                console.log(res.data);
            });
        };

        if ($stateParams.productID) {
            productUploadService.getProductByProductID($stateParams.productID).then(function (res) {
                $scope.productVM = res.data;
            });
            productUploadService.getProductImageByProductID($stateParams.productID).then(function (res) {
                $scope.imageList = res.data;
            });
        }

        $scope.save = function () {
            if ($stateParams.productID) {
                productUploadService.updateProduct($scope.productVM).then(function (res) {
                    if (res.status === 201) {
                        toaster.pop('success', "Product Update", "Your Product is successfully uploaded");
                    } else {
                        toaster.pop('error', "Server Error", "Please contact with site administrator");
                    }
                });
            } else {
                productUploadService.uploadProduct($scope.productVM).then(function (res) {
                    if (res.status === 201) {
                        $scope.productID = res.data;
                        $state.go('productUpdate', { 'productID': res.data });
                        toaster.pop('success', "Product Update", "Your Product is successfully uploaded");
                    } else {
                        toaster.pop('error', "Server Error", "Please contact with site administrator");
                    }
                });
            }
            
        };

}]);