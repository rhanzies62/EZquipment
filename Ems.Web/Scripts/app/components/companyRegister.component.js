/// <reference path="../../app.module.js" />
/// <reference path="../../models/CompanyRegistrationModel.js" />
/// <reference path="../../views/companyRegister.component.html" />
/// <reference path="../../../loading.js" />
/// <reference path="../../models/Response.js" />

emsModule
    .component('companyRegistration', {
        templateUrl: window.location.origin + '/home/CompanyRegister',
        controller: function homeController(homeService,$scope) {
            $scope.CompanyRegister = new CompanyRegistrationModel();
            $scope.pattern = {
                emailPattern: /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i
            };
            $scope.registrationAlert = new Response(false, "");
            $scope.clickRegister = function (model) {
                loader().show('Submitting Information please wait');
                homeService.registerCompany(model, $('input[name="__RequestVerificationToken"]').val()).then(function (response) {
                    loader().hide();
                    if (!response.data.IsSuccess) {
                        $scope.registrationAlert = new Response(true, response.data.Message);
                    } else {
                        window.location = "home/success";
                    }
                }, function () {
                    loader().hide();
                });
                return false;
            }
            $scope.getMaxYear = function () {
                var date = new Date();
                return date.getFullYear();
            }
        }
    })
