namespace SoftJail.Common
{
    public static class ValidationConstants
    {
        public static class Prisoner
        {
            public const int FullNameMinLength = 3;

            public const int FullNameMaxLength = 20;

            public const int AgeMin = 18;

            public const int AgeMax = 65;
        }

        public static class Officer
        {
            public const int FullNameMinLength = 3;

            public const int FullNameMaxLength = 30;
        }

        public static class Department
        {
            public const int NameMinLength = 3;

            public const int NameMaxLength = 25;
        }
    }
}
