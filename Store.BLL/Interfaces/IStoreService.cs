﻿using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Interfaces
{
    public interface IStoreService: IDisposable
    {
        Task<bool> Exists(CategoryDTO item);
        Task<bool> Exists(ProductDTO item);
        Task CreateAsync(CategoryDTO item);
        Task<OperationDetails> AddProductToCategory(ProductDTO item, string CategoryName);
        Task<IList<CategoryDTO>> GetCategories();
        Task<PageInfoDTO> GetProductsByCategory(string CategoryName, int Page);
        Task<ProductDTO> GetProductById(int Id);
    }
}
