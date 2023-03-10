// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportSparkInfrastructureLibrary.Database;

#nullable disable

namespace SportSparkInfrastructureLibrary.Data.Migrations
{
    [DbContext(typeof(SportSparkDBContext))]
    [Migration("20230314211549_FriendshipStatus")]
    partial class FriendshipStatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BlobId")
                        .IsRequired()
                        .HasMaxLength(511)
                        .HasColumnType("nvarchar(511)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Duration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Lat")
                        .HasColumnType("decimal(8, 6)");

                    b.Property<decimal?>("Long")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<int?>("NumberOfParticipants")
                        .HasColumnType("int");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Privacy")
                        .HasColumnType("int");

                    b.Property<int>("RepeatTypeId")
                        .HasColumnType("int");

                    b.Property<string>("TimeOfDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("RepeatTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.EventRepeatType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EventRepeatType");
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EventType");
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.Friendship", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("User2Id")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("UserId", "User2Id");

                    b.ToTable("Friendship");
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .UseCollation("SQL_Latin1_General_CP1_CS_AS");

                    b.Property<int?>("ProfileImageId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("decimal(9, 6)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ProfileImageId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.Event", b =>
                {
                    b.HasOne("SportSparkCoreLibrary.Entities.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeId");

                    b.HasOne("SportSparkCoreLibrary.Entities.EventRepeatType", "RepeatType")
                        .WithMany("Events")
                        .HasForeignKey("RepeatTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportSparkCoreLibrary.Entities.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventType");

                    b.Navigation("RepeatType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.Friendship", b =>
                {
                    b.HasOne("SportSparkCoreLibrary.Entities.User", null)
                        .WithMany("Friendships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.User", b =>
                {
                    b.HasOne("SportSparkCoreLibrary.Entities.Document", "ProfileImage")
                        .WithMany()
                        .HasForeignKey("ProfileImageId");

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.EventRepeatType", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.EventType", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("SportSparkCoreLibrary.Entities.User", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Friendships");
                });
#pragma warning restore 612, 618
        }
    }
}
