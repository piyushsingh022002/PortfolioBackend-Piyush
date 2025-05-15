namespace Backend.Services{
    public interface IFeedbackService{
         Task SendFeedbackEmailAsync(string name, string email, int rating, string feedback);
    }
}
