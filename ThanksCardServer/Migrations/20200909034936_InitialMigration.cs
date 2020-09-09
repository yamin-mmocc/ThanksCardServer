using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ThanksCardServer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Card_ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Card_Type = table.Column<string>(nullable: true),
                    Card_Style = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    timeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Card_ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Department_ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Department_Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    timeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Department_ID);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Message_ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message_Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Message_ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Role_ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role_Type = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    timeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Role_ID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Status_Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Status_Code);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    timeStamp = table.Column<DateTime>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    newPassword = table.Column<string>(nullable: true),
                    Role_ID = table.Column<long>(nullable: true),
                    Department_ID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                    table.ForeignKey(
                        name: "FK_Users_Departments_Department_ID",
                        column: x => x.Department_ID,
                        principalTable: "Departments",
                        principalColumn: "Department_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Role_ID",
                        column: x => x.Role_ID,
                        principalTable: "Roles",
                        principalColumn: "Role_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogSends",
                columns: table => new
                {
                    SendLog_ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    MessageText = table.Column<string>(nullable: true),
                    Card_ID = table.Column<long>(nullable: true),
                    Status_Code = table.Column<int>(nullable: true),
                    Message_ID = table.Column<long>(nullable: false),
                    Sender_ID = table.Column<long>(nullable: true),
                    FromUser_ID = table.Column<long>(nullable: true),
                    Receiver_ID = table.Column<long>(nullable: true),
                    ToUser_ID = table.Column<long>(nullable: true),
                    timeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogSends", x => x.SendLog_ID);
                    table.ForeignKey(
                        name: "FK_LogSends_Cards_Card_ID",
                        column: x => x.Card_ID,
                        principalTable: "Cards",
                        principalColumn: "Card_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogSends_Users_FromUser_ID",
                        column: x => x.FromUser_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogSends_Messages_Message_ID",
                        column: x => x.Message_ID,
                        principalTable: "Messages",
                        principalColumn: "Message_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogSends_Status_Status_Code",
                        column: x => x.Status_Code,
                        principalTable: "Status",
                        principalColumn: "Status_Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogSends_Users_ToUser_ID",
                        column: x => x.ToUser_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogReceives",
                columns: table => new
                {
                    ReceiveLog_ID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status_Code = table.Column<int>(nullable: true),
                    SendLog_ID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogReceives", x => x.ReceiveLog_ID);
                    table.ForeignKey(
                        name: "FK_LogReceives_LogSends_SendLog_ID",
                        column: x => x.SendLog_ID,
                        principalTable: "LogSends",
                        principalColumn: "SendLog_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogReceives_Status_Status_Code",
                        column: x => x.Status_Code,
                        principalTable: "Status",
                        principalColumn: "Status_Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogReceives_SendLog_ID",
                table: "LogReceives",
                column: "SendLog_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LogReceives_Status_Code",
                table: "LogReceives",
                column: "Status_Code");

            migrationBuilder.CreateIndex(
                name: "IX_LogSends_Card_ID",
                table: "LogSends",
                column: "Card_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LogSends_FromUser_ID",
                table: "LogSends",
                column: "FromUser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LogSends_Message_ID",
                table: "LogSends",
                column: "Message_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LogSends_Status_Code",
                table: "LogSends",
                column: "Status_Code");

            migrationBuilder.CreateIndex(
                name: "IX_LogSends_ToUser_ID",
                table: "LogSends",
                column: "ToUser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Department_ID",
                table: "Users",
                column: "Department_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role_ID",
                table: "Users",
                column: "Role_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogReceives");

            migrationBuilder.DropTable(
                name: "LogSends");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
