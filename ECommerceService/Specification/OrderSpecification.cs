using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Specification
{
    public class OrderSpecification : BaseSpecification<ECommerceDomain.Entities.OrderModule.Order, Guid>
    {
        public OrderSpecification(string userEmail)
            : base(o => o.UserEmail == userEmail)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderSpecification(string userEmail,Guid id)
           : base(o => o.Id == id
           && (string.IsNullOrEmpty(userEmail)|| o.UserEmail.ToLower()== userEmail.ToLower())
           )
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);
            //AddOrderByDescending(o => o.OrderDate);
        }
    }
}
