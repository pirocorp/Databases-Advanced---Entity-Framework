namespace RelicFinder
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Data;
    using Models;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RelicFinderDbContext context = new RelicFinderDbContext();

        public MainWindow()
        {
            this.InitializeComponent();

            RelicDbInitializer.Seed(this.context);

            this.LoadRelics();
        }

        private void AddElement(object sender, RoutedEventArgs e)
        {
            var relic = new Relic()
            {
                Name = "New Relic"
            };
            this.context.Relics.Add(relic);
            this.LoadRelics(relic);
        }

        private void Save_Changes_Click(object sender, RoutedEventArgs e)
        {
            this.context.SaveChanges();
        }

        private void LoadRelics(Relic newRelic = null)
        {
            var relics = this.context.Relics.ToList();
            relics.Add(newRelic);
            this.DataContext = relics;
        }

        private void CheckValue_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
