﻿/// <reference path="../../app.module.js" />
/// <reference path="../../../angular.min.js" />

emsModule.service('homeService', function ($http, $q) {
    return {
        registerCompany: function (model) {
            var root = "http://localhost:56048/";
            var defer = $q.defer();
            console.log(model);
            $http({
                method: 'POST',
                url: root + '/home/CompanyRegister',
                data: model,
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {
                defer.resolve(response);
            }, function () {
                defer.reject();
            });

            return defer.promise;
        }
    }
});