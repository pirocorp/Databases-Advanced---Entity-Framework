namespace Theatre.Data
{
    using System;

    public static class ValidationConstants
    {
        public static class Theatre
        {
            public const int NameMinLength = 4;

            public const int NameMaxLength = 30;

            public const sbyte NumberOfHallsMinValue = 1;

            public const sbyte NumberOfHallsMaxValue = 10;

            public const int DirectorMinLength = 4;

            public const int DirectorMaxLength = 30;
        }

        public static class Play
        {
            public const int TitleMinLength = 4;

            public const int TitleMaxLength = 50;

            public static TimeSpan DurationMinValue = new TimeSpan(0, 1, 0, 0);

            public const float RatingMinValue = 0;

            public const float RatingMaxValue = 10;

            public const int DescriptionMaxLength = 700;

            public const int ScreenwriterMinLength = 4;

            public const int ScreenwriterMaxLength = 30;
        }

        public static class Cast
        {
            public const int FullNameMinLength = 4;

            public const int FullNameMaxLength = 30;
        }

        public static class Ticket
        {
            public const decimal PriceMinValue = 1;

            public const decimal PriceMaxValue = 100;

            public const sbyte RowNumberMinValue = 1;

            public const sbyte RowNumberMaxValue = 10;
        }
    }
}
