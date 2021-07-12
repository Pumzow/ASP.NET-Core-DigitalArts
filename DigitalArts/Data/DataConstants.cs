namespace DigitalArts.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;

        public const string EmailAddresslRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int FirstNameMinLength = 3;
        public const int FirstNameMaxLength = 36;

        public const int LastNameMinLength = 3;
        public const int LastNameMaxLength = 36;

        public const int UsernameMinLength = 4;
        public const int UsernameMaxLength = 36;

        public const int PasswordMinLegth = 6;
        public const int PasswordMaxLegth = 36;

        public const int ArtDescriptionLegth = 155;
    }
}
