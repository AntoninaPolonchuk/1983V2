
let login="";

$('.LoginButton').click(function () {
    console.log($('#login').val());
    login = $('#login').val();
    //CheckLogin();

});


//function CheckLogin() {

//    $.ajax({
//        type: "POST",
//        url: "Home/CheckLogin",       //контроллер
//        async: true,
//        cashe: false,
//        timeout: 0,
//        data: { log: login },
//        dataType: "json", //

//        success: function (responce) {
//            console.log(responce);

//        },
//        error: function (XML, status, error) {
//            console.log(status);

//        }
//    })
//}







