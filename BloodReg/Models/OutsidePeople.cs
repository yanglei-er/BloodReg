using SqlSugar;

namespace BloodReg.Models
{
    public record class OutsidePeople
    {
        public string Name { get; set; } = null!;
        public string EmployeeID { get; set; } = null!;
        [SugarColumn(IsPrimaryKey = true)]
        public string IDNumber { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string AccountBank { get; set; } = null!;
        public string DonationVolume { get; set; } = null!;
        public string Clerk { get; set; } = null!;

        public OutsidePeople() { }

        public OutsidePeople(string name, string employeeID, string iDNumber, string phoneNumber, string accountNumber, string accountBank, string donationVolume, string clerk)
        {
            Name = name;
            EmployeeID = employeeID;
            IDNumber = iDNumber;
            PhoneNumber = phoneNumber;
            AccountNumber = accountNumber;
            AccountBank = accountBank;
            DonationVolume = donationVolume;
            Clerk = clerk;
        }
    }
}
