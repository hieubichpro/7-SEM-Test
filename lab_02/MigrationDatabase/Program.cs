

using lab_01.DA.dbContext;
using lab_03.BL.Models;
using Microsoft.EntityFrameworkCore;

var builder = new DbContextOptionsBuilder<AppDbContext>();
builder.UseNpgsql("Server=localhost;Username=postgres;Password=123456789;Database=TestTestTest");

var context = new AppDbContext(builder.Options);
//await context.Database.EnsureDeletedAsync();
//await context.Database.EnsureCreatedAsync();
//await context.Database.MigrateAsync();

context.Database.EnsureDeleted();
context.Database.EnsureCreated();
context.Database.Migrate();

//context.SaveChanges();
//context.users.Add(new User("hieu", "bich", "Admin", "hieubichpro", 100));