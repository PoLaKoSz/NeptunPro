using System;

namespace NeptunPro.Models
{
    public class NeptunBuildDetails
    {
        public int Version { get; }
        public DateTime Date { get; }



        public NeptunBuildDetails(int version, DateTime date)
        {
            Version = version;
            Date = date;
        }



        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(NeptunBuildDetails))
                return false;

            var anotherBuildDetails = (NeptunBuildDetails)obj;

            if (Version != anotherBuildDetails.Version)
                return false;

            if (Date != anotherBuildDetails.Date)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = -682107832;
            hashCode = hashCode * -1521134295 + Version.GetHashCode();
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            return hashCode;
        }
    }
}
