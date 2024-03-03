using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changelanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "GaussianMixture",
                column: "Description",
                value: "A clustering method that models the data as a mixture of Gaussian partitions");

            migrationBuilder.UpdateData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "KMeans",
                column: "Description",
                value: "Arrangement of a set of objects into relatively homogeneous groups.");

            migrationBuilder.UpdateData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "OneCluster",
                columns: new[] { "Description", "Name" },
                values: new object[] { "Combining all elements into one cluster", "One cluster" });

            migrationBuilder.UpdateData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "SpectralClustering",
                column: "Description",
                value: "Spectral clustering is based on the principles of graph theory and linear algebra");

            migrationBuilder.UpdateData(
                table: "ClusterizationTypes",
                keyColumn: "Id",
                keyValue: "Comments",
                column: "Name",
                value: "Comments");

            migrationBuilder.UpdateData(
                table: "ClusterizationTypes",
                keyColumn: "Id",
                keyValue: "External",
                column: "Name",
                value: "From file");

            migrationBuilder.UpdateData(
                table: "MyTaskStates",
                keyColumn: "Id",
                keyValue: "Completed",
                column: "Name",
                value: "Completed");

            migrationBuilder.UpdateData(
                table: "MyTaskStates",
                keyColumn: "Id",
                keyValue: "Error",
                column: "Name",
                value: "Error");

            migrationBuilder.UpdateData(
                table: "MyTaskStates",
                keyColumn: "Id",
                keyValue: "Process",
                column: "Name",
                value: "Process");

            migrationBuilder.UpdateData(
                table: "MyTaskStates",
                keyColumn: "Id",
                keyValue: "Stopped",
                column: "Name",
                value: "Stopped");

            migrationBuilder.UpdateData(
                table: "MyTaskStates",
                keyColumn: "Id",
                keyValue: "Wait",
                column: "Name",
                value: "Wait");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "GaussianMixture",
                column: "Description",
                value: "Метод кластеризації, який моделює дані як суміш розділів Гауса");

            migrationBuilder.UpdateData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "KMeans",
                column: "Description",
                value: "Впорядкування множини об'єктів у порівняно однорідні групи.");

            migrationBuilder.UpdateData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "OneCluster",
                columns: new[] { "Description", "Name" },
                values: new object[] { "Об'єднання елементів в один кластер", "Один кластер" });

            migrationBuilder.UpdateData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "SpectralClustering",
                column: "Description",
                value: "Cпектральна кластеризація базується на принципах теорії графів і лінійної алгебри");

            migrationBuilder.UpdateData(
                table: "ClusterizationTypes",
                keyColumn: "Id",
                keyValue: "Comments",
                column: "Name",
                value: "Коментарі");

            migrationBuilder.UpdateData(
                table: "ClusterizationTypes",
                keyColumn: "Id",
                keyValue: "External",
                column: "Name",
                value: "З файлу");

            migrationBuilder.UpdateData(
                table: "MyTaskStates",
                keyColumn: "Id",
                keyValue: "Completed",
                column: "Name",
                value: "Виконалася");

            migrationBuilder.UpdateData(
                table: "MyTaskStates",
                keyColumn: "Id",
                keyValue: "Error",
                column: "Name",
                value: "Помилка");

            migrationBuilder.UpdateData(
                table: "MyTaskStates",
                keyColumn: "Id",
                keyValue: "Process",
                column: "Name",
                value: "Виконується");

            migrationBuilder.UpdateData(
                table: "MyTaskStates",
                keyColumn: "Id",
                keyValue: "Stopped",
                column: "Name",
                value: "Призупинено");

            migrationBuilder.UpdateData(
                table: "MyTaskStates",
                keyColumn: "Id",
                keyValue: "Wait",
                column: "Name",
                value: "Очікування");
        }
    }
}
