using BlazorFastReport.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorFastReport.Data
{
	public class BlazorDbContext:DbContext
	{
		public BlazorDbContext(DbContextOptions<BlazorDbContext> options) : base(options)
		{

		}
		public DbSet<TablaProducto> TablaProductos { get; set; }
	}
}
