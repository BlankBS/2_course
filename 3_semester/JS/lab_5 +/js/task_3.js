function* MoveObject()
{
    let x = 0, y = 0;
    console.log(`Начальные координаты: x(${x}) - y(${y})`);

    while(true)
    {
        let direction = yield(x,y);

        switch(direction)
        {
            case("left"): x-=10; break;
            case("right"): x+=10; break;
            case("up"): y+=10; break;
            case("down"): y-=10; break;
            default: console.log(`Неизвестная команда: ${direction}`); 
        }

        console.log(`Новые координаты объекта: x(${x}) - y (${y})`);        
    }
}

const objectMover = MoveObject();
objectMover.next();

for(let i = 0; i < 10; i++)
{
    let answer = prompt("Введите команду (left/right/up/down): ");
    objectMover.next(answer);
}