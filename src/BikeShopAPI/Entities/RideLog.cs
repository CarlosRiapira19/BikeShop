namespace BikeShopAPI.Entities
{
    public record RideLog(DateTime Date, string Start, string End, string Route)
    {
        public static RideLog Parse(string signature)
        {
            if (string.IsNullOrWhiteSpace(signature))
            {
                throw new ArgumentException("Signature cannot be null or empty", nameof(signature));
            }

            var parts = signature.Split('-');
            
            if (parts.Length != 4)
            {
                throw new FormatException("Invalid signature format. Expected format: DDMMYYYY-START-END-ROUTE");
            }

            // Parse date from DDMMYYYY format
            var dateString = parts[0];
            if (dateString.Length != 8)
            {
                throw new FormatException("Date must be in DDMMYYYY format");
            }

            var day = int.Parse(dateString.Substring(0, 2));
            var month = int.Parse(dateString.Substring(2, 2));
            var year = int.Parse(dateString.Substring(4, 4));
            
            var date = new DateTime(year, month, day);
            var start = parts[1];
            var end = parts[2];
            var route = parts[3];

            return new RideLog(date, start, end, route);
        }

        public override string ToString()
        {
            return $"{Date:ddMMyyyy}-{Start}-{End}-{Route}";
        }
    }
}