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

escoladevoce.courses.startCourse = function(courseId, videoId) {
    $.ajax({
        url: "/cursos/startCourse",
        type: "post",
        data: {
            courseId: courseId,
        },beforeSend: function(){
            escoladevoce.ui.block("Estamos carregando o conteúdo do curso...");
        }, success: function(data){
            location.href = "/cursos/sala/" + courseId + "/video/" + videoId;
        }, error: function(error){
            
        }, complete: function(){
            escoladevoce.ui.unblock();
        }
    })
}