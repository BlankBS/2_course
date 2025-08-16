let a = parseFloat(prompt("Введите длину: "));
let b = parseFloat(prompt("Введите ширину: "));

//Function Declaration:

function params(a, b){
    if(a==b){
        return a*4;
    }
    return a*b;
}

//Function Expression

// let params = function(a, b){
//     if(a==b){
//         return a*4;
//     }
//     return a*b;
// }

//Функция стрелка

// let params = (a, b) => {
//     if(a == b){
//         return a*4;
//     }
//     return a*b;
// }

alert(params(a,b));