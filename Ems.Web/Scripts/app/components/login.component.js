/// <reference path="../../app.module.js" />
/// <reference path="../../models/CompanyRegistrationModel.js" />
/// <reference path="../../views/companyRegister.component.html" />
/// <reference path="../../../loading.js" />
/// <reference path="../../models/Response.js" />
/// <reference path="../models/LoginCredential.js" />

emsModule
    .component('loginForm', {
        templateUrl: window.location.origin + '/login/loginform',
        controller: function loginController(loginService, $scope, $location) {
            $scope.loginCrendetial = new LoginCredential();
            $scope.login = function (login) {
                var url = new URL(window.location.href);
                var returnUrl = url.searchParams.get("ReturnUrl");
                returnUrl = returnUrl == null ? "dashboard/index" : returnUrl;
                loader().show();
                loginService.login(login, '', $('input[name="__RequestVerificationToken"]').val()).then(function (response) {
                    if (!response.data.IsSuccess) {
                        loader().hide();
                        $scope.registrationAlert = new Response(true, response.data.Message);
                    } else {
                        window.location = returnUrl;
                    }
                }, function () {
                    loader().hide();
                });
            }
        }
    });