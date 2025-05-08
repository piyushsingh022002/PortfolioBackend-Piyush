namespace backend.Models{
    public class QueryForm{
        public int Id{get;set;}
        public string? Name{get;set;}
        public string? Designation{get;set;}
        public string? Email{get;set;}
        public string? Phone{get;set;}
        public string? Query{get;set;}
        public DateTime SubmittedAt{get;set;}
    }

}