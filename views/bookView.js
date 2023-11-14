class BookView {
  render(book) {
    const container = document.getElementById('book-container');
    container.innerHTML = `
      <div>
        <h2>${book.title}</h2>
        <p>ID: ${book.id}</p>
        <p>Description: ${book.description}</p>
        <p>Author: ${book.author}</p>
      </div>
    `;
  }
}
