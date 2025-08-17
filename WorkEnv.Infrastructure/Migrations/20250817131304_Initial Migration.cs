using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkEnv.Infrastructure.WorkEnv.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CpfCnpj = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DateBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProfilePicture = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PersonalDescription = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true),
                    RegisteredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Privacy = table.Column<string>(type: "text", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    FollowerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FolloweeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => new { x.FollowerId, x.FolloweeId });
                    table.ForeignKey(
                        name: "FK_Follows_Users_FolloweeId",
                        column: x => x.FolloweeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Follows_Users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpace",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumberOfActivities = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    NumberOfCollaborators = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    SecretKey = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpace_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Images = table.Column<List<string>>(type: "text[]", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkSpaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityType = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    NumberOfParticipants = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    MaxNumberOfParticipants = table.Column<int>(type: "integer", nullable: true),
                    AdminInvite_Code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    AdminInvite_ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AdminInvite_AccessLink = table.Column<string>(type: "text", nullable: true),
                    Privacy = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    AccessOption = table.Column<string>(type: "text", nullable: true),
                    AccessKey = table.Column<string>(type: "text", nullable: true),
                    AdminAdmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: true),
                    Task_Status = table.Column<string>(type: "text", nullable: true, defaultValue: "Created"),
                    TaskPriority = table.Column<string>(type: "text", nullable: true),
                    NumberOfAssignedUsers = table.Column<int>(type: "integer", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activity_Users_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activity_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Permissions = table.Column<int[]>(type: "integer[]", nullable: false),
                    WorkSpaceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkSpaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Message_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkSpaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => new { x.WorkSpaceId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Collaborators_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collaborators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collaborators_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipant",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipant", x => new { x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_EventParticipant_Activity_EventId",
                        column: x => x.EventId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventParticipant_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventParticipant_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskAssignments",
                columns: table => new
                {
                    AssignedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AssignedUserWorkSpaceId = table.Column<Guid>(type: "uuid", nullable: true),
                    AssignedUserUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignments", x => new { x.TaskId, x.AssignedUserId });
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Activity_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Collaborators_AssignedUserWorkSpaceId_Assig~",
                        columns: x => new { x.AssignedUserWorkSpaceId, x.AssignedUserUserId },
                        principalTable: "Collaborators",
                        principalColumns: new[] { "WorkSpaceId", "UserId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_AdminId",
                table: "Activity",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_WorkSpaceId",
                table: "Activity",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_RoleId",
                table: "Collaborators",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserId",
                table: "Collaborators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipant_EventId",
                table: "EventParticipant",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipant_RoleId",
                table: "EventParticipant",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FolloweeId",
                table: "Follows",
                column: "FolloweeId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ActivityId",
                table: "Message",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_WorkSpaceId",
                table: "Message",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_WorkSpaceId",
                table: "Role",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_AssignedUserWorkSpaceId_AssignedUserUserId",
                table: "TaskAssignments",
                columns: new[] { "AssignedUserWorkSpaceId", "AssignedUserUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CpfCnpj",
                table: "Users",
                column: "CpfCnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpace_OwnerId",
                table: "WorkSpace",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventParticipant");

            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "TaskAssignments");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Collaborators");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "WorkSpace");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
