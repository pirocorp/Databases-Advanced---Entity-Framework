namespace _03._Factory_Method
{
    /// <summary>
    /// A 'ConcreteCreator' class
    /// </summary>
    public class Resume : Document
    {
        // Factory Method implementation
        public override void CreatePages()
        {
            this.Pages.Add(new SkillsPage());
            this.Pages.Add(new EducationPage());
            this.Pages.Add(new ExperiencePage());
        }
    }
}
