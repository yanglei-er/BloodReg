using SqlSugar;

namespace BloodReg.Models
{
    public record class Student
    {
        public string Name { get; set; } = null!;
        [SugarColumn(IsPrimaryKey = true)]
        public string StudentId { get; set; } = null!;
        public string DonationVolume { get; set; } = null!;
        public string Clerk { get; set; } = null!;

        public Student() { }

        public Student(string name, string studentId, string donationVolume, string clerk)
        {
            Name = name;
            StudentId = studentId;
            DonationVolume = donationVolume;
            Clerk = clerk;
        }
    }
}