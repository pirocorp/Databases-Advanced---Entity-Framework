namespace TeisterMask.Common
{
    public static class ValidationConstants
    {
        public const string DateTimeFormat = "dd/MM/yyyy";

        public static class Employee
        {
            public const int UsernameMinLength = 3;

            public const int UsernameMaxLength = 40;
        }

        public static class Project
        {
            public const int NameMinLength = 2;

            public const int NameMaxLength = 40;
        }

        public static class Task
        {
            public const int NameMinLength = 2;

            public const int NameMaxLength = 40;
        }
    }
}
