namespace LMS.Core.Models
{
    public class LibraryOptions
    {
        public const string SettingName = "LibrarySettings";
        public int MaxBookBorrowed { get; set; }
        public int MaxBookLoanDuration { get; set; }
        public int PenaltyPerDay { get; set; }
    }
}
