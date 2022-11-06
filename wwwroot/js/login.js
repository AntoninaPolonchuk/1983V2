
$(document).ready(function () {

    $('.CheckLoginView').hide();
    $('.RegisterLoginView').hide();

    setInterval(function () { BroilerMove() }, 2000);

    let loadInfoStatus = Boolean(loadRegisterInfo.status);
    let loadInfoText = loadRegisterInfo.text;

    Loadstatus(loadInfoStatus);
    $('.infoText').text(loadInfoText);
});


function Loadstatus(loadInfo) {
    if (loadInfo == true) {
        
        $('.RegisterLoginView').show();
        
    }
    else if (loadInfo == false) {
        
        $('.CheckLoginView').show();
        
    }
}


/* БРОЙЛЕР */
function BroilerMove() {

    $('.broiler').css('top', '0px');
    $('.broiler').css('right', '-250px');
    $('.broiler').css('transition', '0s')


    setTimeout(function () {
        $('.broiler').css('top', '110vh')
        $('.broiler').css('right', '550px')
        $('.broiler').css('transition', '3s')

    }, 2000)
}





