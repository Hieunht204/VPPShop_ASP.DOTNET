using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VPPShop.Entities;

public partial class TmdtContext : DbContext
{
    public TmdtContext()
    {
    }

    public TmdtContext(DbContextOptions<TmdtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EmailVerification> EmailVerifications { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Statuss> Statusses { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<Webpage> Webpages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__ASSIGNME__B07CF83F3AE33951");

            entity.ToTable("ASSIGNMENT");

            entity.Property(e => e.AssignmentId)
                .HasMaxLength(20)
                .HasColumnName("ASSIGNMENT_ID");
            entity.Property(e => e.AssignDate)
                .HasColumnType("datetime")
                .HasColumnName("ASSIGN_DATE");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(20)
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(20)
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");

            entity.HasOne(d => d.Department).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSIGNMEN__DEPAR__6EF57B66");

            entity.HasOne(d => d.Employee).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSIGNMEN__EMPLO__6E01572D");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__CATEGORI__E7DA297CD8F32139");

            entity.ToTable("CATEGORIES");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(20)
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.AliasName)
                .HasMaxLength(50)
                .HasColumnName("ALIAS_NAME");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.Description).HasColumnName("_DESCRIPTION");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasColumnName("_IMAGE");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__CUSTOMER__1CE12D37CA7CC4B2");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(20)
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("_ADDRESS");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("DATE_OF_BIRTH");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Gender).HasColumnName("GENDER");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasColumnName("_IMAGE");
            entity.Property(e => e.PassWord)
                .HasMaxLength(50)
                .HasColumnName("PASS_WORD");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(24)
                .HasColumnName("PHONENUMBER");
            entity.Property(e => e.Role).HasColumnName("_ROLE");
            entity.Property(e => e.Validity).HasColumnName("VALIDITY");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__DEPARTME__63E6136256547E15");

            entity.ToTable("DEPARTMENT");

            entity.Property(e => e.DepartmentId)
                .HasMaxLength(20)
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .HasColumnName("DEPARTMENT_NAME");
            entity.Property(e => e.Description).HasColumnName("_DESCRIPTION");
        });

        modelBuilder.Entity<EmailVerification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EMAIL_VE__3214EC27D7F1612F");

            entity.ToTable("EMAIL_VERIFICATION");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(20)
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("EXPIRES_AT");
            entity.Property(e => e.IsUsed).HasColumnName("IS_USED");
            entity.Property(e => e.Token)
                .HasMaxLength(100)
                .HasColumnName("TOKEN");

            entity.HasOne(d => d.Customer).WithMany(p => p.EmailVerifications)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_EMAIL_VERIFICATION_CUSTOMER");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__EMPLOYEE__CBA14F48BDD8156F");

            entity.ToTable("EMPLOYEE");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(20)
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.PassWord)
                .HasMaxLength(50)
                .HasColumnName("PASS_WORD");
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.HasKey(e => e.FaqId).HasName("PK__FAQ__838154B4733C4CFE");

            entity.ToTable("FAQ");

            entity.Property(e => e.FaqId)
                .HasMaxLength(20)
                .HasColumnName("FAQ_ID");
            entity.Property(e => e.Answer)
                .HasMaxLength(50)
                .HasColumnName("ANSWER");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(20)
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.Question)
                .HasMaxLength(50)
                .HasColumnName("QUESTION");

            entity.HasOne(d => d.Employee).WithMany(p => p.Faqs)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FAQ__EMPLOYEE_ID__693CA210");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__FAVORITE__2BE3556A1618B7AB");

            entity.ToTable("FAVORITE");

            entity.Property(e => e.FavoriteId)
                .HasMaxLength(20)
                .HasColumnName("FAVORITE_ID");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(20)
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("_DESCRIPTION");
            entity.Property(e => e.ProductId)
                .HasMaxLength(20)
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.SelectedDate)
                .HasColumnType("datetime")
                .HasColumnName("SELECTED_DATE");

            entity.HasOne(d => d.Customer).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__FAVORITE__CUSTOM__778AC167");

            entity.HasOne(d => d.Product).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__FAVORITE__PRODUC__787EE5A0");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__FEEDBACK__BAEB513E3D832B0E");

            entity.ToTable("FEEDBACK");

            entity.Property(e => e.FeedbackId)
                .HasMaxLength(20)
                .HasColumnName("FEEDBACK_ID");
            entity.Property(e => e.Content).HasColumnName("CONTENT");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FeedbackDate).HasColumnName("FEEDBACK_DATE");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.NeedReply).HasColumnName("NEED_REPLY");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("PHONE");
            entity.Property(e => e.Reply)
                .HasMaxLength(50)
                .HasColumnName("REPLY");
            entity.Property(e => e.ReplyDate).HasColumnName("REPLY_DATE");
            entity.Property(e => e.TopicId)
                .HasMaxLength(20)
                .HasColumnName("TOPIC_ID");

            entity.HasOne(d => d.Topic).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FEEDBACK__TOPIC___66603565");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => e.FriendId).HasName("PK__FRIENDS__5E3CF815F26A89F9");

            entity.ToTable("FRIENDS");

            entity.Property(e => e.FriendId)
                .HasMaxLength(20)
                .HasColumnName("FRIEND_ID");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(20)
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Note).HasColumnName("NOTE");
            entity.Property(e => e.ProductId)
                .HasMaxLength(20)
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.SentDate)
                .HasColumnType("datetime")
                .HasColumnName("SENT_DATE");

            entity.HasOne(d => d.Customer).WithMany(p => p.Friends)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__FRIENDS__CUSTOME__5FB337D6");

            entity.HasOne(d => d.Product).WithMany(p => p.Friends)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FRIENDS__PRODUCT__60A75C0F");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__INVOICE__0CE91F08C2BD9C62");

            entity.ToTable("INVOICE");

            entity.Property(e => e.InvoiceId)
                .HasMaxLength(20)
                .HasColumnName("INVOICE_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasColumnName("_ADDRESS");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(20)
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("DELIVERY_DATE");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(20)
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Note)
                .HasMaxLength(50)
                .HasColumnName("NOTE");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("ORDER_DATE");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("PAYMENT_METHOD");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(24)
                .HasColumnName("PHONENUMBER");
            entity.Property(e => e.RequireDate)
                .HasColumnType("datetime")
                .HasColumnName("REQUIRE_DATE");
            entity.Property(e => e.ShippingFee).HasColumnName("SHIPPING_FEE");
            entity.Property(e => e.ShippingMethod)
                .HasMaxLength(50)
                .HasColumnName("SHIPPING_METHOD");
            entity.Property(e => e.StatusId)
                .HasMaxLength(20)
                .HasColumnName("STATUS_ID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__INVOICE__CUSTOME__4F7CD00D");

            entity.HasOne(d => d.Employee).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__INVOICE__EMPLOYE__5165187F");

            entity.HasOne(d => d.Status).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__INVOICE__STATUS___5070F446");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailId).HasName("PK__INVOICE___215C4D2AB005EC36");

            entity.ToTable("INVOICE_DETAIL");

            entity.Property(e => e.InvoiceDetailId)
                .HasMaxLength(20)
                .HasColumnName("INVOICE_DETAIL_ID");
            entity.Property(e => e.Discount).HasColumnName("DISCOUNT");
            entity.Property(e => e.InvoiceId)
                .HasMaxLength(20)
                .HasColumnName("INVOICE_ID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(20)
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.UnitPrice).HasColumnName("UNIT_PRICE");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__INVOICE_D__INVOI__5BE2A6F2");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__INVOICE_D__PRODU__5CD6CB2B");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__PERMISSI__EB3C611ED527EC70");

            entity.ToTable("PERMISSION");

            entity.Property(e => e.PermissionId)
                .HasMaxLength(20)
                .HasColumnName("PERMISSION_ID");
            entity.Property(e => e.CanAdd).HasColumnName("CAN_ADD");
            entity.Property(e => e.CanDelete).HasColumnName("CAN_DELETE");
            entity.Property(e => e.CanEdit).HasColumnName("CAN_EDIT");
            entity.Property(e => e.CanView).HasColumnName("CAN_VIEW");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(20)
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.PageId)
                .HasMaxLength(20)
                .HasColumnName("PAGE_ID");

            entity.HasOne(d => d.Department).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__PERMISSIO__DEPAR__74AE54BC");

            entity.HasOne(d => d.Page).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PageId)
                .HasConstraintName("FK__PERMISSIO__PAGE___73BA3083");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__PRODUCTS__52B41763B0334BB4");

            entity.ToTable("PRODUCTS");

            entity.Property(e => e.ProductId)
                .HasMaxLength(20)
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.AliasName)
                .HasMaxLength(50)
                .HasColumnName("ALIAS_NAME");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(20)
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Description).HasColumnName("_DESCRIPTION");
            entity.Property(e => e.Discount).HasColumnName("DISCOUNT");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasColumnName("_IMAGE");
            entity.Property(e => e.ManufactureDate)
                .HasColumnType("datetime")
                .HasColumnName("MANUFACTURE_DATE");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("PRODUCT_NAME");
            entity.Property(e => e.SupplierId)
                .HasMaxLength(20)
                .HasColumnName("SUPPLIER_ID");
            entity.Property(e => e.UnitDescription)
                .HasMaxLength(50)
                .HasColumnName("UNIT_DESCRIPTION");
            entity.Property(e => e.UnitPrice).HasColumnName("UNIT_PRICE");
            entity.Property(e => e.ViewCount).HasColumnName("VIEW_COUNT");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PRODUCTS__CATEGO__5812160E");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PRODUCTS__SUPPLI__59063A47");
        });

        modelBuilder.Entity<Statuss>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__STATUSS__D8827E7185E50329");

            entity.ToTable("STATUSS");

            entity.Property(e => e.StatusId)
                .HasMaxLength(20)
                .HasColumnName("STATUS_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("_DESCRIPTION");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("STATUS_NAME");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__SUPPLIER__88565CC1CA4C9DE8");

            entity.ToTable("SUPPLIERS");

            entity.Property(e => e.SupplierId)
                .HasMaxLength(20)
                .HasColumnName("SUPPLIER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("_ADDRESS");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .HasColumnName("COMPANY_NAME");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(50)
                .HasColumnName("CONTACT_PERSON");
            entity.Property(e => e.Description).HasColumnName("_DESCRIPTION");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Logo)
                .HasMaxLength(50)
                .HasColumnName("LOGO");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("PHONE_NUMBER");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__TOPIC__A3451659ABA450B6");

            entity.ToTable("TOPIC");

            entity.Property(e => e.TopicId)
                .HasMaxLength(20)
                .HasColumnName("TOPIC_ID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(20)
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.TopicName)
                .HasMaxLength(50)
                .HasColumnName("TOPIC_NAME");

            entity.HasOne(d => d.Employee).WithMany(p => p.Topics)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__TOPIC__EMPLOYEE___6383C8BA");
        });

        modelBuilder.Entity<Webpage>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("PK__WEBPAGE__125498A36F583EAA");

            entity.ToTable("WEBPAGE");

            entity.Property(e => e.PageId)
                .HasMaxLength(20)
                .HasColumnName("PAGE_ID");
            entity.Property(e => e.PageName)
                .HasMaxLength(50)
                .HasColumnName("PAGE_NAME");
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .HasColumnName("_URL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
