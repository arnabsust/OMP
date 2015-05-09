'use strict';
omp.controller('indexCtrl', [
    '$scope',
    '$rootScope',
    '$state',
    'authService',
    'localStorageService', function ($scope, $rootScope, $state, authService, localStorageService) {

    $scope.logOut = function () {
        authService.logOut();
        $rootScope.authenticated = false;
        $state.go('products');
    };

    $scope.authentication = authService.authentication;

    if (localStorageService.get('cartData')) {
        console.log(localStorageService.get('cartData'));
        $rootScope.totalCartItem = localStorageService.get('cartData').length;
    }
    

    if (!$scope.totalCartItem)
        $scope.totalCartItem = 0;
}]);