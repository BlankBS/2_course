interface IPost {
    id: number;
    message: string;
    likesCount: number;
}

interface IDialog {
    id: number;
    name: string;
}

interface IMessage {
    id: number;
    message: string;
}

interface IProfilePage {
    posts: IPost[];
    newPostText: string;
}

interface IDialogsPage {
    dialogs: IDialog[];
    messages: IMessage[];
}

interface IState {
    profilePage: IProfilePage;
    dialogsPage: IDialogsPage;
}

interface IStore {
    state: IState;
}


let store: IStore = {
    state: {
        profilePage: {
            posts: [
                {id: 1, message: 'Hi', likesCount: 12},
                {id: 2, message: 'By', likesCount: 1}
            ],
            newPostText: 'About me'
        },
        dialogsPage: {
            dialogs: [
                {id: 1, name: 'Valera'},
                {id: 2, name: 'Andrey'},
                {id: 3, name: 'Sasha'},
                {id: 4, name: 'Viktor'}
            ],
            messages: [
                {id: 1, message: 'hi'},
                {id: 2, message: 'hi hi'},
                {id: 3, message: 'hi hi hi'}
            ]
        },
    }
};

let store_: IStore = {
    ...store,
    state: {
        ...store.state,
        profilePage: {
            ...store.state.profilePage,
            posts: store.state.profilePage.posts.map((elem: IPost): IPost => ({ ...elem }))
        },
        dialogsPage: {
            ...store.state.dialogsPage,
            dialogs: store.state.dialogsPage.dialogs.map((elem: IDialog): IDialog => ({ ...elem })),
            
            messages: store.state.dialogsPage.dialogs.map((elem: IDialog): IMessage => (
                { id: elem.id, message: "Hello" }
            ))
        },
    }
};

store_.state.profilePage.posts.forEach((elem: IPost) => elem.message = "Hello");
store_.state.dialogsPage.messages.forEach((elem: IMessage) => elem.message = "Hello");

console.log(store);
console.log(store_);