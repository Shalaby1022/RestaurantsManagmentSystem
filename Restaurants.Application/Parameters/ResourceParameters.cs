using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Parameters
{
	public class ResourceParameters
	{
		// Filtering Seacrh Criteria prop
		public string? SearchQuery { get; set; }

		// Pagination Props
		public int PageNumber { get; set; }
		public int PageSize { get; set; } = 10;


		//Sorting 
		public string? SortBy { get; set; }
		public SortDirection SortDirection { get; set; }

	}
}
