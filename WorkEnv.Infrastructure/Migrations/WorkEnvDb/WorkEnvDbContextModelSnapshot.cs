﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorkEnv.Infrastructure.Context;

#nullable disable

namespace WorkEnv.Infrastructure.Migrations
{
    [DbContext(typeof(WorkEnvDbContext))]
    partial class WorkEnvDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WorkEnv.Domain.Entities.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccessOptions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AccessPassword")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("ActivityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Created");

                    b.Property<string>("ActivityType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<Guid?>("AdminId")
                        .HasColumnType("uuid");

                    b.Property<int>("MaxNumberOfParticipants")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("NumberOfParticipants")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Privacy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("WorkSpaceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("WorkSpaceId");

                    b.ToTable("Activity", (string)null);

                    b.HasDiscriminator<string>("ActivityType").HasValue("Activity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.Message", b =>
                {
                    b.Property<Guid>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.HasKey("MessageId");

                    b.HasIndex("ActivityId");

                    b.ToTable("Message", (string)null);
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("RoleId");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<DateTime>("_expirationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("_refreshToken")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("_refreshToken");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.UserActivity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "ActivityId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserActivities");
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.WorkSpace", b =>
                {
                    b.Property<Guid>("WorkSpaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("NumberOfActivities")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("_masterCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("_masterCode");

                    b.HasKey("WorkSpaceId");

                    b.HasIndex("OwnerId");

                    b.ToTable("WorkSpace", (string)null);
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.Event", b =>
                {
                    b.HasBaseType("WorkEnv.Domain.Entities.Activity");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasDiscriminator().HasValue("Event");
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.Task", b =>
                {
                    b.HasBaseType("WorkEnv.Domain.Entities.Activity");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasDiscriminator().HasValue("Task");
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.Activity", b =>
                {
                    b.HasOne("WorkEnv.Domain.Entities.User", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.HasOne("WorkEnv.Domain.Entities.WorkSpace", "WorkSpace")
                        .WithMany("Activities")
                        .HasForeignKey("WorkSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WorkEnv.Domain.ValueObjects.AdminInvite", "AdminInviteCode", b1 =>
                        {
                            b1.Property<Guid>("ActivityId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Code")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("CodeExpirationDate")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("ActivityId");

                            b1.ToTable("Activity");

                            b1.WithOwner()
                                .HasForeignKey("ActivityId");
                        });

                    b.Navigation("Admin");

                    b.Navigation("AdminInviteCode")
                        .IsRequired();

                    b.Navigation("WorkSpace");
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.Message", b =>
                {
                    b.HasOne("WorkEnv.Domain.Entities.Activity", "Activity")
                        .WithMany("Messages")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.UserActivity", b =>
                {
                    b.HasOne("WorkEnv.Domain.Entities.Activity", "Activity")
                        .WithMany("UserActivities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkEnv.Domain.Entities.Role", "Role")
                        .WithMany("UserActivities")
                        .HasForeignKey("RoleId");

                    b.HasOne("WorkEnv.Domain.Entities.User", "User")
                        .WithMany("UserActivities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.WorkSpace", b =>
                {
                    b.HasOne("WorkEnv.Domain.Entities.User", "Owner")
                        .WithMany("WorkSpaces")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.Activity", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserActivities");
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.Role", b =>
                {
                    b.Navigation("UserActivities");
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.User", b =>
                {
                    b.Navigation("UserActivities");

                    b.Navigation("WorkSpaces");
                });

            modelBuilder.Entity("WorkEnv.Domain.Entities.WorkSpace", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}
