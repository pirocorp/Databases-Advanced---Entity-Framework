namespace _06._Memento
{
    /// <summary>
    /// The 'Caretaker' class
    /// </summary>
    public class ProspectMemory
    {
        private Memento _memento;

        public ProspectMemory()
        {
        }

        public Memento Memento
        {
            set => this._memento = value;
            get => this._memento;
        }
    }
}
