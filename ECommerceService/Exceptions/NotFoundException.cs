using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Exceptions
{
    public abstract class NotFoundException(string message) : Exception(message)
    {
      
    }

    public class ProductNotFoundException(int id) : NotFoundException( $"Product with id {id} was not found.")
    {

    }
 

    public class BasketNotFoundException(string id) : NotFiniteNumberException($"Basket with id {id} was not found.") 
    {
       

    }
}
