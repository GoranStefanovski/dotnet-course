namespace DotnetAPI.Dtos
{
    public partial class UserSalaryUpdateDto
    {
        public int UserId { get; set; }
        public decimal? Salary {get; set;}
        public decimal? AvgSalary {get; set;}
    }
}