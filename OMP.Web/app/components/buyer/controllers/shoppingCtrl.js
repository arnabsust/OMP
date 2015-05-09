'use strict';

omp.controller('shoppingCtrl', [	'$scope',	'$state',	'items', 
				function(			 $scope,	 $state,	 items) {

	$scope.items = items;

}]);