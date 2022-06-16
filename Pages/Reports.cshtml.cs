using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NorthWind.Model;
using System.ComponentModel;

namespace NorthWind.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly NorthWind.Model.NorthWindContext _context;

        public IndexModel(NorthWind.Model.NorthWindContext context)
        {
            _context = context;
        }

        public string ReportName;

        public IList<object> Result { get;set; }

        public void OnGetOrders() {
            ReportName = "Pedidos";
            Result = (from p in _context.Orders
                        select (object) new {
                            Código = p.OrderId,
                            Cliente = p.Customer.CompanyName
                        }
                    ).ToList();
        }

        public void OnGetProducts() {
            ReportName = "Produtos Descontinuados";
            Result = (from p in _context.Products
                        where p.Discontinued
                        select (object) new {
                            Produto = p.ProductName,
                            Categoria = p.Category.CategoryName,
                            Preço = p.UnitPrice,
                        }
            ).ToList();
        }

        public void OnGetCustomers() {
            ReportName = "Clientes";
            Result = _context.RawSqlQuery("select CompanyName, Address from Customers", 
                it => (object)new {
                    Nome = it[0],
                    Endereço = it[1]}).ToList();
        }
    }
}