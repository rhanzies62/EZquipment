/// <reference path="../../app.module.js" />
/// <reference path="../../../angular.min.js" />

emsModule.service('userService', function ($http, $q) {
    return {
        getProfile: function (model, returnurl, token) {
            var root = "http://localhost:56048/";
            //var root = "http://localhost/ems";
            var defer = $q.defer();
            $http({
                method: 'GET',
                url: root + '/Profile/GetProfile',
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