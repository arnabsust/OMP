'use strict';
omp.controller('signupCtrl', ['$scope', '$state', '$timeout', 'authService', function ($scope, $state, $timeout, authService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        userName: "",
        email: "",
        password: "",
        confirmPassword: ""
    };

    $scope.signUp = function () {
        
        authService.saveRegistration($scope.registration).then(function (res) {
            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";

            startTimer();
        },
        function (res) {
            var errors = [];
            for (var key in res.data.modelState) {
                for (var i = 0; i < res.data.modelState[key].length; i++) {
                    errors.push(res.data.modelState[key][i]);
                }
            }
            $scope.message = "Failed to register user due to:" + errors.join(' ');
        });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $state.go('login');
        }, 2000);
    }
}]);