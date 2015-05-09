'use strict';
omp.controller('profileCtrl', ['$scope', '$state', 'profileService', 'toaster', function ($scope, $state, profileService, toaster) {

    profileService.getProfile().then(function (res) {
        $scope.userProfile = res.data;
    });

    $scope.save = function () {
        profileService.updateProfile($scope.userProfile).then(function (res) {
            if (res.status === 201) {
                toaster.pop('success', "Profile Update", "Your profile is successfully updated");
            } else {
                toaster.pop('error', "Server Error", "Please contact with site administrator");
            }
        });
    };

}]);