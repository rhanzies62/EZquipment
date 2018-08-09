/// <reference path="../../app.module.js" />
/// <reference path="../../models/CompanyRegistrationModel.js" />
/// <reference path="../../views/companyRegister.component.html" />
/// <reference path="../../../loading.js" />
/// <reference path="../../models/Response.js" />
/// <reference path="../models/LoginCredential.js" />

emsModule
    .component('loginForm', {
        templateUrl: window.location.href + '/loginform',
        controller: function loginController($scope) {
            $scope.loginCrendetial = new LoginCredential();
            $scope.login = function (login) {
                console.log(login);
            }
        }
    });