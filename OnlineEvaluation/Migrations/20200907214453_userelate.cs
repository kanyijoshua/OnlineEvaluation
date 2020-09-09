using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineEvaluation.Migrations
{
    public partial class userelate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationSubject_CEO_CEOId",
                table: "EvaluationSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationSubject_Chairperson_ChairpersonId",
                table: "EvaluationSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationSubject_Director_DirectorId",
                table: "EvaluationSubject");

            migrationBuilder.AlterColumn<string>(
                name: "DirectorId",
                table: "EvaluationSubject",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChairpersonId",
                table: "EvaluationSubject",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CEOId",
                table: "EvaluationSubject",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Id",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubject_AspNetUsers_CEOId",
                table: "EvaluationSubject",
                column: "CEOId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubject_AspNetUsers_ChairpersonId",
                table: "EvaluationSubject",
                column: "ChairpersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubject_AspNetUsers_DirectorId",
                table: "EvaluationSubject",
                column: "DirectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationSubject_AspNetUsers_CEOId",
                table: "EvaluationSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationSubject_AspNetUsers_ChairpersonId",
                table: "EvaluationSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationSubject_AspNetUsers_DirectorId",
                table: "EvaluationSubject");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Id",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "DirectorId",
                table: "EvaluationSubject",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChairpersonId",
                table: "EvaluationSubject",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CEOId",
                table: "EvaluationSubject",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubject_CEO_CEOId",
                table: "EvaluationSubject",
                column: "CEOId",
                principalTable: "CEO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubject_Chairperson_ChairpersonId",
                table: "EvaluationSubject",
                column: "ChairpersonId",
                principalTable: "Chairperson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubject_Director_DirectorId",
                table: "EvaluationSubject",
                column: "DirectorId",
                principalTable: "Director",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
