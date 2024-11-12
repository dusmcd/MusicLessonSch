using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicLessonSch.Migrations
{
    /// <inheritdoc />
    public partial class InstrumentJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instrument_Student_StudentId",
                table: "Instrument");

            migrationBuilder.DropForeignKey(
                name: "FK_Instrument_Teacher_TeacherId",
                table: "Instrument");

            migrationBuilder.DropIndex(
                name: "IX_Instrument_StudentId",
                table: "Instrument");

            migrationBuilder.DropIndex(
                name: "IX_Instrument_TeacherId",
                table: "Instrument");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Instrument");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Instrument");

            migrationBuilder.CreateTable(
                name: "InstrumentStudent",
                columns: table => new
                {
                    InstrumentsId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentStudent", x => new { x.InstrumentsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_InstrumentStudent_Instrument_InstrumentsId",
                        column: x => x.InstrumentsId,
                        principalTable: "Instrument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentStudent_Student_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentTeacher",
                columns: table => new
                {
                    InstrumentsId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentTeacher", x => new { x.InstrumentsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_InstrumentTeacher_Instrument_InstrumentsId",
                        column: x => x.InstrumentsId,
                        principalTable: "Instrument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentTeacher_Teacher_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentStudent_StudentsId",
                table: "InstrumentStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentTeacher_TeachersId",
                table: "InstrumentTeacher",
                column: "TeachersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentStudent");

            migrationBuilder.DropTable(
                name: "InstrumentTeacher");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Instrument",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Instrument",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_StudentId",
                table: "Instrument",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_TeacherId",
                table: "Instrument",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrument_Student_StudentId",
                table: "Instrument",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrument_Teacher_TeacherId",
                table: "Instrument",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id");
        }
    }
}
