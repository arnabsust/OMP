'use strict';
app.controller('refreshCtrl', ['$scope', '$state', 'authService', function ($scope, $state, authService) {

    $scope.authentication = authService.authentication;
    $scope.tokenRefreshed = false;
    $scope.tokenResponse = null;

    $scope.refreshToken = function () {

        authService.refreshToken().then(function (response) {
            $scope.tokenRefreshed = true;
            $scope.tokenResponse = response;
        },
         function (err) {
             $state.go('login');
         });
    };

}]);