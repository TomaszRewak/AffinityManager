using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AffinityManager.Data
{
	internal sealed record AffinityRule(Regex Pattern, IntPtr AffinityMask);
}
