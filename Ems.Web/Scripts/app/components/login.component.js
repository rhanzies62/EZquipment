/// <reference path="../../app.module.js" />
/// <reference path="../../models/CompanyRegistrationModel.js" />
/// <reference path="../../views/companyRegister.component.html" />
/// <reference path="../../../loading.js" />
/// <reference path="../../models/Response.js" />
/// <reference path="../models/LoginCredential.js" />

emsModule
    .component('loginForm', {
        templateUrl: window.location.href + '/loginform',
        controller: function loginController(loginService,$scope) {
            $scope.loginCrendetial = new LoginCredential();
            $scope.login = function (login) {
                loader().show('');
                loginService.login(login, '', $('input[name="__RequestVerificationToken"]').val()).then(function (response) {
                    loader().hide();
                    if (!response.data.IsSuccess) {
                        $scope.registrationAlert = new Response(true, response.data.Message);
                    } else {
                        window.location = "dashboard/index";
                    }
                }, function () {
                    loader().hide();
                });
            }
        }
    });