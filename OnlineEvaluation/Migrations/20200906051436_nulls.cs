using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineEvaluation.Migrations
{
    public partial class nulls : Migration
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

            migrationBuilder.AlterColumn<Guid>(
                name: "DirectorId",
                table: "EvaluationSubject",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChairpersonId",
                table: "EvaluationSubject",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CEOId",
                table: "EvaluationSubject",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "DirectorId",
                table: "EvaluationSubject",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChairpersonId",
                table: "EvaluationSubject",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CEOId",
                table: "EvaluationSubject",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubject_CEO_CEOId",
                table: "EvaluationSubject",
                column: "CEOId",
                principalTable: "CEO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubject_Chairperson_ChairpersonId",
                table: "EvaluationSubject",
                column: "ChairpersonId",
                principalTable: "Chairperson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubject_Director_DirectorId",
                table: "EvaluationSubject",
                column: "DirectorId",
                principalTable: "Director",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
