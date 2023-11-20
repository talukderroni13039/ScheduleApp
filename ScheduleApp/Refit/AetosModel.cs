using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.Refit
{
    public class AetosModel
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public long SerialNumber { get; set; }
        public long TransactionId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public string Address { get; set; }
        public DateTime Timestamp { get; set; }
        public int Accuracy { get; set; }
        public int Speed { get; set; }
        public int CANSpeed { get; set; }
        public string Heading { get; set; }
        public int Odometer { get; set; }
        public int EngineHours { get; set; }
        public string Ignition { get; set; }
        public int Reason { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
    }
}
