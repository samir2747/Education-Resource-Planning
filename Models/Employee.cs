using System;
using System.Collections.Generic;

namespace InstitudeManagementSystem.Models;

public partial class Employee
{
    public int Eid { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ContactNo { get; set; } = null!;

    public string AlternetContactNo { get; set; } = null!;

    public string CurrentAddress { get; set; } = null!;

    public string PermanentAddress { get; set; } = null!;

    public string Dob { get; set; } = null!;

    public string Qualification { get; set; } = null!;

    public string MaritalStatus { get; set; } = null!;

    public string? Department { get; set; }

    public string SpecializedSubject { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public string DateOfJoining { get; set; } = null!;

    public int Salary { get; set; }

    public string IdproofType { get; set; } = null!;

    public string IdproofNumber { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string PreviousExperience { get; set; } = null!;

    public string Experience { get; set; } = null!;
}
