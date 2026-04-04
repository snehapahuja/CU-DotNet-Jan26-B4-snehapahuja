namespace VagaBond.Backend.Exceptions
{
    public class DestinationNotFoundException : Exception
    {
        public DestinationNotFoundException(int id) : base($"Destination with ID {id} not found.")
        {

        }
    }
}
