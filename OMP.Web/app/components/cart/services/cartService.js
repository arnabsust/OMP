'use strict';
omp.factory('cartService', ['$http', 'localStorageService', function ($http, localStorageService) {
    var cartServiceFactory = {};

    var _saveCart = function (_cartData) {

        localStorageService.set('cartData', _cartData);
    };

    var _resetCart = function () {

        localStorageService.remove('cartData');
    };

    var _restoreCart = function () {
        if (localStorageService.get('cartData')) {
            var cartData = localStorageService.get('cartData');
            if (!cartData[0]) {
                _resetCart();
                cartData = [];
            }
            return cartData;
        } else {
            return [];
        }
    };

    var _updateCart = function (cartVM) {
        return $http.post(serviceBase + 'api/cart/update', cartVM).then(function (response) {
            return response;
        });
    };

    var _getCart = function () {
        return $http.get(serviceBase + 'api/cart/getcart').then(function (results) {
            return results;
        });
    };

    var _order = function (cartVMs) {
        return $http.post(serviceBase + 'api/order/create', cartVMs).then(function (response) {
            return response;
        });
    }

    var _createProductReview = function (productID, reviewText) {
        return $http({
            url: serviceBase + 'api/product/CreateProductReview',
            method: "POST",
            params: { productID: productID, reviewText: reviewText }
        });
    }

    cartServiceFactory.saveCart = _saveCart;
    cartServiceFactory.resetCart = _resetCart;
    cartServiceFactory.restoreCart = _restoreCart;
    cartServiceFactory.updateCart = _updateCart;
    cartServiceFactory.getCart = _getCart;
    cartServiceFactory.order = _order;
    cartServiceFactory.createProductReview = _createProductReview;

    return cartServiceFactory;
}]);