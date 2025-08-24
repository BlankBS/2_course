class Task {
    constructor(id, name, state)
    {
        this.id = id;
        this.name = name;
        this.state = state;
    }

    ChangeName(newName)
    {
        this.name = newName;
    }

    ChangeState(newState)
    {
        this.state = newState;
    }
};

let tasks = [];
let taskId = 1;

function AddTask()
{
    let taskNameInput = document.getElementById('taskName');
    let taskName = taskNameInput.value;
    if(taskName)
    {
        let newTask = new Task(taskId++, taskName, 'не выполнено');
        tasks.push(newTask);
        taskNameInput.value = '';
        RenderTasks();
    }
};

function RenderTasks(filter = 'all')
{
    let taskList = document.getElementById('taskList');
    taskList.innerHTML = '';
    let filtredTasks = tasks.filter(task => {
        if(filter === 'completed') return task.state === 'выполнено';
        if(filter === 'not_completed') return task.state === 'не выполнено';
        return true;
    });

    filtredTasks.forEach(task => {
        let li = document.createElement('li');
        li.className = 'taskItem';
        li.innerHTML = `
            <input type = "checkbox" onchange = "toggleTaskState(${task.id}, this.checked)" ${task.state === 'выполнено' ? 'checked' : ''}>
            <span>${task.name}</span>
            <button class = "editButton" onclick = "EditTask(${task.id})">Редактировать</button>
            <button class = "deleteButton" onclick = "DeleteTask(${task.id})">Удалить</button>
        `;
        taskList.appendChild(li);
    })
};

function toggleTaskState(taskId, checked)
{
    let task = tasks.find(t => t.id === taskId);
    if(task)
    {
        task.ChangeState(checked ? 'выполнено' : 'не выполнено');
        RenderTasks();
    }
};

function DeleteTask(taskId)
{
    tasks = tasks.filter(task => task.id !== taskId);
    RenderTasks();
};

function EditTask(taskId)
{
    let task = tasks.find(t => t.id === taskId);
    let newName = prompt("Измените название задачи: ", task.name);
    if(newName)
    {
        task.ChangeName(newName);
        RenderTasks();
    }
};

function FilterTasks(state)
{
    RenderTasks(state);
}