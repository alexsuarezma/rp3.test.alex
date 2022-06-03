using Rp3.Test.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rp3.Test.WebApi.Data.Controllers
{
    public class TransactionDataController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Balance(Rp3.Test.Common.Models.BalanceView balanceView)
        {
            List<Rp3.Test.Common.Models.BalanceView> commonModel = new List<Common.Models.BalanceView>();

            using (DataService service = new DataService())
            {
                IEnumerable<Rp3.Test.Data.Models.Balance>
                    dataModel = service.Transactions.GetBalance(balanceView.AccountId, balanceView.dateStart, balanceView.dateEnd);

                commonModel = dataModel.Select(p => new Common.Models.BalanceView()
                {
                    Categoria = p.Categoria,
                    Saldo = p.Saldo,
                }).ToList();
            }

            return Ok(commonModel);
        }

        [HttpGet]
        public IHttpActionResult Get(int personId)
        {            
            List<Rp3.Test.Common.Models.TransactionView> commonModel = new List<Common.Models.TransactionView>();

            using (DataService service = new DataService())
            {
                IEnumerable<Rp3.Test.Data.Models.Transaction> 
                    dataModel = service.Transactions.Get( p => p.PersonId == personId,
                    includeProperties: "Category,TransactionType,Person", 
                    orderBy: p=> p.OrderByDescending(o=>o.RegisterDate) );

                //Para incluir una condición, puede usar el primer parametro de Get
                /*
                 * Ejemplo
                 IEnumerable<Rp3.Test.Data.Models.Transaction>
                    dataModel = service.Transactions.Get(p=> p.TransactionId > 0
                    includeProperties: "Category,TransactionType",
                    orderBy: p => p.OrderByDescending(o => o.RegisterDate));

                 */

                commonModel = dataModel.Select(p => new Common.Models.TransactionView()
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Notes = p.Notes,
                    Amount = p.Amount,
                    RegisterDate = p.RegisterDate,
                    ShortDescription = p.ShortDescription,
                    TransactionId = p.TransactionId,
                    TransactionType = p.TransactionType.Name,
                    TransactionTypeId = p.TransactionTypeId,
                    PersonName = p.Person.Name,
                    PersonId = p.PersonId
                }).ToList();
            }

            return Ok(commonModel);
        }

        [HttpGet]
        public IHttpActionResult GetById(int transactionId)
        {
            Rp3.Test.Common.Models.Transaction commonModel = null;
            using (DataService service = new DataService())
            {
                var model = service.Transactions.GetByID(transactionId);

                commonModel = new Rp3.Test.Common.Models.Transaction()
                {
                    TransactionId = model.TransactionId,
                    TransactionTypeId = model.TransactionTypeId,
                    CategoryId = model.CategoryId,
                    ShortDescription = model.ShortDescription,
                    Amount = model.Amount,
                    Notes = model.Notes,
                    RegisterDate = model.RegisterDate
                };
            }
            return Ok(commonModel);
        }

        public IHttpActionResult Insert(Rp3.Test.Common.Models.Transaction transaction)
        {
            //Complete the code
            using (DataService service = new DataService())
            {
                Rp3.Test.Data.Models.Transaction model = new Test.Data.Models.Transaction();
                model.TransactionId = service.Transactions.GetMaxValue<int>(p => p.TransactionId, 0) + 1;

                model.TransactionTypeId = transaction.TransactionTypeId;
                model.CategoryId = transaction.CategoryId;
                model.RegisterDate = transaction.RegisterDate;
                model.ShortDescription = transaction.ShortDescription;
                model.Notes = transaction.Notes;
                model.Amount = transaction.Amount;
                model.PersonId = transaction.PersonId;

                service.Transactions.Insert(model);
                service.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Update(Rp3.Test.Common.Models.Transaction transaction)
        {
            //Complete the code
            using (DataService service = new DataService())
            {
                Rp3.Test.Data.Models.Transaction model = service.Transactions.GetByID(transaction.TransactionId);

                model.TransactionTypeId = transaction.TransactionTypeId;
                model.CategoryId = transaction.CategoryId;
                model.RegisterDate = transaction.RegisterDate;
                model.ShortDescription = transaction.ShortDescription;
                model.Notes = transaction.Notes;
                model.Amount = transaction.Amount;
                model.PersonId = transaction.PersonId;

                service.Transactions.Update(model);
                service.SaveChanges();
            }

            return Ok();
        }

    }
}
