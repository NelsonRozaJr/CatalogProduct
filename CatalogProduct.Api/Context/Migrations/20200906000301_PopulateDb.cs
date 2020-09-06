using Microsoft.EntityFrameworkCore.Migrations;

namespace CatalogProduct.Api.Context.Migrations
{
    public partial class PopulateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Bebidas', 'http://www.macoratti.net/Imagens/1.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Lanches', 'http://www.macoratti.net/Imagens/2.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('Sobremesas', 'http://www.macoratti.net/Imagens/3.jpg')");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, UnitPrice, ImageUrl, Stock, CreateDate, CategoryId) " +
                "VALUES('Coca-Cola Diet', 'Refrigerante de Cola 350 ml', 5.45, 'http://www.macoratti.net/Imagens/coca.jpg', 50, now(), " +
                "(SELECT CategoryId FROM Categories WHERE Name='Bebidas'))");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, UnitPrice, ImageUrl, Stock, CreateDate, CategoryId) " +
                "VALUES('Lanche de Atum', 'Lanche de Atum com maionese', 8.50, 'http://www.macoratti.net/Imagens/atum.jpg', 10, now(), " +
                "(SELECT CategoryId FROM Categories WHERE Name='Lanches'))");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, UnitPrice, ImageUrl, Stock, CreateDate, CategoryId) " +
                "VALUES('Pudim 100 g', 'Pudim de leite condensado 100g', 6.75, 'http://www.macoratti.net/Imagens/pudim.jpg', 20, now(), " +
                "(SELECT CategoryId FROM Categories WHERE Name='Sobremesas'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
            migrationBuilder.Sql("DELETE FROM Categories");
        }
    }
}
