namespace AuthControl.Entities
{
    public class OperationaDay
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal StartBalance { get; set; }
        public decimal EndBalance { get; set; }
        public int QuantOperations { get; set; }
        //<robot, accuracy>
        public Dictionary<string, double> RobotsAccuracy { get; set; }

        //<robot, Incomings>

        public Dictionary<string, string> GainIncoming { get; set; }

        public UserBase User { get; set; }

    }
}
