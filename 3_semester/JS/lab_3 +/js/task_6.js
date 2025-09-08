function piramid(levels)
{
    for(let i = 1; i<= levels; i++)
    {
        let row = '';
        for(let j = 1; j <= levels - i; j++)
        {
            row+= ' ';
        }
        for(let k = 1; k <= 2 * i - 1; k++)
        {
            row += '*';
        }
        console.log(row);
    }
}

piramid(15);