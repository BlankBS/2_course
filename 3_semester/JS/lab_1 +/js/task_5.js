let correct = ["марина", "марина федоровна", "кудлацкая марина федоровна"];
let isCorrect = false;

do{
    let userAnswer = prompt("Введите имя преподавателя: ", "");
    if (correct.includes(userAnswer.toLowerCase())){
        alert("Данные введены верно");
        isCorrect = true;
    }
    else alert("Данные введены не верно");
}while(!isCorrect)