﻿using ShoppingCart.Application.Dto;

namespace ShoppingCart.Application.Products
{
    public interface IProducts
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
    }
}
