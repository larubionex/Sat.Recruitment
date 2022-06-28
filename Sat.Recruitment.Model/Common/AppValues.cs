namespace Sat.Recruitment.Model.Common
{
    public class AppValues
    {
        public UsersPercentage UsersPercentage { get; set; }
    }

    public class UsersPercentage
    {
        public decimal Normal { get; set; }
        public decimal NormalLowerLimit { get; set; }
        public decimal SuperUser { get; set; }
        public decimal Premium { get; set; }
    }
}
