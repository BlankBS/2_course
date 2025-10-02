function Book(title_, author_){
    this.title = title_;
    this.author = author_;

    this.GetTitle = () => "Название: " + this.title;
    this.GetAuthor = () => "Автор: " + this.author;
}

let book1 = new Book("После", "Анна Тодд");
let book2 = new Book("451 градус по Фаренгейту", "Рэй Брэдбери");

console.log(book1.GetTitle());
console.log(book1.GetAuthor());

console.log(book2.GetTitle());
console.log(book2.GetAuthor());