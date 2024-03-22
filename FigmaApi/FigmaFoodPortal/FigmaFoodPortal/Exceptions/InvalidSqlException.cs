namespace FoodPortal.Exceptions
{
    public class InvalidSqlException : Exception
    {
        string message;
        public InvalidSqlException()
        {
            message = "SQL Exception";
        }
        public InvalidSqlException(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
