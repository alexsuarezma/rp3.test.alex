using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rp3.Test.Mvc.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {

        public async Task<ActionResult> Index(int? transactionId)
        {
            Rp3.Test.Mvc.Models.TransactionViewModel transaction = new Rp3.Test.Mvc.Models.TransactionViewModel();
            Proxies.Proxy proxy = new Proxies.Proxy();
            Rp3.Test.Mvc.Models.PersonViewModel person = (Models.PersonViewModel)Session["Person"];

            var data = proxy.GetTransactions(person.PersonId);
            List<Rp3.Test.Mvc.Models.TransactionViewModel> model = new List<Models.TransactionViewModel>();


            foreach(var item in data)
            {
                model.Add(new Models.TransactionViewModel()
                {
                    Amount = item.Amount,
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    Notes = item.Notes,
                    RegisterDate = item.RegisterDate,
                    ShortDescription = item.ShortDescription,
                    TransactionId = item.TransactionId,
                    TransactionType = item.TransactionType,
                    TransactionTypeId = item.TransactionTypeId,
                    PersonName = item.PersonName
                });
            }



            //List<Rp3.Test.Mvc.Models.CategoryViewModel> modelCategoria = new List<Models.CategoryViewModel>();

            //foreach (var item in categorias)
            //{
            //    modelCategoria.Add(new Models.CategoryViewModel()
            //    {
            //        CategoryId = item.CategoryId,
            //        Name = item.Name,
            //    });
            //}

            var categorias = proxy.GetCategories();
            var transactionTypes = proxy.GetTransactionTypes();


            ViewBag.Categorias = categorias;
            ViewBag.TransactionTypes = transactionTypes;
            ViewBag.Transactions = model;

            if (transactionId == null)
            {
                return View(transaction);
            }
            else
            {
                var transactionModel = proxy.GetTrasanction(transactionId);

                transaction = new Models.TransactionViewModel()
                {
                    TransactionId = transactionModel.TransactionId,
                    TransactionTypeId = transactionModel.TransactionTypeId,
                    CategoryId = transactionModel.CategoryId,
                    ShortDescription = transactionModel.ShortDescription,
                    Amount = transactionModel.Amount,
                    Notes = transactionModel.Notes,
                    RegisterDate = transactionModel.RegisterDate
                };

                return View(transaction);
            }

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Rp3.Test.Mvc.Models.TransactionViewModel vm)
        {

            Rp3.Test.Mvc.Models.PersonViewModel person = (Models.PersonViewModel)Session["Person"];
            Common.Models.Transaction transaction = new Common.Models.Transaction();

            transaction.TransactionTypeId = vm.TransactionTypeId;
            transaction.CategoryId = vm.CategoryId;
            transaction.RegisterDate = Convert.ToDateTime(vm.RegisterDate);
            transaction.ShortDescription = vm.ShortDescription;
            transaction.Notes = vm.Notes;
            transaction.Amount = vm.Amount;
            transaction.PersonId = person.PersonId;
            
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));


            Proxies.Proxy proxy = new Proxies.Proxy();
                
            if (vm.TransactionId == 0)
            {
                proxy.InsertTransactions(transaction);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                transaction.TransactionId = vm.TransactionId;
                proxy.UpdateTransactions(transaction);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        public ActionResult Balance()
        {
            Rp3.Test.Proxies.Proxy proxy = new Proxies.Proxy();
            var transactionTypes = proxy.GetTransactionTypes();

            Rp3.Test.Common.Models.BalanceView balanceView = new Rp3.Test.Common.Models.BalanceView();

            balanceView.dateStart = Convert.ToDateTime("01/01/2018");
            balanceView.dateEnd = DateTime.Now.Date;
            balanceView.AccountId = 1;

            List<Rp3.Test.Common.Models.BalanceView> balances = proxy.GetBalance(balanceView).
                Select(p => new Rp3.Test.Common.Models.BalanceView()
                {
                    Categoria = p.Categoria,
                    Saldo = p.Saldo
                }).ToList();

            ViewBag.Balances = balances;
            ViewBag.TransactionTypes = transactionTypes;

            return View(balanceView);
        }

        [HttpPost]
        public ActionResult Balance(Rp3.Test.Common.Models.BalanceView balanceView)
        {
            Rp3.Test.Proxies.Proxy proxy = new Proxies.Proxy();
            var transactionTypes = proxy.GetTransactionTypes();

            List<Rp3.Test.Common.Models.BalanceView> balances = proxy.GetBalance(balanceView).
                Select(p => new Rp3.Test.Common.Models.BalanceView()
                {
                    Categoria = p.Categoria,
                    Saldo = p.Saldo
                }).ToList();

            ViewBag.Balances = balances;
            ViewBag.TransactionTypes = transactionTypes;

            return View(balanceView);
        }

    }
}
