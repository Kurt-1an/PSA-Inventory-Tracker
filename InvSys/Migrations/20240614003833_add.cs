using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvSys.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    empPos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    empMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    propCustodian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true),
                    propArticle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propPropertyNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    propUnitOfMeasure = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    propUnitValue = table.Column<double>(type: "float", nullable: true),
                    propBalancePerCardQty = table.Column<int>(type: "int", nullable: true),
                    propOnHandPerCardQty = table.Column<int>(type: "int", nullable: true),
                    propShortageOverageQty = table.Column<int>(type: "int", nullable: true),
                    propShortageOverageValue = table.Column<double>(type: "float", nullable: true),
                    propYrAcquired = table.Column<DateOnly>(type: "date", nullable: true),
                    propRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    propStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Supplies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    supStockLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supArticle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    supStockNumber = table.Column<int>(type: "int", nullable: true),
                    supDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supUnitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supAverageUnitCost = table.Column<double>(type: "float", nullable: true),
                    supDelivery = table.Column<int>(type: "int", nullable: true),
                    supBegInvDate = table.Column<DateOnly>(type: "date", nullable: false),
                    supBeginningInventoryQty = table.Column<int>(type: "int", nullable: true),
                    supEndingInventoryQty = table.Column<int>(type: "int", nullable: true),
                    supBalance = table.Column<int>(type: "int", nullable: true),
                    supTotalAmount = table.Column<double>(type: "float", nullable: true),
                    supIssuance = table.Column<int>(type: "int", nullable: true),
                    supRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SupplyHistories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplyID = table.Column<int>(type: "int", nullable: false),
                    DateModified = table.Column<DateOnly>(type: "date", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyHistories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a8083892-ecd2-49b9-ad54-1fcfc99b1241", 0, "7f09f932-7f8c-42e3-adbb-527fa1dac321", "admin@psa.gov.ph", true, false, null, null, "ADMIN@PSA.GOV.PH", "AQAAAAIAAYagAAAAEF8VNN2IVZeshaPmCloPjO4tHuYNQ93J+1kataEFq5XBTF3psyEkj1lheJAQk8c1PA==", null, false, "23d5b2d4-58f6-4e7f-b3a0-e1dedf3b6d0d", false, "admin@psa.gov.ph" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "IsRemoved", "empMail", "empName", "empPos" },
                values: new object[,]
                {
                    { 1, false, null, "YRA B. SIBUG", "Chief Statistical Specialist" },
                    { 2, false, null, "MERCY LIZA B. TIBAY", "Supervising Statistical Specialist" },
                    { 3, false, null, "MA. CRISTINA C. CRISOL", "Statistical Specialist II" },
                    { 4, false, null, "VANESSA A. ABARQUEZ", "Senior Statistical Specialist" },
                    { 5, false, null, "LYN A. JERUSALEM", "Statistical Specialist II" },
                    { 6, false, null, "MARIA TERESITA E. FELIX", "Statistical Specialist II" },
                    { 7, false, null, "GEMALLI I. AGUSTIN", "Statistical Specialist II" },
                    { 8, false, null, "MARITES T. JUAN", "Statistical Specialist II" },
                    { 9, false, null, "LOWELL D. SAN ESTEBAN", "Statistical Specialist II" },
                    { 10, false, null, "ROXANNE P. VILLANUEVA", "Registration Officer II" },
                    { 11, false, null, "GEMMA DC. SIPPI", "Statistical Analyst" },
                    { 12, false, null, "CHERRY ANN A. VILLANUEVA", "Statistical Analyst" },
                    { 13, false, null, "LINO BILOLO JR", "Registration Officer I" },
                    { 14, false, null, "JOMARI T. PERLAS", "Registration Officer I" },
                    { 15, false, null, "KIMBERLY JOYCE M. ATOLE", "Administrative Officer I" },
                    { 16, false, null, "DAN LEONEL T. ANISCOL", "Registration Officer I" },
                    { 17, false, null, "DANICA O. CABALSA", "Registration Officer I" },
                    { 18, false, null, "ROSELYND N. LUCAS", "Accountant I" },
                    { 19, false, null, "ANGELA B. VILLANUEVA", "Registration Officer I" },
                    { 20, false, null, "MARVIN SA. SAGUN", "Registration Officer I" },
                    { 21, false, null, "RICHIEGOLD VARCA", "Administrative Officer I" },
                    { 22, false, null, "MAC-MAC R. ABDULLAH", "Administrative Assistant III" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Supplies");

            migrationBuilder.DropTable(
                name: "SupplyHistories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
