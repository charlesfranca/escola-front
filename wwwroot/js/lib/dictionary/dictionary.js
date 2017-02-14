escoladevoce = escoladevoce || {};
escoladevoce.dictionary = {};

escoladevoce.dictionary.init = function() {

}

escoladevoce.dictionary.addDictionaryItem = function(dictionaryType, title, content) {
    $.ajax({
        url: "/cursos/adddictionaryitem",
        data: {
            dictionaryType: dictionaryType,
            title: title,
            content: content
        }, beforeSend: function(){
            escoladevoce.ui.block('Estamos adicionando esse novo conteúdo ao seu dicionário. Espera só um minutinh');
        },
        type: "post",
        success: function(data){
            console.log(data);
        }, error: function(err){
            console.log(err);
        }, complete: function(){
            escoladevoce.ui.unblock();
        }
    });
}

escoladevoce.dictionary.addDictionaryClassItem = function(){
    var title = $("#class-dictionary-title").val();
    var content = tinymce.get("classDictionaryContent").getContent();
    var dictionaryType = 0;
    escoladevoce.dictionary.addDictionaryItem(dictionaryType, title, content);
}

escoladevoce.dictionary.addDictionaryDreamItem = function(){
    var title = $("#dreams-dictionary-title").val();
    var content = tinymce.get("dreamsDictionaryContent").getContent();
    var dictionaryType = 1;
    escoladevoce.dictionary.addDictionaryItem(dictionaryType, title, content);
}

escoladevoce.dictionary.addDictionaryChallengeItem = function(){
    var title = $("#challenges-dictionary-title").val();
    var content = tinymce.get("challengesDictionaryContent").getContent();
    var dictionaryType = 2;
    escoladevoce.dictionary.addDictionaryItem(dictionaryType, title, content);
}