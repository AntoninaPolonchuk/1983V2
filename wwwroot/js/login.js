
$(document).ready(function () {

    $('.CheckLoginView').hide();
    $('.RegisterLoginView').hide();

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








