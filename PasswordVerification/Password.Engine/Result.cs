namespace Password.Engine
{
    public class Result
    {
        public int Id { get; private set; }
        public bool IsPass { get; private set; }
        public string Message { get; private set; }
        public Result(int id, bool pass, string message)
        {
            Id = id;
            IsPass = pass;
            Message = message;
        }
    }
}
