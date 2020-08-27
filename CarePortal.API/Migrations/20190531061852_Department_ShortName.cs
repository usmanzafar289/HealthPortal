using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarePortal.API.Migrations
{
    public partial class Department_ShortName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserDepartment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Subscription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Finance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "FeedResponse",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Feed",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Department",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser_DoctorId",
                table: "Conversation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser_PatientId",
                table: "Conversation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser_DoctorId",
                table: "Calendar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser_PatientId",
                table: "Calendar",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConversationViewModel",
                columns: table => new
                {
                    ConversationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<string>(nullable: true),
                    PatientName = table.Column<string>(nullable: true),
                    DoctorId = table.Column<string>(nullable: true),
                    DoctorName = table.Column<string>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    MessageType = table.Column<int>(nullable: false),
                    Category = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationViewModel", x => x.ConversationId);
                });

            migrationBuilder.CreateTable(
                name: "FeedViewModel",
                columns: table => new
                {
                    FeedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Likes = table.Column<int>(nullable: false),
                    Dislikes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedViewModel", x => x.FeedId);
                });

            migrationBuilder.CreateTable(
                name: "CommentsViewModel",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    ProfilePic = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false),
                    FeedId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsViewModel", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_CommentsViewModel_FeedViewModel_FeedId",
                        column: x => x.FeedId,
                        principalTable: "FeedViewModel",
                        principalColumn: "FeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartment_ApplicationUserId",
                table: "UserDepartment",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_ApplicationUserId",
                table: "Subscription",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_ApplicationUserId",
                table: "Finance",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedResponse_ApplicationUserId",
                table: "FeedResponse",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feed_ApplicationUserId",
                table: "Feed",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_ApplicationUser_DoctorId",
                table: "Conversation",
                column: "ApplicationUser_DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_ApplicationUser_PatientId",
                table: "Conversation",
                column: "ApplicationUser_PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_ApplicationUser_DoctorId",
                table: "Calendar",
                column: "ApplicationUser_DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_ApplicationUser_PatientId",
                table: "Calendar",
                column: "ApplicationUser_PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsViewModel_FeedId",
                table: "CommentsViewModel",
                column: "FeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_AspNetUsers_ApplicationUser_DoctorId",
                table: "Calendar",
                column: "ApplicationUser_DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_AspNetUsers_ApplicationUser_PatientId",
                table: "Calendar",
                column: "ApplicationUser_PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_AspNetUsers_ApplicationUser_DoctorId",
                table: "Conversation",
                column: "ApplicationUser_DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_AspNetUsers_ApplicationUser_PatientId",
                table: "Conversation",
                column: "ApplicationUser_PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_AspNetUsers_ApplicationUserId",
                table: "Feed",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedResponse_AspNetUsers_ApplicationUserId",
                table: "FeedResponse",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_AspNetUsers_ApplicationUserId",
                table: "Finance",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_AspNetUsers_ApplicationUserId",
                table: "Subscription",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDepartment_AspNetUsers_ApplicationUserId",
                table: "UserDepartment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_AspNetUsers_ApplicationUser_DoctorId",
                table: "Calendar");

            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_AspNetUsers_ApplicationUser_PatientId",
                table: "Calendar");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_AspNetUsers_ApplicationUser_DoctorId",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_AspNetUsers_ApplicationUser_PatientId",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Feed_AspNetUsers_ApplicationUserId",
                table: "Feed");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedResponse_AspNetUsers_ApplicationUserId",
                table: "FeedResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_Finance_AspNetUsers_ApplicationUserId",
                table: "Finance");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_AspNetUsers_ApplicationUserId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDepartment_AspNetUsers_ApplicationUserId",
                table: "UserDepartment");

            migrationBuilder.DropTable(
                name: "CommentsViewModel");

            migrationBuilder.DropTable(
                name: "ConversationViewModel");

            migrationBuilder.DropTable(
                name: "FeedViewModel");

            migrationBuilder.DropIndex(
                name: "IX_UserDepartment_ApplicationUserId",
                table: "UserDepartment");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_ApplicationUserId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Finance_ApplicationUserId",
                table: "Finance");

            migrationBuilder.DropIndex(
                name: "IX_FeedResponse_ApplicationUserId",
                table: "FeedResponse");

            migrationBuilder.DropIndex(
                name: "IX_Feed_ApplicationUserId",
                table: "Feed");

            migrationBuilder.DropIndex(
                name: "IX_Conversation_ApplicationUser_DoctorId",
                table: "Conversation");

            migrationBuilder.DropIndex(
                name: "IX_Conversation_ApplicationUser_PatientId",
                table: "Conversation");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_ApplicationUser_DoctorId",
                table: "Calendar");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_ApplicationUser_PatientId",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserDepartment");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FeedResponse");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Feed");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "ApplicationUser_DoctorId",
                table: "Conversation");

            migrationBuilder.DropColumn(
                name: "ApplicationUser_PatientId",
                table: "Conversation");

            migrationBuilder.DropColumn(
                name: "ApplicationUser_DoctorId",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "ApplicationUser_PatientId",
                table: "Calendar");
        }
    }
}
