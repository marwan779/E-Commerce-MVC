using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderHeaderRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }

		public void Update(OrderHeader orderheader)
        {
            _context.Update(orderheader);
        }

		public void UpdateStatus(int id, string OrderStatus, string? PaymentStatus)
		{
			OrderHeader? OrderForDB = _context.OrderHeaders.FirstOrDefault(o=>o.Id == id);
			if(OrderForDB != null)
			{
				OrderForDB.OrderStatus = OrderStatus;
				if(!String.IsNullOrEmpty(PaymentStatus))
				{
					OrderForDB.PaymentStatus = PaymentStatus;
				}
			}
		}

		public void UpdateStripePaymentID(int id, string SessionId, string PaymentIntentId)
		{
			OrderHeader? OrderForDB = _context.OrderHeaders.FirstOrDefault(o => o.Id == id);
			if(!String.IsNullOrEmpty (SessionId))
			{
				OrderForDB.SessionId = SessionId;
			}

			if (!String.IsNullOrEmpty(PaymentIntentId))
			{
				OrderForDB.PaymentIntentId = PaymentIntentId;
				OrderForDB.OrderDate = DateTime.Now;	
			}
		}
	}
}
