using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExcelToSQLDemo.Model;
using System.IO;
using OfficeOpenXml;

namespace ExcelToSQLDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly sqlDemoContext _context;

        public ProductsController(sqlDemoContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Product>> PostProduct(Product product)
        //{
        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        //}

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            string description;
            double? price;
            int? quantity;
            DateTime expiryDate;
            int? categoryid;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;            
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[0];
                    var rowcount = excelWorksheet.Dimension.Rows;
                    List<string> errorRows = new List<string>();
                    List<Product> list = new List<Product>();
                    List<string> stringlist = new List<string>();
                    for (int rownumber = 2; rownumber <= rowcount; rownumber++)
                    {
                        try
                        {
                            try
                            {
                                description = excelWorksheet.Cells[rownumber, 3].Value.ToString();
                            }
                            catch (NullReferenceException)
                            {
                                description = null;
                            }
                            try
                            {
                                categoryid = Convert.ToInt32(excelWorksheet.Cells[rownumber, 7].Value.ToString());
                            }
                            catch (NullReferenceException)
                            {
                                categoryid = null;
                            }
                            list.Add
                                (new Product
                                {
                                    Name = excelWorksheet.Cells[rownumber, 2].Value.ToString(),
                                    Description = description,
                                    Price = Convert.ToDouble(excelWorksheet.Cells[rownumber, 4].Value.ToString()),
                                    Quantity = Convert.ToInt32(excelWorksheet.Cells[rownumber, 5].Value.ToString()),
                                    ExpiryDate = Convert.ToDateTime(excelWorksheet.Cells[rownumber, 6].Value.ToString()),
                                    Categoryid = categoryid
                                });

                            //stringlist.Add(excelWorksheet.Cells[rownumber, 2].Value.ToString());
                            //stringlist.Add(excelWorksheet.Cells[rownumber, 3].Value.ToString());
                            //stringlist.Add(excelWorksheet.Cells[rownumber, 4].Value.ToString());
                            //stringlist.Add(excelWorksheet.Cells[rownumber, 5].Value.ToString());
                            //stringlist.Add(excelWorksheet.Cells[rownumber, 6].Value.ToString());
                            //stringlist.Add(excelWorksheet.Cells[rownumber, 7].Value.ToString());

                        }
                        catch (Exception e)
                        {
                            errorRows.Add(e.Message);
                            errorRows.Add("Check data in row "+rownumber);
                        }
                    }
                    await _context.Products.AddRangeAsync(list);
                    await _context.SaveChangesAsync();
                    if (errorRows.Count != 0 && list.Count!=0)
                    {
                        errorRows.Add("Remaining data has been added successfully");
                        return new ObjectResult(errorRows);
                    }
                    if (errorRows.Count!=0)
                    {

                        return new ObjectResult(errorRows);
                    }
                    return Ok(list);
                }
            }

        }

            // DELETE: api/Products/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
