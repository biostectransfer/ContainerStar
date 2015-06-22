using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace ContainerStar.API.Controllers
{
	public class Paging
	{
		public int Skip { get; set; }
		public int Take { get; set; }
	}

	public class Filter
	{
		public string Operator { get; set; }
		public string Field { get; set; }
		public string Value { get; set; }		
	}

	public class CompositeFilter
	{
		public List<Filter> Filters { get; set; }
		public string Logic { get; set; }
	}

	public class Filtering
	{
		public List<CompositeFilter> Filters { get; set; }
		public string Logic { get; set; }
	}

	public class Sorting
	{
		public string Field { get; set; }
		public string Direction { get; set; }
	}

	[ModelBinder(typeof(GridArgsModelBinder))]
	public class GridArgs
	{
		public Paging Paging { get; set; }
		public Filtering Filtering { get; set; }
		public Sorting Sorting { get; set; }
	}
}
