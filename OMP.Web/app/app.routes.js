'use strict';
omp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/products");
    $stateProvider
        .state('home', {
            url: '/',
            controller: 'homeCtrl',
            //templateUrl: 'app/components/home/views/home.html',
            /*resolve: {
                products: ['productService', function( productService){ return productService.query(); }]
            }*/
        })
        .state('about', {
            url: '/about',
            controller: 'aboutCtrl',
            templateUrl: 'app/components/home/views/about.html'
        })
        .state('signup', {
            url: '/signup',
            controller: 'signupCtrl',
            templateUrl: 'app/components/authentication/views/signup.html'
        })
        .state('login', {
            url: '/login',
            controller: 'loginCtrl',
            templateUrl: 'app/components/authentication/views/login.html'
        })
        .state('refresh', {
            url: '/refresh',
            controller: 'refreshCtrl',
            templateUrl: 'app/components/authentication/views/refresh.html'
        })
        .state('associate', {
            url: '/associate',
            controller: 'associateCtrl',
            templateUrl: 'app/components/authentication/views/associate.html'
        })
        .state('profile', {
            url: '/profile',
            controller: 'profileCtrl',
            templateUrl: 'app/components/user/views/profile.html'
        })
        .state('shop', {
            url: '/shop',
            controller: 'shopCtrl',
            templateUrl: 'app/components/shop/views/shop.html'
        })
        .state('productUpload', {
            url: '/productupload',
            controller: 'productUploadCtrl',
            templateUrl: 'app/components/product/views/productUpload.html'
        })
        .state('productUpdate', {
            url: '/productupload/:productID',
            controller: 'productUploadCtrl',
            templateUrl: 'app/components/product/views/productUpload.html'
        })
        .state('products', {
            url: '/products',
            controller: 'productCtrl',
            templateUrl: 'app/components/product/views/products.html'
        })
        .state('myproducts', {
            url: '/products/:own',
            controller: 'productCtrl',
            templateUrl: 'app/components/product/views/products.html'
        })
        .state('productdetails', {
            url: '/productdetails/:productID',
            controller: 'productDetailsCtrl',
            templateUrl: 'app/components/product/views/productDetails.html'
        })
        .state('cart', {
            url: '/cart',
            controller: 'cartCtrl',
            templateUrl: 'app/components/cart/views/cart.html'
        })
        .state('wishlist', {
            url: '/wishlist',
            controller: 'wishListCtrl',
            templateUrl: 'app/components/product/views/wishList.html'
        })
        .state('myorders', {
            url: '/myorders',
            controller: 'orderCtrl',
            templateUrl: 'app/components/product/views/orderList.html'
        })


}]);

omp.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

omp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

omp.run(['authService', '$rootScope', function (authService, $rootScope) {
    authService.fillAuthData();
    $rootScope.baseUrl = 'https://localhost:44301';
}]);