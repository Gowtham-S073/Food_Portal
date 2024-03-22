namespace FoodPortal.Exceptions
{
    public class NullException : Exception
    {
        string message;
        public NullException()
        {
            message = "Null Exception";
        }
        public NullException(string message)
        {
            this.message = message;
        }

        public override string Message
        {
            get { return message; }
        }

    }
}
