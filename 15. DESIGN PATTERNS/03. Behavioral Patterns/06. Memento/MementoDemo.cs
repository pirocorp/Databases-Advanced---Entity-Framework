namespace _06._Memento
{
    public static class MementoDemo
    {
        public static void Main()
        {
            var salesProspect = new SalesProspect();
            salesProspect.Name = "Noel van Halen";
            salesProspect.Phone = "(412) 256-0990";
            salesProspect.Budget = 25000.0;

            // Store internal state
            var memory = new ProspectMemory();
            memory.Memento = salesProspect.SaveMemento();

            // Continue changing originator
            salesProspect.Name = "Leo Welch";
            salesProspect.Phone = "(310) 209-7111";
            salesProspect.Budget = 1000000.0;

            // Restore saved state
            salesProspect.RestoreMemento(memory.Memento);
        }
    }
}
