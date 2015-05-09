'use strict';
omp.factory('productUploadService', ['$http', function ($http) {
    var productUploadServiceFactory = {};

    var _uploadProduct = function (productVM) {
        return $http.post(serviceBase + 'api/product/create', productVM).then(function (response) {
            return response;
        });
    };

    var _updateProduct = function (productVM) {
        return $http.put(serviceBase + 'api/product/update', productVM).then(function (response) {
            return response;
        });
    };

    var _getProductByProductID = function (productID) {
        return $http({
            url: serviceBase + 'api/product/GetProductByID',
            method: "GET",
            params: { ProductID: productID }
        });
    };

    var _getProductImageByProductID = function (productID) {
        return $http({
            url: serviceBase + 'api/product/GetProductImageByProductID',
            method: "GET",
            params: { ProductID: productID }
        });
    };

    productUploadServiceFactory.uploadProduct = _uploadProduct;
    productUploadServiceFactory.updateProduct = _updateProduct;
    productUploadServiceFactory.getProductByProductID = _getProductByProductID;
    productUploadServiceFactory.getProductImageByProductID = _getProductImageByProductID;

    return productUploadServiceFactory;
}]);