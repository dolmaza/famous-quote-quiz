// <auto-generated />
using System;
using Famous.Quote.Quiz.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Famous.Quote.Quiz.Infrastructure.Migrations
{
    [DbContext(typeof(FamousQuoteQuizDbContext))]
    [Migration("20210614115810_AddSortIndexColumnInAnswersTable")]
    partial class AddSortIndexColumnInAnswersTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateOfCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<int>("SortIndex")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("DateOfCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("QuizId")
                        .HasColumnType("integer");

                    b.Property<int>("SortIndex")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("DateOfCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("DateOfCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("QuizMode")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("DateOfCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.UserQuiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("DateOfCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("QuizId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.HasIndex("UserId");

                    b.ToTable("UserQuizzes");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.UserQuizAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AnswerId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("DateOfCreate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserQuizId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId")
                        .IsUnique();

                    b.HasIndex("UserQuizId");

                    b.ToTable("UserQuizAnswers");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Answer", b =>
                {
                    b.HasOne("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Question", b =>
                {
                    b.HasOne("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.Setting", b =>
                {
                    b.HasOne("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.User", "User")
                        .WithOne("Setting")
                        .HasForeignKey("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.Setting", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.UserQuiz", b =>
                {
                    b.HasOne("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Quiz", "Quiz")
                        .WithMany("UserQuizzes")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.User", "User")
                        .WithMany("UserQuizzes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.UserQuizAnswer", b =>
                {
                    b.HasOne("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Answer", "Answer")
                        .WithOne("UserQuizAnswer")
                        .HasForeignKey("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.UserQuizAnswer", "AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.UserQuiz", "UserQuiz")
                        .WithMany("UserQuizAnswers")
                        .HasForeignKey("UserQuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("UserQuiz");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Answer", b =>
                {
                    b.Navigation("UserQuizAnswer");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.QuizAggregate.Quiz", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("UserQuizzes");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.User", b =>
                {
                    b.Navigation("Setting");

                    b.Navigation("UserQuizzes");
                });

            modelBuilder.Entity("Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate.UserQuiz", b =>
                {
                    b.Navigation("UserQuizAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
