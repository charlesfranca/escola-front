var escoladevoce = {};

escoladevoce.endpoints = {
    login: "/account/login",
    authenticate: "/account/authenticateuser",
    signup: "/account/signup"
}

escoladevoce.initSliders = function() {
    $(document).ready(function() {
        $(".slick-slider-container").each(function(index, value) {
            var columns = 3;
            var containerColumns = $(this).data("slick-collumns");
            var slider = $(this).find(".is-slick-slider");
            slider.slick({
                infinite: false,
                speed: 300,
                slidesToShow: containerColumns || columns,
                prevArrow: $(this).find(".prev-arrow"),
                nextArrow: $(this).find(".next-arrow"),
                adaptiveHeight: true,
                responsive: [{
                        breakpoint: 768,
                        settings: {
                            slidesToShow: 1,
                            slidesToScroll: 1
                        }
                    },
                    {
                        breakpoint: 480,
                        settings: {
                            slidesToShow: 1,
                            slidesToScroll: 1
                        }
                    }
                ],
                arrows: true
            });
        })
    })
}

escoladevoce.ui = {
    block: function(message) {
        $.blockUI({
            message: message,
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff',
                "z-index": 5011
            }
        });
    },
    unblock: function() {
        $.unblockUI();
    },
    notify: {

        warning: function(title, message) {
            $.notify({
                // options
                // icon: 'glyphicon glyphicon-warning-sign',
                // title: title,
                message: message,
                // url: 'https://github.com/mouse0270/bootstrap-notify',
                // target: '_blank'
            }, {
                // settings
                element: 'body',
                position: "absolute",
                type: "warning",
                allow_dismiss: true,
                newest_on_top: true,
                showProgressbar: false,
                placement: {
                    from: "top",
                    align: "center"
                },
                offset: 20,
                "offset.y": 200,
                spacing: 10,
                z_index: 5000,
                delay: 3000,
                timer: 1000,
                // url_target: '_blank',
                mouse_over: null,
                animate: {
                    enter: 'animated fadeInDown',
                    exit: 'animated fadeOutUp'
                },
                onShow: null,
                onShown: null,
                onClose: null,
                onClosed: null,
                icon_type: 'class',
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span> ' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
            });
        }

    }
}

escoladevoce.console = {
    log: {
        type: {
            ERROR: "ERROR",
            WARNING: "WARNING",
            DEBUG: "DEBUG",
            INFO: "INFO",
            FATAL: "FATAL"
        },
        debug: function(message) {
            escoladevoce.console.doLog(escoladevoce.console.log.type.DEBUG, message);
        },
        error: function(message) {
            escoladevoce.console.doLog(escoladevoce.console.log.type.ERROR, message);
        },
        fatal: function(message) {
            escoladevoce.console.doLog(escoladevoce.console.log.type.FATAL, message);
        },
        info: function(message) {
            escoladevoce.console.doLog(escoladevoce.console.log.type.INFO, message);
        },
        warning: function(message) {
            escoladevoce.console.doLog(escoladevoce.console.log.type.WARNING, message);
        }
    },
    doLog: function(logType, message) {
        console.log(logType);
        console.log(message);
    }
}

escoladevoce.uploadPhoto = function(inputFile) {
    escoladevoce.ui.block("Estamos atualizando seu cadastro tá. Aguenta só um pouquinho que jaja termina :).");
    var fileReader = new FileReader();
    fileReader.onload = function(fileLoadedEvent) {
        $.ajax({
            url: "/account/ProfilePhotoUpload",
            type: "post",
            data: {
                image: fileLoadedEvent.target.result
            },
            success: function(data) {
                if (data.success) {
                    location.reload();
                }
                console.log(data);
            },
            error: function(err) {
                console.log(err);
            },
            complete: function() {
                escoladevoce.ui.unblock();
            }
        });
    };

    var file = inputFile.files[0];
    fileReader.readAsDataURL(file);
}

escoladevoce.uploadCover = function(inputFile) {
    escoladevoce.ui.block("Estamos atualizando seu cadastro tá. Aguenta só um pouquinho que jaja termina :).");
    var fileReader = new FileReader();
    fileReader.onload = function(fileLoadedEvent) {
        $.ajax({
            url: "/account/CoverPhotoUpload",
            type: "post",
            data: {
                image: fileLoadedEvent.target.result
            },
            success: function(data) {
                if (data.success) {
                    location.reload();
                }
                console.log(data);
            },
            error: function(err) {
                console.log(err);
            },
            complete: function() {
                escoladevoce.ui.unblock();
            }
        })
    };

    var file = inputFile.files[0];
    fileReader.readAsDataURL(file);
}