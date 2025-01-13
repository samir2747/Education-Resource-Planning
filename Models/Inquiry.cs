using System;
using System.Collections.Generic;

namespace InstitudeManagementSystem.Models;

public partial class Inquiry
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Dob { get; set; } = null!;

    public string Qualification { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Course { get; set; } = null!;

    public int Fees { get; set; }

    public string Remark { get; set; } = null!;

    public string InquiryTakenBy { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int? Courseid { get; set; }

    public virtual ICollection<Addmission> Addmissions { get; set; } = new List<Addmission>();

    public virtual Course? CourseNavigation { get; set; }
}
