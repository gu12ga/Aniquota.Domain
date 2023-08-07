using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Aniquota.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");
            /*migrationBuilder.Sql(@"
create view aplicaprodutoclientemodel as
select C.IdCliente, P.IdProduto, P.Nome, P.Rendimento, A.ValorAplicado, A.ValorAplicado*(1+P.Rendimento) as ValorAtual, A.DataAplica
from aplica A join cliente C on A.IdCliente = C.IdCliente join produto P on P.IdProduto = A.IdProduto
");*/

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CPF = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Senha = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    IdProduto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Rendimento = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.IdProduto);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Tel = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => new { x.IdCliente, x.Tel });
                    table.ForeignKey(
                        name: "FK_Telefone_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Aplica",
                columns: table => new
                {
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    DataAplica = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ValorAplicado = table.Column<float>(type: "float", nullable: false),
                    DataResgate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplica", x => new { x.IdCliente, x.IdProduto, x.DataAplica });
                    table.ForeignKey(
                        name: "FK_Aplica_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aplica_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Aplica_IdProduto",
                table: "Aplica",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CPF",
                table: "Cliente",
                column: "CPF",
                unique: true);

            migrationBuilder.Sql(@"INSERT INTO cliente(IdCliente, CPF, Nome, Senha, Email)
VALUES(1, '12345678901', 'Nome do Cliente', 'Senha123', 'cliente@email.com')");
            migrationBuilder.Sql(@"INSERT INTO produto(IdProduto, Nome, Rendimento)
VALUES(1, 'CDB', 0.1)");

        migrationBuilder.Sql(@"INSERT INTO aplica(idProduto, IdCliente, ValorAplicado, DataAplica)
VALUES(1, 1, 100, NOW())");

            migrationBuilder.Sql(@"create view aplicaprodutoclientemodel as
select C.IdCliente, P.IdProduto, P.Nome, P.Rendimento, A.ValorAplicado, A.ValorAplicado*(1+P.Rendimento*TIMESTAMPDIFF(SECOND, A.DataAplica, NOW())) AS ValorAtual, A.DataAplica
from aplica A join cliente C on A.IdCliente = C.IdCliente join produto P on P.IdProduto = A.IdProduto");
        
            /*grationBuilder.Sql(@"INSERT INTO cliente(IdCliente, CPF, Nome, Senha, Email)
VALUES(1, '12345678901', 'Nome do Cliente', 'Senha123', 'cliente@email.com')");
        */
            }
    

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aplica");

            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.Sql(@"drop view aplicaprodutoclientemodel");
        }
    }
}
