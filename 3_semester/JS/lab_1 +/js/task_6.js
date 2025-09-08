let russian = +prompt("Введите вашу отметку по русскому: ");
let mathematics = prompt("Введите вашу отметку по математике: ");
let english = prompt("Введите вашу отметку по английскому: ");

let amount = 0;

if(russian > 3) amount++;
if(mathematics > 3) amount++;
if(english > 3) amount++;

switch(amount){
    case(0): alert("Поздравляю! Вы отчислены :)"); break; 
    case(1):
    case(2): alert("Вас ожидает пересдача :("); break;
    case(3): alert("Поздравляю с прохождением уровня!"); break;
}