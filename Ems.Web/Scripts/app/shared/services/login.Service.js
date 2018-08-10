/// <reference path="../../app.module.js" />
/// <reference path="../../../angular.min.js" />

emsModule.service('loginService', function ($http, $q) {
    return {
        login: function (model,returnurl, token) {
            var root = "http://localhost:56048/";
            //var root = "http://localhost/ems";
            var defer = $q.defer();
            $http.defaults.headers.common['X-XSRF-Token'] = token;
            $http({
                method: 'POST',
                url: root + '/login/login',
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