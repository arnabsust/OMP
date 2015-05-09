'use strict';
omp.controller('orderCtrl', ['$scope', 'productService', 'toaster', function ($scope, productService, toaster) {

    productService.getAllOrder().then(function (res) {
        $scope.orderList = res.data;
    });

    $scope.delivered = function (orderID) {
        productService.deliver(orderID).then(function (res) {
            if (res.status === 201) {
                toaster.pop('success', "Product Delivery", "Product is successfully Delivered");
            } else {
                toaster.pop('error', "Server Error", "Please contact with site administrator");
            }
        });
    };

    $scope.reject = function (orderID) {
        productService.reject(orderID).then(function (res) {
            if (res.status === 201) {
                toaster.pop('success', "Product Rejection", "Product is successfully Rejected");
            } else {
                toaster.pop('error', "Server Error", "Please contact with site administrator");
            }
        });
    };
}]);