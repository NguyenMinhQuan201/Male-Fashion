namespace API.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class Mylist<T>
    {
        public List<T> List { get; set; }
    }
    public class TestTClass
    {
        public int Id { get; set; }
    }
}