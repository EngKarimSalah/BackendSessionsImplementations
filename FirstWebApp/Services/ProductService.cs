using FirstWebApp.DTOs;
using FirstWebApp.Models;
using FirstWebApp.Repositories;
using System.Security.Cryptography.Pkcs;

namespace FirstWebApp.Services
{
    public class ProductService
    {


        //ProductRepo repo = new ProductRepo();

        private ProductRepo repo;

        public ProductService(ProductRepo _repo)
        {
            repo = _repo;
        }


        //Auto mapper


        public List<ProductOutputDTO> GetAllProducts()
        {
            List<Product> products = repo.GetAllProducts();

            List<ProductOutputDTO> outputs = new List<ProductOutputDTO>();
            ProductOutputDTO output = new ProductOutputDTO();

            foreach (Product product in products)
            {
                output.Price = product.Price;
                output.Name = product.Name;
                outputs.Add(output);
            }
            return outputs;
        }

        public ProductAllOutputDTO GetProductById(int id)
        {
            Product p = repo.GetProductById(id);

            ProductAllOutputDTO output = new ProductAllOutputDTO();
            output.Price= p.Price;
            output.Name = p.Name;
            output.Description = p.Description;

            return output;
        }


        public int Create(Product product)
        {

            repo.Add(product);
            return product.Id;
        }


        public int Create(ProductInputDTO product)
        {

            Product p = new Product();
            p.Name = product.Name;
            p.Price=product.Price;
            p.Description = product.Description;
            p.createdDate = DateTime.Now;
            p.Count = 0;

            repo.Add(p);
            return p.Id;
        }

        public bool UpdatePrice(int productId, int newPrice)
        {
            Product product = repo.GetProductById(productId);
            if (product == null)
            {
                return false;
            }

            product.Price = newPrice;
            repo.Update();
            return true;
         
        }


        public bool Delete(int productId)
        {
            Product product = repo.GetProductById(productId);
            if (product == null)
            {
                return false;
            }

            repo.Delete(product);
            return true;
        }
    }
}
