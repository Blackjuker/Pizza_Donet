using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pizza_mama.Data;
using pizza_mama.Models;
using System.Threading.Tasks;

namespace pizza_mama.Pages
{
    public class MenuPizzasModel : PageModel
    {
        private readonly DataContext _context;
        public MenuPizzasModel(DataContext context)
        {
            _context = context;
        }

        public IList<Pizza> Pizza { get; set; } = default!;
        public async Task OnGet()
        {
            Pizza = await _context.Pizzas.OrderBy(p => p.prix).ToListAsync();
        }
    }
}
