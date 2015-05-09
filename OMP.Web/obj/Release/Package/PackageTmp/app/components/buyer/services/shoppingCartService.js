omp.factory('shoppingCartService', ['$resource',
  function($resource){
    return $resource('data/shopping/:productId.json', {}, {
      query: {method:'GET', params:{productId:'cartItems'}, isArray:true}
    });
  }]);