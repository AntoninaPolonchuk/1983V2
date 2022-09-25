$(document).ready(function () {

    housePlase();

    //$('body').css({ 'overflow': 'hidden' });
    //$('body').css({ 'transition': '1s' });
    //$('body').css({ 'animation': 'day 20s ease-in-out infinite' });

});

//let housestatus = "1234454212334543226";

//function housePlase() {

//    for (let i = 1; i < 20; i++) {
//        $('#Yl_' + i + ' #Дом_' + housestatus[i-1]).show();
//    }
//}



/* САМА ИГРА */

let $drag = $('.move').draggabilly({})


/*console.log($(this).parent().attr("class"))*/



let buttonplace1 = '-150px';
let buttonplace2 = '50px';



/*Домики, которые строятся с поля*/

$('#Дом_6').click(function () {

    let getID = $(this).parent().attr("id");
    /*проверка для того, чтобы не строились все домики, на которые был клик*/
    let check = false;
    let check2 = false;


    $('.buttons2').css({ 'bottom': buttonplace2 });
    check = true;
    forbidClick();

    /*прозрачный домик до выбора*/
    check2 = true;
    $('#' + getID + ' #Дом_6').hide();
    $('#' + getID + ' #Дом_1').show().css({ 'fill-opacity': '0.5' });


    $('#Отмена2').click(function () {

        $('.buttons2').css({ 'bottom': buttonplace1 });

        /*убираем прозначный домик*/
        if (check2 == true & check == true) {
            $('#' + getID + ' #Дом_6').show();
            $('#' + getID + ' #Дом_1').hide();
            check2 == false;
        }
        check = false;
        allowClick();
    });

    $('#Построить').click(function () {
        if (check == true) {
            $('#' + getID + ' #Дом_6').hide();
            $('#' + getID + ' #Дом_1').show().css({ 'fill-opacity': '1' });
            $('.buttons2').css({ 'bottom': buttonplace1 });
            check = false;
            allowClick();
            check2 = false;
        }
    });
});


$('#Дом_1').click(function () {

    let getID = $(this).parent().attr("id");
    let check = false;


    $('.buttons').css({ 'bottom': buttonplace2 });


    check = true;
    forbidClick();


    $('#Отмена').click(function () {

        $('.buttons').css({ 'bottom': buttonplace1 });
        check = false;
        allowClick();
    });

    $('#Улучшить').click(function () {
        if (check == true) {
            $('#' + getID + ' #Дом_1').hide();
            $('#' + getID + ' #Дом_2').show();
            $('.buttons').css({ 'bottom': buttonplace1 });
            check = false;
            allowClick();
        }
    });
});

/*Домики, которые со старта*/
$('#Дом_0').click(function () {

    let getID = $(this).parent().attr("id");
    let check = false;

    $('.buttons').css({ 'bottom': buttonplace2 });
    check = true;
    forbidClick();

    $('#Отмена').click(function () {

        $('.buttons').css({ 'bottom': buttonplace1 });
        check = false;
        allowClick();
    });

    $('#Улучшить').click(function () {
        if (check == true) {
            $('#' + getID + ' #Дом_0').hide();
            $('#' + getID + ' #Дом_2').show();
            $('.buttons').css({ 'bottom': buttonplace1 });
            check = false;
            allowClick();
        }
    });
});


$('#Дом_2').click(function () {

    let getID = $(this).parent().attr("id");
    let check = false;

    $('.buttons').css({ 'bottom': buttonplace2 });
    check = true;
    forbidClick();

    $('#Отмена').click(function () {

        $('.buttons').css({ 'bottom': buttonplace1 });
        check = false;
        allowClick();
    });

    $('#Улучшить').click(function () {
        if (check == true) {
            $('#' + getID + ' #Дом_2').hide();
            $('#' + getID + ' #Дом_3').show();
            $('.buttons').css({ 'bottom': buttonplace1 });
            check = false;
            allowClick();
        }
    });

});

$('#Дом_3').click(function () {

    let getID = $(this).parent().attr("id");
    let check = false;

    $('.buttons').css({ 'bottom': buttonplace2 });
    check = true;
    forbidClick();

    $('#Отмена').click(function () {

        $('.buttons').css({ 'bottom': buttonplace1 });
        check = false;
        allowClick();
    });

    $('#Улучшить').click(function () {
        if (check == true) {
            $('#' + getID + ' #Дом_3').hide();
            $('#' + getID + ' #Дом_4').show();
            $('.buttons').css({ 'bottom': buttonplace1 });
            check = false;
            allowClick();
        }
    });

});

$('#Дом_4').click(function () {

    let getID = $(this).parent().attr("id");
    let check = false;

    $('.buttons').css({ 'bottom': buttonplace2 });
    check = true;
    forbidClick();

    $('#Отмена').click(function () {

        $('.buttons').css({ 'bottom': buttonplace1 });
        check = false;
        allowClick();
    });

    $('#Улучшить').click(function () {
        if (check == true) {
            $('#' + getID + ' #Дом_4').hide();
            $('#' + getID + ' #Дом_5').show();
            $('.buttons').css({ 'bottom': buttonplace1 });
            check = false;
            allowClick();
        }
    });
});


/*Убираем возможность повторного клика на домик*/
function forbidClick() {
    $('#Дом_6').css({ 'pointer-events': 'none' })
    $('#Дом_0').css({ 'pointer-events': 'none' })
    $('#Дом_1').css({ 'pointer-events': 'none' })
    $('#Дом_2').css({ 'pointer-events': 'none' })
    $('#Дом_3').css({ 'pointer-events': 'none' })
    $('#Дом_4').css({ 'pointer-events': 'none' })
    $('#Дом_5').css({ 'pointer-events': 'none' })
}

function allowClick() {
    $('#Дом_6').css({ 'pointer-events': 'auto' })
    $('#Дом_0').css({ 'pointer-events': 'auto' })
    $('#Дом_1').css({ 'pointer-events': 'auto' })
    $('#Дом_2').css({ 'pointer-events': 'auto' })
    $('#Дом_3').css({ 'pointer-events': 'auto' })
    $('#Дом_4').css({ 'pointer-events': 'auto' })
    $('#Дом_5').css({ 'pointer-events': 'auto' })
}



