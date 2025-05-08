namespace Backend.Services{
    public interface IEmailService{
         Task SendContactEmailAsync(string name, string email,string message);
    }
}