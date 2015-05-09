'use strict';
omp.factory('shopService', ['$http', function ($http) {
    var shopServiceFactory = {};

    var _saveShop = function (shopVM) {
        return $http.post(serviceBase + 'api/shop/create', shopVM).then(function (response) {
            return response;
        });
    };

    var _getShop = function () {
        return $http.get(serviceBase + 'api/shop/getshop').then(function (results) {
            return results;
        });
    };

    shopServiceFactory.saveShop = _saveShop;
    shopServiceFactory.getShop = _getShop;

    return shopServiceFactory;
}]);