class ProductView {
    render(product) {
        const container = document.getElementById('product-container');
        container.innerHTML = `
        <div>
          <h2>${product.title}</h2>
          <p>ID: ${product.id}</p>
          <p>Description: ${product.description}</p>
        </div>
      `;
    }
}
