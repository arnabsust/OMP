'use strict';
omp.factory('profileService', ['$http', function ($http) {
    var profileServiceFactory = {};

    var _updateProfile = function (userVM) {
        return $http.post(serviceBase + 'api/profile/update', userVM).then(function (response) {
            return response;
        });
    };

    var _getProfile = function () {
        return $http.get(serviceBase + 'api/profile/getprofile').then(function (results) {
            return results;
        });
    };

    var _getUserSummary = function (userName) {
        return $http({
            url: serviceBase + 'api/profile/getUserSummary',
            method: "GET",
            params: { username: userName }
        });
    };

    var _ratingUser = function (username, rating) {
        return $http({
            url: serviceBase + 'api/profile/updateUserRating',
            method: "POST",
            params: { rating: rating, username: username }
        });
    };

    profileServiceFactory.updateProfile = _updateProfile;
    profileServiceFactory.getProfile = _getProfile;
    profileServiceFactory.getUserSummary = _getUserSummary;
    profileServiceFactory.ratingUser = _ratingUser;


    return profileServiceFactory;
}]);