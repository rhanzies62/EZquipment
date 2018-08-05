/// <reference path="../../app.module.js" />
/// <reference path="../../models/CompanyRegistrationModel.js" />
/// <reference path="../../views/companyRegister.component.html" />
/// <reference path="../../../loading.js" />

emsModule
    .component('companyRegistration', {
        templateUrl: '/scripts/app/views/companyRegister.component.html',
        controller: function homeController(homeService) {
            this.CompanyRegister = new CompanyRegistrationModel();
            this.clickRegister = function (model) {
                loader().show('Submitting Information please wait');
                homeService.registerCompany({ model: model, __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() }).then(function (response) {
                    loader().hide();
                }, function () {
                    loader().hide();
                });
                return false;
            }
            this.getMaxYear = function () {
                var date = new Date();
                return date.getFullYear();
            }
        }
    })
