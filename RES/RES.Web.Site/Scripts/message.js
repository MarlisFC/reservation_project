﻿
function setMessage(type,message) {
        Command: toastr[type](message, "Reservation Notiﬁcation");
                toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
       "showMethod": "fadeIn",
       "hideMethod": "fadeOut"
};  
}

