using CodeFirst.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Data
{
    public class SocialDbContext : IdentityDbContext<User>
    {
        public SocialDbContext(DbContextOptions<SocialDbContext> options) : base(options)
        {

        }

        public DbSet<FriendConfirmation> FriendConfirmations { get; set; }
        public DbSet<GroupFollower> GroupFollowers { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<GroupPost> GroupPosts { get; set; }
        public DbSet<MessageHistory> MessageHistories { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<SocialGroup> SocialGroups { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupFollower>(entity =>
            {
                entity.HasOne(d => d.SocialGroup)
                    .WithMany(p => p.GroupFollowers)
                    .HasForeignKey(d => d.SocialGroupId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupFollowers)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasOne(d => d.SocialGroup)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.SocialGroupId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<GroupPost>(entity =>
            {

                entity.HasOne(d => d.SocialGroup)
                    .WithMany(p => p.GroupPosts)
                    .HasForeignKey(d => d.SocialGroupId);

                entity.HasOne(d => d.GroupMember)
                    .WithMany(p => p.GroupPosts)
                    .HasForeignKey(d => d.GroupMemberId);
            });

            modelBuilder.Entity<PostLike>(entity =>
            {
                entity.HasOne(d => d.UserPost)
                    .WithMany(p => p.PostLikes)
                    .HasForeignKey(d => d.UserPostId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostLikes)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ_MyTable_Email")
                    .IsUnique();
            });

            modelBuilder.Entity<UserFollower>(entity =>
            {
                entity.HasOne(d => d.Source)
                    .WithMany(p => p.UserFollowerSources)
                    .HasForeignKey(d => d.SourceId);

                entity.HasOne(d => d.Target)
                    .WithMany(p => p.UserFollowerTargets)
                    .HasForeignKey(d => d.TargetId);
            });

            modelBuilder.Entity<UserFriend>(entity =>
            {
                entity.HasOne(d => d.Source)
                    .WithMany(p => p.UserFriendSources)
                    .HasForeignKey(d => d.SourceId);

                entity.HasOne(d => d.Target)
                    .WithMany(p => p.UserFriendTargets)
                    .HasForeignKey(d => d.TargetId);
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.Property(e => e.MessageType)
                    .HasDefaultValueSql("('text')")
                    .IsFixedLength();

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.UserMessageSources)
                    .HasForeignKey(d => d.SourceId);

                entity.HasOne(d => d.Target)
                    .WithMany(p => p.UserMessageTargets)
                    .HasForeignKey(d => d.TargetId);
            });

            modelBuilder.Entity<UserPost>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPosts)
                    .HasForeignKey(d => d.UserId);
            });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
