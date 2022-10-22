
let $drag = $('.move').draggabilly({}) // движение платформы

let buttonplace1 = '-25vh'; //выезд таблички построить-отмена
let buttonplace2 = '0';
let CurrentHouse;
let Houses = houseStateData.HouseList;
let ImproveCost;
let Taxes = 0;
let Rent = 0;

$(document).ready(function () {

    HouseForeach(Houses);
    GameLoad();
    GameSave();
    LevelDown();
    
    setTimeout(function () {
        PayTaxes();
    }, 25000)

    setTimeout(function () {
        GetRent();
    }, 50000)

    //$('body').css({ 'overflow': 'hidden' });
    //$('body').css({ 'transition': '1s' });
    //$('body').css({ 'animation': 'day 20s ease-in-out infinite' });
});

/*console.log($(this).parent().attr("class"))*/

function HouseForeach(Houses) {

    for (let i = 0; i < Houses.length; i++) {

        LoadHouseInfo(Houses[i]); 
    }
}

function LoadHouseInfo(House) {

    for (let i = 0; i <= 5; i++) { // Прячем лишние домики
        $('#' + House.Position + ' #Дом_' + i).hide(); 
    }

    $('#' + House.Position + ' #Дом_' + House.Level).show().css({ 'fill-opacity': House.Visability});;
}

// Постройка домиков по клику
$('.house').click(function () {

    let housePlase = $(this).attr("id");
    CurrentHouse = Houses.find(x => x.Position == housePlase);

    $('#TextChange').text("Улучшить");
    if (CurrentHouse.Level == 0) {
        $('#TextChange').text("Построить");  
    }
    else if (CurrentHouse.Level == 5) {
        ChangeText("Домик достиг максимального уровня");
        return;
    }
    forbidClick();

    ImprovementInfo(CurrentHouse.Level); 
    $('.downInfo').css({ 'bottom': buttonplace2 });
});

$('.buttonpartСansel').click(function () {
    $('.downInfo').css({ 'bottom': buttonplace1 });
    allowClick();
});

$('.buttonpartImprove').click(function () { 

    CurrentHouse.Level++;
    CurrentHouse.DaysForLevelDown = 300000;
    houseStateData.MoneyRest -= ImproveCost;
    $('.count').text(houseStateData.MoneyRest);
    CurrentHouse.Visability = 1;

    HouseForeach(Houses);
    $('.downInfo').css({ 'bottom': buttonplace1 });
    allowClick();
    GameSave(); // Жрать не просит, но делает жизнь лучше. 
});

//Убираем возможность повторного клика на домик
function forbidClick() {

    for (let i = 0; i <= 5; i++) {
        $('#Дом_' + i).css({ 'pointer-events': 'none' })
    }
}

function allowClick() {

    for (let i = 0; i <= 5; i++) {
        $('#Дом_' + i).css({ 'pointer-events': 'auto' })
    }
}

// Сохранение состояния игры
function GameSave() {

    $.ajax({
        type: "POST",
        url: "GameSave",       //контроллер
        async: true,
        cashe: false,
        timeout: 15000,
        data: "datainfo=" + JSON.stringify(houseStateData),
        success: function (responce) {
            console.log(responce);
            setTimeout(function () {
                GameSave();
            }, 5000);
        },
        error: function (XML, status, error) {
            console.log(status);
            setTimeout(function () {
                GameSave();
            }, 10000);
        }
    })
}


let info = false;

