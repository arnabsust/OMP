'use strict';
omp.controller('cartCtrl', [
    '$scope',
    '$rootScope',
    '$state',
    'cartService',
    'toaster',
    'localStorageService', function ($scope, $rootScope, $state, cartService, toaster, localStorageService) {

    $scope.invoice = { items: cartService.restoreCart() };
    if($scope.invoice.items)
        angular.forEach($scope.invoice.items, function(item) {
            item.product.totalPrice = function() { return this.qty*this.unitPrice;};
        })
    else
        $scope.invoice={ items: []};

    /*
    cartService.getCart().then(function (res) {
        $scope.userCart = res.data;
    });


    $scope.save = function () {
        cartService.updateCart($scope.userCart).then(function (res) {
            if (res.status === 201) {
                toaster.pop('success', "Cart Update", "Your cart is successfully updated");
            } else {
                toaster.pop('error', "Server Error", "Please contact with site administrator");
            }
        });
    };    
    */

    $scope.addToCart = function (productID,quantity) {
        if(!productID)
            productID='';
        if(!quantity)
            quantity=1;

        $scope.invoice.items.push({
            product:{
                id: productID,
                name: "Motorola XOOM\u2122 with Wi-Fi", 
                imageUrl: 'app/components/Product/views/product/images/motorola-xoom-with-wi-fi.0.jpg',
                qty: quantity,
                unitPrice: 9.95,
                totalPrice: function() { return this.qty*this.unitPrice;},
                description: "The Next, Next Generation\r\n\r\nExperience the future with Motorola XOOM with Wi-Fi, the world's first tablet powered by Android 3.0 (Honeycomb)."
            }
        });

        cartService.saveCart($scope.invoice.items);
    };

    $scope.order = function () {
        var cartVMs = [];
        var cartList = localStorageService.get('cartData');
        for (var i in cartList) {
            cartVMs.push({
                productID: cartList[i].product.id,
                unitPrice: cartList[i].product.unitPrice,
                quantity: cartList[i].product.qty
            });
        }
        cartService.order(cartVMs).then(function (res) {
            if (res.status === 201) {
                cartService.resetCart();
                $rootScope.totalCartItem = 0;
                toaster.pop('success', 'Success', 'Your order is successfully placed');
                $state.go('products');
            } else {
                toaster.pop('error', "Server Error", "Please contact with site administrator");
            }
            
        });
    };


    $scope.removeFromCart = function(index) {
        $scope.invoice.items.splice(index, 1);
        $rootScope.totalCartItem--;
        cartService.saveCart($scope.invoice.items);
    },

    $scope.total = function() {
        var total = 0;
        angular.forEach($scope.invoice.items, function(item) {
            total += item.product.totalPrice();
        })
        return total;
    }

    $scope.totalVat = function() {
        var total = 0;
        total=$scope.total()*0.15;

        return total;
    }

    $scope.totalWithVat = function() {
        var total = 0;
        total=$scope.total()+$scope.totalVat();

        return total;
    }

    $scope.review = function (index) {
        var productID = $scope.invoice.items[index].product.id;
        var reviewText = $scope.invoice.items[index].product.reviewText;
        cartService.createProductReview(productID, reviewText).then(function (res) {
            if (res.status === 201) {
                toaster.pop('success', 'Success', 'Your order is successfully placed');
            }
        });
    }

}]);