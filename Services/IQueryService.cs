namespace Backend.Services{
    public interface IQueryService{
         Task SendQueryEmailAsync(string name, string email, string designation, string PhoneNumber, string query);
    }
}