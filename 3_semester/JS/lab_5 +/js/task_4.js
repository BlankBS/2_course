let x = 3;
var y = 4;

alert(window.x);
alert(window.y);

window.y = 7;
alert(y);

function foo()
{
    return "BlankBS";
}
alert(window.foo());