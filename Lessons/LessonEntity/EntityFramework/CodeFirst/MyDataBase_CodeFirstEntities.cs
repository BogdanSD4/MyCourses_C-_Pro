using System;
using System.Data.Entity;
using System.Linq;

namespace LessonEntity.EntityFramework.CodeFirst
{
    public class MyDataBase_CodeFirstEntities : DbContext
    {
        public MyDataBase_CodeFirstEntities()
            : base("name=MyDataBase_CodeFirstEntities")
        {
        }

        public virtual DbSet<Drinks> DrinkSet { get; set; }
    }

    public class Drinks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}