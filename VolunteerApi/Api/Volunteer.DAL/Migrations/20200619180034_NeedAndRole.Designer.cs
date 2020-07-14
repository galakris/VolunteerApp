﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volunteer.DAL;

namespace Volunteer.DAL.Migrations
{
    [DbContext(typeof(DalContext))]
    [Migration("20200619180034_NeedAndRole")]
    partial class NeedAndRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Volunteer.DAL.Entities.NeedEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Category")
                        .HasColumnName("category")
                        .HasColumnType("text");

                    b.Property<DateTime>("DeadlineDate")
                        .HasColumnName("deadline_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnName("latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnName("longitude")
                        .HasColumnType("double precision");

                    b.Property<int>("NeedStatus")
                        .HasColumnName("need_status")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("pk_needs");

                    b.ToTable("needs");
                });

            modelBuilder.Entity("Volunteer.DAL.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("RoleDetails")
                        .HasColumnName("role_details")
                        .HasColumnType("text");

                    b.Property<string>("RoleName")
                        .HasColumnName("role_name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Volunteer.DAL.Entities.UserAccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnName("password_hash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnName("password_salt")
                        .HasColumnType("bytea");

                    b.Property<string>("UserName")
                        .HasColumnName("user_name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Volunteer.DAL.Relations.RoleUserAccount", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("integer");

                    b.Property<int>("UserAccountId")
                        .HasColumnName("user_account_id")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "UserAccountId")
                        .HasName("pk_role_user_accounts");

                    b.HasIndex("UserAccountId")
                        .HasName("ix_role_user_accounts_user_account_id");

                    b.ToTable("role_user_accounts");
                });

            modelBuilder.Entity("Volunteer.DAL.Relations.UserAccountNeed", b =>
                {
                    b.Property<int>("NeedId")
                        .HasColumnName("need_id")
                        .HasColumnType("integer");

                    b.Property<int>("UserAccountId")
                        .HasColumnName("user_account_id")
                        .HasColumnType("integer");

                    b.Property<int>("Role")
                        .HasColumnName("role")
                        .HasColumnType("integer");

                    b.HasKey("NeedId", "UserAccountId")
                        .HasName("pk_user_account_needs");

                    b.HasIndex("UserAccountId")
                        .HasName("ix_user_account_needs_user_account_id");

                    b.ToTable("user_account_needs");
                });

            modelBuilder.Entity("Volunteer.DAL.Relations.RoleUserAccount", b =>
                {
                    b.HasOne("Volunteer.DAL.Entities.RoleEntity", "Role")
                        .WithMany("RoleUserAccount")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_role_user_accounts_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Volunteer.DAL.Entities.UserAccountEntity", "UserAccount")
                        .WithMany("RoleUserAccount")
                        .HasForeignKey("UserAccountId")
                        .HasConstraintName("fk_role_user_accounts_users_user_account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Volunteer.DAL.Relations.UserAccountNeed", b =>
                {
                    b.HasOne("Volunteer.DAL.Entities.NeedEntity", "Need")
                        .WithMany("UserAccountNeeds")
                        .HasForeignKey("NeedId")
                        .HasConstraintName("fk_user_account_needs_needs_need_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Volunteer.DAL.Entities.UserAccountEntity", "UserAccount")
                        .WithMany("UserAccountNeeds")
                        .HasForeignKey("UserAccountId")
                        .HasConstraintName("fk_user_account_needs_users_user_account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
