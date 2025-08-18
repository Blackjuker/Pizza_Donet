using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pizza_mama.Data;
using pizza_mama.Models;

namespace pizza_mama.Pages.Admin.Pizzas
{
    public class DetailsModel : PageModel
    {
        private readonly pizza_mama.Data.DataContext _context;

        public DetailsModel(pizza_mama.Data.DataContext context)
        {
            _context = context;
        }

        public Pizza Pizza { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas.FirstOrDefaultAsync(m => m.PizzaID == id);
            if (pizza == null)
            {
                return NotFound();
            }
            else
            {
                Pizza = pizza;
            }
            return Page();
        }
    }
}
