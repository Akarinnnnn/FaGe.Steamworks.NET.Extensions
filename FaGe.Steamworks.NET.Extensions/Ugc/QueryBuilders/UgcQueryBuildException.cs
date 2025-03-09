using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaGe.Steamworks.NET.Extensions.Ugc.QueryBuilders;

[Serializable]
public class UgcQueryBuildException : ArgumentException
{
	public UgcQueryBuildException() { }
	public UgcQueryBuildException(string message) : base(message) { }
	public UgcQueryBuildException(string message, Exception inner) : base(message, inner) { }
}
