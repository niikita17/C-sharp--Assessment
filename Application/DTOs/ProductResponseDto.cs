using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs;

public class ProductResponseDto
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }
}
