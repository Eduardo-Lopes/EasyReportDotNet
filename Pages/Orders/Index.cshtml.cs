using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NorthWind.Model;

namespace NorthWind.Pages_Orders
{
    public class IndexModel : PageModel
    {
        private readonly NorthWind.Model.NorthWindContext _context;

        public IndexModel(NorthWind.Model.NorthWindContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation).ToListAsync();
        }
    }
}
