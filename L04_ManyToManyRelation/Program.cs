namespace L04_ManyToManyRelation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new MyDbContext();
            ClearDatabase(context);
        }

        private static void ClearDatabase(MyDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}