using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.DirectoryServices.ActiveDirectory;
using System.Text;

namespace Lopushok1.Entities;

public partial class Product
{
    private string? _image;
    private decimal _minCost;

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int? ProductTypeId { get; set; }

    public string ArticleNumber { get; set; } = null!;

    public string? Description { get; set; }

    public string? Image { get => _image; set => _image = value; }

    public int? ProductionPersonCount { get; set; }

    public int? ProductionWorkshopNumber { get; set; }

    public decimal MinCostForAgent { get => _minCost; set => _minCost = value; }

    [NotMapped]
    public string? ImagePath
    {
        get => (_image == null) ?
            "..\\Resources\\picture.png" :
            $"..\\Resources{_image.Replace(".jpg", ".jpeg")}";
    }

    [NotMapped]
    public string Materials
    {
        get
        {
            if (ProductMaterials.Count == 0)
                return "Материалов нет";

            StringBuilder sb = new StringBuilder();
            sb.Append("Материалы: ");

            foreach (var pm in ProductMaterials)
                sb.Append($"{pm.Material.Title}, ");

            return sb.Remove(sb.Length - 2, 2).ToString();
        }
    }

    [NotMapped]
    public decimal Cost
    {
        get
        {
            if (ProductMaterials.Count == 0)
                return _minCost;

            decimal cost = 0;
            foreach (var pm in ProductMaterials)
                cost += Math.Ceiling(pm.Material.Cost * (decimal)pm.Count);

            return cost;
        }
    }

    public virtual ICollection<ProductCostHistory> ProductCostHistories { get; } = new List<ProductCostHistory>();

    public virtual ICollection<ProductMaterial> ProductMaterials { get; } = new List<ProductMaterial>();

    public virtual ICollection<ProductSale> ProductSales { get; } = new List<ProductSale>();

    public virtual ProductType? ProductType { get; set; }
}