//Понижение уровня домика
function LevelDown() {

    for (let i = 0; i < Houses.length; i++) { // ПЕРЕОСМЫСЛИТЬ ЭТО КУСОК
    
        if (Houses[i].DaysForLevelDown > 0) {
            Houses[i].DaysForLevelDown -= 50000; //ВРЕМЕННО. ПОМЕНЯТЬ ПРИМЕРНО НА 10000
        }
        else    
        {
            if (Houses[i].Level == 1 || Houses[i].Level == 5 || Houses[i].Level == 0) { // костыль. по идее, ничего не должно происходить   
                Houses[i].DaysForLevelDown = 0;
            }
            else {
                Houses[i].Level--;
                Houses[i].Visability = 1;
                HouseForeach(Houses);
                Houses[i].DaysForLevelDown = 300000;
            }
        }
        //console.log(Houses[i].DaysForLevelDown + " " + Houses[i].Level + " " + Houses[i].Position); /// проверка. убрать

        if (Houses[i].DaysForLevelDown == 100000) {
            if (Houses[i].Level == 2 || Houses[i].Level == 3 || Houses[i].Level == 4) {
                info = true;
                Houses[i].Visability = 0.6;
            }
        }
    }

    if (info == true) {
        ChangeText("Скоро понизится уровень домиков");
        HouseForeach(Houses);
        info = false;
    }

    setTimeout(function () {

        LevelDown();
    }, 10000)   
}

//Налоги по всем домикам
function PayTaxes() { //ДОБАВИТЬ В SQL ФИКСАЦИЮ ВРЕМЕНИ. МОЖНО ОБЩУЮ ДЛЯ ВСЕГО (НАЛОГОВ, АРЕНДЫ И УМЕНЬШЕНИЯ УРОВНЯ)

    for (let i = 0; i < Houses.length; i++) {
        if (Houses[i].Level == 1) {Taxes += 20000;}
        else if (Houses[i].Level == 2) {Taxes += 30000;}
        else if (Houses[i].Level == 3) {Taxes += 40000;}
        else if (Houses[i].Level == 4) {Taxes += 70000;}
        else if (Houses[i].Level == 5) {Taxes += 80000;}
    }
    houseStateData.MoneyRest -= Taxes;
    ChangeText("Заплатил " + Taxes + " налогов и спи спокойно");
    Taxes = 0;
    $('.count').text(houseStateData.MoneyRest);

    setTimeout(function () {
        PayTaxes();
    }, 100000)
}

// Аренда по всем домикам
function GetRent() { 

    for (let i = 0; i < Houses.length; i++) {
        if (Houses[i].Level == 1) {Rent += 40000;}
        else if (Houses[i].Level == 2) {Rent += 50000;}
        else if (Houses[i].Level == 3) {Rent += 50000;}
        else if (Houses[i].Level == 4) {Rent += 80000;}
        else if (Houses[i].Level == 5) {Rent += 100000;}
    }
    houseStateData.MoneyRest += Rent;
    ChangeText("Получил " + Rent + " аренды, чтобы заплатить налоги и спать спокойно");
    $('.count').text(houseStateData.MoneyRest); 
    Rent = 0;

    setTimeout(function () {
        GetRent();
    }, 100000)
}

// Определение характеристик конкретного домика
function ImprovementInfo(Level) {
   
    let cost; let tax; let rent;

         if (Level == 0) {cost = 200000; tax = 15000; rent = 30000;}
    else if (Level == 1) {cost = 300000; tax = 20000; rent = 40000;}
    else if (Level == 2) {cost = 400000; tax = 30000; rent = 50000;}
    else if (Level == 3) {cost = 500000; tax = 40000; rent = 80000;}
    else if (Level == 4) {cost = 700000; tax = 70000; rent = 100000;}

    ImproveCost = cost / 100 * houseStateData.LevelUpCost;
    $('.dmtext1').text("Стоимость улучшения - " + ImproveCost);
    $('.dmtext2').text("Стоимость налогов после улучшения - " + tax / 100 * houseStateData.TaxPercent);
    $('.dmtext3').text("Стоимость аренды после улучшения - " + rent / 100 * houseStateData.RentPercent);
}

// Изменение текста в верхнем блоке
function ChangeText(mytext) {

    houseStateData.Text1 = houseStateData.Text2;
    houseStateData.Text2 = houseStateData.Text3;
    houseStateData.Text3 = mytext;
    GameLoad();
}

function GameLoad() {

    $('.mtext1').text(houseStateData.Text1);
    $('.mtext2').text(houseStateData.Text2);
    $('.mtext3').text(houseStateData.Text3);
    $('.count').text(houseStateData.MoneyRest);
}








