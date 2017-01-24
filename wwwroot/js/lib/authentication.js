escoladevoce = escoladevoce || {};
escoladevoce.auth = {};

escoladevoce.auth.init = function() {
    // validate signup form on keyup and submit
    $("#signup-basic-data").validate({
        rules: {

        },
        messages: {

        },
        submitHandler: function(form) {
            escoladevoce.auth.goToStep(2);
        }
    });

    // validate signup form on keyup and submit
    $("#signup-password-form").validate({
        rules: {
            password: "required",
            passwordConfirmation: {
                equalTo: "#password"
            },
            name: "required",
            email: {
                required: true,
                email: true
            },
            emailConfirmation: {
                equalTo: "#email"
            },
            terms: "required"
        },
        messages: {
            password: "Informe uma senha.",
            passwordConfirmation: {
                equalTo: "Confirmação diferente da senha."
            },
            terms: "É necessário aceitar os termos.",
            name: "Digite seu nome.",
            email: {
                required: "Digite seu e-mail.",
                email: "E-mail digitado inválido."
            },
            emailConfirmation: {
                equalTo: "Confirmação diferente do E-mail informado.",
                email: "E-mail digitado inválido."
            }
        },
        submitHandler: function(form) {
            if (!$("#terms").is(":checked")) {
                alert("Para poder concluir seu cadastro você precisa informar que esta de acordo com os termos de privacidade.");
                return false;
            }
            var options = {};
            options.username = $("form#signup-password-form input#email").val();
            options.email = $("form#signup-password-form input#email").val();
            options.name = $("form#signup-password-form input#name").val();
            options.password = $("form#signup-password-form input#password").val();

            options.beforeSend = function() {
                escoladevoce.ui.block("Estamos quase lá, aguenta só mais um pouquinho...");
            }

            options.complete = function() {
                escoladevoce.ui.unblock();
            }

            options.success = function(data) {
                if (data.status) {
                    location.href = "/account";
                } else {
                    alert("Ocorreu um erro ao efetuar seu login.");
                }
            }

            options.error = function(error) {
                escoladevoce.ui.notify.warning("Erro ao efetuar o cadastro", "Nome de usuário ou senha inválido.");
            }

            escoladevoce.auth.signup(options);
        }
    });

    // validate signup form on keyup and submit
    $("form#login").validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            password: "required",
        },
        messages: {
            email: {
                required: "Digite seu e-mail.",
                email: "E-mail digitado inválido."
            },
            password: "Informe uma senha.",
        },
        submitHandler: function(form) {
            var options = {};
            options.username = $("form#login input#email").val();
            options.password = $("form#login input#password").val();
            options.isFacebook = false;

            options.beforeSend = function() {
                escoladevoce.ui.block("Estamos quase lá, aguenta só mais um pouquinho...");
            }

            options.complete = function() {
                escoladevoce.ui.unblock();
            }

            options.success = function(data) {
                if (data.status) {
                    location.href = "/home/home";
                } else {
                    alert("Ocorreu um erro ao efetuar seu login.");
                }
            }

            options.error = function(error) {
                escoladevoce.ui.notify.warning("Erro ao efetuar o login", "Nome de usuário ou senha inválido.");
            }

            escoladevoce.auth.dologin(options);
        }
    });
}

/// Method: login
/// Params:
/// string -> username
/// string -> passoword
/// bool -> isFacebook
escoladevoce.auth.dologin = function(options) {
    $.ajax({
        url: escoladevoce.endpoints.login,
        type: "post",
        dataType: "json",
        data: {
            username: options.username,
            password: options.password,
            isFacebook: options.isFacebook
        },
        beforeSend: function() {
            if (options.beforeSend) {
                options.beforeSend();
            }
        },
        complete: function() {
            if (options.complete) {
                options.complete();
            }
        },
        success: function(data) {
            if (options.success) {
                options.success(data);
            }
        },
        error: function(error) {
            if (options.error) {
                options.error(error);
            }
        }
    })
}

escoladevoce.auth.signup = function(options) {
    $.ajax({
        url: escoladevoce.endpoints.signup,
        type: "post",
        dataType: "json",
        data: {
            username: options.username,
            password: options.password,
            name: options.name,
            email: options.email,
            lastname: options.lastname
        },
        beforeSend: function() {
            if (options.beforeSend) {
                options.beforeSend();
            }
        },
        complete: function() {
            if (options.complete) {
                options.complete();
            }
        },
        success: function(data) {
            if (options.success) {
                options.success(data);
            }
        },
        error: function(error) {
            if (options.error) {
                options.error(error);
            }
        }
    })
}

escoladevoce.auth.openSignupForm = function() {
    // $('#signupmodal').modal({ backdrop: 'static', keyboard: false });
    $('#signupmodal').modal('show');
}

escoladevoce.auth.openLoginForm = function() {
    // $('#loginModal').modal({ backdrop: 'static', keyboard: false });
    $('#loginModal').modal('show');
}

escoladevoce.auth.goToStep = function(step) {
    $('#signupmodal').removeClass('step1').removeClass('step2');
    $('#signupmodal').addClass('step' + step);
}