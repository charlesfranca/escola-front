// Write your Javascript code.
var divHeight = 750;
function setMainSize(){
    var documentHeight = $(window).height();
    if (documentHeight > divHeight) {
        divHeight = documentHeight;
    }
}

$.fn.extend({
    animateCss: function (animationName, intervalToStart, callback) {
        var $element = $(this);
        var interval = 0;
        if (intervalToStart) {
            interval = intervalToStart * 1000;
        }
        
        setTimeout(function (){
            $element.css('visibility', 'visible');
            var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
            $element.addClass('animated ' + animationName).one(animationEnd, function() {
                if (callback) {
                    callback();
                }
                $element.removeClass('animated ' + animationName);
            });
        }, interval);
    }
});

$(document).ready(function(){
    if ($(window).width() > 768) {
        setMainSize();
        $(document).on("resize", function () {
            setMainSize();
        });
    }
});

$(function () {
    var shrinkHeader = 100;
    var documentHeight = $(window).height();
    if ($(window).width() > 768) {

        $(window).scroll(function () {
            var scroll = getCurrentScroll();
            
            if($("body.logged").length > 0){
                divHeight = 446;
            }else{
                divHeight = 600;
            }

            if (scroll >= (divHeight - 70)) {
                $('.navbar').css({display: "none"});
            } else {
                $('.navbar').css({display: "block"});
            }

            if (scroll >= (divHeight)) {
                $('section#menu').addClass("fixed-nav");
            } else {
                $('section#menu').removeClass("fixed-nav");
            }
        });

        function getCurrentScroll() {
            return window.pageYOffset || document.documentElement.scrollTop;
        }
    }
});