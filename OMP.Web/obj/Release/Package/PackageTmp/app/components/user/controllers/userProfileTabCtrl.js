'use strict';
omp.controller('userProfileTabCtrl', ['$scope', '$state', 'profileService', function ($scope, $state, profileService) {
    
    $scope.getUserSummary = function () {
        console.log($scope.$parent.productVM.userName);
    };
    //profileService.getUserSummary().then()
    
}]);