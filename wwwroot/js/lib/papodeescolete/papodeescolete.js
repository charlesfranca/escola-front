var escoladevoce = escoladevoce || {};
escoladevoce.papodeescolete = {};

escoladevoce.papodeescolete.saveComment = function(escoletetalkid) {
    var message = tinymce.get("newMessage").getContent();

    $.ajax({
        url: "/papodeescolete/SaveComment",
        type: "post",
        data: {
            comment: message,
            id: escoletetalkid
        },
        beforeSend: function() {
            escoladevoce.ui.block("Estamos enviando seu comentário amiga ;). Vai levar só alguns segundinhos.");
        },
        success: function(retorno) {
            if (retorno.success) {
                location.reload();
            } else {
                escoladevoce.ui.notify.warning("Erro ao enviar comentário", "Amiga. Tivemos um pequeno problema enviando seu comentário. Tenta novamente por favor ;).");
            }
        },
        error: function(erro) {

        }
    });
}