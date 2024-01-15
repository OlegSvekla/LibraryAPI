namespace LibraryApi.BL.Validators.IValidators
{
    public interface INameValidator
    {
        void ValidateFirstName(string? firstName);
        void ValidateLastName(string? lastName);
        void ValidateFullName(string? fullname);
    }
}