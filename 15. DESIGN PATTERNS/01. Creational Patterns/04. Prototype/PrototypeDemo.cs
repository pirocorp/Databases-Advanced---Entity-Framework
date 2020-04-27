namespace _04._Prototype
{
    /// <summary>
    /// PrototypeDemo startup class for
    /// Prototype Design Pattern Demo.
    /// </summary>
    public static class PrototypeDemo
    {
        public static void Main()
        {
            var colorManager = new ColorManager();

            // Initialize with standard colors
            colorManager["red"] = new Color(255, 0, 0);
            colorManager["green"] = new Color(0, 255, 0);
            colorManager["blue"] = new Color(0, 0, 255);

            // User adds personalized colors
            colorManager["angry"] = new Color(255, 54, 0);
            colorManager["peace"] = new Color(128, 211, 128);
            colorManager["flame"] = new Color(211, 34, 20);

            // User clones selected colors

            var color1 = colorManager["red"].Clone() as Color;
            var color2 = colorManager["peace"].Clone() as Color;
            var color3 = colorManager["flame"].Clone() as Color;
        }
    }
}
