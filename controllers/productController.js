class ProductController {
    constructor(view, config) {
        this.view = view;
        this.config = config;
    }

    async fetchProductData() {
        try {
            this.view.render(new Product(
                1, "title", "desc"
            ))

            const response = await fetch(this.config.product_api_url);
            const data = await response.json();

            if (data.success) {
                const productData = data.data[0];
                const product = new Product(
                    productData.id,
                    productData.title,
                    productData.description
                );

                this.view.render(product);
            } else {
                console.error('Error fetching product data:', data.message);
            }
        } catch (error) {
            console.error('Error fetching product data:', error);
        }
    }

    async fetchProductById(productId) {
        try {
            if (!this.config) {
                console.error('Configuration not loaded. Aborting data fetch by ID.');
                return;
            }

            const url = `${this.config.product_api_url}/${productId}`;
            const response = await fetch(url);
            const data = await response.json();

            if (data.success) {
                const productData = data.data;
                const product = new Product(
                    productData.id,
                    productData.title,
                    productData.description
                );

                this.view.render(product);
            } else {
                console.error('Error fetching product data by ID:', data.message);
            }
        } catch (error) {
            console.error('Error fetching product data by ID:', error);
        }
    }

    async updateProductData(updatedProductData) {
        try {
            if (!this.config) {
                console.error('Configuration not loaded. Aborting data update.');
                return;
            }

            const response = await fetch(this.config.product_api_url, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedProductData),
            });

            const data = await response.json();

            if (data.success) {
                console.log('Product data updated successfully:', data.message);
                this.fetchProductById(updatedProductData.id);
            } else {
                console.error('Error updating product data:', data.message);
            }
        } catch (error) {
            console.error('Error updating product data:', error);
        }
    }

    async deleteProductById(productId) {
        try {
            if (!this.config) {
                console.error('Configuration not loaded. Aborting product deletion by ID.');
                return;
            }

            const url = `${this.config.product_api_url}/${productId}`;
            const response = await fetch(url, {
                method: 'DELETE',
            });

            const data = await response.json();

            if (data.success) {
                console.log('Product deleted successfully:', data.message);
                this.fetchProductById(updatedProductData.id);
            } else {
                console.error('Error deleting product by ID:', data.message);
            }
        } catch (error) {
            console.error('Error deleting product by ID:', error);
        }
    }

    async createProduct(newProductData) {
        try {
            if (!this.config) {
                console.error('Configuration not loaded. Aborting product creation.');
                return;
            }

            const response = await fetch(this.config.product_api_url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newProductData),
            });

            const data = await response.json();

            if (data.success) {
                console.log('Product created successfully:', data.message);
                this.fetchProductById(newProductData.id);
            } else {
                console.error('Error creating product:', data.message);
            }
        } catch (error) {
            console.error('Error creating product:', error);
        }
    }
}
