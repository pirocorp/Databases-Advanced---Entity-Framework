namespace _10._Template_Method
{
    public static class TemplateMethodDemo
    {
        public static void Main(string[] args)
        {
            DataAccessObject daoCategories = new Categories();
            daoCategories.Run();

            DataAccessObject daoProducts = new Products();
            daoProducts.Run();
        }
    }
}
