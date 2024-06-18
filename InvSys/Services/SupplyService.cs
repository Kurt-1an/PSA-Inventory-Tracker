using System;
using Serilog;
using InvSys.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using InvSys.Data;
using InvSys.Services.Interface;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.InkML;


namespace InvSys.Services
{

    public class SupplyService : ICrudService<Supply>
    {
        private readonly ApplicationDbContext _context;

        public SupplyService(ApplicationDbContext context)
        {
            _context = context;
        }



        public List<Supply> GetAll()
        {
            return _context.Supplies.ToList();
        }

        public Supply GetById(int id)
        {
            return _context.Supplies.Find(id);
        }

        public void Add(Supply supply)
        {
            _context.Supplies.Add(supply);
            _context.SaveChanges();
        }

        public void Update(Supply supply)
        {
            _context.Entry(supply).State = EntityState.Modified;

            //SET HISTORY
            if (supply != null)
            {
                var historyRecord = new SupplyHistory
                {
                    SupplyID = supply.ID,
                    StartDate = supply.supBegInvDate,
                    DateModified = DateOnly.FromDateTime(DateTime.Today),
                    EndDate = DateOnly.FromDateTime(DateTime.Today)
                };

                _context.SupplyHistories.Add(historyRecord);
            }

                _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var supply = _context.Supplies.Find(id);
            if (supply != null)
            {
                _context.Supplies.Remove(supply);
                _context.SaveChanges();
            }
        }

        //IMPORT
        public void ImportSuppliesFromExcel(string filePath)
        {
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1); // Assuming data is on the first worksheet

                    foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip header row
                    {
                        var sup = new Supply
                        {
                            supStockLetter = row.Cell(1).GetString(), // Adjust column indexes as per your Excel file
                            supStockNumber = Convert.ToInt32(row.Cell(2).GetString()),
                            supArticle = row.Cell(3).GetString(),
                            supDescription = row.Cell(4).GetString(),
                            supUnitType = row.Cell(5).GetString(),

                            supAverageUnitCost = Convert.ToDouble(row.Cell(6).GetString()),
                            supBeginningInventoryQty = Convert.ToInt32(row.Cell(7).GetString()),
                            supDelivery = Convert.ToInt32(row.Cell(8).GetString())
                        };

                        _context.Supplies.Add(sup);
                    }

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error importing supplies list from Excel: {ex.Message}");
                throw; // Rethrow the exception to handle it at a higher level if necessary
            }
        }


        //EXPORT
        public byte[] GenerateExcelFile(List<Supply> supplies)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Supplies");

                // Add headers
                worksheet.Cell(1, 1).Value = "Stock Letter";
                worksheet.Cell(1, 2).Value = "Stock No.";
                worksheet.Cell(1, 3).Value = "Article";
                worksheet.Cell(1, 4).Value = "Description";
                worksheet.Cell(1, 5).Value = "Unit Type";
                worksheet.Cell(1, 6).Value = "Average Unit Value";
                worksheet.Cell(1, 7).Value = "Beginning Inventory Quantity";
                worksheet.Cell(1, 8).Value = "Delivery";
                worksheet.Cell(1, 9).Value = "Total Amount";
                // Add other headers similarly

                int row = 2;

                // Populate data
                foreach (var supply in supplies)
                {
                    //var supply = supplies[i];
                    worksheet.Cell(row, 1).Value = supply.supStockLetter;
                    worksheet.Cell(row, 2).Value = supply.supStockNumber;
                    worksheet.Cell(row, 3).Value = supply.supArticle;
                    worksheet.Cell(row, 4).Value = supply.supDescription;
                    worksheet.Cell(row, 5).Value = supply.supUnitType;
                    worksheet.Cell(row, 6).Value = supply.supAverageUnitCost;
                    worksheet.Cell(row, 7).Value = supply.supBeginningInventoryQty;
                    worksheet.Cell(row, 8).Value = supply.supDelivery;
                    worksheet.Cell(row, 9).Value = supply.supTotalAmount;
                    row++;
                    // Add other supplies similarly
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }


        //DUPLICATE CHECKER
        public bool IsStockNumberDuplicate(int? stockNumber)
        {
            // Check if any supply in the database has the same stock number
            bool isDuplicate = _context.Supplies.Any(s => s.supStockNumber == stockNumber);
            return isDuplicate;
        }
    }
}