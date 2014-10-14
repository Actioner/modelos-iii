namespace BE.ModelosIII.Tasks
{
    public class CommandResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Command { get; set; }
    }
}
