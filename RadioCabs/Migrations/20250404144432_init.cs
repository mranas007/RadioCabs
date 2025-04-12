using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RadioCabs.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DriverID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(19)", maxLength: 19, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyID = table.Column<long>(type: "bigint", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Membership = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisements_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { "user1", 0, "XYZ", "user1@example.com", true, "User One", true, null, "USER1@EXAMPLE.COM", "USER1@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user1@example.com", "Company" },
                    { "user10", 0, "XYZ", "user10@example.com", true, "User Ten", true, null, "USER10@EXAMPLE.COM", "USER10@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user10@example.com", "Company" },
                    { "user11", 0, "XYZ", "user11@example.com", true, "User Eleven", true, null, "USER11@EXAMPLE.COM", "USER11@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user11@example.com", "Driver" },
                    { "user12", 0, "XYZ", "user12@example.com", true, "User Twelve", true, null, "USER12@EXAMPLE.COM", "USER12@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user12@example.com", "Driver" },
                    { "user13", 0, "XYZ", "user13@example.com", true, "User Thirteen", true, null, "USER13@EXAMPLE.COM", "USER13@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user13@example.com", "Driver" },
                    { "user14", 0, "XYZ", "user14@example.com", true, "User Fourteen", true, null, "USER14@EXAMPLE.COM", "USER14@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user14@example.com", "Driver" },
                    { "user15", 0, "XYZ", "user15@example.com", true, "User Fifteen", true, null, "USER15@EXAMPLE.COM", "USER15@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user15@example.com", "Driver" },
                    { "user16", 0, "XYZ", "user16@example.com", true, "User Sixteen", true, null, "USER16@EXAMPLE.COM", "USER16@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user16@example.com", "Driver" },
                    { "user17", 0, "XYZ", "user17@example.com", true, "User Seventeen", true, null, "USER17@EXAMPLE.COM", "USER17@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user17@example.com", "Driver" },
                    { "user18", 0, "XYZ", "user18@example.com", true, "User Eighteen", true, null, "USER18@EXAMPLE.COM", "USER18@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user18@example.com", "Driver" },
                    { "user19", 0, "XYZ", "user19@example.com", true, "User Nineteen", true, null, "USER19@EXAMPLE.COM", "USER19@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user19@example.com", "Driver" },
                    { "user2", 0, "XYZ", "user2@example.com", true, "User Two", true, null, "USER2@EXAMPLE.COM", "USER2@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user2@example.com", "Company" },
                    { "user3", 0, "XYZ", "user3@example.com", true, "User Three", true, null, "USER3@EXAMPLE.COM", "USER3@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user3@example.com", "Company" },
                    { "user4", 0, "XYZ", "user4@example.com", true, "User Four", true, null, "USER4@EXAMPLE.COM", "USER4@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user4@example.com", "Company" },
                    { "user5", 0, "XYZ", "user5@example.com", true, "User Five", true, null, "USER5@EXAMPLE.COM", "USER5@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user5@example.com", "Company" },
                    { "user6", 0, "XYZ", "user6@example.com", true, "User Six", true, null, "USER6@EXAMPLE.COM", "USER6@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user6@example.com", "Company" },
                    { "user7", 0, "XYZ", "user7@example.com", true, "User Seven", true, null, "USER7@EXAMPLE.COM", "USER7@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user7@example.com", "Company" },
                    { "user8", 0, "XYZ", "user8@example.com", true, "User Eight", true, null, "USER8@EXAMPLE.COM", "USER8@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user8@example.com", "Company" },
                    { "user9", 0, "XYZ", "user9@example.com", true, "User Nine", true, null, "USER9@EXAMPLE.COM", "USER9@EXAMPLE.COM", "AQAAAAEAACcQAAAAE...", "1234567890", true, "XYZ", false, "user9@example.com", "Company" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "CompanyID", "CompanyName", "Description", "Designation", "Membership", "PaymentId", "UserId" },
                values: new object[,]
                {
                    { 1, "123 Tech Street", 1234567890L, "Tech Solutions", "Leading IT service provider", "IT Services", "Premium", null, "user1" },
                    { 2, "456 Health Ave", 2345678901L, "HealthCare Inc.", "Comprehensive healthcare services", "Healthcare Services", "Basic", null, "user2" },
                    { 3, "789 Edu Lane", 3456789012L, "EduWorld", "Innovative educational solutions", "Educational Services", "Free", null, "user3" },
                    { 4, "101 Finance Blvd", 4567890123L, "Finance Corp", "Expert financial services", "Financial Services", "Premium", null, "user4" },
                    { 5, "202 Retail Road", 5678901234L, "RetailMart", "Top retail services", "Retail Services", "Basic", null, "user5" },
                    { 6, "303 Auto Drive", 6789012345L, "AutoWorks", "Reliable automotive services", "Automotive Services", "Free", null, "user6" },
                    { 7, "404 Food Street", 7890123456L, "Foodies", "Delicious food services", "Food Services", "Premium", null, "user7" },
                    { 8, "505 Travel Blvd", 8901234567L, "TravelCo", "Exceptional travel services", "Travel Services", "Basic", null, "user8" },
                    { 9, "606 Media Lane", 9012345678L, "MediaHouse", "Creative media services", "Media Services", "Free", null, "user9" },
                    { 10, "707 Build Ave", 1234567891L, "BuildIt", "Quality construction services", "Construction Services", "Premium", null, "user10" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "Address", "City", "ContactPerson", "Description", "DriverID", "DriverName", "Experience", "UserId" },
                values: new object[,]
                {
                    { 1, "123 Driver Street", "Tech City", "Jane Doe", "Experienced and reliable driver", "D12345", "John Doe", 5, "user11" },
                    { 2, "456 Driver Ave", "Health City", "Bob Smith", "Reliable and safe driver", "D23456", "Alice Smith", 3, "user12" },
                    { 3, "789 Driver Lane", "Edu City", "Sarah Johnson", "Professional and skilled driver", "D34567", "Michael Johnson", 7, "user13" },
                    { 4, "101 Driver Blvd", "Finance City", "David Davis", "Skilled and courteous driver", "D45678", "Emily Davis", 2, "user14" },
                    { 5, "202 Driver Road", "Retail City", "Linda Brown", "Safe and experienced driver", "D56789", "James Brown", 4, "user15" },
                    { 6, "303 Driver Drive", "Auto City", "Robert Wilson", "Experienced and professional driver", "D67890", "Patricia Wilson", 6, "user16" },
                    { 7, "404 Driver Street", "Food City", "Laura Martinez", "Reliable and professional driver", "D78901", "Christopher Martinez", 8, "user17" },
                    { 8, "505 Driver Blvd", "Travel City", "Daniel Garcia", "Professional and courteous driver", "D89012", "Jessica Garcia", 1, "user18" },
                    { 9, "606 Driver Lane", "Media City", "Maria Rodriguez", "Skilled and experienced driver", "D90123", "David Rodriguez", 9, "user19" }
                });

            migrationBuilder.InsertData(
                table: "Advertisements",
                columns: new[] { "Id", "Address", "CompanyId", "Description", "Designation", "FaxNumber", "Mobile", "Telephone", "Title" },
                values: new object[,]
                {
                    { 1, "123 Tech Street", 1, "Leading cab service provider", "Cab Services", null, "1234567890", null, "Tech Solutions Cab Ad" },
                    { 2, "456 Health Ave", 2, "Comprehensive cab services", "Cab Services", null, "2345678901", null, "HealthCare Inc. Cab Ad" },
                    { 3, "789 Edu Lane", 3, "Innovative cab solutions", "Cab Services", null, "3456789012", null, "EduWorld Cab Ad" },
                    { 4, "101 Finance Blvd", 4, "Expert cab services", "Cab Services", null, "4567890123", null, "Finance Corp Cab Ad" },
                    { 5, "202 Retail Road", 5, "Top cab services", "Cab Services", null, "5678901234", null, "RetailMart Cab Ad" },
                    { 6, "303 Auto Drive", 6, "Reliable cab services", "Cab Services", null, "6789012345", null, "AutoWorks Cab Ad" },
                    { 7, "404 Food Street", 7, "Delicious cab services", "Cab Services", null, "7890123456", null, "Foodies Cab Ad" },
                    { 8, "505 Travel Blvd", 8, "Exceptional cab services", "Cab Services", null, "8901234567", null, "TravelCo Cab Ad" },
                    { 9, "606 Media Lane", 9, "Creative cab services", "Cab Services", null, "9012345678", null, "MediaHouse Cab Ad" },
                    { 10, "707 Build Ave", 10, "Quality cab services", "Cab Services", null, "1234567891", null, "BuildIt Cab Ad" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CompanyId",
                table: "Advertisements",
                column: "CompanyId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Companies_PaymentId",
                table: "Companies",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_UserId",
                table: "Drivers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements");

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
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
