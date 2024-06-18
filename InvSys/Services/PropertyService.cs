using System;
using Serilog;
using InvSys.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using InvSys.Data;
using InvSys.Services.Interface;
using ClosedXML.Excel;

namespace InvSys.Services;

public class PropertyService : ICrudService<Property>
{
    private readonly ApplicationDbContext _context;

    public PropertyService(ApplicationDbContext context)
    {
        _context = context;
    }



    public List<Property> GetAll()
    {
        return _context.Properties.ToList();
    }

    public Property GetById(int id)
    {
        return _context.Properties.Find(id);
    }

    public void Add(Property property)
    {
        _context.Properties.Add(property);
        _context.SaveChanges();
    }

    public void Update(Property property)
    {
        _context.Entry(property).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var property = _context.Properties.Find(id);
        if (property != null)
        {
            _context.Properties.Remove(property);
            _context.SaveChanges();
        }
    }

    public void ImportPropertiesFromExcel(string filePath)
    {
        try
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Assuming data is on the first worksheet

                foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip header row
                {
                    var prop = new Property
                    {
                        propArticle = row.Cell(1).GetString(), // Adjust column indexes as per your Excel file
                        propDescription = row.Cell(2).GetString(),
                        propPropertyNo = row.Cell(3).GetString(),
                        propUnitOfMeasure = row.Cell(4).GetString(),
                        propUnitValue = Convert.ToDouble(row.Cell(5).GetString()),

                        propBalancePerCardQty = Convert.ToInt32(row.Cell(6).GetString()),
                        propOnHandPerCardQty = Convert.ToInt32(row.Cell(7).GetString()),

                        propShortageOverageQty = Convert.ToInt32(row.Cell(8).GetString()),
                        propShortageOverageValue = Convert.ToInt32(row.Cell(9).GetString()),
                        category = row.Cell(10).GetString()
                    };

                    _context.Properties.Add(prop);
                }

                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Error importing properties from Excel: {ex.Message}");
            throw; // Rethrow the exception to handle it at a higher level if necessary
        }
    }

    public byte[] GenerateExcelFile(List<Property> properties)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Supplies");

            // Add headers
            worksheet.Cell(1, 1).Value = "Property Article";
            worksheet.Cell(1, 2).Value = "Property Description";
            worksheet.Cell(1, 3).Value = "Property No.";
            worksheet.Cell(1, 4).Value = "Unit of Measure";
            worksheet.Cell(1, 5).Value = "Unit Value";
            worksheet.Cell(1, 6).Value = "Balance Per Card Qty";
            worksheet.Cell(1, 7).Value = "On Hand Per Card Qty";
            worksheet.Cell(1, 8).Value = "Overage Qty";
            worksheet.Cell(1, 9).Value = "Overage Value";
            worksheet.Cell(1, 10).Value = "Category";

            // Add other headers similarly

            int row = 2;

            // Populate data
            foreach (var supply in properties)
            {
                //var supply = supplies[i];
                worksheet.Cell(row, 1).Value = supply.propArticle;
                worksheet.Cell(row, 2).Value = supply.propDescription;
                worksheet.Cell(row, 3).Value = supply.propPropertyNo;
                worksheet.Cell(row, 4).Value = supply.propUnitOfMeasure;
                worksheet.Cell(row, 5).Value = supply.propUnitValue;
                worksheet.Cell(row, 6).Value = supply.propBalancePerCardQty;
                worksheet.Cell(row, 7).Value = supply.propOnHandPerCardQty;
                worksheet.Cell(row, 8).Value = supply.propShortageOverageQty;
                worksheet.Cell(row, 9).Value = supply.propShortageOverageValue;
                worksheet.Cell(row, 10).Value = supply.category;
                row++;
                // Add other properties similarly
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }

    //DUPLICATE CHECKER
    public bool IsStockNumberDuplicate(string stockNumber)
    {
        // Check if any supply in the database has the same stock number
        bool isDuplicate = _context.Properties.Any(s => s.propPropertyNo == stockNumber);
        return isDuplicate;
    }
}
