namespace E_Commerce.Web.ErrorModel
{
    public class ErrorToReturn
    {
        public int SatusCode { get; set; }
        public string ErrorMessage { get; set; } = default!;
    }
}