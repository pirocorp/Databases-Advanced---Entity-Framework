namespace _03._Factory_Method
{
    /// <summary>
    /// A 'ConcreteCreator' class
    /// </summary>
    public class Report : Document
    {
        // Factory Method implementation
        public override void CreatePages()
        {
            this.Pages.Add(new IntroductionPage());
            this.Pages.Add(new ResultsPage());
            this.Pages.Add(new ConclusionPage());
            this.Pages.Add(new SummaryPage());
            this.Pages.Add(new BibliographyPage());
        }
    }
}
