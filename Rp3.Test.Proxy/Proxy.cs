using Newtonsoft.Json;
using Rp3.Test.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;


namespace Rp3.Test.Proxies
{
    public class Proxy : BaseProxy
    {
        private const string UriGetCategory = "api/categoryData/get?active={0}";
        private const string UriGetCategoryById = "api/categoryData/getById?categoryId={0}";
        private const string UriInsertCategory = "api/categoryData/insert";
        private const string UriUpdateCategory = "api/categoryData/update";

        private const string UriGetTransactionType = "api/transactionTypeData/get";

        private const string UriGetBalance = "api/transactionData/Balance";
        private const string UriGetTransactions = "api/transactionData/get?personId={0}";
        private const string UriGetTransactionById = "api/transactionData/getById?transactionId={0}";
        private const string UriInsertTransaction = "api/transactionData/Insert";
        private const string UriUpdateTransaction = "api/transactionData/Update";

        private const string UriGetPersons = "api/personData/get";
        private const string UriGetPerson = "api/personData/getById?personId={0}";

        /// <summary>
        /// Obtiene el Listado de Tipos de Transacción
        /// </summary>
        /// <returns></returns>
        public List<Person> GetPersons()
        {
            return HttpGet<List<Person>>(UriGetPersons);
        }

        public Person GetPerson(int personId)
        {
            return HttpGet<Person>(UriGetPerson, personId);
        }

        public List<TransactionType> GetTransactionTypes()
        {
            return HttpGet<List<TransactionType>>(UriGetTransactionType);
        }

        #region Category Services

        /// <summary>
        /// Obtiene el Listado de Categorías
        /// </summary>
        /// <param name="active">especifica si la consulta es sobre categorías activas o Inactivas</param>
        /// <returns></returns>
        public List<Category> GetCategories(bool? active = null)
        {
            return HttpGet<List<Category>>(UriGetCategory, active);
        }

        /// <summary>
        /// Obtiene una Categoría por Id
        /// </summary>
        /// <param name="categoryId">Id de la Categoría</param>
        /// <returns></returns>
        public Category GetCategory(int categoryId)
        {
            return HttpGet<Category>(UriGetCategoryById, categoryId);
        }

        /// <summary>
        /// Método para Insertar Categorías
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool InsertCategory(Rp3.Test.Common.Models.Category category)
        {
            return HttpPostAsJson<bool>(UriInsertCategory, category);
        }

        public bool UpdateCategory(Rp3.Test.Common.Models.Category category)
        {
            return HttpPostAsJson<bool>(UriUpdateCategory, category);
        }

        #endregion

        /// <summary>
        /// Obtiene el Listado de Transacciones
        /// </summary>
        /// <returns></returns>
        public List<TransactionView> GetTransactions(int personId)
        {
            return HttpGet<List<TransactionView>>(UriGetTransactions, personId);
        }

        public bool InsertTransactions(Rp3.Test.Common.Models.Transaction transaction)
        {
            return HttpPostAsJson<bool>(UriInsertTransaction, transaction);
        }

        public bool UpdateTransactions(Rp3.Test.Common.Models.Transaction transaction)
        {
            return HttpPostAsJson<bool>(UriUpdateTransaction, transaction);
        }

        public List<BalanceView> GetBalance(BalanceView balanceView)
        {
            return HttpPostAsJson<List<BalanceView>>(UriGetBalance, balanceView);
        }

        public TransactionView GetTrasanction(int? transactionId)
        {
            return HttpGet<TransactionView>(UriGetTransactionById, transactionId);
        }


    }
}