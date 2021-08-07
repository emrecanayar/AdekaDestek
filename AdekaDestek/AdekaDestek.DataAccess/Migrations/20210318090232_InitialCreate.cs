using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdekaDestek.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SapUserName = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    SapEmployeeNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    InfiniUserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TwoFactorType = table.Column<short>(type: "smallint", maxLength: 1, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", maxLength: 30, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", maxLength: 30, nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ModifiedByName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", maxLength: 1, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "45818398-ecc1-42e3-ae7b-61a59e80ab7e", "Payroll.Create", "PAYROLL.CREATE" },
                    { 20, "843402e7-d27d-4443-a89e-fd1c33d59672", "Editor", "EDITOR" },
                    { 19, "75dc651b-eaf9-432e-b203-c6afd73f18b0", "Admin", "ADMIN" },
                    { 18, "3424e2e2-2b8c-4dc6-ae43-80798a030a15", "SuperAdmin", "SUPERADMIN" },
                    { 17, "64e0774f-e0d8-45bd-a8ea-9ce41a0fb84a", "AdminArea.Home.Read", "ADMINAREA.HOME.READ" },
                    { 16, "8e1f07cb-432e-4349-889c-68a965f2b025", "Role.Delete", "ROLE.DELETE" },
                    { 15, "3478ecfa-89b1-4aed-845d-64cad81cdb21", "Role.Update", "ROLE.UPDATE" },
                    { 14, "918832ba-9744-4251-9972-01639bc96745", "Role.Read", "ROLE.READ" },
                    { 13, "7ca52267-8136-48a5-84e4-9c8101386571", "Role.Create", "ROLE.CREATE" },
                    { 12, "229a658f-34e3-4529-8b65-f93a85ba27aa", "User.Delete", "USER.DELETE" },
                    { 11, "cb6d909b-8749-489a-90a0-d06b5d255220", "User.Update", "USER.UPDATE" },
                    { 10, "139e63a1-71f1-415f-a3d1-77cb85cff090", "User.Read", "USER.READ" },
                    { 9, "62a38983-126e-434a-9eb2-5f94457b70d0", "User.Create", "USER.CREATE" },
                    { 8, "17cd0d10-ed3f-4751-90ee-02345b3d75c2", "AnnualPermit.Delete", "ANNUALPERMIT.DELETE" },
                    { 7, "1b8eeb13-2cc9-419b-94ca-7adb518ce4f7", "AnnualPermit.Update", "ANNUALPERMIT.UPDATE" },
                    { 6, "6a410476-3795-4e07-a91f-b8a07893ef6e", "AnnualPermit.Read", "ANNUALPERMIT.READ" },
                    { 5, "ee83b42e-d823-4385-a340-0369fd3b722d", "AnnualPermit.Create", "ANNUALPERMIT.CREATE" },
                    { 4, "4c330874-3d97-4c37-ac80-3c05c675fe48", "Payroll.Delete", "PAYROLL.DELETE" },
                    { 3, "47d60c49-7603-4229-b681-1670beffcd2b", "Payroll.Update", "PAYROLL.UPDATE" },
                    { 2, "f72f133c-dfc6-4886-8dbc-f2bf04313d2c", "Payroll.Read", "PAYROLL.READ" },
                    { 21, "691e22f7-e521-43b5-a059-4a1600bb8bfd", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedByName", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "InfiniUserName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedByName", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SapEmployeeNo", "SapUserName", "SecurityStamp", "TwoFactorEnabled", "TwoFactorType", "UserName" },
                values: new object[] { 1, 0, "5f23d5e5-b2c9-4bc8-b033-b234e68ce6f8", "Emre Can Ayar", new DateTime(2021, 3, 18, 12, 2, 31, 974, DateTimeKind.Local).AddTicks(3383), "adminuser@adeka.com.tr", true, "Admin", "admin.user", true, "User", false, null, "Emre Can Ayar", new DateTime(2021, 3, 18, 12, 2, 31, 975, DateTimeKind.Local).AddTicks(7204), "ADMINUSER@ADEKA.COM.TR", "ADMINUSER", "AQAAAAEAACcQAAAAEEoOW8HxUS4WXwL8+9nwDdSr4bEEheSzQwq/6mKOMFQuwp+ebMYNm9fgZZLfC8KLUw==", "+905555555555", true, "999", "admin.user", "c97dd2ac-3565-4959-b689-098d52cad940", false, (short)0, "adminuser" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
