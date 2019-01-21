using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_HospitalDatabase.Data.Migrations
{
    public partial class ChangeToID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_Patients_PatientId",
                table: "Diagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientsMedicaments_Medicaments_MedicamentId",
                table: "PatientsMedicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientsMedicaments_Patients_PatientId",
                table: "PatientsMedicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitations_Doctors_DoctorId",
                table: "Visitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitations_Patients_PatientId",
                table: "Visitations");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Visitations",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Visitations",
                newName: "DoctorID");

            migrationBuilder.RenameColumn(
                name: "VisitationId",
                table: "Visitations",
                newName: "VisitationID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitations_PatientId",
                table: "Visitations",
                newName: "IX_Visitations_PatientID");

            migrationBuilder.RenameIndex(
                name: "IX_Visitations_DoctorId",
                table: "Visitations",
                newName: "IX_Visitations_DoctorID");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "PatientsMedicaments",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "MedicamentId",
                table: "PatientsMedicaments",
                newName: "MedicamentID");

            migrationBuilder.RenameIndex(
                name: "IX_PatientsMedicaments_PatientId",
                table: "PatientsMedicaments",
                newName: "IX_PatientsMedicaments_PatientID");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patients",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "MedicamentId",
                table: "Medicaments",
                newName: "MedicamentID");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Doctors",
                newName: "DoctorID");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Diagnoses",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "DiagnoseId",
                table: "Diagnoses",
                newName: "DiagnoseID");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnoses_PatientId",
                table: "Diagnoses",
                newName: "IX_Diagnoses_PatientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_Patients_PatientID",
                table: "Diagnoses",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientsMedicaments_Medicaments_MedicamentID",
                table: "PatientsMedicaments",
                column: "MedicamentID",
                principalTable: "Medicaments",
                principalColumn: "MedicamentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientsMedicaments_Patients_PatientID",
                table: "PatientsMedicaments",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitations_Doctors_DoctorID",
                table: "Visitations",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "DoctorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitations_Patients_PatientID",
                table: "Visitations",
                column: "PatientID",
                principalTable: "Patients",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_Patients_PatientID",
                table: "Diagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientsMedicaments_Medicaments_MedicamentID",
                table: "PatientsMedicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientsMedicaments_Patients_PatientID",
                table: "PatientsMedicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitations_Doctors_DoctorID",
                table: "Visitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitations_Patients_PatientID",
                table: "Visitations");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Visitations",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "DoctorID",
                table: "Visitations",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "VisitationID",
                table: "Visitations",
                newName: "VisitationId");

            migrationBuilder.RenameIndex(
                name: "IX_Visitations_PatientID",
                table: "Visitations",
                newName: "IX_Visitations_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Visitations_DoctorID",
                table: "Visitations",
                newName: "IX_Visitations_DoctorId");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "PatientsMedicaments",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "MedicamentID",
                table: "PatientsMedicaments",
                newName: "MedicamentId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientsMedicaments_PatientID",
                table: "PatientsMedicaments",
                newName: "IX_PatientsMedicaments_PatientId");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Patients",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "MedicamentID",
                table: "Medicaments",
                newName: "MedicamentId");

            migrationBuilder.RenameColumn(
                name: "DoctorID",
                table: "Doctors",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Diagnoses",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "DiagnoseID",
                table: "Diagnoses",
                newName: "DiagnoseId");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnoses_PatientID",
                table: "Diagnoses",
                newName: "IX_Diagnoses_PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_Patients_PatientId",
                table: "Diagnoses",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientsMedicaments_Medicaments_MedicamentId",
                table: "PatientsMedicaments",
                column: "MedicamentId",
                principalTable: "Medicaments",
                principalColumn: "MedicamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientsMedicaments_Patients_PatientId",
                table: "PatientsMedicaments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitations_Doctors_DoctorId",
                table: "Visitations",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitations_Patients_PatientId",
                table: "Visitations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
