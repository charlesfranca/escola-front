var escoladevoce = escoladevoce || {};
escoladevoce.quiz = {};

escoladevoce.quiz.answerQuestion = function(rightanswerId, leftanswerId){
    if($( "#slider" ).slider( "value" ) == 50){
        alert("Movimente a bolinha para a esquerda ou para a direita para escolher o valor de sua resposta.");
        return false;
    }
    $.ajax({
        url: "/quiz/answerQuestion",
        type: "post",
        data: {
            answerId: $( "#slider" ).slider( "value" ) > 50 ? rightanswerId : leftanswerId,
            score: $( "#slider" ).slider( "value" )
        },beforeSend: function(){
            escoladevoce.ui.block("Muito bom amiga. Estamos começando a nos conhecer melhor ;).");
        }, success: function(data){
            escoladevoce.quiz.getNextQuestion();
        }, error: function(error){
            console.log(error);
        }, complete: function(){
            escoladevoce.ui.unblock();
        }
    })
}

escoladevoce.quiz.getNextQuestion = function(){
    $.ajax({
        url: "/quiz/getNextQuestion",
        type: "get",
        beforeSend : function(){
            escoladevoce.ui.block("Estou verificando se tem mais alguma perguntinha. Aguenta só mais um pouquinho...");
        }, success: function(data){
            $("#quiz-container").html(data);
        }, error: function(error){
            console.log(error);
        }, complete: function(){
            escoladevoce.ui.unblock();
        }
    })
}