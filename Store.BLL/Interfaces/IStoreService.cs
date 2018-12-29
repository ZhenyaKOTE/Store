using Store.BLL.DTO;
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
        //if null comes, then return all categories
        //else return selected categories by ID
        IEnumerable<PreViewCategoryDTO> Get(int[] CategoriesId = null);
        ProductDTO Get(int Id);
        IEnumerable<ProductDTO> Filter(int[] FiltersId);

    }
}
