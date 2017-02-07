var escoladevoce = escoladevoce || {};
escoladevoce.mensagem = {};

escoladevoce.mensagem.initMessagePlugin = function(){
    $target = $("#mensagens");
    $target.find(".panel-left").find(".mensagem").click(function(){
        $target.find(".panel-left").find(".mensagem").removeClass("active");
        $(this).addClass("active");
        escoladevoce.mensagem.getMessages($(this).data("user-id"));
        $(".panel-right").find(".message-person").text($(this).data("user-name"));
    });;
}

escoladevoce.mensagem.getMessages = function(from){
    $.ajax({
        url: "/mensagens/getMessages",
        data: {
            from: from
        }, beforeSend: function(){
            escoladevoce.ui.block("Estamos carregando suas mensagens. Ta quase acabando...");
        }, complete: function(){
            escoladevoce.ui.unblock();
        }, success: function(response){
            $(".panel-right .messages").html(response);
        }, error: function(error){
            console.log(error);
        }
    })
}

escoladevoce.mensagem.sendMessage = function(){
    var message = tinymce.get("newChatMessage").getContent();
    var id = $(".mensagem.active").data("user-id");

    $.ajax({
        url: "/mensagens/sendMessage",
        type: "post",
        data: {
            message: message,
            to: id
        }, beforeSend: function(){
            escoladevoce.ui.block("Estamos enviando sua mensagens. Só um segundo...");
        }, complete: function(){
            escoladevoce.ui.unblock();
        }, success: function(response){
            $(".panel-right .messages").html(response);
            escoladevoce.mensagem.getMessages(id);
        }, error: function(error){
            console.log(error);
            escoladevoce.mensagem.getMessages(id);
        }
    });

    return false;
}