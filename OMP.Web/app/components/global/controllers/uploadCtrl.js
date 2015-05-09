'use strict';
omp.controller('uploadCtrl', ['$scope', '$http', '$timeout', '$upload', 'toaster', function ($scope, $http, $timeout, $upload, toaster) {

    $scope.upload = [];
    $scope.fileUploadObj = { testString1: "Test string 1", testString2: "Test string 2" };

    $(":file").filestyle({
        buttonName: "btn-success",
        buttonText: "Change Profile Picture",
        input: false
    });

    $scope.percentage = "0%";

    $scope.onFileSelect = function ($files, url) {
        //$files: an array of files selected, each file has name, size, and type.
        for (var i = 0; i < $files.length; i++) {
            var $file = $files[i];
            (function (index) {
                $scope.upload[index] = $upload.upload({
                    url: serviceBase + "api/File/" + url, // webapi url
                    method: "POST",
                    data: { fileUploadObj: $scope.fileUploadObj },
                    file: $file
                }).progress(function (evt) {
                    // get upload percentage
                    $scope.percentage = parseInt(100.0 * evt.loaded / evt.total) + "%";
                    if ($scope.percentage == '100%') {
                        startTimer();
                    }
                    console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                }).success(function (data, status, headers, config) {
                    toaster.pop('success', "Profile Update", "Your profile is successfully updated");
                    $scope.$parent.userProfile.imageUrl = data.returnData;
                }).error(function (data, status, headers, config) {
                    toaster.pop('error', "Server Error", "Please contact with site administrator");
                });
            })(i);
        }
    }

    $scope.abortUpload = function (index) {
        $scope.upload[index].abort();
    }

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $scope.percentage = '0%';
        }, 2000);
    }
}]);

omp.controller('productImageUploadCtrl', ['$scope', '$http', '$stateParams', '$timeout', '$upload', 'toaster',
    function ($scope, $http, $stateParams, $timeout, $upload, toaster) {

    $scope.upload = [];
    $scope.fileUploadObj = { testString1: "Test string 1", testString2: "Test string 2", productID: ''};

    $(":file").filestyle({
        buttonName: "btn-success",
        buttonText: "Upload Product Images",
        input: false
    });

    $scope.percentage = "0%";

    $scope.onFileSelect = function ($files, url) {
        $scope.fileUploadObj.productID = $stateParams.productID;
        //$files: an array of files selected, each file has name, size, and type.
        for (var i = 0; i < $files.length; i++) {
            var $file = $files[i];
            (function (index) {
                $scope.upload[index] = $upload.upload({
                    url: serviceBase + "api/File/" + url, // webapi url
                    method: "POST",
                    data: { fileUploadObj: $scope.fileUploadObj },
                    file: $file
                }).progress(function (evt) {
                    // get upload percentage
                    $scope.percentage = parseInt(100.0 * evt.loaded / evt.total) + "%";
                    if ($scope.percentage == '100%') {
                        startTimer();
                    }
                    console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                }).success(function (data, status, headers, config) {
                    $scope.$parent.imageList = data.returnData;
                }).error(function (data, status, headers, config) {
                });
            })(i);
        }
    }

    $scope.abortUpload = function (index) {
        $scope.upload[index].abort();
    }

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $scope.percentage = '0%';
        }, 2000);
    }
}]);