using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: AssemblyCompany("http://www.nsimple.cn/")]
[assembly: AssemblyProduct("OrientalCharacters")]
[assembly: AssemblyCopyright("Copyright © nsimple.cn 2010")]
[assembly: AssemblyTrademark("software:OrientalCharacters, nsimple, GeanSoft")]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion(RevisionClass.FullVersion)]

internal static class RevisionClass
{
    public const string Major = "0";
    public const string Minor = "1";
    public const string Build = "0";
    public const string Revision = "1216";

    public const string MainVersion = Major + "." + Minor;
    public const string FullVersion = Major + "." + Minor + "." + Build + "." + Revision;
}
