using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;

public class ApplicationDbContextSeed
{
    public static async Task SeedProductUnitAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            // kiểm tra trong bảng ProductUnit có bất kì bản ghi nào không nếu chưa có thì thêm vào database 
            //if (!context.ProductUnits.Any())
            //{
            //    var productUnits = new List<ProductUnit>() {
            //                    new ProductUnit { Name = "Cái", IsBaseUnit=true },
            //                    new ProductUnit { Name = "Hộp", IsBaseUnit=true },
            //                    new ProductUnit { Name = "Gói", IsBaseUnit=true },
            //                    new ProductUnit { Name = "Chai",IsBaseUnit=true },
            //                    new ProductUnit { Name = "Lon", IsBaseUnit=true },
            //                    new ProductUnit { Name = "Hũ", IsBaseUnit=true },
            //                    new ProductUnit { Name = "Thùng" },
            //                    new ProductUnit { Name = "Lốc" },
            //};

            //    await context.ProductUnits.AddRangeAsync(productUnits);
            //    await context.SaveChangesAsync();
            //}
        }
        catch (Exception ex)
        {
        }
    }
}
