/// <reference path="jquery-3.0.0.js" />

function loader() {
    return {
        show: function (message) {
            var loading = $('<div></div>');
            loading.addClass('loading').text(message);
            $('html').append(loading);
        },
        hide: function () {
            $('.loading').remove();
        }
    };
}