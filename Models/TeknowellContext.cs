using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InstitudeManagementSystem.Models;

public partial class TeknowellContext : DbContext
{
    public TeknowellContext()
    {
    }

    public TeknowellContext(DbContextOptions<TeknowellContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Addmission> Addmissions { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Inquiry> Inquiries { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-JAO05T2\\SQLEXPRESS; database=Teknowell; trusted_connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Addmission>(entity =>
        {
            entity.ToTable("Addmission");

            entity.Property(e => e.AddmissionDate).HasColumnType("datetime");
            entity.Property(e => e.BackEnd)
                .IsUnicode(false)
                .HasColumnName("backEnd");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Course)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CurrentAddress).IsUnicode(false);
            entity.Property(e => e.DataBaselanguage)
                .IsUnicode(false)
                .HasColumnName("dataBaselanguage");
            entity.Property(e => e.DiscountRemark).IsUnicode(false);
            entity.Property(e => e.DiscountedFees).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FifthDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FirstDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FourthDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FrontEnd)
                .IsUnicode(false)
                .HasColumnName("frontEnd");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InquiryId).HasColumnName("Inquiry_id");
            entity.Property(e => e.Marks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ParentContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ParentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PermanentAddress).IsUnicode(false);
            entity.Property(e => e.PreviousSchoolClg)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Qualification)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationFees)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SecondDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SixDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TakenBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ThirdDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TransactionID");

            entity.HasOne(d => d.CourseNavigation).WithMany(p => p.Addmissions)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("CourseId");

            entity.HasOne(d => d.Inquiry).WithMany(p => p.Addmissions)
                .HasForeignKey(d => d.InquiryId)
                .HasConstraintName("AddInquiry");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Cid);

            entity.ToTable("Course");

            entity.Property(e => e.BackEnd).IsUnicode(false);
            entity.Property(e => e.CourseFormat)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DataBaseLanguage).IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FrontEnd).IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Eid);

            entity.ToTable("Employee");

            entity.Property(e => e.AlternetContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CurrentAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateOfJoining)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Experience)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdproofNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IDProofNumber");
            entity.Property(e => e.IdproofType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IDProofType");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PermanentAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PreviousExperience).IsUnicode(false);
            entity.Property(e => e.Qualification)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpecializedSubject)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inquiry>(entity =>
        {
            entity.ToTable("Inquiry");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Course)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InquiryTakenBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Qualification)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remark)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CourseNavigation).WithMany(p => p.Inquiries)
                .HasForeignKey(d => d.Courseid)
                .HasConstraintName("CourseCid");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Pid);

            entity.ToTable("Payment");

            entity.Property(e => e.Pid).HasColumnName("PId");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FeesStatus).HasColumnType("decimal(10, 4)");
            entity.Property(e => e.Gmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Narration).IsUnicode(false);
            entity.Property(e => e.ParentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReceiptNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReceiptRemark).IsUnicode(false);
            entity.Property(e => e.ReceivedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReceivedMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TermCondition).IsUnicode(false);
            entity.Property(e => e.TransactionNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("StdentId");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.ToTable("Registration");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ConfirmPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
