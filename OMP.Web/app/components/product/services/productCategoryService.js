'use strict';
omp.factory('productCategoryService', ['$http', function ($http) {
    var productCategoryServiceFactory = {};

    var _getProductCategoryList = function () {
        return $http.get(serviceBase + 'api/category/getAllCategory').then(function (results) {
            return results;
        });
    };

    var _getProductCategoryAnnonymousList = function () {
        return $http.get(serviceBase + 'api/category/getAllCategory').then(function (results) {
            return results;
        });
    };

    var _getProductCategoryHierarchicalList = function () {
        return $http.get(serviceBase + 'api/category/getAllCategoryHierarchical').then(function (results) {
            return results;
        });
    }

    var _getProductAttributeByCategory = function (categoryID) {
        return $http({
            url: serviceBase + 'api/category/GetAllAttributesByID',
            method: "GET",
            params: { CategoryID: categoryID }
        });
        /*return $http.get(serviceBase + 'api/category/GetAllAttributesByID', { CategoryID: categoryID }).then(function (results) {
            return results;
        });*/
    };

    productCategoryServiceFactory.getProductAttributeByCategory = _getProductAttributeByCategory;
    productCategoryServiceFactory.getProductCategoryList = _getProductCategoryList;
    productCategoryServiceFactory.getProductCategoryAnnonymousList = _getProductCategoryAnnonymousList;
    productCategoryServiceFactory.getProductCategoryHierarchicalList = _getProductCategoryHierarchicalList;

    return productCategoryServiceFactory;
}]);