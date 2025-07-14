using Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<LibraryInfo> LibraryInfos { get; set; }
        public DbSet<Acquisition> Acquisitions { get; set; }
        public DbSet<AcquisitionItem> AcquisitionItems { get; set; }
        public DbSet<AcquisitionType> AcquisitionTypes { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DivisionStaff> DivisionStaff { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<FineType> FineTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ItemAuthor> ItemAuthors { get; set; }
        public DbSet<ItemCondition> ItemConditions { get; set; }
        public DbSet<ItemCopy> ItemCopies { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LibraryDivision> LibraryDivisions { get; set; }
        public DbSet<LibraryEvent> LibraryEvents { get; set; }
        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<HomeContent> Contents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Newspaper> Newspapers { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Periodical> Periodicals { get; set; }
        public DbSet<UserCodeCounter> UserCodeCounters { get; set; }
        public DbSet<FieldOfStudy> FieldOfStudies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<LibraryItem>()
                .HasDiscriminator<string>("Item Type")
                .HasValue<Book>("Book")
                .HasValue<Newspaper>("Newspaper")
                .HasValue<Journal>("Journal")
                .HasValue<Periodical>("Periodical");

            modelbuilder.Entity<LibraryEvent>()
                .HasIndex(e => e.EventCode).IsUnique();

            // User can only register once for an event
            modelbuilder.Entity<EventParticipant>()
                .HasIndex(ep => new { ep.LibraryEventId, ep.ApplicationUserId })
                .IsUnique();

            modelbuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.UserCode)
                .IsUnique();

            modelbuilder.Entity<LibraryItem>()
               .Property(p => p.PublishedYear)
               .HasColumnType("int");
        }

    }
}
