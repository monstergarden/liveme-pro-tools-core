﻿// <auto-generated />
using System;
using LMPT.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LMPT.DB.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20190519153100_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("LMPT.Core.Contract.DB.Bookmark.Bookmark", b =>
                {
                    b.Property<string>("Uid")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookmarkType");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Face");

                    b.Property<long>("FollowerCount");

                    b.Property<long>("FollowingCount");

                    b.Property<int>("Gender");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Nickname");

                    b.Property<long>("ReplayCount");

                    b.Property<long>("Shortid");

                    b.Property<string>("Signature");

                    b.HasKey("Uid");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("LMPT.Core.Contract.DB.Cache.ProfileSeen", b =>
                {
                    b.Property<string>("Uid")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Seen");

                    b.HasKey("Uid");

                    b.ToTable("ProfileSeen");
                });

            modelBuilder.Entity("LMPT.Core.Contract.DB.Cache.Replay", b =>
                {
                    b.Property<string>("VId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedAt");

                    b.Property<bool>("Downloaded");

                    b.Property<TimeSpan>("Duration");

                    b.Property<int?>("FromUserInternalId");

                    b.Property<bool>("Liked");

                    b.Property<long>("ShareNum");

                    b.Property<long>("StartTimeStamp");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.Property<string>("VideoUrl");

                    b.Property<bool>("Watched");

                    b.HasKey("VId");

                    b.HasIndex("FromUserInternalId");

                    b.ToTable("Replays");
                });

            modelBuilder.Entity("LMPT.Core.Contract.DB.Cache.User", b =>
                {
                    b.Property<int>("InternalId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("UId");

                    b.HasKey("InternalId");

                    b.HasIndex("UId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("LMPT.Core.Contract.DB.LivemeAuthentication", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("LoginTimestamp");

                    b.Property<string>("Sid");

                    b.Property<string>("SsoToken");

                    b.Property<string>("Token");

                    b.Property<string>("Tuid");

                    b.HasKey("ID");

                    b.ToTable("LivemeAuthentication");
                });

            modelBuilder.Entity("LMPT.Core.Contract.DB.ScanResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BookmarkUid");

                    b.Property<int>("Delta");

                    b.Property<int>("ScanType");

                    b.HasKey("Id");

                    b.HasIndex("BookmarkUid");

                    b.ToTable("LastScanResult");
                });

            modelBuilder.Entity("LMPT.Core.Contract.DB.Cache.Replay", b =>
                {
                    b.HasOne("LMPT.Core.Contract.DB.Cache.User", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserInternalId");
                });

            modelBuilder.Entity("LMPT.Core.Contract.DB.ScanResult", b =>
                {
                    b.HasOne("LMPT.Core.Contract.DB.Bookmark.Bookmark", "Bookmark")
                        .WithMany()
                        .HasForeignKey("BookmarkUid");
                });
#pragma warning restore 612, 618
        }
    }
}
