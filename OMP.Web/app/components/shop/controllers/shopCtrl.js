'use strict';
omp.controller('shopCtrl', ['$scope', '$state', 'shopService', 'toaster', function ($scope, $state, shopService, toaster) {

    shopService.getShop().then(function (res) {
        $scope.shop = res.data;
    });

    $scope.save = function () {
        shopService.saveShop($scope.shop).then(function (res) {
            if (res.status === 201) {
                toaster.pop('success', "Shop Create", "Your shop is successfully created");
            } else {
                toaster.pop('error', "Server Error", "Please contact with site administrator");
            }
        });
    };
}]);