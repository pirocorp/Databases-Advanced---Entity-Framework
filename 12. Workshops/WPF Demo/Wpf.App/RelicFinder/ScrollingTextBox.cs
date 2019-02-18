namespace RelicFinder
{
    using System;
    using System.Windows.Controls;

    public class ScrollingTextBox : TextBox
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.Focus();
            this.CaretIndex = this.Text.Length;
            this.ScrollToEnd();
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            this.Focus();
            this.CaretIndex = this.Text.Length;
            this.ScrollToEnd();
        }
    }
}