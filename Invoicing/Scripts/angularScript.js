/// <reference path="angular.min.js" />

//The Module
var app = angular.module('md', []);

//The service with the get method. 
app.service('ms', function ($http) {
    this.getData = function () {
        var resp = $http.get(getEmailsUrl);
        return resp;
    };
});

//The Controller

app.controller('ctrl', function ($scope, ms) {

    $scope.Message = "Hola";

    loadData();

    function loadData() {
        //The controller call the function from the service
        var promiseGetData = ms.getData();
        //Callback with success and failure
        promiseGetData.then(function (res) {            
            $scope.Order = res.data;
            $scope.Order.SubTotal = $scope.Order.SubTotal.toFixed(2);
            $scope.Order.ShippingCharges = $scope.Order.ShippingCharges.toFixed(2);
            $scope.Order.Tax = $scope.Order.Tax.toFixed(2);
            $scope.Order.Total = $scope.Order.Total.toFixed(2);
            $scope.Message = "Call Completed Successfully...";
        }, function (err) {
            $scope.Message = "Call failed" + err.status + "  " + err.statusText;
        });
    };

    $scope.calcLine = function (detail) {
        detail.Total = detail.Qty * detail.Price;
        $scope.calcTotals();
    };       

    $scope.calcTotals = function () {
        var subtotal = 0;
        var shipping = 0;
        var tax = 0;
        var total = 0;
        for (var i = 0; i < $scope.Order.OrderDetails.length; i++) {
            var product = $scope.Order.OrderDetails[i];
            subtotal += (product.Total);
        };                       
  

        subtotal = (subtotal).toFixed(2);
        shipping = $scope.Order.ShippingCharges;
        //shipping = shipping.toFixed(2);
        tax = $scope.Order.Tax;
        //tax = tax.toFixed(2);
        total = (parseFloat(subtotal) + parseFloat(shipping) + parseFloat(tax)).toFixed(2);
        
        $scope.Order.SubTotal = subtotal;
        $scope.Order.Total = total;
    }

    $scope.copyFromTeacher = function () {
        $scope.Order.FD_FirstName = $scope.Order.FirstName;
        $scope.Order.FD_LastName = $scope.Order.LastName;
        $scope.Order.FD_Email = $scope.Order.Email;
        $scope.Order.FD_Phone = $scope.Order.Phone;
        $scope.Order.FD_Mobile = $scope.Order.Mobile;
        $scope.Order.FD_ShippingAddress = $scope.Order.ShippingAddress;
        $scope.Order.FD_ShippingCountry = $scope.Order.ShippingCountry;
        $scope.Order.FD_ShippingCity = $scope.Order.ShippingCity;
        $scope.Order.FD_ShippingState = $scope.Order.ShippingState;
        $scope.Order.FD_ShippingZip = $scope.Order.ShippingZip;
    };

});

/*angular.module("app", ["Invoicing.InvoicesController"]);


var theApp = angular
            .module("Invoicing.InvoicesController", [])
            .controller("InvoicesCtrl", ['$scope', '$http', function ($scope) {


                $http.get("/Invoices/CreateVM")
                    .then(
                     function (data) {
                        $scope.modelo = data;
                     },
                     function(data) {
                        $scope.modelo = "error in fetching data"; //return if error on fetch
                     });

}]);
*/

//$scope.message = "LISTO";
//$scope.message = "El mensaje";
//$scope.mydata = $scope.getdata();
//$scope.getdata = function () {

/*
var myApp = angular
            .module("myModule", [])
            .controller("myController", function ($scope) {
                $scope.message = "Hello Angular!";

                var countries = [
                    {
                        name: "UK",
                        cities: [
                            { name: "London" },
                            { name: "Birminham" },
                            { name: "Manchester" }
                        ]
                    },
                    {
                        name: "USA",
                        cities: [
                            { name: "San Francisco" },
                            { name: "Washington DC" },
                            { name: "Texas" }
                        ]
                    }
                ];

                var technologies = [
                    { name: "C#", Likes: 0, Dislikes: 0 },
                    { name: "ASP .NET", Likes: 0, Dislikes: 0 },
                    { name: "SQL Server", Likes: 0, Dislikes: 0 },
                    { name: "AngularJS", Likes: 0, Dislikes: 0 }
                ]

                $scope.countries = countries;
                $scope.technologies = technologies;

                $scope.incrementLikes = function (technology) {
                    technology.Likes++;
                }
                $scope.incrementDislikes = function (technology) {
                    technology.Dislikes++;
                }


            });
*/