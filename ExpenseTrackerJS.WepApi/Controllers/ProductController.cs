using ExpenseTrackerJS.DataAccess;
using ExpenseTrackerJS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseTrackerJS.WepApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IUnitOfWork uow = null;
        private readonly IRepository<Product> repository = null;

        public ProductController(IUnitOfWork uow, IRepository<Product> repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        public ProductController()
        {
            uow = new UnitOfWork();
            repository = new Repository<Product>(uow);
        }

        // GET: api/Expense
        public IEnumerable<Product> Get()
        {
            IList<Product> list = repository.AllByUser(User.Identity.Name).ToList();
            return list;
        }

        // GET: api/Expense/5
        public Product Get(int id)
        {
            var product = repository.Find(id, User.Identity.Name);
            if (product == null) //|| product.UserId != User.Identity.Name)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Id {0} not found.", id)),
                    ReasonPhrase = "Expense id Not Found"
                };
                throw new HttpResponseException(resp);
            }
            return product;
        }

        // POST: api/Expense
        public IHttpActionResult Post(Product expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //expense.UserId = User.Identity.Name;
                repository.Insert(expense);
                uow.Save();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Expense/5
        public IHttpActionResult Put(int id, Product product)
        {
            if (product == null || !ModelState.IsValid || id != product.ID)
            {
                return BadRequest(ModelState);
            }
            //if (product.UserId != User.Identity.Name)
            //{
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            //}

            if (IsExpenseAvailable(id))
            {
                repository.Update(product);
                uow.Save();
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Id {0} not found.", id)),
                    ReasonPhrase = "Expense id Not Found"
                };
                throw new HttpResponseException(resp);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Expense/5
        public void Delete(int id)
        {
            Product product = repository.Find(id, User.Identity.Name);
            if (product == null) //|| expense.UserId != User.Identity.Name)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Id {0} not found.", id)),
                    ReasonPhrase = "Expense id Not Found"
                };
                throw new HttpResponseException(resp);
            }
            repository.Delete(id);
            uow.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (repository != null)
                repository.Dispose();
            if (uow != null)
                uow.Dispose();
            base.Dispose(disposing);
        }


        private bool IsExpenseAvailable(int id)
        {
            return repository.FindId(id, User.Identity.Name);
        }
    }
}
