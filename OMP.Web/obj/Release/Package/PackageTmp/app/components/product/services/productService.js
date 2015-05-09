omp.factory('productService', ['$http',
  function ($http) {
      var productServiceFactory = {};

      var _getAllProducts = function (searchCriteria) {
          return $http({
              url: serviceBase + 'api/product/GetAllProducts',
              method: "GET",
              params: searchCriteria
          });
      };

      var _myProducts = function (searchCriteria) {
          return $http({
              url: serviceBase + 'api/product/MyProducts',
              method: "GET",
              params: searchCriteria
          });
      };

      var _getAllProductsByCategoryID = function (categoryName, categoryID) {
          return $http({
              url: serviceBase + 'api/product/GetAllProducts',
              method: "GET",
              params: { categoryName: categoryName, pageNumber: pageID }
          });
      };

      var _getProductSummaryByID = function (productID) {
          return $http({
              url: serviceBase + 'api/product/GetProductSummaryByID',
              method: "GET",
              params: { ProductID: productID }
          });
      }

      var _getRelatedProducts = function (productID, categoryName) {
          return $http({
              url: serviceBase + 'api/product/GetRelatedProducts',
              method: "GET",
              params: { productID: productID, categoryName: categoryName }
          });
      };

      var _getRecommendedProducts = function () {
          return $http({
              url: serviceBase + 'api/product/GetRecommendedProducts',
              method: "GET"
          });
      };

      var _wishList = function (productID) {
          return $http({
              url: serviceBase + 'api/wishlist/create',
              method: "POST",
              params: {productID: productID}
          });
      };

      var _getWishList = function () {
          return $http({
              url: serviceBase + 'api/wishlist/GetAllWishList',
              method: "GET",
          });
      };

      var _deleteWishList = function (productID) {
          return $http({
              url: serviceBase + 'api/wishlist/delete',
              method: "POST",
              params: { productID: productID }
          });
      };

      var _getAllOrder = function () {
          return $http({
              url: serviceBase + 'api/order/GetAllOrder',
              method: "GET",
          });
      };

      var _deliver = function (orderID) {
          return $http({
              url: serviceBase + 'api/order/UpdateOrderStatusAsDelivered',
              method: "POST",
              params: { id: orderID }
          });
      };

      var _reject = function (orderID) {
          return $http({
              url: serviceBase + 'api/order/UpdateOrderStatusAsRejected',
              method: "POST",
              params: { id: orderID }
          });
      };

      productServiceFactory.getAllProducts = _getAllProducts;
      productServiceFactory.myProducts = _myProducts;
      productServiceFactory.getRelatedProducts = _getRelatedProducts;
      productServiceFactory.getRecommendedProducts = _getRecommendedProducts;
      productServiceFactory.getProductSummaryByID = _getProductSummaryByID;
      productServiceFactory.wishList = _wishList;
      productServiceFactory.getWishList = _getWishList;
      productServiceFactory.deleteWishList = _deleteWishList;
      productServiceFactory.getAllOrder = _getAllOrder;
      productServiceFactory.deliver = _deliver;
      productServiceFactory.reject = _reject;

      return productServiceFactory;
  }]);