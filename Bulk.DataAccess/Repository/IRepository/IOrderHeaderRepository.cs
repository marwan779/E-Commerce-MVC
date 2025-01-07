using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderheader);
        void UpdateStatus(int id, string OrderStatus, string? PaymentStatus);
        void UpdateStripePaymentID(int id, string SessionId,string PaymentIntentId);

    }
}
