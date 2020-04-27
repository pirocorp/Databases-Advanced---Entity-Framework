namespace _01._Adapter
{
    /// <summary>
    /// AdapterDemo startup class for  
    /// Adapter Design Pattern Demo.
    /// </summary>
    public static class AdapterDemo
    {
        public static void Main() 
        {
            // Non-adapted chemical compound
            var unknown = new Compound("Unknown");
            unknown.Display();

            // Adapted chemical compounds
            Compound water = new RichCompound("Water");
            water.Display();

            Compound benzene = new RichCompound("Benzene");
            benzene.Display();

            Compound ethanol = new RichCompound("Ethanol");
            ethanol.Display();
        }
    }
}
