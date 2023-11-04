namespace AuthControl.Models.Response
{
    public class BaseResponse <E>
    {
        public E Data { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
