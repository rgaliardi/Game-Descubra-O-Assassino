var pathHTTP = window.location.href.match(/^http:\/\/[^/]+/)[0] + window.location.pathname.substring(0, window.location.pathname.indexOf('/', 1)) + '/';
var pathRaiz = window.location.pathname.substring(0, window.location.pathname.lastIndexOf('/') + 1);
var pathLocation = window.location.pathname;
var serverTime = new Date();
var wWidth = $(window).width();
var wHeight = $(window).height();
var searchFirst;

$(document).on("ajaxStart", "ajaxSend", "load", function () {
    waiting(true);
});

//This handles the queues    
(function ($) {
    $.ajaxSetup({ cache: false });
})(jQuery);

//This handles the loaded
$(document).ready(function () {

    $('a.button').click(function (e) {
        waiting(true);
    });
});

var displayMessage = function (message, type) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-center",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "11000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    toastr[type](message);

    if ($('#success').val()) {
        displayMessage($('#success').val(), 'success');
    }
    if ($('#info').val()) {
        displayMessage($('#info').val(), 'info');
    }
    if ($('#warning').val()) {
        displayMessage($('#warning').val(), 'warning');
    }
    if ($('#error').val()) {
        displayMessage($('#error').val(), 'error');
    }

    $("#btnConfirm").on("click", function (event) {
        location.reload();
    });

    $("#btnQuit").on("click", function (event) {
        window.location.href = pathHTTP + 'home/index';
    });   
};

// bloqueia teclas de suporte: f5...
var checkKeyCode = (function (evt) {
    var evts = evt ? evt : (event ? event : null);
    var node = evts.target ? evts.target : (evts.srcElement ? evts.srcElement : null);

    if (event !== null && event !== undefined) {

        if (event.keyCode === 116)//disable F5
        {
            evts.keyCode = 0;
            return false;
        }
    }
});

var waiting = (function (ative) {

    if (ative) {
        // add the overlay with loading image to the page
        var over = '<div id="loading"></div>';
        $(over).appendTo('body');
    } else {
        $('#loading').remove();
    }
});

function callMethodAPI(action, parameters) {
    var result; 

    $.ajax({
        url: pathHTTP + action,
        type: 'GET',
        dataType: "json",
        data: parameters,
        async: false,
        cache: false,
        success: function (result) {
            result = "Success";
            waiting(false);
        },
        error: function (xhr, status, error) {
            operationError(xhr, status, error);
            result = "Error";
        }
    });
    return result;
}

var operationError = function (xhr, status, error) {

    if (xhr.responseText !== undefined) {

        if (xhr.responseText.indexOf("Sua sessão foi expirada!") >= 0)
            displayMessage("Sua sessão foi expirada!", "info");
        else if (xhr.responseText.indexOf("Notify.show") >= 0 || xhr.responseText.indexOf("Usuário sem permissão de acesso!") >= 0)
            displayMessage("Usuário sem permissão de acesso a funcionalidade!", "info");
        else {
            displayMessage(xhr.responseText.text, "error");
        }
    } else {
        displayMessage(xhr.statusText, "error");
    }
    waiting(false);
};


