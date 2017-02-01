var escoladevoce = escoladevoce || {};
escoladevoce.facebook = {};

escoladevoce.facebook.init = function() {}

escoladevoce.facebook.login = function() {
    FB.login(function(response) {
        if (response.authResponse) {
            FB.api('/me', { fields: 'id,name,email' }, function(response) {
                console.log('Successful login for: ' + response.name);
                console.log(response);

                var options = {};
                options.username = response.email;
                options.isFacebook = true;

                escoladevoce.ui.block("Estamos quase lá, aguenta só mais um pouquinho...");

                options.success = function(data) {
                    if (data.status) {
                        if (location.href.indexOf("escolabrilhante") > -1) {
                            location.href = "/escolabrilhante/home/home";
                        } else {
                            location.href = "/home/home";
                        }
                    } else {
                        escoladevoce.ui.notify.warning("Erro ao efetuar o login", "Amiga. Não conseguimos encontrar seu cadastro :(. Confira seus dados e tente novamente ;).");
                        escoladevoce.ui.unblock();
                    }
                }

                options.error = function(error) {
                    escoladevoce.ui.notify.warning("Erro ao efetuar o login", "Amiga. Não conseguimos encontrar seu cadastro :(. Confira seus dados e tente novamente ;).");
                    escoladevoce.ui.unblock();
                }

                escoladevoce.auth.dologin(options);
            });
        } else {
            console.log('User cancelled login or did not fully authorize.');
        }
    }, {
        scope: 'email, public_profile',
        return_scopes: true
    });
}

escoladevoce.facebook.cadastro = function() {
    FB.login(function(response) {
        if (response.authResponse) {
            FB.api('/me', { fields: 'id,name,email' }, function(response) {
                console.log('Successful login for: ' + response.name);
                console.log(response);

                var options = {};
                options.username = response.email;
                options.email = response.email;
                options.name = response.name;
                options.isFacebook = true;

                options.beforeSend = function() {
                    escoladevoce.ui.block("Estamos quase lá, aguenta só mais um pouquinho...");
                }

                options.success = function(data) {
                    if (data.status) {
                        if (location.href.indexOf("escolabrilhante") > -1) {
                            location.href = "/escolabrilhante/account?newcad";
                        } else {
                            location.href = "/account?newcad";
                        }
                    } else {
                        alert("Amiga. Não conseguimos encontrar seu cadastro :(. Confira seu e-mail e tente novamente ;).");
                        escoladevoce.ui.unblock();
                    }
                }

                options.error = function(error) {
                    escoladevoce.ui.notify.warning("Erro ao efetuar o cadastro", "Nome de usuário ou senha inválido.");
                    escoladevoce.ui.unblock();
                }

                escoladevoce.auth.signup(options);
            });
        } else {
            console.log('User cancelled login or did not fully authorize.');
        }
    }, {
        scope: 'email, public_profile',
        return_scopes: true
    });
}