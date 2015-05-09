'use strict';
omp.controller('homeCtrl', ['$scope','$state',
				function ($scope, $state) {
				    $state.go('products');
				}]);