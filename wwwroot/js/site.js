
// Меню для компа

$(document).ready(function () {
    setInterval(function () { BroilerMove() }, 2000);
});





/* БРОЙЛЕР */
function BroilerMove() {

    $('.broiler').css('top', '0px');
    $('.broiler').css('right', '-200px');
    $('.broiler').css('transition', '0s')


    setTimeout(function () {
        $('.broiler').css('top', '550px')
        $('.broiler').css('right', '550px')
        $('.broiler').css('transition', '3s')

    }, 2000)
}






