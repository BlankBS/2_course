class Task {
    constructor (id, name, state) {
        this.id = id;
        this.name = name;
        this.state = state;
    }

    changeName (newName) {
        this.name = newName;
    }

    changeState (newState) {
        this.state = newState;
    }
};

class Todolist {
    constructor(id, name, todolist) {
        this.id = id;
        this.name = name;
        this.todolist = todolist;
    }

    changeName(newName) {
        this.name = newName;
    }

    addTask(newTask){
        this.todolist.push(newTask);
    }

    filterTasks(state) {
        let filterTasks = this.todolist.filter(task => task.state === state);
        return filterTasks;
    }
};

let task1 = new Task(1, "Сделать лабку", "выполнена");
let task2 = new Task(2, "Сходить в магазин", "не выполнена");
let task3 = new Task(3, "Заплатить налоги", "выполнена");
let task4 = new Task(4, "Обновить плейлист", "не выполнена");
let task5 = new Task(5, "Провести ген уборку", "выполнена");
let task6 = new Task(6, "Отдохнуть", "не выполнена");
let task7 = new Task(7, "Сходить в зал", "выполнена");

let list1 = new Todolist(1, "list1", [task1, task2]);
let list2 = new Todolist(2, "list2", [task3, task4, task5]);

list2.changeName("list3");
task4.changeName("task4");

list1.addTask(task6);
list1.addTask(task7);

console.log(list1);

let filtredTasksByDone = list1.filterTasks("выполнена");
console.log(filtredTasksByDone);