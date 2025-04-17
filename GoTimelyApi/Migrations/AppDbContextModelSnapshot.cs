﻿// <auto-generated />
using System;
using GoTimelyAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoTimelyAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoTimelyAPI.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("GoTimelyAPI.Bus", b =>
                {
                    b.Property<int>("BusNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusNumber"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("FromDestination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToDestination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BusNumber");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("GoTimelyAPI.Passenger", b =>
                {
                    b.Property<int>("PassengerID")
                        .HasColumnType("int")
                        .HasColumnName("passengerID");

                    b.Property<string>("EmailID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("emailID");

                    b.Property<string>("NameID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneID")
                        .HasColumnType("bigint")
                        .HasColumnName("phoneID");

                    b.HasKey("PassengerID");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("GoTimelyAPI.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentID"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TicketID")
                        .HasColumnType("int");

                    b.HasKey("PaymentID");

                    b.HasIndex("TicketID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("GoTimelyAPI.Seat", b =>
                {
                    b.Property<int>("SeatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeatID"));

                    b.Property<int>("BusID")
                        .HasColumnType("int");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("bit");

                    b.HasKey("SeatID");

                    b.HasIndex("BusID");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("GoTimelyAPI.Ticket", b =>
                {
                    b.Property<int>("TicketNumber")
                        .HasColumnType("int");

                    b.Property<int>("BusNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PassengerID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TicketNumber");

                    b.HasIndex("BusNumber");

                    b.HasIndex("PassengerID");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("GoTimelyAPI.TimeTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("ArrivalTime")
                        .HasColumnType("time");

                    b.Property<int>("BusNumber")
                        .HasColumnType("int");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Delay")
                        .HasColumnType("int");

                    b.Property<string>("Station")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("BusNumber");

                    b.ToTable("TimeTables");
                });

            modelBuilder.Entity("GoTimelyAPI.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(510)
                        .HasColumnType("nvarchar(510)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GoTimelyAPI.Passenger", b =>
                {
                    b.HasOne("GoTimelyAPI.User", null)
                        .WithMany("Passengers")
                        .HasForeignKey("PassengerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoTimelyAPI.Payment", b =>
                {
                    b.HasOne("GoTimelyAPI.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("GoTimelyAPI.Seat", b =>
                {
                    b.HasOne("GoTimelyAPI.Bus", "Bus")
                        .WithMany()
                        .HasForeignKey("BusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bus");
                });

            modelBuilder.Entity("GoTimelyAPI.Ticket", b =>
                {
                    b.HasOne("GoTimelyAPI.Bus", "Bus")
                        .WithMany()
                        .HasForeignKey("BusNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoTimelyAPI.Passenger", "Passenger")
                        .WithMany("Tickets")
                        .HasForeignKey("PassengerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoTimelyAPI.User", null)
                        .WithMany("BookedTickets")
                        .HasForeignKey("TicketNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bus");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("GoTimelyAPI.TimeTable", b =>
                {
                    b.HasOne("GoTimelyAPI.Bus", "Bus")
                        .WithMany()
                        .HasForeignKey("BusNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bus");
                });

            modelBuilder.Entity("GoTimelyAPI.Passenger", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("GoTimelyAPI.User", b =>
                {
                    b.Navigation("BookedTickets");

                    b.Navigation("Passengers");
                });
#pragma warning restore 612, 618
        }
    }
}
