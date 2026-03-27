namespace SmartBank.Helpers
{
    public class AccountNumberGenerator
    {
        public static string GenerateAccountNumber(int id)
        {
            string prefix = "SB";
            string year = DateTime.Now.Year.ToString();   
            string newId = id.ToString().PadLeft(6, '0');   
            return $"{prefix}-{year}-{newId}";
        }
    }
}
