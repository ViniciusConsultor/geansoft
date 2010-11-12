using System;
using System.IO;
using Gean;

namespace Gean.Option
{
    public interface IOptionManager : IService
    {
        FileInfo Backup(string file);
        OptionCollection Options { get; }
        bool IsChange { get; }
        bool Save();
    }
}
