using System;
using System.Collections.Generic;
using Domain;
using Repository;
using Repository.Interface;
using Services.Interface;

namespace Services.Implementation;

public class ProductService(IRepository<Product> productRepository) : IProductService
{
    public IEnumerable<Product> GetAllProducts()
    {
        return productRepository.GetAll();
    }

    public Product GetById(Guid id)
    {
        return productRepository.GetById(id);
    }
}
