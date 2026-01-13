using SqlSugar;

namespace BloodReg.Models
{
    public record class InternationalStudent
    {
        public string Name { get; set; } = null!;
        public string ChineseName { get; set; } = null!;
        [SugarColumn(IsPrimaryKey = true)]
        public string StudentId { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string AccountBank { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public string Birthday { get; set; } = null!;
        public string FirstEntryDate { get; set; } = null!;
        public string WeixinID { get; set; } = null!;
        public string DonationVolume { get; set; } = null!;
        public string Clerk { get; set; } = null!;

        public InternationalStudent() { }

        public InternationalStudent(string name, string chineseName, string studentId, string phoneNumber, string accountNumber, string accountBank, string passportNumber, string nationality, string birthday, string firstEntryDate, string weixinID, string donationVolume, string clerk)
        {
            Name = name;
            ChineseName = chineseName;
            StudentId = studentId;
            PhoneNumber = phoneNumber;
            AccountNumber = accountNumber;
            AccountBank = accountBank;
            PassportNumber = passportNumber;
            Nationality = nationality;
            Birthday = birthday;
            FirstEntryDate = firstEntryDate;
            WeixinID = weixinID;
            DonationVolume = donationVolume;
            Clerk = clerk;
        }
    }
}
