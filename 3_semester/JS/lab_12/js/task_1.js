class Sudoku {
    constructor() 
    {
        this.board = this.CreateEmptyBoard();
    }

    CreateEmptyBoard() 
    {
        return Array(9).fill().map(() => Array(9).fill(0));
    }

    CheckRows() 
    {
        for(let i = 0; i < 9 ; i++)
        {
            let seen = new Set();
            for(let j = 0; j < 9; j++)
            {
                let num = this.board[i][j];
                if(num !== 0 && seen.has(num))
                {
                    console.log(`Неправильное заполнение в строке ${i + 1}`);
                    break;
                }
                seen.add(num);
            }
        }
    }

    CheckColumns() 
    {
        for(let j = 0; j < 9; j++)
        {
            let seen = new Set();
            for(let i = 0; i < 9; i++)
            {
                let num = this.board[i][j];
                if(num !== 0 && seen.has(num))
                {
                    console.log(`Неправильное запонление в столбце ${j+1}`);
                    break;
                }
                seen.add(num);
            }
        }
    }

    CheckBoxes()
    {
        for(let boxRow = 0; boxRow < 3; boxRow++)
        {
            for(let boxCol = 0; boxCol < 3; boxCol++)
            {
                let seen = new Set();
                for (let i = 0; i < 3; i++)
                {
                    for(let j = 0; j < 3; j++)
                    {
                        let num = this.board[boxRow * 3 + i][boxCol * 3 + j];
                        if(num !== 0 && seen.has(num))
                        {
                            console.log(`Неправильное заполнение в квадрате ${boxRow * 3 + boxCol + 1}`);
                            break;
                        }
                        seen.add(num);
                    }
                }
            }
        }
    }

    ResetBoard()
    {
        this.board = this.CreateEmptyBoard();
    }

    CheckBoard()
    {
        this.CheckRows();
        this.CheckColumns();
        this.CheckBoxes();
    }

    GenerateSolvedBoard() 
    {
        this.board = [
            [5, 3, 4, 6, 7, 8, 9, 1, 2],
            [6, 7, 2, 1, 9, 5, 3, 4, 8],
            [1, 9, 8, 3, 4, 2, 5, 6, 7],
            [8, 5, 9, 7, 6, 1, 4, 2, 3],
            [4, 2, 6, 8, 5, 3, 7, 9, 1],
            [7, 1, 3, 9, 2, 4, 8, 5, 6],
            [9, 6, 1, 5, 3, 7, 2, 8, 4],
            [2, 8, 7, 4, 1, 9, 6, 3, 5],
            [3, 4, 5, 2, 8, 6, 1, 7, 9]
        ];
    }
};

let sudoku = new Sudoku();
sudoku.GenerateSolvedBoard();
sudoku.CheckBoard();