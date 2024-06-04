﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaGym.Migrations
{
    /// <inheritdoc />
    public partial class TestSubscription1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                table: "Members");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubscriptionType",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
