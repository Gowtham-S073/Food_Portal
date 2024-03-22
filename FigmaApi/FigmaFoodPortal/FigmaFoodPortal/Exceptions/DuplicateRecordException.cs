namespace FoodPortal.Exceptions
{
    public class DuplicateRecordException : Exception
    {
        string message;
        public DuplicateRecordException()
        {
            message = "This Record is already present";
        }
        public DuplicateRecordException(string message)
        {
            this.message = message;
        }

        public override string Message
        {
            get { return message; }
        }
    }
}
