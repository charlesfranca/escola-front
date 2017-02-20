escoladevoce = escoladevoce || {};
escoladevoce.account = {};

escoladevoce.account.init = function() {

    // validate signup form on keyup and submit
    $("#personalData").validate({
        rules: {
            name: "required",
            email: {
                required: true,
                email: true
            },
            emailConfirmation: {
                equalTo: "#email"
            },
            cpf: "required",
            genre: "required",
            birthday: "required"
        },
        messages: {
            name: "Digite seu nome.",
            email: {
                required: "Digite seu e-mail.",
                email: "E-mail digitado inválido."
            },
            emailConfirmation: {
                equalTo: "Confirmação diferente do E-mail informado.",
                email: "E-mail digitado inválido."
            },
            cpf: "Informe o CPF",
            genre: "Selecione um gênero",
            birthday: "Informe a data de nascimento"
        },
        submitHandler: function(form) {
            $.ajax({
                url: "/account/updateaccount",
                data: $("#personalData").serialize(),
                type: "post",
                beforeSend: function(){
                    escoladevoce.ui.block("Amiga. Estamos atualizando suas informações. Só vai levar um minuto.");
                },
                success:function(data){
                    console.log(data)
                }, error: function(err){
                    console.log(err);
                }, complete:function(){
                    escoladevoce.ui.unblock();
                }
            });
        }
    });
}

escoladevoce.account.getAddressData = function(cep){
    var onlynumberCep = cep.replace(/\D/, "");
    
    if(onlynumberCep.length == 8){
        $.ajax({
            url: "/account/consultaCep",
            data: {
                cep: cep
            }, beforeSend: function(){
                escoladevoce.ui.block("Estamos buscando as informações do seu CEP...");
            }, complete: function(){
                escoladevoce.ui.unblock();
            }, success: function(response){
                $("#addressStreet").val(response.logradouro);
                $("#addressCity").val(response.localidade);
                $("#addressState").val(response.uf);
                $("#addressNeighborhood").val(response.bairro);
                console.log(response);
            }, error: function(error){
                console.log(error);
            }
        });
    }
}

escoladevoce.account.updateData = function(options) {
    $.ajax({
        url: "/",
        type: "post",
        dataType: "json",
        data: {
            username: options.username,
            password: options.password,
            name: options.name,
            email: options.email,
            cover: options.cover,
            image: options.image,
            lastname: options.lastname,
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