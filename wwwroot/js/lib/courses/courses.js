var escoladevoce = escoladevoce || {};
escoladevoce.courses = {};

escoladevoce.courses.addToFavorites = function(videoId) {
    $.ajax({
        url: "/cursos/AddToFavorite",
        type: "post",
        data: {
            videoId: videoId,
        },beforeSend: function(){
            escoladevoce.ui.block("Seu video está sendo adicionado aos seus favoritos. Só mais um segundo amiga.");
        }, success: function(data){
            
        }, error: function(error){
            
        }, complete: function(){
            escoladevoce.ui.unblock();
        }
    })
}