using SqlSugar;

namespace BloodReg.Models
{
    public record class Teacher
    {
        public string Name { get; set; } = null!;
        [SugarColumn(IsPrimaryKey = true)]
        public string EmployeeID { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string AccountBank { get; set; } = null!;
        public string DonationVolume { get; set; } = null!;
        public string Clerk { get; set; } = null!;

        public Teacher() { }
        public Teacher(string name, string employeeID, string phoneNumber, string accountNumber, string accountBank, string donationVolume, string clerk)
        {
            Name = name;
            EmployeeID = employeeID;
            PhoneNumber = phoneNumber;
            AccountNumber = accountNumber;
            AccountBank = accountBank;
            DonationVolume = donationVolume;
            Clerk = clerk;
        }
    }
}